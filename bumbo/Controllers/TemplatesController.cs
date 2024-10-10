using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class TemplatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
