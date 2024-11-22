using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
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

            List<SchoolSchedule> roster = _schoolRosterRepository.getSchedulesBetweenDates(startDate, endDate, employeeId);

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
                        : "Nog in te vullen"
                });
            }

            return View(viewModel);
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
