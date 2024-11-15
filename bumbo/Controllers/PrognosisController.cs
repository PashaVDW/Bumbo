using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace bumbo.Controllers
{
    public class PrognosisController : Controller
    {
        private readonly IPrognosisRepository _prognosisRepository;

        public PrognosisController(IPrognosisRepository prognosisRepository)
        {
            _prognosisRepository = prognosisRepository;

        }

        public ActionResult Index(int? weekNumber, int? year, int? weekInc)
        {
            DateTime firstDayOfWeek;
            DateTime lastDayOfWeek;

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
                if (weekInc.HasValue)
                {
                    weekNumber += weekInc.Value;

                    DateTime jan1;

                    if (weekNumber.Value >= 53)
                    {
                        jan1 = new DateTime(year.Value + 1, 1, 1);
                    }
                    else
                    {
                        jan1 = new DateTime(year.Value, 1, 1);
                    }
                    CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                    Calendar calendar = cultureInfo.Calendar;
                    CalendarWeekRule weekrule = CalendarWeekRule.FirstFourDayWeek;
                    int weekOfJan1 = calendar.GetWeekOfYear(jan1, weekrule, DayOfWeek.Monday);
                    DateTime firstWeekStart = jan1.AddDays(-(int)jan1.DayOfWeek + (int)DayOfWeek.Monday);

                    if (weekOfJan1 != 1)
                    {
                        firstWeekStart = firstWeekStart.AddDays(7);
                    }

                    if (weekNumber.Value == 53)
                    {
                        lastDayOfWeek = LastDateOfWeek(year.Value, weekNumber.Value);
                        if (lastDayOfWeek.CompareTo(firstWeekStart) >= 0)
                        {
                            weekNumber = 1;
                            year = (year ?? DateTime.Now.Year) + 1;
                        }
                    }
                    else if (weekNumber.Value > 53)
                    {
                        weekNumber = 1;
                        year = (year ?? DateTime.Now.Year) + 1;
                    }
                    else if (weekNumber.Value < 1)
                    {
                        weekNumber = weekOfJan1;
                        if (weekOfJan1 == 1)
                        {
                            weekNumber = 52;
                        }

                        year = (year ?? DateTime.Now.Year) - 1;
                    }
                }
                prognosis = _prognosisRepository.GetPrognosisByWeekAndYear(weekNumber.Value, year.Value);
            }

            firstDayOfWeek = FirstDateOfWeek(year.Value, weekNumber.Value);
            lastDayOfWeek = LastDateOfWeek(year.Value, weekNumber.Value);

            ViewBag.FirstDayOfWeek = firstDayOfWeek;
            ViewBag.LastDayOfWeek = lastDayOfWeek;
            ViewBag.MonthName = firstDayOfWeek.ToString("MMMM");
            if (firstDayOfWeek.Month != lastDayOfWeek.Month)
            {
                ViewBag.MonthName += " - " + lastDayOfWeek.ToString("MMMM");
            }

            ViewBag.Title = "Terugblik - Weekoverzicht";

            var days = new List<PrognosisDay>();

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
                .Select((day, index) =>
                {
                    var departmentList = day.prognosis_Has_Days_Has_Department.Select(dept => new Prognosis_has_days_has_Department
                    {
                        DepartmentName = dept.DepartmentName,
                        AmountWorkersNeeded = dept.AmountWorkersNeeded,
                        HoursWorkNeeded = dept.HoursWorkNeeded
                    }).ToList();

                    int totalWorkersNeeded = departmentList.Sum(d => d.AmountWorkersNeeded);
                    int totalHoursWorkNeeded = departmentList.Sum(d => d.HoursWorkNeeded);

                    Console.WriteLine("count: " + departmentList.Count());
                    return new PrognosisDay
                    {
                        DayName = day.Days_name,
                        Date = firstDayOfWeek.AddDays(index),
                        DepartmentList = departmentList,
                        TotalWorkersNeeded = totalWorkersNeeded,
                        TotalHoursWorkNeeded = totalHoursWorkNeeded
                    };
                }).ToList();
            }

            if (prognosis == null)
            {
                ViewBag.Message = "Geen prognose gevonden.";

                // Maak een leeg model
                var emptyModel = new PrognosisViewModel
                {
                    Year = year ?? DateTime.Now.Year,
                    WeekNr = weekNumber ?? CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                    Days = days // Hier is de lijst van dagen nog steeds leeg
                };
                return View(emptyModel);
            }

            var viewModel = new PrognosisViewModel
            {
                Year = prognosis.Year,
                WeekNr = prognosis.WeekNr,
                Days = days // Vul het model met de berekende dagen
            };

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

        //        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        //        {
        //            DateTime jan1 = new DateTime(year, 1, 1);
        //            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

        //            DateTime firstThursday = jan1.AddDays(daysOffset);
        //            var cal = CultureInfo.CurrentCulture.Calendar;
        //            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        //            DateTime firstWeekStart = firstThursday.AddDays(-3);
        //            if (firstWeek <= 1)
        //            {
        //                firstWeekStart = jan1;
        //            }

        //            return firstWeekStart.AddDays((weekOfYear - 1) * 7);
        //        }

        //        // GET: PrognosisController/Details/5
        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }

        //        // GET: PrognosisController/Create
        //        [HttpGet]
        //        public ActionResult Create(int? id)
        //        {
        //            var template = templates.Find(t => t.TemplateId == id);

        //            if (template != null)
        //            {
        //                ViewBag.daysList = template;
        //                ViewBag.templateName = template.TemplateName;
        //            }
        //            else
        //            {
        //                ViewBag.daysList = null;
        //                ViewBag.templateName = "Er is geen template geimporteerd";
        //            }

        //            ViewBag.days = new List<Days>
        //            {
        //                new Days { Name = "Maandag" },
        //                new Days { Name = "Dinsdag" },
        //                new Days { Name = "Woensdag" },
        //                new Days { Name = "Donderdag" },
        //                new Days { Name = "Vrijdag" },
        //                new Days { Name = "Zaterdag" },
        //                new Days { Name = "Zondag" },
        //            };

        //            ViewBag.CurrentWeek = _currentWeek;
        //            ViewBag.CurrentYear = _currentYear;

        //            return View();
        //        }

        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult CreatePrognosis(List<Days> prognosisCreateDaysList, List<int> CustomerAmount, List<int> PackagesAmount, int weeknr, int year)
        //        {
        //            _prognosisRepository.AddPrognosis(prognosisCreateDaysList, CustomerAmount, PackagesAmount, weeknr, year);

        //            return View("Index");
        //        }



        //        // GET: PrognosisController/Edit/5 PARAMETER INT ID MUST BE ADDED
        //        public ActionResult Edit()
        //        {
        //            ViewBag.CurrentWeek = _currentWeek;
        //            ViewBag.CurrentYear = _currentYear;
        //            return View();
        //        }

        //        // POST: PrognosisController/Edit/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit(int id, IFormCollection collection)
        //        {
        //            try
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: PrognosisController/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }

        //        // POST: PrognosisController/Delete/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Delete(int id, IFormCollection collection)
        //        {
        //            try
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
        //        public ActionResult AddTemplate(string searchTerm, int page = 1)
        //        {
        //            var headers = new List<string> { "Naam" };


        //            var tableBuilder = new TableHtmlBuilder<Template>();
        //            var htmlTable = tableBuilder.GenerateTable("", headers, templates, "", item =>
        //            {
        //                return $@"
        //        <td class='py-2 px-4'>{item.TemplateName}</td>
        //        <td class='py-2 px-4 text-right'>
        //            <button class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl' 
        //                    onclick=""window.location.href='../prognosis/Create?id={item.TemplateId}'"">
        //                Kies
        //            </button>
        //        </td>";
        //            }, searchTerm, page);

        //            ViewBag.HtmlTable = htmlTable;
        //            return View();
        //        }
    }
}