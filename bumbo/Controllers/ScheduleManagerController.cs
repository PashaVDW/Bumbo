using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bumbo.Controllers
{
    public class ScheduleManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IBranchesRepository _branchesRepository;
        private readonly IPrognosisRepository _prognosisRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ISchoolScheduleRepository _schoolScheduleRepository;
        private readonly ILabourRulesRepository _labourRulesRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentsRepository _departmentRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;

        public ScheduleManagerController(
            UserManager<Employee> userManager,
            IScheduleRepository scheduleRepository,
            IBranchesRepository branchesRepository,
            IPrognosisRepository prognosisRepository,
            IBranchesRepository branchRepository,
            IAvailabilityRepository availabilityRepository,
            ISchoolScheduleRepository schoolScheduleRepository,
            ILabourRulesRepository labourRulesRepository,
            IEmployeeRepository employeeRepository,
            IDepartmentsRepository departmentRepository,
            IBranchHasEmployeeRepository branchHasEmployeeRepository)
        {
            _userManager = userManager;
            _scheduleRepository = scheduleRepository;
            _branchesRepository = branchesRepository;
            _prognosisRepository = prognosisRepository;
            _availabilityRepository = availabilityRepository;
            _schoolScheduleRepository = schoolScheduleRepository;
            _labourRulesRepository = labourRulesRepository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
        }

        public async Task<IActionResult> Index(int? weekNumber, int? year, int? weekInc)
        {
            SetTempDataForEmployeeToast("scheduleManagerToast");

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            if (!weekNumber.HasValue || !year.HasValue)
            {
                DateTime today = DateTime.Now;
                weekNumber = today.GetWeekOfYear();
                year = today.Year;
            }

            if (weekInc.HasValue)
            {
                weekNumber += weekInc.Value;

                if (weekNumber > 52)
                {
                    weekNumber = 1;
                    year++;
                }
                else if (weekNumber < 1)
                {
                    weekNumber = 52;
                    year--;
                }
            }
            else
            {
                if (weekNumber > 52 || weekNumber < 1)
                {
                    TempData["ToastMessage"] = "Je kan alleen weken tussen 1 en 52 kiezen.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction(nameof(Index), new { weekNumber = DateTime.Now.GetWeekOfYear(), year = DateTime.Now.Year });
                }
            }

            List<DateTime> dates = GetDatesOfWeek(year.Value, weekNumber.Value);

            List<string> departments = _scheduleRepository.GetDepartments();

            List<Schedule> schedules = _scheduleRepository.GetSchedulesForBranchByWeek(branchId, dates.Select(d => DateOnly.FromDateTime(d)).ToList());

            List<PrognosisHasDaysHasDepartment> prognosisDetails = _prognosisRepository.GetPrognosisDetailsByBranchWeekAndYear(branchId, weekNumber.Value, year.Value);

            Branch branch = _branchesRepository.GetBranch(branchId);

            var viewModel = new ScheduleManagerViewModel
            {
                Year = year.Value,
                WeekNumber = weekNumber.Value,
                Dates = dates,
                DaySchedules = dates.Select(date => new DayScheduleViewModel
                {
                    Date = date,
                    Departments = departments.Select(department =>
                    {
                        List<Schedule> schedulesForDepartment = schedules
                            .Where(s => s.Date == DateOnly.FromDateTime(date) && s.DepartmentName == department)
                            .OrderBy(s => s.StartTime)
                            .ToList();

                        double hoursNeededForDepartment = prognosisDetails
                            .Where(pd => pd.DayName == date.DayOfWeek.ToString() && pd.DepartmentName == department)
                            .Sum(pd => pd.HoursOfWorkNeeded);

                        return new DepartmentScheduleViewModel
                        {
                            DepartmentName = department,
                            Employees = BuildEmployeeAndGapList(schedulesForDepartment, branch),
                            TotalHours = schedulesForDepartment
                                .Where(s => s.StartTime < s.EndTime && !s.IsSick)
                                .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                            HoursNeeded = hoursNeededForDepartment
                        };
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditDay(string date)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null || date.IsNullOrEmpty())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            ScheduleManagerEditViewModel? viewModel = null;
            if (TempData["EditDayModel"] is string serializedModel)
            {
                viewModel = System.Text.Json.JsonSerializer.Deserialize<ScheduleManagerEditViewModel>(serializedModel);
            }

            DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime);
            string formattedDate = dateTime.ToString("ddd dd MMMM - yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dayTitle = $"{formattedDate.Substring(0, 2)} {formattedDate.Substring(4)}";

            List<string> departments = _scheduleRepository.GetDepartments();
            List<Schedule> schedules = _scheduleRepository.GetScheduleForBranchByDay(branchId, DateOnly.FromDateTime(dateTime));

            int weekNumber = dateTime.GetWeekOfYear();
            int year = dateTime.Year;

            List<PrognosisHasDaysHasDepartment> prognosisDetails = _prognosisRepository.GetPrognosisDetailsByBranchWeekAndYear(branchId, weekNumber, year);

            if (viewModel == null)
            {
                viewModel = new ScheduleManagerEditViewModel
                {
                    Date = date,
                    titleDate = dayTitle,
                    Departments = departments.Select(department =>
                    {
                        var schedulesForDepartment = schedules
                            .Where(s => s.Date == DateOnly.FromDateTime(dateTime) && s.DepartmentName == department)
                            .OrderBy(s => s.StartTime)
                            .ToList();

                        var hoursNeededForDepartment = prognosisDetails
                            .Where(pd => pd.DayName == dateTime.DayOfWeek.ToString() && pd.DepartmentName == department)
                            .Sum(pd => pd.HoursOfWorkNeeded);

                        return new DepartmentScheduleEditViewModel
                        {
                            DepartmentName = department,
                            Employees = BuildEmployeeList(schedulesForDepartment, department, new Dictionary<string, List<string>>()),
                            TotalHours = schedulesForDepartment
                                .Where(s => s.StartTime < s.EndTime)
                                .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                            HoursNeeded = hoursNeededForDepartment
                        };
                    }).ToList()
                };
            }
            else
            {
                var existingErrors = viewModel.Departments
                    .SelectMany(d => d.Employees)
                    .ToDictionary(e => e.EmployeeId, e => e.ValidationErrors);

                // Rebuild departments
                viewModel.Departments = departments.Select(department =>
                {
                    var schedulesForDepartment = schedules
                        .Where(s => s.Date == DateOnly.FromDateTime(dateTime) && s.DepartmentName == department)
                        .OrderBy(s => s.StartTime)
                        .ToList();

                    var hoursNeededForDepartment = prognosisDetails
                        .Where(pd => pd.DayName == dateTime.DayOfWeek.ToString() && pd.DepartmentName == department)
                        .Sum(pd => pd.HoursOfWorkNeeded);

                    return new DepartmentScheduleEditViewModel
                    {
                        DepartmentName = department,
                        Employees = BuildEmployeeList(schedulesForDepartment, department, existingErrors),
                        TotalHours = schedulesForDepartment
                            .Where(s => s.StartTime < s.EndTime)
                            .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                        HoursNeeded = hoursNeededForDepartment
                    };
                }).ToList();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDay(ScheduleManagerEditViewModel model)
        {
            bool hasValidDepartmentName;
            bool isAvailable;
            bool isFreeFromSchool;
            bool isWithinLabourRules;
            bool hasUpdatedAllEmployees = false;

            string date = model.Date;

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || user.ManagerOfBranchId == null)
                {
                    return RedirectToAction("AccessDenied", "Home");
                }

                int branchId = user.ManagerOfBranchId.Value;
                string countryName = _branchesRepository.GetBranchCountryName(branchId);
                
                DateTime.TryParseExact(model.Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime);
                var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(countryName);

                foreach (var department in model.Departments)
                {
                    foreach (var employee in department.Employees)
                    {
                        string employeeId = employee.EmployeeId;
                        var fullEmployeeData = _employeeRepository.GetEmployeeById(employeeId);

                        var today = DateTime.Today;
                        int employeeBirthDate = fullEmployeeData.BirthDate.Year;
                        int employeeAge = today.Year - employeeBirthDate;

                        if (fullEmployeeData.BirthDate > today.AddYears(-employeeAge))
                        {
                            employeeAge--;
                        }

                        string labourRulesToUseString;

                        switch (employeeAge)
                        {
                            case < 16:
                                labourRulesToUseString = "<16";
                                break;

                            case 16:
                                labourRulesToUseString = "16-17";
                                break;

                            case 17:
                                labourRulesToUseString = "16-17";
                                break;

                            case > 17:
                                labourRulesToUseString = ">17";
                                break;
                        }

                        LabourRules labourRulesToUse = labourRules
                            .FirstOrDefault(l => l.AgeGroup.Equals(labourRulesToUseString));

                        var employeeAvailability = _availabilityRepository.GetEmployeeDayAvailability(dateTime, employeeId);
                        var employeeSchoolSchedule = _schoolScheduleRepository.GetEmployeeDaySchoolSchedule(dateTime, employeeId);

                        hasValidDepartmentName = _departmentRepository.IsValidDepartmentName(employee.DepartmentName);
                        isAvailable = CheckAvailabilityEmployee(employeeAvailability, employee);
                        isFreeFromSchool = CheckSchoolScheduleEmployee(employeeSchoolSchedule, employee);
                        isWithinLabourRules = CheckLabourRulesEmployee(labourRulesToUse, employeeSchoolSchedule, employee, labourRulesToUseString, dateTime);

                        if (hasValidDepartmentName && isAvailable && isFreeFromSchool && isWithinLabourRules)
                        {
                            _scheduleRepository.UpdateEmployeeDaySchedule(
                                employeeId,
                                dateTime,
                                employee.StartTime,
                                employee.EndTime,
                                branchId,
                                employee.DepartmentName
                            );

                            hasUpdatedAllEmployees = true;
                        }
                        else
                        {
                            if (!hasValidDepartmentName)
                            {
                                employee.ValidationErrors.Add("De afdeling moet één van de keuzes zijn!");
                            }
                            if (!isAvailable)
                            {
                                employee.ValidationErrors.Add("De medewerker mag niet eerder starten dan zijn/haar beschikbaarheid van " + employeeAvailability.StartTime.ToString() + " en mag niet tot later werken dan zijn/haar beschikbaarheid van " + employeeAvailability.EndTime.ToString() + "!");
                            }
                            if (!isFreeFromSchool)
                            {
                                employee.ValidationErrors.Add("De medewerker mag niet werken tijdens schooltijd van " + employeeSchoolSchedule.StartTime.ToString() + " tot " + employeeSchoolSchedule.EndTime.ToString() + "!");
                            }
                            if (!isWithinLabourRules)
                            {
                                employee.ValidationErrors.Add("De medewerker voldoet niet aan de arbeidstijdenwet!");
                            }

                            hasUpdatedAllEmployees = false;
                        }

                    }
                }

                if (hasUpdatedAllEmployees)
                {
                    TempData["ToastMessage"] = "Alle medewerkers succesvol geupdatet!";
                    TempData["ToastType"] = "success";
                    TempData["ToastId"] = "scheduleToast";
                    TempData["AutoHide"] = "yes";
                    TempData["MilSecHide"] = 3000;
                }
                else
                {
                    TempData["ToastMessage"] = "Geen of niet alle medewerkers geupdatet";
                    TempData["ToastType"] = "error";
                    TempData["ToastId"] = "scheduleToast";
                    TempData["AutoHide"] = "yes";
                    TempData["MilSecHide"] = 3000;
                }

                TempData["EditDayModel"] = System.Text.Json.JsonSerializer.Serialize(model);
                TempData.Keep("EditDayModel");

                return RedirectToAction("EditDay", new { date });
            }

            TempData["EditDayModel"] = System.Text.Json.JsonSerializer.Serialize(model);
            TempData.Keep("EditDayModel");

            TempData["ToastMessage"] = "Medewerkers niet geupdatet wegens foutieve data!";
            TempData["ToastType"] = "error";
            TempData["ToastId"] = "scheduleToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("EditDay", new { date });
        }

        public async Task<IActionResult> ChooseEmployee(string date)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null || date.IsNullOrEmpty())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee(string date, string employeeId)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var fullEmployeeData = _employeeRepository.GetEmployeeById(employeeId);
            var employeeBranches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);
            
            foreach(var branch in employeeBranches)
            {
                if(branch.BranchId != user.ManagerOfBranchId)
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }



            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RemoveEmployeeFromDay(string specificDate, string employeeId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null || specificDate.IsNullOrEmpty() || employeeId.IsNullOrEmpty())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            DateTime.TryParseExact(specificDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime);

            int branchId = user.ManagerOfBranchId.Value;

            if (_scheduleRepository.CanRemoveEmployeeFromDay(DateOnly.FromDateTime(dateTime), employeeId, branchId))
            {
                _scheduleRepository.RemoveEmployeeFromDay(DateOnly.FromDateTime(dateTime), employeeId, branchId);

                TempData["ToastMessage"] = "Employee succesfully removed from this day!";
                TempData["ToastType"] = "success";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
            }
            else
            {
                TempData["ToastMessage"] = "Employee doesn't exist, isn't part of your branch or isn't scheduled today!";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
            }

            return RedirectToAction("EditDay", "ScheduleManager", new { date = specificDate });
        }

        private List<EmployeeScheduleViewModel> BuildEmployeeAndGapList(List<Schedule> sortedSchedules, Branch branch)
        {
            List<EmployeeScheduleViewModel> result = new List<EmployeeScheduleViewModel>();

            TimeOnly workDayStart = branch.OpeningTime;
            TimeOnly workDayEnd = branch.ClosingTime;

            if (sortedSchedules.Count == 0)
            {
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeName = "Gat",
                    StartTime = workDayStart,
                    EndTime = workDayEnd,
                    IsGap = true
                });
                return result;
            }

            List<Schedule> availableSchedules = sortedSchedules.Where(s => !s.IsSick).OrderBy(s => s.StartTime).ToList();

            if (!availableSchedules.Any() || availableSchedules.First().StartTime > workDayStart)
            {
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeName = "Gat",
                    StartTime = workDayStart,
                    EndTime = availableSchedules.Any() ? availableSchedules.First().StartTime : workDayEnd,
                    IsGap = true
                });
            }

            for (int i = 0; i < sortedSchedules.Count; i++)
            {
                Schedule schedule = sortedSchedules[i];

                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeId = schedule.EmployeeId,
                    EmployeeName = $"{schedule.Employee.FirstName} {schedule.Employee.LastName}",
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    IsSick = schedule.IsSick
                });
            }

            for (int i = 0; i < availableSchedules.Count; i++)
            {
                Schedule currentSchedule = availableSchedules[i];

                if (i < availableSchedules.Count - 1)
                {
                    Schedule nextSchedule = availableSchedules[i + 1];
                    if (currentSchedule.EndTime < nextSchedule.StartTime)
                    {
                        result.Add(new EmployeeScheduleViewModel
                        {
                            EmployeeName = "Gat",
                            StartTime = currentSchedule.EndTime,
                            EndTime = nextSchedule.StartTime,
                            IsGap = true
                        });
                    }
                }
            }

            Schedule lastAvailableSchedule = availableSchedules.LastOrDefault();
            if (lastAvailableSchedule != null && lastAvailableSchedule.EndTime < workDayEnd)
            {
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeName = "Gat",
                    StartTime = lastAvailableSchedule.EndTime,
                    EndTime = workDayEnd,
                    IsGap = true
                });
            }

            return result;
        }

        private List<EmployeeScheduleEditViewModel> BuildEmployeeList(List<Schedule> sortedSchedules, string department, Dictionary<string, List<string>>? existingErrors)
        {
            List<EmployeeScheduleEditViewModel> result = new List<EmployeeScheduleEditViewModel>();


            foreach (var schedule in sortedSchedules)
            {
                var employeeModel = new EmployeeScheduleEditViewModel
                {
                    EmployeeId = schedule.EmployeeId,
                    EmployeeName = $"{schedule.Employee.FirstName} {schedule.Employee.LastName}",
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    DepartmentName = department
                };

                if (existingErrors != null && existingErrors.TryGetValue(schedule.EmployeeId, out var errors))
                {
                    employeeModel.ValidationErrors.AddRange(errors);
                }

                result.Add(employeeModel);
            }

            return result;
        }

        private List<DateTime> GetDatesOfWeek(int year, int weekNumber)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek == 1)
            {
                weekNumber -= 1;
            }

            DateTime startOfWeek = firstMonday.AddDays(weekNumber * 7);

            List<DateTime> dates = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                dates.Add(startOfWeek.AddDays(i));
            }

            return dates;
        }

        private void SetTempDataForEmployeeToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;
        }

        private bool CheckAvailabilityEmployee(Availability employeeDayAvailability, EmployeeScheduleEditViewModel employee)
        {
            if (employeeDayAvailability == null)
            {
                return false;
            }

            if (employeeDayAvailability.StartTime <= employee.StartTime)
            {
                if (employeeDayAvailability.EndTime >= employee.EndTime)
                {
                    if (employeeDayAvailability.StartTime < employeeDayAvailability.EndTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckSchoolScheduleEmployee(SchoolSchedule employeeDaySchoolSchedule, EmployeeScheduleEditViewModel employee)
        {
            if (employeeDaySchoolSchedule == null)
            {
                return true;
            }
            else
            {
                if (employee.StartTime < employeeDaySchoolSchedule.StartTime)
                {
                    if (employee.EndTime <= employeeDaySchoolSchedule.StartTime) { return true; }
                }
                else if (employee.StartTime >= employeeDaySchoolSchedule.EndTime)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckLabourRulesEmployee(LabourRules labourRulesToUse, SchoolSchedule employeeDaySchoolSchedule, EmployeeScheduleEditViewModel employee, string labourRulesToUseString, DateTime date)
        {
            if (labourRulesToUse != null)
            {
                var shiftDuration = employee.EndTime.ToTimeSpan() - employee.StartTime.ToTimeSpan();

                if (shiftDuration.TotalHours > labourRulesToUse.MaxShiftDuration)
                {
                    return false;
                }

                if (employee.StartTime < employee.EndTime)
                {
                    double schoolHours = 0;
                    if (employeeDaySchoolSchedule != null)
                    {
                        var schoolDuration = employeeDaySchoolSchedule.EndTime.ToTimeSpan() - employeeDaySchoolSchedule.StartTime.ToTimeSpan();
                        schoolHours = schoolDuration.TotalHours;
                    }

                    if (labourRulesToUseString.Equals("<16") || labourRulesToUseString.Equals("16-17"))
                    {
                        double totalWorkAndSchoolHours = shiftDuration.TotalHours + schoolHours;

                        if (totalWorkAndSchoolHours > labourRulesToUse.MaxHoursPerDay)
                        {
                            return false;
                        }

                        if (employee.EndTime.ToTimeSpan() > labourRulesToUse.MaxEndTime)
                        {
                            return false;
                        }
                    }
                    else if (labourRulesToUseString == ">17")
                    {
                        if (shiftDuration.TotalHours > labourRulesToUse.MaxHoursPerDay)
                        {
                            return false;
                        }
                    }
                }

                int daysToMonday = (int)date.DayOfWeek - (int)DayOfWeek.Monday;
                if (daysToMonday < 0)
                {
                    daysToMonday += 7;
                }

                DateTime monday = date.AddDays(-daysToMonday);

                int daysToSunday = (int)DayOfWeek.Sunday - (int)date.DayOfWeek;
                if (daysToSunday < 0)
                {
                    daysToSunday += 7;
                }

                DateTime sunday = date.AddDays(daysToSunday);

                var weekScheduleEmployee = _scheduleRepository.GetWeekScheduleForEmployee(employee.EmployeeId, monday, sunday);

                double totalWeeklyHours = CalculateEmployeeWeeklyHours(weekScheduleEmployee);

                if (totalWeeklyHours + shiftDuration.TotalHours > labourRulesToUse.MaxHoursPerWeek)
                {
                    return false;
                }

                int totalWorkDaysThisWeek = CalculateEmployeeWorkDaysThisWeek(weekScheduleEmployee);
                if (totalWorkDaysThisWeek > labourRulesToUse.MaxWorkDaysPerWeek)
                {
                    return false;
                }

                int totalRestDaysThisWeek = CalculateEmployeeRestDaysThisWeek(weekScheduleEmployee);
                if (totalRestDaysThisWeek < labourRulesToUse.MinRestDaysPerWeek)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private double CalculateEmployeeWeeklyHours(List<Schedule> weekSchedule)
        {
            double totalHours = 0;

            foreach (var schedule in weekSchedule)
            {
                var shiftDuration = schedule.EndTime.ToTimeSpan() - schedule.StartTime.ToTimeSpan();
                totalHours += shiftDuration.TotalHours;
            }

            return totalHours;
        }

        private int CalculateEmployeeWorkDaysThisWeek(List<Schedule> weekSchedule)
        {
            var workDays = weekSchedule.Select(s => s.Date).Distinct().Count();
            return workDays;
        }

        private int CalculateEmployeeRestDaysThisWeek(List<Schedule> weekSchedule)
        {
            int totalWorkDays = CalculateEmployeeWorkDaysThisWeek(weekSchedule);
            return 7 - totalWorkDays;
        }
    }

    public static class DateTimeExtensions
    {
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }

}
