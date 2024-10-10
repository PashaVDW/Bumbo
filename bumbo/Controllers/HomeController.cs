using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;

namespace bumbo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

