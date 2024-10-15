using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public TemplatesController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Update()
        {
            return View();
        }
    }
}
