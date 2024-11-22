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

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var schoolSchedules = new List<SchoolSchedule>();
            var startDate = GetFirstDateOfWeek(viewModel.StartYear, viewModel.StartWeek);
            var endDate = GetFirstDateOfWeek(viewModel.EndYear, viewModel.EndWeek).AddDays(6);

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var dayName = date.ToString("dddd", new CultureInfo("nl-NL"));
                var daySchedule = viewModel.Days.FirstOrDefault(d => d.DayName.Equals(dayName, StringComparison.OrdinalIgnoreCase));

                if (daySchedule != null && daySchedule.StartHour.HasValue && daySchedule.StartMinute.HasValue
                    && daySchedule.EndHour.HasValue && daySchedule.EndMinute.HasValue)
                {
                    var schoolSchedule = new SchoolSchedule
                    {
                        EmployeeId = user.Id,
                        Date = DateOnly.FromDateTime(date),
                        StartTime = new TimeOnly(daySchedule.StartHour.Value, daySchedule.StartMinute.Value),
                        EndTime = new TimeOnly(daySchedule.EndHour.Value, daySchedule.EndMinute.Value)
                    };

                    schoolSchedules.Add(schoolSchedule);
                }
            }

            //_schoolScheduleRepository.AddSchoolSchedulesForEmployee(user.Id, schoolSchedules);

            // Redirect naar een succespagina of een overzicht
            TempData["ToastMessage"] = "Schoolrooster succesvol toegevoegd!";
            TempData["ToastType"] = "success";
            return RedirectToAction("Index");
        }

        private DateTime GetFirstDateOfWeek(int year, int week)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;

            var firstMonday = jan1.AddDays(daysOffset);
            var calendar = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

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
    }
}
