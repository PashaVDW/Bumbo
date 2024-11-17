using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class RequestsController : Controller
    {

        private readonly UserManager<Employee> _userManager;

        public RequestsController(UserManager<Employee> userManager)
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

        public IActionResult Read()
        {
            return View();
        }
    }
}
