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

        public async Task<IActionResult> RegisteredHoursView(int year, int month)
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
            
            List<Schedule> thisWeekWeekSchedule = _scheduleRepository.GetWeekScheduleForEmployee(user.Id, firstDayThisWeek, lastDayThisWeek).Where(s => s.Date >= DateOnly.FromDateTime(today)).ToList();

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

            DateTime date;
            
            if (year <= 0 || month <= 0)
            {
                date = today;
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            List<Schedule> registeredHoursInMonthPlanned = _scheduleRepository.GetRegisteredHoursInMonthScheduleForEmployee(user.Id, firstDayOfMonth, lastDayOfMonth);
            List<RegisteredHours> registeredHoursInMonthSchedule = _registeredHoursRepository.GetRegisteredHoursFromEmployee(user.Id).Where(r => r.EndTime.Value.Date < today).ToList();

            List<RegisteredHours> tempHours = new List<RegisteredHours>();
            List<RegisteredHours> extraRegisteredHoursInMonthSchedule = new List<RegisteredHours>();

            foreach (Schedule schedule in registeredHoursInMonthPlanned)
            {
                foreach (RegisteredHours hour in registeredHoursInMonthSchedule)
                {
                    if (!tempHours.Contains(hour) && hour.StartTime.Day == schedule.Date.Day && hour.EndTime.Value.Day == schedule.Date.Day)
                    {
                        tempHours.Add(hour);
                    }
                }
            }

            foreach (RegisteredHours hour in registeredHoursInMonthSchedule)
            {
                foreach (Schedule schedule in registeredHoursInMonthPlanned)
                {
                    if (!tempHours.Contains(hour) 
                        && !extraRegisteredHoursInMonthSchedule.Contains(hour)
                        && !(hour.StartTime.Day == schedule.Date.Day))
                    {
                        extraRegisteredHoursInMonthSchedule.Add(hour);
                    }
                }
            }

            EmployeeRegisterHoursViewModel viewModel = new EmployeeRegisterHoursViewModel()
            {
                ClockedInTime = _registeredHoursRepository.GetClockedInTime(employeeId),
                Today = today,
                DayName = GetDayNameByDate(today),
                WeekSchedule = thisWeekWeekSchedule,
                FirstShift = thisWeekWeekSchedule.First(),

                Year = date.Year,
                Month = date.Month,
                MonthName = GetMonthNameByDate(date),
                RegisteredHoursPlanned = registeredHoursInMonthPlanned,
                RegisteredHoursSchedule = registeredHoursInMonthSchedule,
                ExtraRegisteredHoursSchedule = extraRegisteredHoursInMonthSchedule,
            };

            return View(viewModel);
        }
        
        public IActionResult NavigateMonth(string direction, int newYear, int newMonth)
        {

            if (direction.ToLower().Equals("forward"))
            {
                newMonth++;
                if(newMonth > 12)
                {
                    newMonth = 1;
                    newYear++;
                }
            }
            if (direction.ToLower().Equals("backwards"))
            {
                newMonth--;
                if (newMonth < 1)
                {
                    newMonth = 12;
                    newYear--;
                }
            }
            return RedirectToAction("RegisteredHoursView", new { year = newYear, month = newMonth } );
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

            DateTime time = DateTime.Now;

            List<BranchHasEmployee> branches = _branchHasEmployeeRepository.GetBranchesForEmployee(currentUser.Id);

            BranchHasEmployee branchHasEmployee = branches.FirstOrDefault();

            RegisteredHours newShift = new RegisteredHours()
            {
                EmployeeId = currentUser.Id,
                EmployeeBID = currentUser.BID,
                BranchId = branchHasEmployee.BranchId,
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
