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

        public SwapShiftController(UserManager<Employee> userManager, ISwapShiftRequestRepository swapShiftRequestRepository, IScheduleRepository scheduleRepository)
        {
            _userManager = userManager;
            _swapShiftRequestRepository = swapShiftRequestRepository;
            _scheduleRepository = scheduleRepository;
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

        public async Task<IActionResult> Create(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;

            var schedules = _scheduleRepository.GetSchedulesForEmployee(employeeId);

            var headers = new List<string> { "Datum", "Tijd", "Afdeling", "Acties" };

            var tableBuilder = new TableHtmlBuilder<SwapShiftScheduleViewModel>();
            var htmlTable = tableBuilder.GenerateTable("Beschikbare Diensten", headers,
                schedules.Select(s => new SwapShiftScheduleViewModel
                {
                    Date = s.Date,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    DepartmentName = s.DepartmentName
                }).ToList(),
                "../ShiftSwap/Create",
                item =>
                {
                    return $@"
                <td class='py-2 px-4'>{item.Date.Day}-{item.Date.Month}-{item.Date.Year}</td>
                <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                <td class='py-2 px-4'>{item.DepartmentName}</td>
                <td class='py-2 px-4 text-right'>
                    <form method='post' action='/ShiftSwap/Select'>
                        <input type='hidden' name='ScheduleId' value='{item.Date}-{item.StartTime}' />
                       <button type='submit' class='bg-gray-800 text-white px-4 py-2 rounded-lg hover:bg-gray-700'>
                            Selecteren
                        </button>
                    </form>
                </td>";
                },
                searchTerm,
                page);

            string adjustedHtmlTable = htmlTable.Replace("text-4xl", "text-3xl");
            ViewBag.HtmlTable = adjustedHtmlTable;

            return View();
        }


    }
}
