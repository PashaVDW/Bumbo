using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
