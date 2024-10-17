using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using DataLayer.Interfaces;
using bumbo.ViewModels;
using System.Globalization;

namespace bumbo.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        readonly IPrognosisHasDaysRepository _prognosisHasDaysRepository;
        readonly IPrognosisRepository _prognosisRepository;

        public ReviewsController(UserManager<Employee> userManager, IPrognosisRepository prognosisRepository, IPrognosisHasDaysRepository prognosisHasDaysRepository)
        {
            _userManager = userManager;
            _prognosisRepository = prognosisRepository;
            _prognosisHasDaysRepository = prognosisHasDaysRepository;
        }

        public async Task<IActionResult> Index(int? weekNumber, int? year)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            Prognosis prognosis;

            if (!weekNumber.HasValue || !year.HasValue)
            {
                prognosis = _prognosisRepository.GetLatestPrognosis();

                if (prognosis != null)
                {
                    weekNumber = prognosis.WeekNr;
                    year = prognosis.Year;
                }
                else
                {
                    DateTime today = DateTime.Now;
                    GregorianCalendar gc = new GregorianCalendar();
                    weekNumber = gc.GetWeekOfYear(today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    year = today.Year;
                }
            }
            else
            {
                prognosis = _prognosisRepository.GetPrognosisByWeekAndYear(weekNumber.Value, year.Value);
            }

            // Bereken de eerste en laatste dag van de week
            DateTime firstDayOfWeek = FirstDateOfWeek(year.Value, weekNumber.Value);
            DateTime lastDayOfWeek = LastDateOfWeek(year.Value, weekNumber.Value);

            ViewBag.FirstDayOfWeek = firstDayOfWeek;
            ViewBag.LastDayOfWeek = lastDayOfWeek;

            ViewBag.MonthName = firstDayOfWeek.ToString("MMMM");
            if (firstDayOfWeek.Month != lastDayOfWeek.Month)
            {
                ViewBag.MonthName += " - " + lastDayOfWeek.ToString("MMMM");
            }

            ViewBag.Title = "Terugblik - Weekoverzicht";

            // Bereken de dagen voor het model
            var days = new List<DayOverviewViewModel>();
            if (prognosis != null)
            {
                days = prognosis.Prognosis_Has_Days
                    .OrderBy(d => d.Days_name switch
                    {
                        "Maandag" => 1,
                        "Dinsdag" => 2,
                        "Woensdag" => 3,
                        "Donderdag" => 4,
                        "Vrijdag" => 5,
                        "Zaterdag" => 6,
                        "Zondag" => 7,
                        _ => 8
                    })
                    .Select(day => new DayOverviewViewModel
                    {
                        DayName = day.Days_name,
                        CustomerAmount = day.CustomerAmount,
                        PackagesAmount = day.PackagesAmount
                    }).ToList();
            }

            // Als er geen prognose is gevonden, maak dan een leeg model aan
            if (prognosis == null)
            {
                ViewBag.Message = "Geen prognose gevonden.";

                // Bereken de vorige of volgende week, en het jaar indien nodig
                if (weekNumber.HasValue)
                {
                    if (weekNumber > 52) // Bij het overschrijden van het jaar
                    {
                        weekNumber = 1;
                        year = (year ?? DateTime.Now.Year) + 1;
                    }
                    else if (weekNumber < 1) // Bij het overschrijden naar het vorige jaar
                    {
                        weekNumber = 52; // Dit kan ook anders worden ingericht afhankelijk van het jaar
                        year = (year ?? DateTime.Now.Year) - 1;
                    }
                }

                // Maak een leeg model
                var emptyModel = new WeekOverviewViewModel
                {
                    Year = year ?? DateTime.Now.Year,
                    WeekNr = weekNumber ?? CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                    Days = days // Hier is de lijst van dagen nog steeds leeg
                };
                return View(emptyModel);
            }

            var viewModel = new WeekOverviewViewModel
            {
                Year = prognosis.Year,
                WeekNr = prognosis.WeekNr,
                Days = days // Vul het model met de berekende dagen
            };

            return View(viewModel);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            DateTime firstWeekStart = jan1.AddDays((weekOfYear - 1) * 7 - (int)jan1.DayOfWeek + (int)DayOfWeek.Monday);

            return firstWeekStart;
        }

        public static DateTime LastDateOfWeek(int year, int weekOfYear)
        {
            return FirstDateOfWeek(year, weekOfYear).AddDays(6);
        }

    }
}
