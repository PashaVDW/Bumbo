using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;  // Adjust to the actual namespace of your Employee model

namespace bumbo.Controllers
{
    public class TestController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public TestController(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Automatically logs in as the admin
        public async Task<IActionResult> LoginAsAdmin()
        {
            var adminEmail = "admin@company.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser != null)
            {
                await _signInManager.SignInAsync(adminUser, isPersistent: false);
                return RedirectToAction("Index", "Home");  // Redirect to Home after login
            }

            return Content("Admin user not found.");
        }
    }
}
