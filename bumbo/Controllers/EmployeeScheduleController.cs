using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace bumbo.Controllers
{
    public class EmployeeScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRegisteredHoursRepository _registeredHoursRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;
        private readonly UserManager<Employee> _userManager;

        public EmployeeScheduleController(IScheduleRepository scheduleRepository, IRegisteredHoursRepository registeredHoursRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository, UserManager<Employee> userManager)
        {
            _scheduleRepository = scheduleRepository;
            _registeredHoursRepository = registeredHoursRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
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
                .Where(schedule => schedule.IsFinal)
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

        public async Task<IActionResult> RegisteredHoursView(int year, int week)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;

            DateTime today = DateTime.Now;
            DateTime firstDayThisWeek = ISOWeek.ToDateTime(today.Year, today.GetWeekOfYear(), DayOfWeek.Monday);
            DateTime lastDayThisWeek = ISOWeek.ToDateTime(today.Year, today.GetWeekOfYear(), DayOfWeek.Sunday);
            
            List<Schedule> thisWeekWeekSchedule = _scheduleRepository.GetWeekScheduleForEmployee(user.Id, firstDayThisWeek, lastDayThisWeek);


            DateTime firstDayOtherWeek; 
            DateTime lastDayOtherWeek; 
            DateTime date;
            
            if (year <= 0 || week <= 0)
            {
                firstDayOtherWeek = ISOWeek.ToDateTime(today.Year, today.GetWeekOfYear(), DayOfWeek.Monday);
                lastDayOtherWeek = ISOWeek.ToDateTime(today.Year, today.GetWeekOfYear(), DayOfWeek.Sunday);
                date = DateTime.Now;
            }
            else
            {
                firstDayOtherWeek = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
                lastDayOtherWeek = ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday);
                if (week == 1)
                {
                    date = new DateTime(year, lastDayOtherWeek.Month, lastDayOtherWeek.Day);
                } else
                {
                    date = new DateTime(year, firstDayOtherWeek.Month, firstDayOtherWeek.Day);
                }
            }

            List<Schedule> otherWeekWeekSchedule = _scheduleRepository.GetWeekScheduleForEmployee(user.Id, firstDayOtherWeek, lastDayOtherWeek);

            if (thisWeekWeekSchedule.Count == 0)
            {
                thisWeekWeekSchedule.Add(new Schedule()
                {
                    EmployeeId = "",
                    StartTime = new TimeOnly(0, 0),
                    EndTime = new TimeOnly(0, 0),
                    BranchId = 0,
                    DepartmentName = "",
                    Date = new DateOnly(),
                });
            }

            EmployeeRegisterHoursViewModel viewModel = new EmployeeRegisterHoursViewModel()
            {
                HasStarted = _registeredHoursRepository.IsClockedIn(employeeId),
                Today = today,
                DayName = GetDayNameByDate(today),
                WeekSchedule = thisWeekWeekSchedule,
                FirstShift = thisWeekWeekSchedule.First(),

                Year = date.Year,
                Week = date.GetWeekOfYear(),
                MonthName = GetMonthNameByDate(date),
                RegisteredHoursSchedule = otherWeekWeekSchedule,
            };

            return View(viewModel);
        }

        public IActionResult NavigateWeek(string direction, int newYear, int newWeek)
        {

            if (direction.ToLower().Equals("forward"))
            {
                newWeek++;
                if(newWeek > ISOWeek.GetWeeksInYear(newYear))
                {
                    newWeek = 1;
                    newYear++;
                }
            }
            if (direction.ToLower().Equals("backwards"))
            {
                newWeek--;
                if (newWeek < 1)
                {
                    newWeek = 53;
                    newYear--;
                }
            }
            return RedirectToAction("RegisteredHoursView", new { year = newYear, week = newWeek } );
        }

        public async Task<IActionResult> CallSick()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            SetUpToast("callSickToast");

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

            SetUpToast("callBetterToast");

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

        public async Task<IActionResult> ClockIn(int week, int year)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = currentUser.Id;

            DateTime time = DateTime.Now;

            RegisteredHours newShift = new RegisteredHours()
            {
                EmployeeId = employeeId,
                StartTime = time,
            };

            _registeredHoursRepository.AddShift(newShift);

            return RedirectToAction("RegisteredHoursView", new { year, week });
        }
        
        public async Task<IActionResult> ClockOut(int week, int year)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = currentUser.Id;

            DateTime time = DateTime.Now;

            _registeredHoursRepository.ClockOut(employeeId, time);

            return RedirectToAction("RegisteredHoursView", new { year, week });
        }

        private void SetUpToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
        }

        private string GetDayNameByDate(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Maandag";
                case DayOfWeek.Tuesday:
                    return "Dinsdag"; ;
                case DayOfWeek.Wednesday:
                    return "Woensdag"; ;
                case DayOfWeek.Thursday:
                    return "Donderdag"; ;
                case DayOfWeek.Friday:
                    return "Vrijdag"; ;
                case DayOfWeek.Saturday:
                    return "Zaterdag"; ;
                case DayOfWeek.Sunday:
                    return "Zondag"; ;
            }
            return "";
        }

        private string GetMonthNameByDate(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "Januari";
                case 2:
                    return "Februari";
                case 3:
                    return "Maart";
                case 4:
                    return "April";
                case 5:
                    return "Mei";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "Augustus";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }
            return "";
        }
    }
}
