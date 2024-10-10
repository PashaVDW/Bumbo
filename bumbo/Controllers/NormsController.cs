using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using bumbo.Data;

namespace bumbo.Controllers;

public class NormsController : Controller
{
    private readonly ILogger<NormsController> _logger;
    private readonly IConfiguration _configuration;

    private readonly BumboDBContext _context;
    private readonly UserManager<Employee> _userManager;

    public NormsController(UserManager<Employee> userManager, BumboDBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var norms = _context.Norms.ToList();

        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }
        return View(norms);
    }

    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }
        return View();
    }

    public async Task<IActionResult> Update()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }
        return View();
    }

    public async Task<IActionResult> Insert(int week, int unloadColis, int fillShelves, int cashier, int fresh, int fronting)
    {
        try
        {
            Norm Coli = new Norm();
            Coli.branchId = 1;
            Coli.week = week;
            Coli.year = DateTime.Now.Year;
            Coli.activity = "Coli uitladen";
            Coli.normInSeconds = (unloadColis * 60);

            _context.Norms.Add(Coli);

            Norm Fillshelves = new Norm();
            Fillshelves.branchId = 1;
            Fillshelves.week = week;
            Fillshelves.year = DateTime.Now.Year;
            Fillshelves.activity = "Vakkenvullen";
            Fillshelves.normInSeconds = (fillShelves * 60);

            _context.Norms.Add(Fillshelves);

            Norm Cashregister = new Norm();
            Cashregister.branchId = 1;
            Cashregister.week = week;
            Cashregister.year = DateTime.Now.Year;
            Cashregister.activity = "Kassa";
            Cashregister.normInSeconds = (cashier * 3600);

            _context.Norms.Add(Cashregister);

            Norm Fresh = new Norm();
            Fresh.branchId = 1;
            Fresh.week = week;
            Fresh.year = DateTime.Now.Year;
            Fresh.activity = "Vers";
            Fresh.normInSeconds = (fresh * 3600);

            _context.Norms.Add(Fresh);

            Norm Fronting = new Norm();
            Fronting.branchId = 1;
            Fronting.week = week;
            Fronting.year = DateTime.Now.Year;
            Fronting.activity = "Spiegelen";
            Fronting.normInSeconds = (fronting);

            _context.Norms.Add(Fronting);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while inserting norms");
            return View("Error");
        }
    }
}