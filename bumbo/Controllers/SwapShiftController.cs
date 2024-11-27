using bumbo.Models;
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

        public SwapShiftController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            Employee user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;

            List<SwitchRequest> incomingRequests = _branchRequestsEmployeeRepository.GetAllIncomingRequests(employeeId);
            List<SwitchRequest> outgoingRequests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(employeeId);


            return View();
        }
    }
}
