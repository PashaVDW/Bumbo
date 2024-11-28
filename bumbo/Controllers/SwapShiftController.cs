using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace bumbo.Controllers
{
    public class SwapShiftController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ISwapShiftRequestRepository _swapShiftRequestRepository;

        public SwapShiftController(UserManager<Employee> userManager, ISwapShiftRequestRepository swapShiftRequestRepository)
        {
            _userManager = userManager;
            _swapShiftRequestRepository = swapShiftRequestRepository;
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

            var incomingRequests = _swapShiftRequestRepository.GetAllIncomingRequests(employeeId);
            var outgoingRequests = _swapShiftRequestRepository.GetAllOutgoingRequests(employeeId);

            var viewModel = new SwapShiftViewModel
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


    }
}
