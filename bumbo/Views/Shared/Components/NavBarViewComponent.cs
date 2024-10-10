using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var model = new NavBarViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.Email = user.Email;
                    model.IsSystemManager = user.IsSystemManager;
                    model.IsAuthenticated = true;
                    model.IsBranchManager = user.ManagerOfBranchId != null;
                }
            }
            else
            {
                model.IsAuthenticated = false;
            }

            return View(model);
        }
    }
}


public class NavBarViewModel
{
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsSystemManager { get; set; }
        public bool IsBranchManager { get; set; }

}
