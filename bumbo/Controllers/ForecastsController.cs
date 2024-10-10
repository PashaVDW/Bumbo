using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class ForecastsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
