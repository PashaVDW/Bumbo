using bumbo.Data;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using bumbo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bumbo.Controllers
{
    public class RegisteredHoursManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRegisteredHoursRepository _registeredHoursRepository;
        private readonly BumboDBContext _dbContext;

        public RegisteredHoursManagerController(
            UserManager<Employee> userManager,
            IEmployeeRepository employeeRepository,
            IScheduleRepository scheduleRepository,
            IRegisteredHoursRepository registeredHoursRepository,
            BumboDBContext dbContext
        )
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _scheduleRepository = scheduleRepository;
            _registeredHoursRepository = registeredHoursRepository;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int? year, int? monthNumber)
        {
            Employee user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;
            DateTime now = DateTime.Now;
            int targetYear = year ?? now.Year;
            int targetMonth = monthNumber ?? now.Month;

            // Eventueel niet meer echt nodig:
            List<RegisteredHours> allRH = _dbContext.RegisteredHours.ToList();

            List<Employee> employees = await _employeeRepository.GetEmployeesOfBranch(branchId);

            List<Schedule> schedulesThisMonth = _scheduleRepository
                .GetSchedulesForBranchByMonth(branchId, targetYear, targetMonth);

            List<EmployeeRowViewModel> allRows = new List<EmployeeRowViewModel>();

            foreach (Employee employee in employees)
            {
                List<Schedule> schedulesOfEmployee = schedulesThisMonth
                    .Where(s => s.EmployeeId == employee.Id)
                    .ToList();

                List<RegisteredHours> regHoursOfEmployee =
                    (employee.RegisteredHours ?? new List<RegisteredHours>())
                    .Where(rh => rh.StartTime.Year == targetYear && rh.StartTime.Month == targetMonth)
                    .ToList();

                double totalScheduledHours = schedulesOfEmployee.Sum(s =>
                    (s.EndTime - s.StartTime).TotalHours);

                double totalWorkedHours = 0;

                foreach (var rh in regHoursOfEmployee)
                {
                    DateOnly day = DateOnly.FromDateTime(rh.StartTime.Date);

                    bool isSickDay = schedulesThisMonth.Any(s => s.Date == day && s.IsSick);
                    if (!isSickDay && rh.EndTime.HasValue)
                    {
                        totalWorkedHours += (rh.EndTime.Value - rh.StartTime).TotalHours;
                    }
                }



                // Totale afwijking medewerker
                double difference = totalWorkedHours - totalScheduledHours;

                // We halen de dagelijkse data op
                List<DateOnly> scheduleDates = schedulesOfEmployee
                    .Select(s => s.Date)
                    .Distinct()
                    .ToList();

                List<DateOnly> regHourDates = regHoursOfEmployee
                    .Select(rh => DateOnly.FromDateTime(rh.StartTime.Date))
                    .Distinct()
                    .ToList();

                List<DateOnly> allDates = scheduleDates
                    .Union(regHourDates)
                    .OrderBy(d => d)
                    .ToList();

                List<RegisteredHoursDetailViewModel> detailRows = new List<RegisteredHoursDetailViewModel>();

                // Per dag: bereken dag-toeslagen etc.
                foreach (DateOnly day in allDates)
                {
                    List<Schedule> daySchedules = schedulesOfEmployee
                        .Where(s => s.Date == day)
                        .ToList();

                    Schedule scheduleOfDay = daySchedules.FirstOrDefault();

                    List<RegisteredHours> dayRegHours = regHoursOfEmployee
                        .Where(rh => rh.StartTime.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                        .ToList();

                    RegisteredHours rhOfDay = dayRegHours.FirstOrDefault();

                    TimeOnly? scheduledStart = scheduleOfDay?.StartTime;
                    TimeOnly? scheduledEnd = scheduleOfDay?.EndTime;

                    TimeOnly? workedStart = rhOfDay != null
                         ? TimeOnly.FromDateTime(rhOfDay.StartTime)
                         : null;

                    TimeOnly? workedEnd = rhOfDay != null && rhOfDay.EndTime.HasValue
                        ? TimeOnly.FromDateTime(rhOfDay.EndTime.Value)
                        : null;


                    double dayScheduledHours = (scheduledStart != null && scheduledEnd != null)
                        ? (scheduledEnd.Value - scheduledStart.Value).TotalHours
                        : 0;

                    double dayWorkedHours = (workedStart != null && workedEnd != null)
                        ? (workedEnd.Value - workedStart.Value).TotalHours
                        : 0;

                    bool isSick = daySchedules.Any(sch => sch.IsSick);
                    string notes = isSick ? "Ziek" : "";

                    if (isSick)
                    {
                        // Geklokte uren op 0, en geen start/end
                        dayWorkedHours = 0;
                        workedStart = null;
                        workedEnd = null;
                    }

                    double dayDifference = dayWorkedHours - dayScheduledHours;

                    // Ophalen CAO-regels
                    LabourRules labourRule = _employeeRepository.GetLabourRulesForEmployee(employee);

                    // Dagelijkse toeslagberekening
                    double dayBonusHours = 0;
                    if (dayDifference > 0)
                    {
                        double overHoursByWork = dayDifference * ((double)labourRule.OvertimePayPercentage / 100.0);
                        dayBonusHours += overHoursByWork;
                    }
                    if (isSick)
                    {
                        double sickHours = dayScheduledHours * ((double)labourRule.SickPayPercentage / 100.0);
                        dayBonusHours += sickHours;
                    }

                    detailRows.Add(new RegisteredHoursDetailViewModel
                    {
                        Date = day,
                        ScheduledStartTime = scheduledStart,
                        ScheduledEndTime = scheduledEnd,
                        WorkedStartTime = workedStart,
                        WorkedEndTime = workedEnd,
                        ScheduledHoursDay = dayScheduledHours,
                        WorkedHoursDay = dayWorkedHours,
                        Difference = dayDifference,
                        Notes = notes,
                        BonusHours = dayBonusHours
                    });
                }

                // **Nu** sommatie van alle dagtoeslagen:
                double sumOfDailyBonuses = detailRows.Sum(d => d.BonusHours);

                // Hoofdrij
                EmployeeRowViewModel rowVm = new EmployeeRowViewModel
                {
                    EmployeeId = employee.Id,
                    FullName = $"{employee.FirstName} {employee.MiddleName} {employee.LastName}".Trim(),
                    EmployeeBID = employee.BID,
                    TotalScheduledHours = totalScheduledHours,
                    TotalWorkedHours = totalWorkedHours,
                    Difference = difference,
                    BonusHours = sumOfDailyBonuses,
                    RegisteredHours = detailRows
                };

                allRows.Add(rowVm);
            }

            // Bouw het model en geef door aan de View
            RegisteredHoursManagerOverview overview = new RegisteredHoursManagerOverview
            {
                Employees = allRows,
                DisplayYear = targetYear,
                DisplayMonth = targetMonth
            };

            return View(overview);
        }

        [HttpGet]
        public IActionResult Update(string employeeBID, DateTime date)
        {
            // Haal de ingelogde gebruiker op
            Employee user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // Haal geregistreerde uren op
            RegisteredHours registeredHours = _registeredHoursRepository.GetRegisteredHoursDayFromBID(employeeBID, date);
            if (registeredHours == null || registeredHours.BranchId != user.ManagerOfBranchId)
            {
                return NotFound("Geregistreerde uren niet gevonden of u heeft geen toegang.");
            }

            // Haal de planning (schedule) op
            Schedule schedule = _scheduleRepository.GetScheduleByEmployeeBranchDate(
                registeredHours.EmployeeId,
                registeredHours.BranchId,
                DateOnly.FromDateTime(date)
            );

            // Controleer of de planning bestaat
            if (schedule == null)
            {
                return NotFound("Planning (Schedule) niet gevonden.");
            }

            // Vul het ViewModel
            UpdateRegisteredHoursViewModel model = new UpdateRegisteredHoursViewModel
            {
                EmployeeBID = registeredHours.EmployeeBID,
                EmployeeName = $"{registeredHours.Employee.FirstName} {registeredHours.Employee.LastName}".Trim(),
                Date = registeredHours.StartTime.Date,
                WorkedStartTime = registeredHours.StartTime,
                WorkedEndTime = registeredHours.EndTime,
                Notes = registeredHours.IsDefenitive ? "Definitief" : "",
                IsSick = schedule.IsSick,

                // Geplande gegevens
                ScheduledDepartment = schedule.DepartmentName,
                ScheduledStartTime = DateTime.Parse($"{date:yyyy-MM-dd} {schedule.StartTime:HH\\:mm}"),
                ScheduledEndTime = DateTime.Parse($"{date:yyyy-MM-dd} {schedule.EndTime:HH\\:mm}")
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(UpdateRegisteredHoursViewModel model)
        {
            TempData["ToastId"] = "UpdateRegisteredHours";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;

            Employee user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData["ToastMessage"] = "Er zijn fouten in het formulier. Controleer de invoer.";
                TempData["ToastType"] = "error";
                return View(model);
            }

            RegisteredHours existingHours = _registeredHoursRepository.GetRegisteredHoursDayFromBID(model.EmployeeBID, model.Date);
            if (existingHours == null || existingHours.BranchId != user.ManagerOfBranchId)
            {
                TempData["ToastMessage"] = "Geregistreerde uren niet gevonden of u heeft geen toegang.";
                TempData["ToastType"] = "error";
                return View(model);
            }

            Schedule schedule = _scheduleRepository.GetScheduleByEmployeeBranchDate(
                existingHours.EmployeeId,
                existingHours.BranchId,
                DateOnly.FromDateTime(model.Date)
            );

            if (schedule == null)
            {
                TempData["ToastMessage"] = "Planning (Schedule) niet gevonden.";
                TempData["ToastType"] = "error";
                return View(model);
            }

            // Update RegisteredHours
            existingHours.StartTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.WorkedStartTime.Hour, model.WorkedStartTime.Minute, 0);
            existingHours.EndTime = model.WorkedEndTime.HasValue
                ? new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.WorkedEndTime.Value.Hour, model.WorkedEndTime.Value.Minute, 0)
                : null;
            existingHours.IsDefenitive = model.Notes?.ToLower().Contains("definitief") ?? false;

            // Update Schedule
            schedule.IsSick = model.IsSick;

            try
            {
                _registeredHoursRepository.UpdateRegisteredHours(existingHours);
                _scheduleRepository.UpdateSicknessSchedule(schedule);

                TempData["ToastMessage"] = "Uren en planning zijn succesvol aangepast.";
                TempData["ToastType"] = "success";
                return RedirectToAction("Index", "RegisteredHoursManager");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating: {ex.Message}");
                TempData["ToastMessage"] = "Er is een fout opgetreden tijdens het bijwerken van de uren en planning.";
                TempData["ToastType"] = "error";
                return View(model);
            }
        }

    }
}