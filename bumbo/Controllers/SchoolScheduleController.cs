using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using DataLayer.Models;
using bumbo.ViewModels;
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

            var viewModel = new AddSchoolScheduleViewModel
            {
                StartWeek = currentWeek,
                StartYear = currentYear,
                EndWeek = currentWeek,
                EndYear = currentYear,
                Days = new List<DaySchoolScheduleViewModel>
                {
                    new DaySchoolScheduleViewModel { DayName = "Maandag" },
                    new DaySchoolScheduleViewModel { DayName = "Dinsdag" },
                    new DaySchoolScheduleViewModel { DayName = "Woensdag" },
                    new DaySchoolScheduleViewModel { DayName = "Donderdag" },
                    new DaySchoolScheduleViewModel { DayName = "Vrijdag" },
                    new DaySchoolScheduleViewModel { DayName = "Zaterdag" },
                    new DaySchoolScheduleViewModel { DayName = "Zondag" }
                }
            };

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

            for (int i = 0; i < viewModel.Days.Count; i++)
            {
                DaySchoolScheduleViewModel day = viewModel.Days[i];
                if (day.StartHour.HasValue && day.StartMinute.HasValue && day.EndHour.HasValue && day.EndMinute.HasValue)
                {
                    TimeOnly startTime = new TimeOnly(day.StartHour.Value, day.StartMinute.Value);
                    TimeOnly endTime = new TimeOnly(day.EndHour.Value, day.EndMinute.Value);

                    if (startTime > endTime)
                    {
                        ModelState.AddModelError($"Days[{i}].EndHour", "Eindtijd mag niet eerder zijn dan begintijd.");
                    }
                }
            }

            // Validatie van de ingevoerde periode
            if (!IsValidPeriod(viewModel.StartWeek, viewModel.StartYear, viewModel.EndWeek, viewModel.EndYear))
            {
                ModelState.AddModelError("", "De ingevoerde periode is ongeldig. Controleer de weken en jaren.");
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

                if (daySchedule != null && daySchedule.StartHour.HasValue && daySchedule.StartMinute.HasValue
                    && daySchedule.EndHour.HasValue && daySchedule.EndMinute.HasValue)
                {
                    SchoolSchedule schoolSchedule = new SchoolSchedule
                    {
                        EmployeeId = user.Id,
                        Date = DateOnly.FromDateTime(date),
                        StartTime = new TimeOnly(daySchedule.StartHour.Value, daySchedule.StartMinute.Value),
                        EndTime = new TimeOnly(daySchedule.EndHour.Value, daySchedule.EndMinute.Value)
                    };

                    schoolSchedules.Add(schoolSchedule);
                }
            }

            _schoolScheduleRepository.AddSchoolSchedulesForEmployee(user.Id, schoolSchedules);

            TempData["ToastMessage"] = "Schoolrooster succesvol toegevoegd!";
            TempData["ToastType"] = "success";
            return View(viewModel);
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

        private bool IsValidWeekAndYear(int week, int year)
        {
            if (week < 1 || week > 53) return false;

            if (week == 53)
            {
                DateTime dec31 = new DateTime(year, 12, 31);
                return dec31.DayOfWeek == DayOfWeek.Thursday;
            }

            return true;
        }

        private bool IsValidPeriod(int startWeek, int startYear, int endWeek, int endYear)
        {
            if (startYear > endYear) return false;
            if (startYear == endYear && startWeek > endWeek) return false;

            return IsValidWeekAndYear(startWeek, startYear) && IsValidWeekAndYear(endWeek, endYear);
        }

        private void SetTempDataForSchoolScheduleToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;
        }
    }
}
