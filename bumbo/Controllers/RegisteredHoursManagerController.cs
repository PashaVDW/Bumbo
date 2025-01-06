using bumbo.Data; // voor BumboDBContext
using bumbo.ViewModels;
using DataLayer.Interfaces;
using bumbo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;

namespace bumbo.Controllers
{
    public class RegisteredHoursManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly BumboDBContext _dbContext; // <-- extra voor debug

        public RegisteredHoursManagerController(
            UserManager<Employee> userManager,
            IEmployeeRepository employeeRepository,
            IScheduleRepository scheduleRepository,
            BumboDBContext dbContext // <-- extra
        )
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _scheduleRepository = scheduleRepository;
            _dbContext = dbContext; // <-- opslaan
        }

        public async Task<IActionResult> Index(int? year, int? month)
        {
            Employee user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            DateTime now = DateTime.Now;
            int targetYear = year ?? now.Year;
            int targetMonth = month ?? now.Month;

            // 1) Pak alle RegisteredHours in de hele DB (debug) en print
            List<RegisteredHours> allRH = _dbContext.RegisteredHours.ToList();
            Console.WriteLine("[DEBUG] ----- ALL RegisteredHours in DB -----");
            Console.WriteLine($"[DEBUG]   Found {allRH.Count} RegisteredHours total.");
            foreach (RegisteredHours rh in allRH)
            {
                // Laat de EmployeeBID, StartTime, EndTime zien
                Console.WriteLine($"[DEBUG]   RH => BID={rh.EmployeeBID}, Start={rh.StartTime}, End={rh.EndTime}");
            }
            Console.WriteLine("[DEBUG] --------------------------------------");

            // 2) Haal employees van deze branch
            List<Employee> employees = await _employeeRepository.GetEmployeesOfBranch(branchId);
            Console.WriteLine($"[DEBUG] Found {employees.Count} employees in branch {branchId}");

            // 3) Haal alle schedules (ingeplande uren) van deze branch en maand
            List<Schedule> schedulesThisMonth = _scheduleRepository
                .GetSchedulesForBranchByMonth(branchId, targetYear, targetMonth);

            Console.WriteLine($"[DEBUG] Found {schedulesThisMonth.Count} schedules in {targetYear}-{targetMonth}");

            // 4) Voor elke employee debug:
            foreach (Employee emp in employees)
            {
                // Print basics
                Console.WriteLine($"[DEBUG] EMP Id={emp.Id}, BID={emp.BID}, Name={emp.FirstName} {emp.LastName}");

                // Print wat EF in memory heeft in emp.RegisteredHours
                if (emp.RegisteredHours == null)
                {
                    Console.WriteLine($"[DEBUG]   emp.RegisteredHours is NULL");
                }
                else
                {
                    Console.WriteLine($"[DEBUG]   emp.RegisteredHours.Count = {emp.RegisteredHours.Count}");
                    foreach (RegisteredHours rh in emp.RegisteredHours)
                    {
                        Console.WriteLine($"[DEBUG]     -> RH: Start={rh.StartTime}, End={rh.EndTime}, EmployeeBID={rh.EmployeeBID}");
                    }
                }
            }

            // NU pas de 'gewone' logic
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

                Console.WriteLine($"[DEBUG] => {employee.FirstName} {employee.LastName}: SchedCount={schedulesOfEmployee.Count}, RegHoursCount={regHoursOfEmployee.Count}");

                double totalScheduledHours = schedulesOfEmployee.Sum(s =>
                    (s.EndTime.ToTimeSpan() - s.StartTime.ToTimeSpan()).TotalHours);

                double totalWorkedHours = regHoursOfEmployee.Sum(rh =>
                    (rh.EndTime - rh.StartTime).TotalHours);

                double difference = totalScheduledHours - totalWorkedHours;
                double bonusHours = 0;
                if (totalWorkedHours > 8) bonusHours = totalWorkedHours - 8;

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

                foreach (DateOnly day in allDates)
                {
                    // Alle schedules van die dag
                    List<Schedule> daySchedules = schedulesOfEmployee
                        .Where(s => s.Date == day)
                        .ToList();

                    // Als je er vanuit gaat dat er max 1 schedule per dag, kun je .FirstOrDefault() nemen:
                    Schedule scheduleOfDay = daySchedules.FirstOrDefault();

                    // Alle geregistreerde uren van die dag
                    List<RegisteredHours> dayRegHours = regHoursOfEmployee
                        .Where(rh => rh.StartTime.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                        .ToList();

                    // Zelfde: neem 1 record of meer
                    RegisteredHours rhOfDay = dayRegHours.FirstOrDefault();

                    // Bepaal start-/eindtijd van schedule
                    TimeOnly? scheduledStart = null;
                    TimeOnly? scheduledEnd = null;
                    if (scheduleOfDay != null)
                    {
                        scheduledStart = scheduleOfDay.StartTime;
                        scheduledEnd = scheduleOfDay.EndTime;
                    }

                    // Bepaal start-/eindtijd van RegisteredHours
                    TimeOnly? workedStart = null;
                    TimeOnly? workedEnd = null;
                    if (rhOfDay != null)
                    {
                        workedStart = TimeOnly.FromDateTime(rhOfDay.StartTime);
                        workedEnd = TimeOnly.FromDateTime(rhOfDay.EndTime);
                    }

                    // Verschil in uren
                    double dayScheduledHours = (scheduledStart != null && scheduledEnd != null)
                        ? (scheduledEnd.Value.ToTimeSpan() - scheduledStart.Value.ToTimeSpan()).TotalHours
                        : 0;

                    double dayWorkedHours = (workedStart != null && workedEnd != null)
                        ? (workedEnd.Value - workedStart.Value).TotalHours
                        : 0;

                    double dayDifference = dayScheduledHours - dayWorkedHours;
                    bool isSick = daySchedules.Any(sch => sch.IsSick);
                    string notes = isSick ? "Ziek" : "";

                    detailRows.Add(new RegisteredHoursDetailViewModel
                    {
                        Date = day,

                        // in je VM nieuwe props
                        ScheduledStartTime = scheduledStart,
                        ScheduledEndTime = scheduledEnd,
                        WorkedStartTime = workedStart,
                        WorkedEndTime = workedEnd,

                        // evt. nog steeds deze kolommen
                        ScheduledHoursDay = dayScheduledHours,
                        WorkedHoursDay = dayWorkedHours,
                        Difference = dayDifference,
                        Notes = notes
                    });
                }


                EmployeeRowViewModel rowVm = new EmployeeRowViewModel
                {
                    EmployeeId = employee.Id,
                    FullName = $"{employee.FirstName} {employee.MiddleName} {employee.LastName}".Trim(),
                    TotalScheduledHours = totalScheduledHours,
                    TotalWorkedHours = totalWorkedHours,
                    Difference = difference,
                    BonusHours = bonusHours,
                    RegisteredHours = detailRows
                };

                allRows.Add(rowVm);
            }

            RegisteredHoursManagerOverview overview = new RegisteredHoursManagerOverview
            {
                Employees = allRows,
                DisplayYear = targetYear,
                DisplayMonth = targetMonth
            };

            return View(overview);
        }
    }
}
