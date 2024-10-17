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

        // Automatically logs in as John Doe
        public async Task<IActionResult> LoginAsJohnDoe()
        {
            var johnDoeEmail = "john.doe@example.com";
            var johnDoeUser = await _userManager.FindByEmailAsync(johnDoeEmail);

            if (johnDoeUser != null)
            {
                await _signInManager.SignInAsync(johnDoeUser, isPersistent: false);
                return RedirectToAction("Index", "Home");  // Redirect to Home after login
            }

            return Content("John Doe user not found.");
        }
    }
}
