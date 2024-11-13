using bumbo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class SchedulesController : Controller
    {

        private readonly UserManager<Employee> _userManager;

        public SchedulesController(UserManager<Employee> userManager)
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

            var viewModel = new ScheduleOverviewViewModel() 
            {
                WeekHasSchedule = true,
                DateUsed = DateTime.Today.AddDays(-2),
            };
            
            return View(viewModel);
        }

        public IActionResult LoadPreviousWeek(DateTime oldDate)
        {
            DateTime newDate = oldDate.AddDays(-7);
            var viewModel = new ScheduleOverviewViewModel()
            {
                WeekHasSchedule = true,
                DateUsed = newDate,
            };
            return View("Index", viewModel);
        }

        public IActionResult LoadNextWeek(DateTime oldDate)
        {
            DateTime newDate = oldDate.AddDays(7);
            var viewModel = new ScheduleOverviewViewModel()
            {
                WeekHasSchedule = true,
                DateUsed = newDate,
            };
            return View("Index", viewModel);
        }
    }
}
