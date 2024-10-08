using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using bumbo.Models;  // Make sure you have the correct namespace

public class NavBarViewComponent : ViewComponent
{
    private readonly UserManager<Employee> _userManager;

    public NavBarViewComponent(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Admin");

        ViewBag.IsAdmin = isAdmin;

        return View();
    }
}
