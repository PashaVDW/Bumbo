using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class BranchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
