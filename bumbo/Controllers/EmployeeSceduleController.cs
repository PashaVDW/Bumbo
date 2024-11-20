using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace bumbo.Controllers
{
    public class EmployeeSceduleController : Controller
    {
        //private readonly IEmployeeScheduleRepository _employeeScheduleRepository;
        public EmployeeSceduleController()
        {
            //_employeeScheduleRepository = employeeScheduleRepository;
        }
        public IActionResult Index(int plusWeeks = 0)
        {
            DateTime currentDay = DateTime.Today;
            DateOnly today = DateOnly.FromDateTime(currentDay);
            Calendar cal = new CultureInfo("en-NL").Calendar;
            int thisWeek = cal.GetWeekOfYear(currentDay, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int selectedWeek = thisWeek;
            if (plusWeeks != 0)
            {
                selectedWeek = plusWeeks;
            }

            int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7;
            DateOnly startOfWeek = today.AddDays(-daysSinceMonday);

            var days = new List<DateOnly>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(startOfWeek.AddDays(i));
            }

            var schedules = new List<ScheduleViewModel>
            {
                new ScheduleViewModel
                {
                    EmployeeId = 1,
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa",
                    isSick = 0
                },
                new ScheduleViewModel
                {
                    EmployeeId = 1,
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa",
                    isSick = 0
                },
                new ScheduleViewModel
                {
                    EmployeeId = 1,
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Magazijn",
                    isSick = 0
                }
            };
            var groupedSchedules = schedules
                .GroupBy(s => s.Date)
                .Select(group => new DayScheduleViewModel
                {
                    Date = group.Key,
                    Schedules = group.ToList()
                })
                .ToList();

            var employeeSchedulesViewModel = new EmployeeSchedulesViewModel
            {
                Schedules = groupedSchedules,
                Today = today,
                StartOfWeek = startOfWeek,
                SelectedWeek = thisWeek
            };

            return View(employeeSchedulesViewModel);
        }

        public int CallSick(ScheduleViewModel schedule)
        {
            return schedule.BranchId;
        }
    }
}
