using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using System.Globalization;
using DataLayer.Models;
using bumbo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bumbo.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AvailabilityController(IAvailabilityRepository availabilityRepository, IEmployeeRepository employeeRepository)
        {
            _availabilityRepository = availabilityRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index(int? weekNumber, int? yearNumber)
        {
            if (weekNumber == null || yearNumber == null)
            {
                DateTime today = DateTime.Now;
                weekNumber = ISOWeek.GetWeekOfYear(today);
                yearNumber = today.Year;
            }

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

            DateTime startDate = FirstDateOfWeek((int)yearNumber, (int)weekNumber);
            DateTime endDate = LastDateOfWeek((int)yearNumber, (int)weekNumber);

            List<Availability> availabilities = _availabilityRepository.GetAvailabilitiesBetweenDates(startDate, endDate);

            AvailabilityWeekView weekView = new AvailabilityWeekView();

            weekView.Year = (int)yearNumber;
            weekView.Month = startDate.ToString("MMMM", new CultureInfo("nl-NL"));
            weekView.Week = (int)weekNumber;

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                var availability = availabilities.FirstOrDefault(a => a.Date == DateOnly.FromDateTime(currentDate));

                weekView.Days.Add(new DayOverview
                {
                    DayName = currentDate.ToString("dddd"),
                    Date = currentDate,
                    Status = availability != null
                        ? $"{availability.StartTime} - {availability.EndTime}"
                        : "Nog in te vullen"
                });
            }

            return View(weekView);
        }

        [HttpGet]
        public IActionResult Create(int? weekNumber, int? yearNumber)
        {
            if (weekNumber == null || yearNumber == null)
            {
                DateTime today = DateTime.Now;
                weekNumber = ISOWeek.GetWeekOfYear(today);
                yearNumber = today.Year;
            }

            DateTime startDate = FirstDateOfWeek((int)yearNumber, (int)weekNumber);

            var model = new AvailabilityInputViewModel
            {
                StartWeek = weekNumber.Value,
                StartYear = yearNumber.Value,
                EndWeek = weekNumber.Value,
                EndYear = yearNumber.Value
            };

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                model.Days.Add(new DayInputViewModel
                {
                    DayName = currentDate.ToString("dddd", new CultureInfo("nl-NL")),
                    Date = currentDate,
                    DayNumber = i
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(AvailabilityInputViewModel model)
        {
            if (model.EndYear < model.StartYear ||
                (model.EndYear == model.StartYear && model.EndWeek < model.StartWeek))
            {
                ModelState.AddModelError("", "De eindweek en het eindjaar moeten na de beginweek en het beginjaar liggen.");
            }

            for (int i = 0; i < 7; i++)
            {
                if (model.Days[i].StartTime.HasValue && model.Days[i].EndTime.HasValue && model.Days[i].EndTime <= model.Days[i].StartTime)
                {
                    ModelState.AddModelError("", $"De eindtijd moet later zijn dan de starttijd voor {model.Days[i].DayName}.");
                }
            }

            if (ModelState.IsValid)
            {
                List<Availability> allAvailabilities = new List<Availability>();

                int currentWeek = model.StartWeek;
                int currentYear = model.StartYear;

                DateTime startDate = FirstDateOfWeek(currentYear, currentWeek);
                DateTime endDate = LastDateOfWeek(model.EndYear, model.EndWeek);

                while (currentYear < model.EndYear || (currentYear == model.EndYear && currentWeek <= model.EndWeek))
                {
                    DateTime currentDate = FirstDateOfWeek(currentYear, currentWeek);

                    var availabilities = new List<Availability>();

                    foreach (var availability in model.Days)
                    {
                        if (availability != null && availability.StartTime.HasValue && availability.EndTime.HasValue)
                        {
                            DateOnly availabilityDate = DateOnly.FromDateTime(currentDate.AddDays(availability.DayNumber));
                            Console.WriteLine(availability.DayNumber);
                            Console.WriteLine(availabilityDate);
                            availabilities.Add(new Availability { Date = availabilityDate,
                                StartTime = availability.StartTime.Value,
                                EndTime = availability.EndTime.Value,
                                EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3"
                            });
                        }
                    }

                    allAvailabilities.AddRange(availabilities);

                    currentWeek++;

                    if (currentWeek > ISOWeek.GetWeeksInYear(currentYear))
                    {
                        currentWeek = 1;
                        currentYear++;
                    }
                }

                _availabilityRepository.AddAvailabilities(allAvailabilities, startDate, endDate);

                return RedirectToAction("Index", new { weekNumber = model.StartWeek, yearNumber = model.StartYear });
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                           .Select(x => x.ErrorMessage)
                           .ToList();
                Console.WriteLine("Errors: " + string.Join(", ", errors));

                ViewBag.Errors = errors;

                return View(model);
            }
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
