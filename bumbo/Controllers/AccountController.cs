using bumbo.Models;
using bumbo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly SignInManager<Employee> _signInManager;
    private readonly UserManager<Employee> _userManager;

    public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        SetTempDataForAccountToast("loginToast");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["ToastMessage"] = "Dit e-mailadres bestaat niet.";
                TempData["ToastType"] = "error";
                return View(model);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ToastMessage"] = "Wachtwoord is onjuist.";
                    TempData["ToastType"] = "error";
                }
            }
        }

        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    private void SetTempDataForAccountToast(string toastId)
    {
        TempData["ToastId"] = toastId;
        TempData["AutoHide"] = "yes";
        TempData["MilSecHide"] = 5000;
    }
}
