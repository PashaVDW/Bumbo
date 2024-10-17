using bumbo.Components;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static bumbo.Controllers.NormeringController;

namespace bumbo.Controllers
{
    public class Template
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; } 
        public List<Days> DaysList { get; set; }
    }
    public class Days
    {
        public string Name { get; set; }
        public int CustomerAmount { get; set; }
        public int Packages {  get; set; }
    }
    public class PrognosisController : Controller
    {
        List<Template> templates = new List<Template>
        {
            new Template
            {
                TemplateName = "week1",
                TemplateId = 1,
                DaysList = new List<Days>
                {
                    new Days { Name = "Ma", CustomerAmount = 5, Packages = 2 },  // Maandag
                    new Days { Name = "Di", CustomerAmount = 3, Packages = 1 },  // Dinsdag
                    new Days { Name = "Wo", CustomerAmount = 4, Packages = 3 },  // Woensdag
                    new Days { Name = "Do", CustomerAmount = 2, Packages = 2 },  // Donderdag
                    new Days { Name = "Vr", CustomerAmount = 6, Packages = 1 },  // Vrijdag
                    new Days { Name = "Za", CustomerAmount = 1, Packages = 4 },  // Zaterdag
                    new Days { Name = "Zo", CustomerAmount = 7, Packages = 2 }   // Zondag
                }
            },
            new Template
            {
                TemplateName = "week2",
                TemplateId = 2,
                DaysList = new List<Days>
                {
                    new Days { Name = "Ma", CustomerAmount = 4, Packages = 2 },  // Maandag
                    new Days { Name = "Di", CustomerAmount = 5, Packages = 1 },  // Dinsdag
                    new Days { Name = "Wo", CustomerAmount = 3, Packages = 3 },  // Woensdag
                    new Days { Name = "Do", CustomerAmount = 4, Packages = 2 },  // Donderdag
                    new Days { Name = "Vr", CustomerAmount = 5, Packages = 2 },  // Vrijdag
                    new Days { Name = "Za", CustomerAmount = 2, Packages = 1 },  // Zaterdag
                    new Days { Name = "Zo", CustomerAmount = 6, Packages = 3 }   // Zondag
                }
            },
            new Template
            {
                TemplateName = "week3",
                TemplateId = 3,
                DaysList = new List<Days>
                {
                    new Days { Name = "Ma", CustomerAmount = 3, Packages = 1 },  // Maandag
                    new Days { Name = "Di", CustomerAmount = 6, Packages = 2 },  // Dinsdag
                    new Days { Name = "Wo", CustomerAmount = 2, Packages = 4 },  // Woensdag
                    new Days { Name = "Do", CustomerAmount = 7, Packages = 3 },  // Donderdag
                    new Days { Name = "Vr", CustomerAmount = 1, Packages = 2 },  // Vrijdag
                    new Days { Name = "Za", CustomerAmount = 4, Packages = 1 },  // Zaterdag
                    new Days { Name = "Zo", CustomerAmount = 5, Packages = 4 }   // Zondag
                }
            }
        };
        // GET: PrognosisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrognosisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrognosisController/Create
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

            ViewBag.days = new string[] { "Ma", "Di", "Wo", "Do", "Vr", "Za", "Zo" };
            return View();
        }

        // POST: PrognosisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PrognosisController/Edit/5 PARAMETER INT ID MUST BE ADDED
        public ActionResult Edit()
        {
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
