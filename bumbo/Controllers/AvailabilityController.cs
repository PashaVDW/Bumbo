using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using System.Globalization;
using DataLayer.Models;
using bumbo.ViewModels;

namespace bumbo.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityRepository availabilityRepository;

        public AvailabilityController(IAvailabilityRepository availabilityRepository)
        {
            this.availabilityRepository = availabilityRepository;
        }

        public IActionResult Index(int? weekNumber, int? yearNumber)
        {
            if (weekNumber == null || yearNumber == null)
            {
                DateTime today = DateTime.Now;
                weekNumber = ISOWeek.GetWeekOfYear(today);
                yearNumber = today.Year;
            }

            DateTime startDate = FirstDateOfWeek((int)yearNumber, (int)weekNumber);
            DateTime endDate = LastDateOfWeek((int)yearNumber, (int)weekNumber);

            List<Availability> availabilities = availabilityRepository.GetAvailabilitiesBetweenDates(startDate, endDate);

            //leeg view mod
            AvailabilityWeekView weekView = new AvailabilityWeekView();

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

            return View();
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
