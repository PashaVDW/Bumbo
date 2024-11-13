using bumbo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class SchedulesController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new ScheduleOverviewViewModel() 
            {
                WeekHasSchedule = true
            };
            return View(viewModel);
        }
    }
}
