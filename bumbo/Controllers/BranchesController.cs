using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class BranchesController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public BranchesController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }
    }
}
