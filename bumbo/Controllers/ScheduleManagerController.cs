using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace bumbo.Controllers
{
    public class ScheduleManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IBranchesRepository _branchesRepository;

        public ScheduleManagerController(
            UserManager<Employee> userManager,
            IScheduleRepository scheduleRepository,
            IBranchesRepository branchesRepository)
        {
            _userManager = userManager;
            _scheduleRepository = scheduleRepository;
            _branchesRepository = branchesRepository;
        }

        public async Task<IActionResult> Index(int? weekNumber, int? year, int? weekInc)
        {
            SetTempDataForEmployeeToast("scheduleManagerToast");

            // Controleer of de gebruiker ingelogd is en gekoppeld is aan een branch
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            // Week en jaar initialiseren als ze ontbreken
            if (!weekNumber.HasValue || !year.HasValue)
            {
                DateTime today = DateTime.Now;
                weekNumber = today.GetWeekOfYear();
                year = today.Year;
            }

            // Navigatie met pijlen
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

            // Bereken de datums van de week
            var dates = GetDatesOfWeek(year.Value, weekNumber.Value);

            // Haal alle afdelingen op
            var departments = _scheduleRepository.GetDepartments();

            // Haal alle ingeplande medewerkers op voor de week
            var schedules = _scheduleRepository.GetSchedulesForBranchByWeek(branchId, dates.Select(d => DateOnly.FromDateTime(d)).ToList());

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

                        return new DepartmentScheduleViewModel
                        {
                            DepartmentName = department,
                            Employees = BuildEmployeeAndGapList(schedulesForDepartment),
                            TotalHours = schedulesForDepartment
                                .Where(s => s.StartTime < s.EndTime)
                                .Sum(s => (s.EndTime - s.StartTime).TotalHours)
                        };
                    }).ToList()
                }).ToList()
            };



            return View(viewModel);
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
