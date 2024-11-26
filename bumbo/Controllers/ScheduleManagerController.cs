using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            IPrognosisRepository prognosisRepository,
            IBranchesRepository branchRepository)
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
