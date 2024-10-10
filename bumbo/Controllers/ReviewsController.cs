using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public ReviewsController(UserManager<Employee> userManager)
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
    }
}
