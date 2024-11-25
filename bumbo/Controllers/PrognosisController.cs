using bumbo.Components;
using bumbo.Models;
using bumbo.ViewModels;
using bumbo.ViewModels.Prognosis;
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
        private readonly IDaysRepositorySQL _daysRepository;
        private readonly ITemplatesRepository _TemplatesRepository;
        private readonly IPrognosisHasDaysHasDepartments _prognosisHasDaysHasDepartments;
        private readonly DateHelper dateHelper;
        private readonly int _currentYear;
        private readonly int _currentWeek;
        public PrognosisController(
            IPrognosisRepository prognosisRepository,
            IPrognosisHasDaysRepository prognosisHasDaysRepository,
            INormsRepository normsRepository,
            IDaysRepositorySQL daysRepository,
            ITemplatesRepository templatesRepository)
        {
            _prognosisRepository = prognosisRepository;
            _prognosisHasDaysRepository = prognosisHasDaysRepository;
            _normsRepository = normsRepository;
            _daysRepository = daysRepository;

            dateHelper = new DateHelper();
            _currentYear = dateHelper.GetCurrentYear();
            _currentWeek = dateHelper.GetCurrentWeek();
            _TemplatesRepository = templatesRepository;
        }

        private List<DailyCalculationResult> CalculateUrenAndMedewerkers(
            List<PrognosisHasDays> prognosisDays,
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
                    var departmentList = day.PrognosisHasDaysHasDepartment.Select(dept => new PrognosisHasDaysHasDepartment
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
                Days = days, // Vul het model met de berekende dagen
                PrognosisId = prognosis.PrognosisId,
                currentWeek = _currentWeek,
                currentYear = _currentYear
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
                Days = _daysRepository.getAllDaysOrdered(),
                CustomerAmount = new List<int>(),
                PackagesAmount = new List<int>(),
                WeekNr = _currentWeek,
                Year = _currentYear,
                TemplateName = string.Empty // Default leeg
            };

            if (templateId.HasValue)
            {
                var template = await _TemplatesRepository.GetByIdAsync(templateId.Value);
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
            var prognosisDays = model.Days.Select((day, index) => new PrognosisHasDays
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
        public ActionResult Edit(int prognosisId)
        {
            PrognosisEditViewModel viewmodel = new PrognosisEditViewModel();

            Prognosis prognosis = _prognosisRepository.GetPrognosisById(prognosisId);

            List<PrognosisHasDays> hasDays = _prognosisHasDaysRepository.GetPrognosisHasDaysByPrognosisId(prognosisId);

            List<string> day_names = new List<string>();
            List<int> customerAmount = new List<int>();
            List<int> packagesAmount = new List<int>();

            List<Days> days = _daysRepository.getAllDaysOrdered();

            foreach (Days day in days)
            {
                foreach (PrognosisHasDays dayName in hasDays)
                {
                    if (day.Name == dayName.Days_name)
                    {
                        day_names.Add(dayName.Days_name);
                        customerAmount.Add(dayName.CustomerAmount);
                        packagesAmount.Add(dayName.PackagesAmount);
                    }
                }
            }

            viewmodel.PrognosisId = prognosisId;
            viewmodel.Days_name = day_names;
            viewmodel.CustomerAmount = customerAmount;
            viewmodel.PackagesAmount = packagesAmount;
            viewmodel.CurrentWeek = _currentWeek;
            viewmodel.CurrentYear = _currentYear;
            viewmodel.WeekNr = prognosis.WeekNr;
            viewmodel.Year = prognosis.Year;

            return View(viewmodel);
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

        public ActionResult Update(PrognosisUpdateViewModel updatedViewModel)
        {
            List<Days> daysOrdered = _daysRepository.getAllDaysOrdered();
            List<Days> daysUnordered = _daysRepository.getAllDaysUnordered();
            Prognosis prognosis = _prognosisRepository.GetPrognosisById(updatedViewModel.PrognosisId);
            List<PrognosisHasDays> prognosisDays = _prognosisHasDaysRepository.GetPrognosisHasDaysByPrognosisId(updatedViewModel.PrognosisId);

            int highestValue = 0;
            bool negativeNumber = false;
            for (int i = 0; i < prognosisDays.Count; i++)
            {
                for (int j = 0; j < daysUnordered.Count; j++)
                {
                    if (daysOrdered[i] == daysUnordered[j])
                    {
                        prognosisDays[j].CustomerAmount = updatedViewModel.CustomerAmount[i];
                        prognosisDays[j].PackagesAmount = updatedViewModel.PackagesAmount[i];

                        if (highestValue < updatedViewModel.CustomerAmount[i])
                            highestValue = updatedViewModel.CustomerAmount[i];
                        if (highestValue < updatedViewModel.PackagesAmount[i])
                            highestValue = updatedViewModel.PackagesAmount[i];
                        if (updatedViewModel.CustomerAmount[i] < 0 || updatedViewModel.PackagesAmount[i] < 0)
                            negativeNumber = true;
                    }
                }
            }

            if (prognosis.Year <= _currentYear && prognosis.WeekNr <= _currentWeek)
            {
                TempData["ToastMessage"] = "Prognoses bijwerken mislukt. Deze prognoses is al in gebruik.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "updatePrognosisToast";
                TempData["AutoHide"] = "no";
            }
            else if (highestValue > 9999)
            {
                TempData["ToastMessage"] = "Prognoses bijwerken mislukt. De maximale waarde is 9999.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "updatePrognosisToast";
                TempData["AutoHide"] = "no";
            }
            else if (negativeNumber)
            {
                TempData["ToastMessage"] = "Prognoses bijwerken mislukt. Een waarde mag niet negatief zijn.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "updatePrognosisToast";
                TempData["AutoHide"] = "no";
            }
            else
            {
                TempData["ToastMessage"] = "Prognoses succesvol bijgewerkt!";
                TempData["ToastType"] = "success";

                TempData["ToastId"] = "updatePrognosisToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                _prognosisHasDaysRepository.UpdatePrognosisHasDays(prognosisDays);
            }

            return RedirectToAction(nameof(PrognosisController.Index), "Prognosis");
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
            List<Template> templates = _TemplatesRepository.GetAllTemplates();

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