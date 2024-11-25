using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace bumbo.Controllers
{
    public class EmployeeScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly UserManager<Employee> _userManager;

        public EmployeeScheduleController(IScheduleRepository scheduleRepository, UserManager<Employee> userManager)
        {
            _scheduleRepository = scheduleRepository;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index(int? weekNumber, int? yearNumber)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            DateTime today = DateTime.Now;
            DateOnly currentDay = DateOnly.FromDateTime(today);

            if (weekNumber == null || yearNumber == null)
            {
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

            DateTime firstDayOfWeek = ISOWeek.ToDateTime(yearNumber.Value, weekNumber.Value, DayOfWeek.Monday);
            DateOnly startOfWeek = DateOnly.FromDateTime(firstDayOfWeek);

            var days = new List<DateOnly>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(startOfWeek.AddDays(i));
            }

            var scheduleViewModels = _scheduleRepository
                .GetSchedulesForEmployeeByWeek(currentUser.Id, days)
                .Select(schedule => new ScheduleViewModel
                {
                    BranchId = schedule.BranchId,
                    Date = schedule.Date,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    DepartmentName = schedule.Department.DepartmentName,
                    IsSick = schedule.IsSick
                })
                .ToList();

            var groupedSchedules = scheduleViewModels
                .GroupBy(vm => vm.Date)
                .Select(group => new EmployeeDayScheduleViewModel
                {
                    Date = group.Key,
                    Schedules = group.OrderBy(s => s.StartTime).ToList()
                })
                .ToList();

            var employeeSchedulesViewModel = new EmployeeSchedulesViewModel
            {
                Schedules = groupedSchedules,
                Today = currentDay,
                StartOfWeek = startOfWeek,
                SelectedWeek = weekNumber.Value,
                Year = yearNumber.Value
            };

            return View(employeeSchedulesViewModel);
        }

        public async Task<IActionResult> CallSick()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            TempData["ToastId"] = "callSickToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            var todaysSchedules = _scheduleRepository.GetSchedulesForEmployeeByDay(currentUser.Id, DateOnly.FromDateTime(DateTime.Now));
            if (!todaysSchedules.Any())
            {
                TempData["ToastMessage"] = "Je hebt geen ingeplande diensten voor vandaag!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }
            else if (todaysSchedules.Any(s => s.IsSick == true))
            {
                TempData["ToastMessage"] = "Je hebt je al ziek gemeld voor vandaag!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }
            var sickSchedules = _scheduleRepository.SetSchedulesSick(todaysSchedules);
            if (sickSchedules.Any())
            {
                TempData["ToastMessage"] = "Je hebt je ziek gemeld!";
                TempData["ToastType"] = "succes";
            }
            else
            {
                TempData["ToastMessage"] = "Er is iets fout gegaan!";
                TempData["ToastType"] = "error";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CallBetter()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            TempData["ToastId"] = "callBetterToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            var todaysSchedules = _scheduleRepository.GetSchedulesForEmployeeByDay(currentUser.Id, DateOnly.FromDateTime(DateTime.Now));
            if (todaysSchedules.Any())
            {
                var sickSchedules = todaysSchedules.Where(s => s.IsSick == true).ToList();
                if (sickSchedules.Any())
                {
                    _scheduleRepository.SetSchedulesBetter(sickSchedules);
                    TempData["ToastMessage"] = "Je hebt je beter gemeld!";
                    TempData["ToastType"] = "succes";
                }
                else
                {
                    TempData["ToastMessage"] = "Je hebt je nog niet ziek gemeld!";
                    TempData["ToastType"] = "error";
                }
            }
            else
            {
                TempData["ToastMessage"] = "Je hebt geen ingeplande diensten voor vandaag!";
                TempData["ToastType"] = "error";
            }

            return RedirectToAction("Index");
        }
    }
}
