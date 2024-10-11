using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using bumbo.Data;
using bumbo.Models.ViewModels.Norms;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Globalization;

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

    public async Task<IActionResult> Create(Boolean lastWeek)
    {
        var user = await _userManager.GetUserAsync(User);

        AddNormViewModel viewModel = new AddNormViewModel();

        if (lastWeek)
            viewModel = GetLastWeek(user.ManagerOfBranchId.Value);

        ViewData["ViewModel"] = viewModel;

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }
        return View(viewModel);
    }
    private AddNormViewModel GetLastWeek(int branchId)
    {
        DateTime currentDate = DateTime.Now;

        Calendar calendar = CultureInfo.CurrentCulture.Calendar;

        CalendarWeekRule calendarWeekRule = CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        int week= calendar.GetWeekOfYear(currentDate, calendarWeekRule, firstDayOfWeek)-1;
        int year = DateAndTime.Now.Year;

        if (week == 0)
        {
            week = 52;
            year--;
        }

        List<Norm> normList = _context.Norms.ToList();

        IEnumerable<Norm> norms = normList
            .Where(n => n.branchId == branchId && n.week == week && n.year == year).ToList();

        AddNormViewModel viewModel = new AddNormViewModel();

        viewModel.UnloadColis = norms.ToList()[0].normInSeconds/60;
        viewModel.FillShelves = norms.ToList()[1].normInSeconds/60;
        viewModel.Cashier = norms.ToList()[2].normInSeconds/3600;
        viewModel.Fresh = norms.ToList()[3].normInSeconds/3600;
        viewModel.Fronting = norms.ToList()[4].normInSeconds/3600;

        return viewModel;
    }

    public async Task<IActionResult> Update(int selectedNormId)
    {
        var user = await _userManager.GetUserAsync(User);

        List<Norm> selectedNorms = GetSelectedNorms(1 + 5 * ((selectedNormId - 1) / 5));

        UpdateNormInputViewModel viewModel = new UpdateNormInputViewModel();

        viewModel.FirstNormId = 1 + 5 * ((selectedNormId - 1) / 5);
        viewModel.BranchId = user.ManagerOfBranchId.Value;
        viewModel.Week = selectedNorms[0].week;
        viewModel.Year = selectedNorms[0].year;
        viewModel.UnloadColis = selectedNorms[0].normInSeconds/60;
        viewModel.FillShelves = selectedNorms[1].normInSeconds/60;
        viewModel.Cashier = selectedNorms[2].normInSeconds/3600;
        viewModel.Fresh = selectedNorms[3].normInSeconds/3600;
        viewModel.Fronting = selectedNorms[4].normInSeconds;

        ViewData["ViewModel"] = viewModel;

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        return View(viewModel);
    }

    private List<Norm> GetSelectedNorms(int FirstNormId)
    {
        List<Norm> norm = _context.Norms.ToList();

        IEnumerable<Norm> norms = norm
            .Where(a => a.normId >= FirstNormId && a.normId < FirstNormId + 5);
        return norms.ToList();
    }

    public async Task<IActionResult> Insert(AddNormViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        try
        {
            Norm Coli = new Norm();
            Coli.branchId = user.ManagerOfBranchId.Value;
            Coli.week = viewModel.Week;
            Coli.year = viewModel.Year;
            Coli.activity = "Coli uitladen";
            Coli.normInSeconds = (viewModel.UnloadColis * 60);

            _context.Norms.Add(Coli);

            Norm Fillshelves = new Norm();
            Fillshelves.branchId = user.ManagerOfBranchId.Value;
            Fillshelves.week = viewModel.Week;
            Fillshelves.year = viewModel.Year;
            Fillshelves.activity = "Vakkenvullen";
            Fillshelves.normInSeconds = (viewModel.FillShelves * 60);

            _context.Norms.Add(Fillshelves);

            Norm Cashregister = new Norm();
            Cashregister.branchId = user.ManagerOfBranchId.Value;
            Cashregister.week = viewModel.Week;
            Cashregister.year = viewModel.Year;
            Cashregister.activity = "Kassa";
            Cashregister.normInSeconds = (viewModel.Cashier * 3600);

            _context.Norms.Add(Cashregister);

            Norm Fresh = new Norm();
            Fresh.branchId = user.ManagerOfBranchId.Value;
            Fresh.week = viewModel.Week;
            Fresh.year = viewModel.Year;
            Fresh.activity = "Vers";
            Fresh.normInSeconds = (viewModel.Fresh * 3600);

            _context.Norms.Add(Fresh);

            Norm Fronting = new Norm();
            Fronting.branchId = user.ManagerOfBranchId.Value;
            Fronting.week = viewModel.Week;
            Fronting.year = viewModel.Year;
            Fronting.activity = "Spiegelen";
            Fronting.normInSeconds = (viewModel.Fronting);

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
    public async Task<IActionResult> InsertUpdate(UpdateNormViewModel viewModel)
    {
        try
        {
            var coli = await _context.Norms.FirstOrDefaultAsync(n => n.normId == viewModel.FirstNormId);
            if (coli != null)
            {
                coli.normInSeconds = viewModel.UnloadColis * 60;
            }

            var shelve = await _context.Norms.FirstOrDefaultAsync(n => n.normId == viewModel.FirstNormId + 1);
            if (shelve != null)
            {
                shelve.normInSeconds = viewModel.FillShelves * 60;
            }

            var cashier = await _context.Norms.FirstOrDefaultAsync(n => n.normId == viewModel.FirstNormId + 2);
            if (cashier != null)
            {
                cashier.normInSeconds = viewModel.Cashier * 3600;
            }

            var fresh = await _context.Norms.FirstOrDefaultAsync(n => n.normId == viewModel.FirstNormId + 3);
            if (fresh != null)
            {
                fresh.normInSeconds = viewModel.Fresh * 3600;
            }

            var fronting = await _context.Norms.FirstOrDefaultAsync(n => n.normId == viewModel.FirstNormId + 4);
            if (fronting != null)
            {
                fronting.normInSeconds = viewModel.Fronting;
            }

            // Save all changes
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating norms");
            return View("Error");
        }
    }



}