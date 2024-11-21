using bumbo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace bumbo.Controllers
{
    public class ScheduleManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public ScheduleManagerController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? weekNumber, int? year, int? weekInc)
        {
            SetTempDataForEmployeeToast("scheduleManagerToast");

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // Week en jaar initialiseren als ze ontbreken
            if (!weekNumber.HasValue || !year.HasValue)
            {
                DateTime today = DateTime.Now;
                GregorianCalendar gc = new GregorianCalendar();
                weekNumber = gc.GetWeekOfYear(today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                year = today.Year;
            }

            // Navigatie met pijlen
            if (weekInc.HasValue)
            {
                weekNumber += weekInc.Value;

                // Ga naar volgend jaar bij week 53
                if (weekNumber > 52)
                {
                    weekNumber = 1;
                    year++;
                }
                // Ga naar vorig jaar bij week 0
                else if (weekNumber < 1)
                {
                    weekNumber = 52;
                    year--;
                }
            }
            else
            {
                // Validatie voor handmatige invoer
                if (weekNumber > 52 || weekNumber < 1)
                {
                    TempData["ToastMessage"] = "Je kan alleen weken tussen 1 en 52 kiezen.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction(nameof(Index), new { weekNumber = DateTime.Now.GetWeekOfYear(), year = DateTime.Now.Year });
                }
            }

            // Bereken de datums voor de week
            var dates = GetDatesOfWeek(year.Value, weekNumber.Value);

            // Stel het viewmodel samen
            var viewModel = new ScheduleManagerViewModel
            {
                Year = year.Value,
                WeekNumber = weekNumber.Value,
                Dates = dates
            };

            return View(viewModel);
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
