using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
