using bumbo.Components;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using static bumbo.Controllers.NormeringController;

namespace bumbo.Controllers
{
    public class Template
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; } 
        public List<Days> DaysList { get; set; }
    }

    public class PrognosisController : Controller
    {
        private readonly IPrognosisRepository _prognosisRepository;
        private readonly int _currentYear;
        private readonly int _currentWeek;

        public PrognosisController(IPrognosisRepository prognosisRepository)
        {
            _prognosisRepository = prognosisRepository;
             DateTime currentDate = DateTime.Now;
            _currentYear = currentDate.Year;

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            _currentWeek = cultureInfo.Calendar.GetWeekOfYear(
                currentDate,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday
            );
        }

        List<Template> templates = new List<Template>
{
    new Template
    {
        TemplateName = "week1",
        TemplateId = 1,
        DaysList = new List<Days>
        {
            new Days { Name = "Ma" },
            new Days { Name = "Di" },  
            new Days { Name = "Wo" },  
            new Days { Name = "Do" },  
            new Days { Name = "Vr" },  
            new Days { Name = "Za" },  
            new Days { Name = "Zo" }   
        }
    },
    new Template
    {
        TemplateName = "week2",
        TemplateId = 2,
        DaysList = new List<Days>
        {
            new Days { Name = "Ma" }, 
            new Days { Name = "Di" },  
            new Days { Name = "Wo" },  
            new Days { Name = "Do" },  
            new Days { Name = "Vr" },  
            new Days { Name = "Za" },  
            new Days { Name = "Zo" }   
        }
    },
    new Template
    {
        TemplateName = "week3",
        TemplateId = 3,
        DaysList = new List<Days>
        {
            new Days { Name = "Ma" },  
            new Days { Name = "Di" },  
            new Days { Name = "Wo" },  
            new Days { Name = "Do" },  
            new Days { Name = "Vr" },  
            new Days { Name = "Za" },  
            new Days { Name = "Zo" }   
        }
    }
};

        public ActionResult Index()
        {
            ViewBag.CurrentWeek = _currentWeek;
            ViewBag.CurrentYear = _currentYear;

            DateTime firstDayOfWeek = FirstDateOfWeekISO8601(_currentYear, _currentWeek);
            List<DateTime> weekDates = Enumerable.Range(0, 7).Select(i => firstDayOfWeek.AddDays(i)).ToList();

            ViewBag.WeekDates = weekDates;

            return View();
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

        // GET: PrognosisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrognosisController/Create
        [HttpGet]
        public ActionResult Create(int? id)
        {
            var template = templates.Find(t => t.TemplateId == id);

            if (template != null)
            {
                ViewBag.daysList = template;
                ViewBag.templateName = template.TemplateName;
            }
            else
            {
                ViewBag.daysList = null;
                ViewBag.templateName = "Er is geen template geimporteerd";
            }

            ViewBag.days = new List<Days>
            {
                new Days { Name = "Monday" },
                new Days { Name = "Tuesday" },
                new Days { Name = "Wednesday" },
                new Days { Name = "Thursday" },
                new Days { Name = "Friday" },
                new Days { Name = "Saturday" },
                new Days { Name = "Sunday" },
            };

            ViewBag.CurrentWeek = _currentWeek; 
            ViewBag.CurrentYear = _currentYear;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrognosis(List<Days> prognosisCreateDaysList, List<int> CustomerAmount, List<int> PackagesAmount, int weeknr, int year)
        {
            _prognosisRepository.AddPrognosis(prognosisCreateDaysList, CustomerAmount, PackagesAmount, weeknr, year);

            return View("Index");
        }



        // GET: PrognosisController/Edit/5 PARAMETER INT ID MUST BE ADDED
        public ActionResult Edit()
        {
            ViewBag.CurrentWeek = _currentWeek;
            ViewBag.CurrentYear = _currentYear;
            return View();
        }

        // POST: PrognosisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrognosisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrognosisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AddTemplate (string searchTerm, int page = 1)
        {
            var headers = new List<string> { "Naam" };


            var tableBuilder = new TableHtmlBuilder<Template>();
            var htmlTable = tableBuilder.GenerateTable("", headers, templates, "", "", item =>
            {
                return $@"
        <td class='py-2 px-4'>{item.TemplateName}</td>
        <td class='py-2 px-4 text-right'>
            <button class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl' 
                    onclick=""window.location.href='../prognosis/Create?id={item.TemplateId}'"">
                Kies
            </button>
        </td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;
            return View();
        }
    }
}
