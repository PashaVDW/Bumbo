using bumbo.Components;
using bumbo.Models;
using bumbo.ViewModels;
using bumbo.ViewModels.Prognosis;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Globalization;

namespace bumbo.Controllers
{
    public class PrognosisController : Controller
    {

        private readonly IPrognosisRepository _prognosisRepository;
        private readonly IPrognosisHasDaysRepository _prognosisHasDaysRepository;
        private readonly IPrognosisHasDaysHasDepartments _prognosisHasDaysHasDepartments;
        private readonly INormsRepository _normsRepository;
        private readonly IDaysRepositorySQL _daysRepository;
        private readonly ITemplatesRepository _templatesRepository;
        private readonly DateHelper dateHelper;
        private readonly int _currentYear;
        private readonly int _currentWeek;
        public PrognosisController(
            IPrognosisRepository prognosisRepository,
            IPrognosisHasDaysRepository prognosisHasDaysRepository,
            INormsRepository normsRepository,
            IDaysRepositorySQL daysRepository,
            IPrognosisHasDaysHasDepartments prognosisHasDaysHasDepartments,
            ITemplatesRepository templatesRepository)
        {
            _templatesRepository = templatesRepository;
            _prognosisRepository = prognosisRepository;
            _prognosisHasDaysRepository = prognosisHasDaysRepository;
            _normsRepository = normsRepository;
            _daysRepository = daysRepository;
            _prognosisHasDaysHasDepartments = prognosisHasDaysHasDepartments;

            dateHelper = new DateHelper();
            _currentYear = dateHelper.GetCurrentYear();
            _currentWeek = dateHelper.GetCurrentWeek();
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
                    "Monday" => 1,
                    "Tuesday" => 2,
                    "Wednesday" => 3,
                    "Thursday" => 4,
                    "Friday" => 5,
                    "Saturday" => 6,
                    "Sunday" => 7,
                    _ => 8
                })
                .Select((day, index) =>
                {
                    var departmentList = day.Prognosis_Has_Days_Has_Department.Select(dept => new Prognosis_has_days_has_Department
                    {
                        DepartmentName = dept.DepartmentName,
                        AmountWorkersNeeded = dept.AmountWorkersNeeded,
                        HoursWorkNeeded = dept.HoursWorkNeeded
                    }).ToList();

                    int totalWorkersNeeded = departmentList.Sum(d => d.AmountWorkersNeeded);
                    int totalHoursWorkNeeded = departmentList.Sum(d => d.HoursWorkNeeded);

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

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create(int? id, int? templateId)
        {
            var viewModel = new PrognosisCreateViewModel
            {
                Days = _daysRepository.getAllDays(),
                CustomerAmount = new List<int>(),
                PackagesAmount = new List<int>(),
                WeekNr = _currentWeek,
                Year = _currentYear,
                TemplateName = string.Empty // Default leeg
            };

            if (templateId.HasValue)
            {
                var template = await _templatesRepository.GetByIdAsync(templateId.Value);
                if (template != null && template.TemplateHasDays != null)
                {
                    viewModel.Days = template.TemplateHasDays.Select(td => td.Days).ToList();
                    viewModel.CustomerAmount = template.TemplateHasDays.Select(td => td.CustomerAmount).ToList();
                    viewModel.PackagesAmount = template.TemplateHasDays.Select(td => td.ContainerAmount).ToList();
                    viewModel.TemplateName = template.Name; // Vul de template-naam in het ViewModel
                }
            }

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrognosis(PrognosisCreateViewModel model)
        {
            var prognosisDays = model.Days.Select((day, index) => new Prognosis_has_days
            {
                Days = day,
                CustomerAmount = model.CustomerAmount[index],
                PackagesAmount = model.PackagesAmount[index]
            }).ToList();

            var days = prognosisDays.Select(p => p.Days).ToList();
            var customerAmounts = prognosisDays.Select(p => p.CustomerAmount).ToList();
            var packagesAmounts = prognosisDays.Select(p => p.PackagesAmount).ToList();

            _prognosisRepository.AddPrognosis(days, customerAmounts, packagesAmounts, model.WeekNr, model.Year);

            CalculatePrognosis(model);

            TempData["ToastMessage"] = "De prognose is succesvol aangemaakt!";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "PrognosisCreateSuccess";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;

            return RedirectToAction("Index");
        }

        public void CalculatePrognosis(PrognosisCreateViewModel model)
        {
            List<Norm> norms = _normsRepository.GetSelectedNorms(1, model.Year, model.WeekNr).Result;
            int prognosisId = _prognosisRepository.GetLatestPrognosis().PrognosisId;
            int shelveMeters = _prognosisRepository.GetShelfMetersByPrognosis(prognosisId);

            int cassiereNorm = 30;
            int cassieresNeededForThirtyPerHour = norms[2].normInSeconds;
            int workersNorm = 100;
            int workersNeededForHundredPerHour = norms[3].normInSeconds;

            int colliUitladenNormInSeconds = norms[0].normInSeconds;
            int stockingNormInSeconds = norms[1].normInSeconds;
            int spiegelenNormInSeconds = norms[4].normInSeconds;

            Dictionary<Days, int> cassiereHours = new Dictionary<Days, int>();
            Dictionary<Days, int> cassieresNeeded = new Dictionary<Days, int>();

            Dictionary<Days, int> versWorkersHours = new Dictionary<Days, int>();
            Dictionary<Days, int> workersNeeded = new Dictionary<Days, int>();

            Dictionary<Days, int> stockingHours = new Dictionary<Days, int>();

            for (int i = 0; i < model.Days.Count; i++)
            {
                Days day = model.Days[i];
                int weekhours = day.Name.Equals("Zondag", StringComparison.OrdinalIgnoreCase) ? 8 : 13;

                if (i < model.CustomerAmount.Count)
                {
                    int customerAmount = model.CustomerAmount[i];

                    int cassiereHoursNeeded = customerAmount / cassiereNorm;
                    int workerHoursNeeded = customerAmount / workersNorm;

                    cassieresNeeded.Add(day, (cassiereHoursNeeded * cassieresNeededForThirtyPerHour));
                    cassiereHours.Add(day, cassiereHoursNeeded);

                    versWorkersHours.Add(day, workerHoursNeeded);
                    workersNeeded.Add(day, (workerHoursNeeded * workersNeededForHundredPerHour));
                }

                if (i < model.PackagesAmount.Count)
                {
                    int packageAmount = model.PackagesAmount[i];

                    int colliUitladenHoursNeeded = (packageAmount * colliUitladenNormInSeconds) / 60;
                    int stockingHoursNeeded = (packageAmount * stockingNormInSeconds) / 60;
                    int spiegelenHoursNeeded = (shelveMeters * spiegelenNormInSeconds) / 3600;
                    int totalForStocking = (colliUitladenHoursNeeded + stockingHoursNeeded + spiegelenHoursNeeded);

                    stockingHours.Add(day, totalForStocking);
                }
            }

            _prognosisHasDaysHasDepartments.createCalculation(prognosisId, cassiereHours, versWorkersHours, stockingHours, cassieresNeeded, workersNeeded);
        }


        // GET: Prognosis/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var prognosis = _prognosisRepository.GetPrognosisById(id);
            if (prognosis == null)
            {
                return NotFound();
            }

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
            List<Template> templates =  _templatesRepository.GetAllTemplates();

            return View(templates);
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