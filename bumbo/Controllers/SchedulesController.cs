using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class SchedulesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
