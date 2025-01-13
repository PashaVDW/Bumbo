using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace bumbo.Controllers
{
    public class SchoolScheduleController : Controller
    {
        private readonly ISchoolScheduleRepository _schoolScheduleRepository;
        private readonly UserManager<Employee> _userManager;

        public SchoolScheduleController(ISchoolScheduleRepository schoolScheduleRepository, UserManager<Employee> userManager)
        {
            _schoolScheduleRepository = schoolScheduleRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? weekNumber, int? yearNumber)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = currentUser.Id;

            if (weekNumber == null || yearNumber == null)
            {
                DateTime today = DateTime.Now;
                weekNumber = ISOWeek.GetWeekOfYear(today);
                yearNumber = today.Year;
            }
            else
            {
                if (weekNumber < 1)
                {
                    yearNumber--;
                    weekNumber = ISOWeek.GetWeeksInYear(yearNumber.Value);
                }
                else if (weekNumber > ISOWeek.GetWeeksInYear(yearNumber.Value))
                {
                    yearNumber++;
                    weekNumber = 1;
                }
            }

            DateTime startDate = FirstDateOfWeek((int)yearNumber, (int)weekNumber);
            DateTime endDate = LastDateOfWeek((int)yearNumber, (int)weekNumber);

            List<SchoolSchedule> roster = _schoolScheduleRepository.getSchedulesBetweenDates(startDate, endDate, employeeId);

            SchoolScheduleViewModel viewModel = new SchoolScheduleViewModel();

            viewModel.Year = (int)yearNumber;
            viewModel.Month = startDate.ToString("MMMM", new CultureInfo("nl-NL"));
            viewModel.Week = (int)weekNumber;

            if (roster.Count > 0)
            {
                viewModel.Edit = true;
            }

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                var schedule = roster.FirstOrDefault(a => a.Date == DateOnly.FromDateTime(currentDate));

                viewModel.Days.Add(new SchoolScheduleDayOverview
                {
                    DayName = currentDate.ToString("dddd"),
                    Date = currentDate,
                    Status = schedule != null
                        ? $"{schedule.StartTime} - {schedule.EndTime}"
                        : "Leeg"
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddSchoolSchedule()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var today = DateTime.Now;
            var currentWeek = GetCurrentWeek();
            var currentYear = today.Year;

            DateTime startDate = FirstDateOfWeek(currentYear, currentWeek);

            var viewModel = new AddSchoolScheduleViewModel
            {
                StartWeek = currentWeek,
                StartYear = currentYear,
                EndWeek = currentWeek,
                EndYear = currentYear
            };

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                viewModel.Days.Add(new DaySchoolScheduleViewModel
                {
                    DayName = currentDate.ToString("dddd", new CultureInfo("nl-NL")),
                    Date = currentDate,
                    DayNumber = i
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchoolSchedule(AddSchoolScheduleViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            SetTempDataForSchoolScheduleToast("addSchoolScheduleToast");

            string employeeId = user.Id;

            if (viewModel.EndYear < viewModel.StartYear ||
                (viewModel.EndYear == viewModel.StartYear && viewModel.EndWeek < viewModel.StartWeek))
            {
                ModelState.AddModelError("", "De eindweek en het eindjaar moeten na de beginweek en het beginjaar liggen.");
            }

            for (int i = 0; i < 7; i++)
            {
                if (viewModel.Days[i].StartTime.HasValue && viewModel.Days[i].EndTime.HasValue && viewModel.Days[i].EndTime <= viewModel.Days[i].StartTime)
                {
                    ModelState.AddModelError("", $"De eindtijd moet later zijn dan de starttijd voor {viewModel.Days[i].DayName}.");
                }
            }

            if (!ModelState.IsValid)
            {
                TempData["ToastMessage"] = "Rooster niet toegevoegd! Controleer de invoer en probeer opnieuw.";
                TempData["ToastType"] = "error";
                return View(viewModel);
            }

            List<SchoolSchedule> schoolSchedules = new List<SchoolSchedule>();
            DateTime startDate = GetFirstDateOfWeek(viewModel.StartYear, viewModel.StartWeek);
            DateTime endDate = GetFirstDateOfWeek(viewModel.EndYear, viewModel.EndWeek).AddDays(6);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string dayName = date.ToString("dddd", new CultureInfo("nl-NL"));
                var daySchedule = viewModel.Days.FirstOrDefault(d => d.DayName.Equals(dayName, StringComparison.OrdinalIgnoreCase));

                if (daySchedule != null && daySchedule.StartTime.HasValue && daySchedule.EndTime.HasValue)
                {
                    SchoolSchedule schoolSchedule = new SchoolSchedule
                    {
                        EmployeeId = user.Id,
                        Date = DateOnly.FromDateTime(date),
                        StartTime = daySchedule.StartTime.Value,
                        EndTime = daySchedule.EndTime.Value
                    };

                    schoolSchedules.Add(schoolSchedule);
                }
            }

            _schoolScheduleRepository.AddSchoolSchedulesForEmployee(user.Id, schoolSchedules);

            TempData["ToastMessage"] = "Schoolrooster succesvol toegevoegd!";
            TempData["ToastType"] = "success";
            return RedirectToAction("Index");
        }

        private DateTime GetFirstDateOfWeek(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);
            var calendar = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                week -= 1;
            }

            return firstMonday.AddDays(week * 7);
        }

        private int GetCurrentWeek()
        {
            var currentDate = DateTime.Now;
            var culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private void SetTempDataForSchoolScheduleToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;
        }

        public static DateTime LastDateOfWeek(int year, int weekOfYear)
        {
            return FirstDateOfWeek(year, weekOfYear).AddDays(6);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int jan1num = (int)jan1.DayOfWeek;
            int mondaynum = (int)DayOfWeek.Monday;
            int weeksubtract = 1;
            if (jan1num > 4)
            {
                weeksubtract = 0;
            }
            int adddays = (weekOfYear - weeksubtract) * 7 - jan1num + mondaynum;
            DateTime firstWeekStart = jan1.AddDays(adddays);

            return firstWeekStart;
        }
    }
}
