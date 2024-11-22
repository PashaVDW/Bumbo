using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
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

        public ScheduleManagerController(
            UserManager<Employee> userManager,
            IScheduleRepository scheduleRepository,
            IBranchesRepository branchesRepository,
            IPrognosisRepository prognosisRepository)
        {
            _userManager = userManager;
            _scheduleRepository = scheduleRepository;
            _branchesRepository = branchesRepository;
            _prognosisRepository = prognosisRepository;
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

                        var hoursNeededForDepartment = prognosisDetails
                            .Where(pd => pd.DayName == date.DayOfWeek.ToString() && pd.DepartmentName == department)
                            .Sum(pd => pd.HoursOfWorkNeeded);

                        return new DepartmentScheduleViewModel
                        {
                            DepartmentName = department,
                            Employees = BuildEmployeeAndGapList(schedulesForDepartment),
                            TotalHours = schedulesForDepartment
                                .Where(s => s.StartTime < s.EndTime)
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

            if (DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                string formattedDate = dateTime .ToString("ddd dd MMMM - yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string dayTitle = $"{formattedDate.Substring(0, 2)} {formattedDate.Substring(4)}";

                ViewBag.Day = dayTitle;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }

            List<string> departments = _scheduleRepository.GetDepartments();
            List<Schedule> schedules = _scheduleRepository.GetScheduleForBranchByDay(branchId, DateOnly.FromDateTime(dateTime));

            int weekNumber = dateTime.GetWeekOfYear();
            int year = dateTime.Year;

            List<PrognosisHasDaysHasDepartment> prognosisDetails = _prognosisRepository.GetPrognosisDetailsByBranchWeekAndYear(branchId, weekNumber, year);

            var viewModel = new ScheduleManagerEditViewModel
            {
                Date = date,
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
                        Employees = BuildEmployeeList(schedulesForDepartment, department),
                        TotalHours = schedulesForDepartment
                            .Where(s => s.StartTime < s.EndTime)
                            .Sum(s => (s.EndTime - s.StartTime).TotalHours),
                        HoursNeeded = hoursNeededForDepartment
                    };
                }).ToList()
            };

            Console.WriteLine();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditDay(string date, string employeeId)
        {


            return View();
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

        private List<EmployeeScheduleViewModel> BuildEmployeeAndGapList(List<Schedule> sortedSchedules)
        {
            var result = new List<EmployeeScheduleViewModel>();

            //TODO: Hier nog werktijden ophalen
            var workDayStart = new TimeOnly(8, 0);
            var workDayEnd = new TimeOnly(21, 30);

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

            if (sortedSchedules.First().StartTime > workDayStart)
            {
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeName = "Gat",
                    StartTime = workDayStart,
                    EndTime = sortedSchedules.First().StartTime,
                    IsGap = true
                });
            }

            for (int i = 0; i < sortedSchedules.Count; i++)
            {
                var schedule = sortedSchedules[i];
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeId = schedule.EmployeeId,
                    EmployeeName = $"{schedule.Employee.FirstName} {schedule.Employee.LastName}",
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    IsSick = schedule.IsSick
                });

                if (i < sortedSchedules.Count - 1)
                {
                    var currentEnd = schedule.EndTime;
                    var nextStart = sortedSchedules[i + 1].StartTime;

                    if (currentEnd < nextStart)
                    {
                        result.Add(new EmployeeScheduleViewModel
                        {
                            EmployeeName = "Gat",
                            StartTime = currentEnd,
                            EndTime = nextStart,
                            IsGap = true
                        });
                    }
                }
            }

            if (sortedSchedules.Last().EndTime < workDayEnd)
            {
                result.Add(new EmployeeScheduleViewModel
                {
                    EmployeeName = "Gat",
                    StartTime = sortedSchedules.Last().EndTime,
                    EndTime = workDayEnd,
                    IsGap = true
                });
            }

            return result;
        }

        private List<EmployeeScheduleEditViewModel> BuildEmployeeList(List<Schedule> sortedSchedules, string department)
        {
            List<EmployeeScheduleEditViewModel> result = new List<EmployeeScheduleEditViewModel>();

            var workDayStart = new TimeOnly(8, 0);
            var workDayEnd = new TimeOnly(21, 30);

            for (int i = 0; i < sortedSchedules.Count; i++)
            {
                var schedule = sortedSchedules[i];
                result.Add(new EmployeeScheduleEditViewModel
                {
                    EmployeeId = schedule.EmployeeId,
                    EmployeeName = $"{schedule.Employee.FirstName} {schedule.Employee.LastName}",
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    DepartmentName = department
                });
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

            var dates = new List<DateTime>();
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
    }

    public static class DateTimeExtensions
    {
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }

}
