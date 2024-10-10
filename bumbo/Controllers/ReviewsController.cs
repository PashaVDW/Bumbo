using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Interfaces;

namespace bumbo.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IWeekOverviewService _weekOverviewService;

        public ReviewsController(UserManager<Employee> userManager, IWeekOverviewService weekOverviewService)
        {
            _userManager = userManager;
            _weekOverviewService = weekOverviewService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            WeekOverview model = _weekOverviewService.GetWeekOverview(12);
            return View(model);
        }
    }
}
