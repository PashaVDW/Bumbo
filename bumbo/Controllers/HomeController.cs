using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using bumbo.Interfaces;

namespace bumbo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeekOverviewService _weekOverviewService;

    public HomeController(ILogger<HomeController> logger, IWeekOverviewService weekOverviewService)
    {
        _logger = logger;
        _weekOverviewService = weekOverviewService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Terugblik(int weekNummer = 12)
    {
        WeekOverview model = _weekOverviewService.GetWeekOverzicht(weekNummer);
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}

