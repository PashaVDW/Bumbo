using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bumbo.Models;

namespace bumbo.ViewComponents
{
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

            var model = new NavBarViewModel
            {
                FirstName = user?.FirstName,
                Email = user?.Email,
                IsAuthenticated = user != null
            };

            return View(model);
        }
    }

    public class NavBarViewModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
