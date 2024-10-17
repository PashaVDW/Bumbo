using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;

        public LogoutController(SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("uitloggen")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");  // Redirects to the Home page after logout
        }
    }
}
