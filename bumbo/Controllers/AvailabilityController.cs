﻿using Microsoft.AspNetCore.Mvc;
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
                Week = weekNumber.Value,
                Year = yearNumber.Value
            };

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                model.Days.Add(new DayInputViewModel
                {
                    DayName = currentDate.ToString("dddd", new CultureInfo("nl-NL")),
                    Date = currentDate
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(AvailabilityInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Maak een lijst van Availability-objecten
                var availabilities = model.Days
                    .Where(day => day.StartTime.HasValue && day.EndTime.HasValue)
                    .Select(day => new Availability
                    {
                        Date = DateOnly.FromDateTime(day.Date),
                        StartTime = day.StartTime.Value,
                        EndTime = day.EndTime.Value,
                        EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3"
                    })
                    .ToList();

                // Stuur de lijst naar de repository
                _availabilityRepository.AddAvailabilities(availabilities);

                return RedirectToAction("Index", new { weekNumber = model.Week, yearNumber = model.Year });
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                                       .Select(x => x.ErrorMessage)
                                       .ToList();
                Console.WriteLine("Errors: " + string.Join(", ", errors));

                // Of geef de errors door aan de view
                ViewBag.Errors = errors;

                return View(model);
            }

            return Redirect("Index");
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
