using bumbo.Components;
using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace bumbo.Controllers
{
    public class SwapShiftController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ISwapShiftRequestRepository _swapShiftRequestRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;

        public SwapShiftController(UserManager<Employee> userManager, ISwapShiftRequestRepository swapShiftRequestRepository, IScheduleRepository scheduleRepository, IEmployeeRepository employeeRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository)
        {
            _userManager = userManager;
            _swapShiftRequestRepository = swapShiftRequestRepository;
            _scheduleRepository = scheduleRepository;
            _employeeRepository = employeeRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
        }

        [Route("ShiftSwap")]
        public async Task<IActionResult> Index()
        {
            Employee user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;

            List<SwitchRequest> incomingRequests = _swapShiftRequestRepository.GetAllIncomingRequests(employeeId);
            List<SwitchRequest> outgoingRequests = _swapShiftRequestRepository.GetAllOutgoingRequests(employeeId);

            SwapShiftViewModel viewModel = new SwapShiftViewModel
            {
                IncomingRequests = incomingRequests.Select(r => new SwitchRequestViewModel
                {
                    RequesterName = r.Employee?.FirstName + " " + r.Employee?.LastName,
                    ReceiverName = user.FirstName + " " + user.LastName,
                    Date = r.Date,
                    StartTime = r.Schedule.StartTime,
                    EndTime = r.Schedule.EndTime,
                    Department = r.Schedule.Department?.DepartmentName ?? "Onbekend",
                    Status = r.Declined ? "Geweigerd" : "In afwachting"
                }).ToList(),
                OutgoingRequests = outgoingRequests.Select(r => new SwitchRequestViewModel
                {
                    RequesterName = user.FirstName + " " + user.LastName,
                    ReceiverName = r.Employee?.FirstName + " " + r.Employee?.LastName,
                    Date = r.Date,
                    StartTime = r.Schedule.StartTime,
                    EndTime = r.Schedule.EndTime,
                    Department = r.Schedule.Department?.DepartmentName ?? "Onbekend",
                    Status = r.Declined ? "Geweigerd" : "Geaccepteerd"
                }).ToList()
            };

            return View(viewModel);
        }

        [Route("ShiftSwap/Create")]
        public async Task<IActionResult> Create()
        {
            Employee user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;
            List<BranchHasEmployee> branches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);
            int branchId = branches.First().BranchId;

            List<SwapShiftScheduleViewModel> schedules = _scheduleRepository.GetSchedulesForEmployee(employeeId)
                .Select(s => new SwapShiftScheduleViewModel
                {
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    DepartmentName = s.DepartmentName
                }).ToList();

            CreateSwapShiftViewModel viewModel = new CreateSwapShiftViewModel
            {
                EmployeeId = employeeId,
                BranchId = branchId,
                Schedules = schedules
            };

            return View(viewModel);
        }

        [HttpPost("ShiftSwap/ChooseEmployee")]
        public async Task<IActionResult> ChooseEmployee(string employeeId, int branchId, DateTime date)
        {
            Employee user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            Schedule schedule = _scheduleRepository.GetScheduleByEmployeeBranchDate(employeeId, branchId, DateOnly.FromDateTime(date));
            if (schedule == null)
            {
                return NotFound("Schema niet gevonden.");
            }

            var availableEmployees = _employeeRepository.GetAvailableEmployees(
                schedule.Date,
                schedule.StartTime,
                schedule.EndTime,
                schedule.BranchId,
                schedule.DepartmentName
            );

            var viewModel = new ChooseEmployeeViewModel
            {
                ScheduleDate = schedule.Date,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                DepartmentName = schedule.DepartmentName,
                AvailableEmployees = availableEmployees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.Id,
                    Name = $"{e.FirstName} {e.LastName}"
                }).ToList()
            };

            return View(viewModel);
        }





    }
}
