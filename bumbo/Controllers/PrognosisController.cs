using bumbo.Components;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace bumbo.Controllers
{

    public class PrognosisController : Controller
    {

        List<Template> templates = new List<Template>();
        private readonly IPrognosisRepository _prognosisRepository;
        private readonly IPrognosisHasDaysRepository _prognosisHasDaysRepository;
        private readonly INormsRepository _normsRepository;

        private readonly int _currentYear;
        private readonly int _currentWeek;

        public PrognosisController(
            IPrognosisRepository prognosisRepository,
            IPrognosisHasDaysRepository prognosisHasDaysRepository,
            INormsRepository normsRepository)
        {
            _prognosisRepository = prognosisRepository;
            _prognosisHasDaysRepository = prognosisHasDaysRepository;
            _normsRepository = normsRepository;

            DateTime currentDate = DateTime.Now;
            _currentYear = currentDate.Year;

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            _currentWeek = cultureInfo.Calendar.GetWeekOfYear(
                currentDate,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday
            );
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.CurrentWeek = _currentWeek;
            ViewBag.CurrentYear = _currentYear;

            var weekPrognosis = _prognosisRepository.GetLatestPrognosis();

            if (weekPrognosis != null)
            {
                ViewBag.LatestPrognosis = weekPrognosis;
                var weekPrognosisHasDays = _prognosisHasDaysRepository.GetLatestPrognosis_has_days();
                var norms = await _normsRepository.GetSelectedNorms(1, weekPrognosis.Year, weekPrognosis.WeekNr);
                var calculationResults = CalculateUrenAndMedewerkers(weekPrognosisHasDays, norms);
                ViewBag.CalculationResults = calculationResults;
                ViewBag.WeekPrognosisHasDays = weekPrognosisHasDays;
            }
            DateTime firstDayOfWeek = FirstDateOfWeekISO8601(_currentYear, _currentWeek);
            List<DateTime> weekDates = Enumerable.Range(0, 7).Select(i => firstDayOfWeek.AddDays(i)).ToList();

            ViewBag.WeekDates = weekDates;

            return View();
        }

        private List<DailyCalculationResult> CalculateUrenAndMedewerkers(
            List<Prognosis_has_days> prognosisDays,
            List<Norm> norms)
        {
            var results = new List<DailyCalculationResult>();

            foreach (var day in prognosisDays)
            {
                var dayResult = new DailyCalculationResult
                {
                    DayName = day.Days_name,
                    DepartmentCalculations = new List<DepartmentCalculationResult>()
                };

                // Define each department (activity) and its associated amount type
                var activities = new List<(string activity, int amount)>
            {
                ("Coli uitladen", day.PackagesAmount),
                ("Vakkenvullen", day.PackagesAmount),
                ("Kassa", day.CustomerAmount),
                ("Vers", day.PackagesAmount),
                ("Spiegelen", day.PackagesAmount)
            };
                ViewBag.Activities = activities;
                foreach (var (activity, amount) in activities)
                {
                    double totalSeconds = amount * GetNormInSeconds(activity, norms);
                    int uren = (int)Math.Ceiling(totalSeconds / 3600);
                    int medewerkersNeeded = (int)Math.Ceiling(uren / 8.0);

                    dayResult.DepartmentCalculations.Add(new DepartmentCalculationResult
                    {
                        Activity = activity,
                        Uren = uren,
                        MedewerkersNeeded = medewerkersNeeded
                    });
                }

                results.Add(dayResult);
            }

            return results;
        }


        private double GetNormInSeconds(string activity, List<Norm> norms)
        {
            var norm = norms.FirstOrDefault(n => n.activity == activity);
            return norm != null ? norm.normInSeconds : 0;
        }

        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            DateTime firstWeekStart = firstThursday.AddDays(-3);
            if (firstWeek <= 1)
            {
                firstWeekStart = jan1;
            }

            return firstWeekStart.AddDays((weekOfYear - 1) * 7);
        }

        // Your existing methods remain untouched and are included here

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            var template = templates.Find(t => t.Id == id);

            if (template != null)
            {
                ViewBag.daysList = template;
                ViewBag.templateName = template.Name;
            }
            else
            {
                ViewBag.daysList = null;
                ViewBag.templateName = "Er is geen template geimporteerd";
            }

            ViewBag.days = new List<Days>
            {
                new Days { Name = "Maandag" },
                new Days { Name = "Dinsdag" },
                new Days { Name = "Woensdag" },
                new Days { Name = "Donderdag" },
                new Days { Name = "Vrijdag" },
                new Days { Name = "Zaterdag" },
                new Days { Name = "Zondag" },
            };

            ViewBag.CurrentWeek = _currentWeek;
            ViewBag.CurrentYear = _currentYear;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrognosis(List<Days> prognosisCreateDaysList, List<int> CustomerAmount, List<int> PackagesAmount, int weeknr, int year)
        {
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 4000;
            TempData["ToastId"] = "createPrognosisToast";
            if (year < DateTime.Now.Year || year > DateTime.Now.Year + 1)
            {
                TempData["ToastMessage"] = "Het jaar moet het huidige of het volgende jaar zijn.";
                TempData["ToastType"] = "error";

                return View("Create");
            }
            _prognosisRepository.AddPrognosis(prognosisCreateDaysList, CustomerAmount, PackagesAmount, weeknr, year);
            TempData["ToastMessage"] = "Prognose succesvol aangemaakt!";
            TempData["ToastType"] = "success";
            return RedirectToAction("Index");
        }

        // GET: Prognosis/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Fetch the prognosis by ID
            var prognosis = _prognosisRepository.GetPrognosisById(id);
            if (prognosis == null)
            {
                return NotFound();
            }

            // Get the related days and populate the ViewBag
            var prognosisDays = _prognosisHasDaysRepository.GetPrognosisHasDaysByPrognosisId(id);
            ViewBag.DaysList = prognosisDays;

            ViewBag.CurrentWeek = prognosis.WeekNr;
            ViewBag.CurrentYear = prognosis.Year;

            return View(prognosis);
        }

        // POST: Prognosis/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, List<int> CustomerAmount, List<int> PackagesAmount)
        {
            try
            {
                // Update the prognosis details
                _prognosisRepository.UpdatePrognosis(id, CustomerAmount, PackagesAmount);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            _prognosisRepository.DeletePrognosisById(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _prognosisRepository.DeletePrognosisById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }


        public ActionResult AddTemplate(string searchTerm, int page = 1)
        {
            var headers = new List<string> { "Naam" };

            var tableBuilder = new TableHtmlBuilder<Template>();
            var htmlTable = tableBuilder.GenerateTable("", headers, templates, "", item =>
            {
                return $@"
                    <td class='py-2 px-4'>{item.Name}</td>
                    <td class='py-2 px-4 text-right'>
                        <button class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl' 
                                onclick=""window.location.href='../prognosis/Create?id={item.Id}'"">
                            Kies
                        </button>
                    </td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;
            return View();
        }
    }

    public class DailyCalculationResult
    {
        public string DayName { get; set; }
        public List<DepartmentCalculationResult> DepartmentCalculations { get; set; }
    }

    public class DepartmentCalculationResult
    {
        public string Activity { get; set; }
        public int Uren { get; set; }
        public int MedewerkersNeeded { get; set; }
    }
}
