using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class StandaardTemplateController : Controller
    {
        private readonly ILogger<StandaardTemplateController> _logger;

        public StandaardTemplateController(ILogger<StandaardTemplateController> logger)
        {
            _logger = logger;
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
