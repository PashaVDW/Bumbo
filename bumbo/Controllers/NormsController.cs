using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class NormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
