using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using System.Globalization;
using DataLayer.Models;
using bumbo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace bumbo.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;
        private readonly IBranchesRepository _branchesRepository;

        public AvailabilityController(IAvailabilityRepository availabilityRepository, UserManager<Employee> userManager, IBranchHasEmployeeRepository branchHasEmployeeRepository, IBranchesRepository branchesRepository)
        {
            _availabilityRepository = availabilityRepository;
            _userManager = userManager;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
            _branchesRepository = branchesRepository;
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

            List<Availability> availabilities = _availabilityRepository.GetAvailabilitiesBetweenDates(startDate, endDate, employeeId);

            AvailabilityWeekView weekView = new AvailabilityWeekView();

            weekView.Year = (int)yearNumber;
            weekView.Month = startDate.ToString("MMMM", new CultureInfo("nl-NL"));
            weekView.Week = (int)weekNumber;

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                var availability = availabilities.FirstOrDefault(a => a.Date == DateOnly.FromDateTime(currentDate));

                weekView.Days.Add(new AvailabilityDayOverview
                {
                    DayName = currentDate.ToString("dddd"),
                    Date = currentDate,
                    Status = availability != null
                        ? $"{availability.StartTime} - {availability.EndTime}"
                        : "Leeg"
                });
            }

            return View(weekView);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? weekNumber, int? yearNumber)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = currentUser.Id;

            List<BranchHasEmployee> branches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);

            BranchHasEmployee branchHasEmployee = branches.FirstOrDefault();

            if (branchHasEmployee == null)
            {
                TempData["Error"] = "U bent niet gekoppeld aan een filiaal. Neem contact op met uw beheerder voor ondersteuning.";
                return RedirectToAction("Index", new { weekNumber, yearNumber });
            }

            Branch branch = _branchesRepository.GetBranch(branchHasEmployee.BranchId);

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
                EndYear = yearNumber.Value,
                OpeningTime = branch.OpeningTime,
                ClosingTime = branch.ClosingTime
            };

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                model.Days.Add(new AvailabilityDayInputViewModel
                {
                    DayName = currentDate.ToString("dddd", new CultureInfo("nl-NL")),
                    Date = currentDate,
                    DayNumber = i
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AvailabilityInputViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = currentUser.Id;

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

                DateTime periodStartDate = FirstDateOfWeek(currentYear, currentWeek);
                DateTime periodEndDate = LastDateOfWeek(model.EndYear, model.EndWeek);

                while (currentYear < model.EndYear || (currentYear == model.EndYear && currentWeek <= model.EndWeek))
                {
                    DateTime currentDate = FirstDateOfWeek(currentYear, currentWeek);

                    var availabilities = new List<Availability>();

                    foreach (var availability in model.Days)
                    {
                        if (availability != null && availability.StartTime.HasValue && availability.EndTime.HasValue)
                        {
                            DateOnly availabilityDate = DateOnly.FromDateTime(currentDate.AddDays(availability.DayNumber));
                            availabilities.Add(new Availability { Date = availabilityDate,
                                StartTime = availability.StartTime.Value,
                                EndTime = availability.EndTime.Value,
                                EmployeeId = employeeId
                            });
                        }
                        else if (availability != null && availability.AllDay)
                        {
                            DateOnly availabilityDate = DateOnly.FromDateTime(currentDate.AddDays(availability.DayNumber));
                            availabilities.Add(new Availability
                            {
                                Date = availabilityDate,
                                StartTime = model.OpeningTime,
                                EndTime = model.ClosingTime,
                                EmployeeId = employeeId
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

                _availabilityRepository.AddAvailabilities(allAvailabilities, periodStartDate, periodEndDate);

                return RedirectToAction("Index", new { weekNumber = model.StartWeek, yearNumber = model.StartYear });
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                           .Select(x => x.ErrorMessage)
                           .ToList();

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
