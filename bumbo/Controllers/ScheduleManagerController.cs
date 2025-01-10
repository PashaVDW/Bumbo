using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.CodeAnalysis.Operations;
using System.Text;

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

                        int hoursNeededForDepartment = prognosisDetails
                            .Where(pd => pd.DaysName.Equals(date.ToString("dddd", new System.Globalization.CultureInfo("nl-NL")), StringComparison.OrdinalIgnoreCase) && pd.DepartmentName == department)
                            .Select(pd => pd.HoursOfWorkNeeded)
                            .FirstOrDefault();

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
                        List<Schedule> schedulesForDepartment = schedules
                            .Where(s => s.Date == DateOnly.FromDateTime(dateTime) && s.DepartmentName == department)
                            .OrderBy(s => s.StartTime)
                            .ToList();

                        int hoursNeededForDepartment = prognosisDetails
                            .Where(pd => pd.DaysName.Equals(dateTime.ToString("dddd", new System.Globalization.CultureInfo("nl-NL")), StringComparison.OrdinalIgnoreCase) && pd.DepartmentName == department)
                            .Select(pd => pd.HoursOfWorkNeeded)
                            .FirstOrDefault();

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
                        .Where(pd => pd.DaysName == dateTime.DayOfWeek.ToString() && pd.DepartmentName == department)
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
            bool isStartTimeBeforeEndTime = false;
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
                        Console.WriteLine(employee.StartTime.ToString());
                        Console.WriteLine(employee.EndTime.ToString());
                        if (employee.StartTime >= employee.EndTime)
                        {
                            isStartTimeBeforeEndTime = true;
                        }
                        hasValidDepartmentName = _departmentRepository.IsValidDepartmentName(employee.DepartmentName);
                        isAvailable = CheckAvailabilityEmployee(employeeAvailability, employee.StartTime, employee.EndTime);
                        isFreeFromSchool = CheckSchoolScheduleEmployee(employeeSchoolSchedule, employee.StartTime, employee.EndTime);
                        isWithinLabourRules = CheckLabourRulesEmployee(labourRulesToUse, employeeSchoolSchedule, employee.StartTime, employee.EndTime, employeeId, labourRulesToUseString, dateTime);

                        if (!isStartTimeBeforeEndTime && hasValidDepartmentName && isAvailable && isFreeFromSchool && isWithinLabourRules)
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
                            if (isStartTimeBeforeEndTime)
                            {
                                employee.ValidationErrors.Add("De starttijd moet eerder dan de eindtijd zijn!");
                            }
                            if (!hasValidDepartmentName)
                            {
                                employee.ValidationErrors.Add("De afdeling moet één van de keuzes zijn!");
                            }
                            if (!isAvailable)
                            {
                                if (employeeAvailability == null)
                                {
                                    employee.ValidationErrors.Add("De medewerker heeft voor deze dag geen beschikbaarheid");
                                }
                                else
                                {
                                    employee.ValidationErrors.Add("De medewerker mag niet eerder starten dan zijn/haar beschikbaarheid van " + employeeAvailability.StartTime.ToString() + " en mag niet tot later werken dan zijn/haar beschikbaarheid van " + employeeAvailability.EndTime.ToString() + "!");
                                }
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

        public async Task<IActionResult> ChooseEmployee(string dateString, string searchTerm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null || dateString.IsNullOrEmpty())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            DateTime.TryParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime outputDate);

            DateOnly date = DateOnly.FromDateTime(outputDate);

            List<ScheduleAddEmployeeSingleViewModel> addingemployees = new List<ScheduleAddEmployeeSingleViewModel>();

            int branchId = user.ManagerOfBranchId.Value;

            Branch branch = _branchesRepository.GetBranch(branchId);
            List<string> departments = _scheduleRepository.GetDepartments();
            List<Schedule> allSchedules = _scheduleRepository.GetScheduleForBranchByDay(branchId, DateOnly.FromDateTime(outputDate));
            string countryName = _branchesRepository.GetBranchCountryName(branchId);
            var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(countryName);

            List<Employee> employees = await _employeeRepository.GetEmployeesOfBranch(branchId);

            foreach (Employee employee in employees)
            {
                List<Availability> availability = await _availabilityRepository.GetAvailabilityOfEmployee(employee.Id);

                foreach (Availability a in availability)
                {
                    if (a.Date.Equals(date))
                    {
                        int weekNumber = ISOWeek.GetWeekOfYear(outputDate);

                        DateTime firstDay = ISOWeek.ToDateTime(outputDate.Year, weekNumber, DayOfWeek.Monday);
                        DateTime lastDay = ISOWeek.ToDateTime(outputDate.Year, weekNumber, DayOfWeek.Sunday);

                        List<Schedule> schedules = _scheduleRepository.GetWeekScheduleForEmployee(employee.Id, firstDay, lastDay);

                        var employeeAvailability = _availabilityRepository.GetEmployeeDayAvailability(outputDate, employee.Id);
                        var employeeSchoolSchedule = _schoolScheduleRepository.GetEmployeeDaySchoolSchedule(outputDate, employee.Id);

                        var today = DateTime.Today;
                        int employeeBirthDate = employee.BirthDate.Year;
                        int employeeAge = today.Year - employeeBirthDate;

                        if (employee.BirthDate > today.AddYears(-employeeAge))
                        {
                            employeeAge--;
                        }

                        string labourRulesToUseString = employeeAge switch
                        {
                            < 16 => "<16",
                            16 or 17 => "16-17",
                            > 17 => ">17"
                        };

                        LabourRules labourRulesToUse = labourRules
                            .FirstOrDefault(l => l.AgeGroup.Equals(labourRulesToUseString));

                        TimeOnly plannableHours = CalculatePlannableHours(labourRulesToUse, employeeSchoolSchedule, employeeAvailability, labourRulesToUseString, outputDate, employee.Id);
                        var totalPlannedHours = schedules.Sum(s => (s.EndTime - s.StartTime).TotalHours);

                        addingemployees.Add(new ScheduleAddEmployeeSingleViewModel()
                        {
                            EmployeeId = employee.Id,
                            EmployeeName = $"{employee.FirstName} {employee.MiddleName} {employee.LastName}",
                            Date = date,
                            PlannedHours = totalPlannedHours,
                            ToPlanHours = plannableHours,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime
                        });
                    }
                }
            }

            string title = "Medewerkers toevoegen";
            var headers = new List<string> { "Naam", "Al ingeplande uren", "Nog in te plannen uren", "Beschikbare tijden", "Acties" };

            TableHtmlBuilderNoButton<ScheduleAddEmployeeSingleViewModel> tableBuilder = new TableHtmlBuilderNoButton<ScheduleAddEmployeeSingleViewModel>();

            string tableHtml = tableBuilder.GenerateTable(
                title: title,
                headers: headers,
                items: addingemployees,
                rowTemplate: item => $@"
                <td class='py-2 px-4'>{item.EmployeeName}</td>
                <td class='py-2 px-4'>{item.PlannedHours}</td>
                <td class='py-2 px-4'>{item.ToPlanHours}</td>
                <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                <td class='py-2 px-4'>
                    <button onclick=""window.location.href='/ScheduleManager/AddEmployee?date={item.Date.ToString("yyyy-MM-dd")}&employeeId={item.EmployeeId}'""
                    class='primary_bg_color hover:bg-yellow-400 text-white font-semibold py-2 px-6 rounded-xl'>Toevoegen</button>
                </td>"
                );

            ViewData["TableHtml"] = tableHtml;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee(string date, string employeeId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var fullEmployeeData = _employeeRepository.GetEmployeeById(employeeId);
            var employeeBranches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);
            string employeeFullName = string.IsNullOrWhiteSpace(fullEmployeeData.MiddleName)
            ? $"{fullEmployeeData.FirstName} {fullEmployeeData.LastName}"
            : $"{fullEmployeeData.FirstName} {fullEmployeeData.MiddleName} {fullEmployeeData.LastName}";

            foreach (var employeeBranch in employeeBranches)
            {
                if (employeeBranch.BranchId == user.ManagerOfBranchId)
                {
                    int branchId = user.ManagerOfBranchId.Value;
                    Branch branch = _branchesRepository.GetBranch(branchId);

                    DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime);

                    List<string> departments = _scheduleRepository.GetDepartments();
                    List<Schedule> schedules = _scheduleRepository.GetScheduleForBranchByDay(branchId, DateOnly.FromDateTime(dateTime));

                    int weekNumber = dateTime.GetWeekOfYear();
                    int year = dateTime.Year;

                    var employeeAvailability = _availabilityRepository.GetEmployeeDayAvailability(dateTime, employeeId);
                    var employeeSchoolSchedule = _schoolScheduleRepository.GetEmployeeDaySchoolSchedule(dateTime, employeeId);

                    List<PrognosisHasDaysHasDepartment> prognosisDetails = _prognosisRepository.GetPrognosisDetailsByBranchWeekAndYear(branchId, weekNumber, year);

                    var today = DateTime.Today;
                    int employeeBirthDate = fullEmployeeData.BirthDate.Year;
                    int employeeAge = today.Year - employeeBirthDate;

                    if (fullEmployeeData.BirthDate > today.AddYears(-employeeAge))
                    {
                        employeeAge--;
                    }

                    string labourRulesToUseString = employeeAge switch
                    {
                        < 16 => "<16",
                        16 or 17 => "16-17",
                        > 17 => ">17"
                    };

                    string countryName = _branchesRepository.GetBranchCountryName(branchId);
                    var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(countryName);
                    LabourRules labourRulesToUse = labourRules
                        .FirstOrDefault(l => l.AgeGroup.Equals(labourRulesToUseString));

                    TimeOnly plannableHours = CalculatePlannableHours(labourRulesToUse, employeeSchoolSchedule, employeeAvailability, labourRulesToUseString, dateTime, employeeId);
                    if (plannableHours < TimeOnly.MinValue)
                    {
                        return RedirectToAction("AccessDenied", "Home");
                    }

                    var viewmodel = new ScheduleManagerAddEmployeeViewModel
                    {
                        Date = date,
                        EmployeeId = employeeId,
                        EmployeeName = employeeFullName,
                        DepartmentName = departments.First(),
                        StartTime = employeeAvailability.StartTime,
                        EndTime = employeeAvailability.EndTime,
                        EmployeeAvailableStartTime = employeeAvailability.StartTime,
                        EmployeeAvailableEndTime = employeeAvailability.EndTime,
                        EmployeeLabourRulesOrAvailabilityAvailableTime = plannableHours,
                        DaySchedule = new DayScheduleAddEmployeeViewModel
                        {
                            Date = dateTime,
                            Departments = departments.Select(department =>
                            {
                                List<Schedule> schedulesForDepartment = schedules
                                    .Where(s => s.Date == DateOnly.FromDateTime(dateTime) && s.DepartmentName == department)
                                    .OrderBy(s => s.StartTime)
                                    .ToList();

                                int hoursNeededForDepartment = prognosisDetails
                                   .Where(pd => pd.DaysName.Equals(dateTime.ToString("dddd", new System.Globalization.CultureInfo("nl-NL")), StringComparison.OrdinalIgnoreCase) && pd.DepartmentName == department)
                                   .Select(pd => pd.HoursOfWorkNeeded)
                                   .FirstOrDefault();

                                return new DepartmentScheduleAddEmployeeViewModel
                                {
                                    DepartmentName = department,
                                    Employees = BuildEmployeeAndGapAddEmployeeViewList(schedulesForDepartment, branch),
                                    TotalHours = schedulesForDepartment
                                        .Where(s => s.StartTime < s.EndTime && !s.IsSick)
                                        .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                                    HoursNeeded = hoursNeededForDepartment
                                };
                            }).ToList()
                        }
                    };

                    return View(viewmodel);
                }
            }
            return RedirectToAction("AccessDenied", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([Bind("Date,EmployeeId,DepartmentName,StartTime,EndTime")] ScheduleManagerAddEmployeeViewModel model)
        {
            bool isSuccess = true;
            bool hasValidStartTime = true;
            bool hasValidDepartmentName;
            bool isAvailable;
            bool isFreeFromSchool;
            bool isWithinLabourRules;

            var user = await _userManager.GetUserAsync(User);
            int branchId = user.ManagerOfBranchId.Value;
            string countryName = _branchesRepository.GetBranchCountryName(branchId);

            if (ModelState.IsValid)
            {
                DateTime.TryParseExact(model.Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime);
                if (!_scheduleRepository.EmployeeIsAlreadyPlanned(dateTime, model.EmployeeId))
                {
                    var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(countryName);

                    string employeeId = model.EmployeeId;
                    var fullEmployeeData = _employeeRepository.GetEmployeeById(employeeId);
                    var employeeBranches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);
                    string employeeFullName = string.IsNullOrWhiteSpace(fullEmployeeData.MiddleName)
                    ? $"{fullEmployeeData.FirstName} {fullEmployeeData.LastName}"
                    : $"{fullEmployeeData.FirstName} {fullEmployeeData.MiddleName} {fullEmployeeData.LastName}";

                    foreach (var employeeBranch in employeeBranches)
                    {
                        if (employeeBranch.BranchId == user.ManagerOfBranchId)
                        {
                            var today = DateTime.Today;
                            int employeeBirthDate = fullEmployeeData.BirthDate.Year;
                            int employeeAge = today.Year - employeeBirthDate;

                            if (fullEmployeeData.BirthDate > today.AddYears(-employeeAge))
                            {
                                employeeAge--;
                            }

                            string labourRulesToUseString = employeeAge switch
                            {
                                < 16 => "<16",
                                16 or 17 => "16-17",
                                > 17 => ">17"
                            };

                            LabourRules labourRulesToUse = labourRules
                                .FirstOrDefault(l => l.AgeGroup.Equals(labourRulesToUseString));

                            var employeeAvailability = _availabilityRepository.GetEmployeeDayAvailability(dateTime, employeeId);
                            var employeeSchoolSchedule = _schoolScheduleRepository.GetEmployeeDaySchoolSchedule(dateTime, employeeId);

                            if (model.StartTime >= model.EndTime)
                            {
                                isSuccess = false;
                                hasValidStartTime = false;
                            }

                            hasValidDepartmentName = _departmentRepository.IsValidDepartmentName(model.DepartmentName);
                            isAvailable = CheckAvailabilityEmployee(employeeAvailability, model.StartTime, model.EndTime);
                            isFreeFromSchool = CheckSchoolScheduleEmployee(employeeSchoolSchedule, model.StartTime, model.EndTime);
                            isWithinLabourRules = CheckLabourRulesEmployee(labourRulesToUse, employeeSchoolSchedule, model.StartTime, model.EndTime, employeeId, labourRulesToUseString, dateTime);

                            if (!hasValidDepartmentName || !isAvailable || !isFreeFromSchool || !isWithinLabourRules)
                            {
                                isSuccess = false;
                            }

                            if (!isSuccess)
                            {
                                var errorMessages = new List<string>();

                                if (!hasValidStartTime)
                                {
                                    errorMessages.Add("Begintijd moet voor de eindtijd zijn.");
                                    ModelState.AddModelError($"StarTime", "Begintijd moet voor de eindtijd zijn.");
                                }
                                if (!hasValidDepartmentName)
                                {
                                    errorMessages.Add("Geen valide afdeling gegeven.");
                                    ModelState.AddModelError($"DepartmentName", "Geen valide afdeling gegeven.");
                                }
                                if (!isAvailable)
                                {
                                    errorMessages.Add("Medewerker is niet beschikbaar in de opgegeven tijden.");
                                    ModelState.AddModelError($"StarTime", "Medewerker is niet beschikbaar in de opgegeven tijden.");
                                }
                                if (!isFreeFromSchool)
                                {
                                    errorMessages.Add("Medewerker mag niet werken tijdens schooltijden.");
                                    ModelState.AddModelError($"StarTime", "Medewerker mag niet werken tijdens schooltijden.");
                                }
                                if (!isWithinLabourRules)
                                {
                                    errorMessages.Add("Medewerker voldoet niet aan CAO regels.");
                                }

                                Branch branch = _branchesRepository.GetBranch(branchId);
                                var departments = _scheduleRepository.GetDepartments();
                                var schedules = _scheduleRepository.GetScheduleForBranchByDay(branchId, DateOnly.FromDateTime(dateTime));
                                int weekNumber = dateTime.GetWeekOfYear();
                                int year = dateTime.Year;
                                var prognosisDetails = _prognosisRepository.GetPrognosisDetailsByBranchWeekAndYear(branchId, weekNumber, year);

                                TimeOnly plannableHours = CalculatePlannableHours(labourRulesToUse, employeeSchoolSchedule, employeeAvailability, labourRulesToUseString, dateTime, model.EmployeeId);

                                model.EmployeeName = employeeFullName;
                                model.EmployeeAvailableStartTime = employeeAvailability.StartTime;
                                model.EmployeeAvailableEndTime = employeeAvailability.EndTime;
                                model.EmployeeLabourRulesOrAvailabilityAvailableTime = plannableHours;
                                model.DaySchedule = new DayScheduleAddEmployeeViewModel
                                {
                                    Date = dateTime,
                                    Departments = departments.Select(department =>
                                    {
                                        List<Schedule> schedulesForDepartment = schedules
                                            .Where(s => s.Date == DateOnly.FromDateTime(dateTime) && s.DepartmentName == department)
                                            .OrderBy(s => s.StartTime)
                                            .ToList();

                                        double hoursNeededForDepartment = prognosisDetails
                                            .Where(pd => pd.DaysName == dateTime.DayOfWeek.ToString() && pd.DepartmentName == department)
                                            .Sum(pd => pd.HoursOfWorkNeeded);

                                        return new DepartmentScheduleAddEmployeeViewModel
                                        {
                                            DepartmentName = department,
                                            Employees = BuildEmployeeAndGapAddEmployeeViewList(schedulesForDepartment, branch),
                                            TotalHours = schedulesForDepartment
                                                .Where(s => s.StartTime < s.EndTime && !s.IsSick)
                                                .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                                            HoursNeeded = hoursNeededForDepartment
                                        };
                                    }).ToList()
                                };

                                TempData["ToastMessage"] = string.Join(" ", errorMessages);
                                TempData["ToastType"] = "error";

                                TempData["ToastId"] = "templateToast";
                                TempData["AutoHide"] = "yes";
                                TempData["MilSecHide"] = 3000;

                                return View(model);
                            }

                            _scheduleRepository.AddEmployee(employeeId, branchId, DateOnly.FromDateTime(dateTime), model.DepartmentName, model.StartTime, model.EndTime);

                            TempData["ToastMessage"] = "Medewerker succesvol toegevoegd!";
                            TempData["ToastType"] = "success";
                            TempData["ToastId"] = "scheduleToast";
                            TempData["AutoHide"] = "yes";
                            TempData["MilSecHide"] = 3000;

                            return RedirectToAction("EditDay", new { date = model.Date });
                        }
                    }
                }
                TempData["ToastMessage"] = "Medewerker werkt al op die dag!";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "scheduleToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                return RedirectToAction("Index");
            }

            TempData["ToastMessage"] = "Medewerker niet toegevoegd wegens foutieve data!";
            TempData["ToastType"] = "error";
            TempData["ToastId"] = "scheduleToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("Index");
        }

        private TimeOnly CalculatePlannableHours(LabourRules labourRulesToUse, SchoolSchedule employeeDaySchoolSchedule, Availability employeeAvailability, string labourRulesToUseString, DateTime date, string employeeId)
        {
            TimeOnly plannableHours = new TimeOnly();
            double schoolHours = 0;
            double hoursLeftAvailable = labourRulesToUse.MaxHoursPerDay;
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
            var weekScheduleEmployee = _scheduleRepository.GetWeekScheduleForEmployee(employeeId, monday, sunday);
            double totalWeeklyHours = CalculateEmployeeWeeklyHours(weekScheduleEmployee);

            if (labourRulesToUseString.Equals("<16") || labourRulesToUseString.Equals("16-17"))
            {
                if (employeeDaySchoolSchedule != null)
                {
                    var schoolDuration = employeeDaySchoolSchedule.EndTime.ToTimeSpan() - employeeDaySchoolSchedule.StartTime.ToTimeSpan();
                    schoolHours = schoolDuration.TotalHours;
                }

                hoursLeftAvailable = -schoolHours;
            }

            if (totalWeeklyHours < labourRulesToUse.MaxHoursPerWeek)
            {
                if (labourRulesToUse.MaxHoursPerWeek - totalWeeklyHours < hoursLeftAvailable)
                {
                    hoursLeftAvailable = -totalWeeklyHours;
                }
            }

            if ((employeeAvailability.EndTime - employeeAvailability.StartTime).TotalHours < hoursLeftAvailable)
            {
                hoursLeftAvailable = (employeeAvailability.EndTime - employeeAvailability.StartTime).TotalHours;
            }

            plannableHours = TimeOnly.FromTimeSpan(TimeSpan.FromHours(hoursLeftAvailable));
            return plannableHours;
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

        [HttpPost]
        public async Task<IActionResult> FinalizeSchedule(int weekNumber, int year)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            SetTempDataForEmployeeToast("scheduleManagerFinializeToast");

            int branchId = user.ManagerOfBranchId.Value;

            List<DateTime> dates = GetDatesOfWeek(year, weekNumber);
            List<DateOnly> weekDates = dates.Select(d => DateOnly.FromDateTime(d)).ToList();

            var schedules = _scheduleRepository.GetSchedulesForBranchByWeek(branchId, weekDates);

            if (!schedules.Any())
            {
                TempData["ToastMessage"] = "Er zijn geen roosters om definitief te maken.";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Index), new { weekNumber, year });
            }

            if (schedules.All(s => s.IsFinal))
            {
                TempData["ToastMessage"] = "Alle roosters voor deze week zijn al definitief.";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Index), new { weekNumber, year });
            }

            _scheduleRepository.FinalizeSchedules(branchId, weekDates);

            TempData["ToastMessage"] = "Het rooster is succesvol definitief gemaakt!";
            TempData["ToastType"] = "success";
            return RedirectToAction(nameof(Index), new { weekNumber, year });
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
                    IsSick = schedule.IsSick,
                    IsFinal = schedule.IsFinal,
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

        private List<EmployeeScheduleAddEmployeeViewModel> BuildEmployeeAndGapAddEmployeeViewList(List<Schedule> sortedSchedules, Branch branch)
        {
            List<EmployeeScheduleAddEmployeeViewModel> result = new List<EmployeeScheduleAddEmployeeViewModel>();

            TimeOnly workDayStart = branch.OpeningTime;
            TimeOnly workDayEnd = branch.ClosingTime;

            if (sortedSchedules.Count == 0)
            {
                result.Add(new EmployeeScheduleAddEmployeeViewModel
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
                result.Add(new EmployeeScheduleAddEmployeeViewModel
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

                result.Add(new EmployeeScheduleAddEmployeeViewModel
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
                        result.Add(new EmployeeScheduleAddEmployeeViewModel
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
                result.Add(new EmployeeScheduleAddEmployeeViewModel
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

        private bool CheckAvailabilityEmployee(Availability employeeDayAvailability, TimeOnly startTime, TimeOnly endTime)
        {
            if (employeeDayAvailability == null)
            {
                return false;
            }

            if (employeeDayAvailability.StartTime <= startTime)
            {
                if (employeeDayAvailability.EndTime >= endTime)
                {
                    if (employeeDayAvailability.StartTime < employeeDayAvailability.EndTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckSchoolScheduleEmployee(SchoolSchedule employeeDaySchoolSchedule, TimeOnly startTime, TimeOnly endTime)
        {
            if (employeeDaySchoolSchedule == null)
            {
                return true;
            }
            else
            {
                if (startTime < employeeDaySchoolSchedule.StartTime && endTime <= employeeDaySchoolSchedule.StartTime)
                {
                    return true;
                }
                else if (startTime >= employeeDaySchoolSchedule.EndTime)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckLabourRulesEmployee(LabourRules labourRulesToUse, SchoolSchedule employeeDaySchoolSchedule, TimeOnly startTime, TimeOnly endTime, string employeeId, string labourRulesToUseString, DateTime date)
        {
            if (labourRulesToUse != null)
            {
                var shiftDuration = endTime.ToTimeSpan() - startTime.ToTimeSpan();

                if (shiftDuration.TotalHours > labourRulesToUse.MaxShiftDuration)
                {
                    return false;
                }

                if (startTime < endTime)
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

                        if (endTime.ToTimeSpan() > labourRulesToUse.MaxEndTime)
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

                var weekScheduleEmployee = _scheduleRepository.GetWeekScheduleForEmployee(employeeId, monday, sunday);

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

    public class TableHtmlBuilderNoButton<TItem>
    {
        public string GenerateTable(string title, List<string> headers, List<TItem> items, Func<TItem, string> rowTemplate, string searchTerm = null, int currentPage = 1, int pageSize = 10)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                items = items.Where(item =>
                {
                    var properties = typeof(TItem).GetProperties();
                    return properties.Any(prop => prop.GetValue(item)?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
                }).ToList();
            }

            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            var pagedItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<div class='container mx-auto p-10'>" +
                                   "<div class='flex justify-between items-center mb-4'>" +
                                   "<h2 class='mb-4 text-4xl font-bold leading-none tracking-tight text-gray-900'>" + title + "</h2>" +
                                   "<form method='get' class='flex items-center space-x-4'>" +
                                   "<input type='text' name='searchTerm' value='" + searchTerm + "' placeholder='Zoek naar " + title.ToLower() + "' class='w-full border border-gray-300 rounded-full py-2 px-6 focus:outline-none focus:ring-2 focus:ring-yellow-400' />" +
                                   "<button type='submit' class='bg-gray-200 text-gray-700 py-2 px-6 rounded-full hover:bg-gray-300'>Zoeken</button>" +
                                   "</form>" +
                                   "</div>"
                                   );
            htmlBuilder.AppendLine("<div class='w-full p-6'>");
            htmlBuilder.AppendLine("<div class='overflow-x-auto w-full'>");
            htmlBuilder.AppendLine("<table class='min-w-full table-auto border-collapse'>");

            htmlBuilder.AppendLine(
                "<thead>" +
                "<tr class='text-left text-gray-600 font-bold'>"
            );
            foreach (var header in headers)
            {
                htmlBuilder.AppendLine($"<th class='py-2 px-4'>{header}</th>");
            }
            htmlBuilder.AppendLine(
                "</tr>" +
                "</thead>"
            );
            htmlBuilder.AppendLine("<tbody>");
            foreach (var item in pagedItems)
            {
                htmlBuilder.AppendLine("<tr class='border-b hover:bg-gray-50'>");
                htmlBuilder.AppendLine(rowTemplate(item));
                htmlBuilder.AppendLine("</tr>");
            }
            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("<div class='flex justify-center items-center mt-4 space-x-2'>");

            if (currentPage > 1)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage - 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Vorige</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Vorige</span>");
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    htmlBuilder.AppendLine($"<span class='py-2 px-4 bg-yellow-400 text-white rounded-lg'>{i}</span>");
                }
                else
                {
                    htmlBuilder.AppendLine($"<a href='?page={i}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300'>{i}</a>");
                }
            }
            if (currentPage < totalPages)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage + 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Volgende</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Volgende</span>");
            }
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("</div>");

            return htmlBuilder.ToString();
        }
    }
}
