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
        ViewBag.ShowNavbar = true;
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}

