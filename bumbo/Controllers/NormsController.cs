using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using bumbo.Models.ViewModels.Norms;
using Microsoft.VisualBasic;
using System.Globalization;
using bumbo.Components;
using DataLayer.Models.DTOs;
using DataLayer.Interfaces;

namespace bumbo.Controllers;

public class NormsController : Controller
{
    private readonly ILogger<NormsController> _logger;
    private readonly IConfiguration _configuration;
    private readonly INormsRepository _normsRepository;

    private readonly UserManager<Employee> _userManager;

    public NormsController(UserManager<Employee> userManager, INormsRepository normsRepository, ILogger<NormsController> logger)
    {
        _userManager = userManager;
        _normsRepository = normsRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string searchTerm, int page = 1)
    {
        List<ReadNormViewModel> list = new List<ReadNormViewModel>();

        List<NormOverviewDTO> norms = await _normsRepository.GetOverview();

        foreach (NormOverviewDTO norm in norms)
        {
            var item = new ReadNormViewModel();

            item.NormId = norm.NormId;
            item.Week = norm.Week;
            item.Year = norm.Year;
            item.UnloadColis = norm.ColiInSeconds;
            item.FillShelves = norm.ShelveInSeconds;
            item.Cashier = norm.CashierInSeconds;
            item.Fresh = norm.FreshInSeconds;
            item.Fronting = norm.FrontInSeconds;

            list.Add(item);
        }

        var headers = new List<string> { "Jaar", "Week", "Coli uitladen", "Vakken vullen", "Kassa", "Vers", "Spiegelen" };
        var tableBuilder = new TableHtmlBuilder<ReadNormViewModel>();
        var htmlTable = tableBuilder.GenerateTable("Normeringen", headers, list, "/Norms/Create/", item =>
{
            return $@"
                    <td class='py-2 px-4'>{item.Year}</td>
                    <td class='py-2 px-4'>{item.Week}</td>
                    <td class='py-2 px-4'>{item.UnloadColis} minuten</td>
                    <td class='py-2 px-4'>{item.FillShelves} minuten per coli</td>
                    <td class='py-2 px-4'>{item.Cashier} kassières</td>
                    <td class='py-2 px-4'>{item.Fresh} medewerkers</td>
                    <td class='py-2 px-4'>{item.Fronting} seconden per meter</td>
                    <td class='py-2 px-4 text-right'>
                    <button onclick = ""window.location.href='../Norms/Update?NormId={item.NormId}'"">✏️</button> 
                    </td>";
        }, searchTerm, page);


        ViewBag.HtmlTable = htmlTable;

        return View();
    }

    public async Task<IActionResult> Create(Boolean lastWeek)
    {
        var user = await _userManager.GetUserAsync(User);

        AddNormViewModel viewModel = new AddNormViewModel();

        if (lastWeek)
            viewModel = await GetLastWeek(user.ManagerOfBranchId.Value);

        ViewData["ViewModel"] = viewModel;

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }
        return View(viewModel);
    }
    private async Task<AddNormViewModel> GetLastWeek(int branchId)
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

        List<Norm> normList = await _normsRepository.GetNorms();

        IEnumerable<Norm> norms = normList
            .Where(n => n.branchId == branchId && n.week == week && n.year == year).ToList();

        AddNormViewModel viewModel = new AddNormViewModel();

        viewModel.UnloadColis = norms.ToList()[0].normInSeconds/60;
        viewModel.FillShelves = norms.ToList()[1].normInSeconds/60;
        viewModel.Cashier = norms.ToList()[2].normInSeconds;
        viewModel.Fresh = norms.ToList()[3].normInSeconds;
        viewModel.Fronting = norms.ToList()[4].normInSeconds;

        return viewModel;
    }

    public async Task<IActionResult> Update([FromQuery(Name = "NormId")] int selectedNormId)
    {
        var user = await _userManager.GetUserAsync(User);

        Norm firstNorm = await _normsRepository.GetNorm(selectedNormId);

        List<Norm> selectedNorms = await _normsRepository.GetSelectedNorms(user.ManagerOfBranchId, firstNorm.year, firstNorm.week);

        UpdateNormInputViewModel viewModel = new UpdateNormInputViewModel();

        viewModel.FirstNormId = 1 + 5 * ((selectedNormId - 1) / 5);
        viewModel.BranchId = user.ManagerOfBranchId.Value;
        viewModel.Week = selectedNorms[0].week;
        viewModel.Year = selectedNorms[0].year;
        viewModel.UnloadColis = selectedNorms[0].normInSeconds/60;
        viewModel.FillShelves = selectedNorms[1].normInSeconds/60;
        viewModel.Cashier = selectedNorms[2].normInSeconds;
        viewModel.Fresh = selectedNorms[3].normInSeconds;
        viewModel.Fronting = selectedNorms[4].normInSeconds;

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        return View(viewModel);
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

            Norm Fillshelves = new Norm();
            Fillshelves.branchId = user.ManagerOfBranchId.Value;
            Fillshelves.week = viewModel.Week;
            Fillshelves.year = viewModel.Year;
            Fillshelves.activity = "Vakkenvullen";
            Fillshelves.normInSeconds = (viewModel.FillShelves * 60);

            Norm Cashregister = new Norm();
            Cashregister.branchId = user.ManagerOfBranchId.Value;
            Cashregister.week = viewModel.Week;
            Cashregister.year = viewModel.Year;
            Cashregister.activity = "Kassa";
            Cashregister.normInSeconds = (viewModel.Cashier);

            Norm Fresh = new Norm();
            Fresh.branchId = user.ManagerOfBranchId.Value;
            Fresh.week = viewModel.Week;
            Fresh.year = viewModel.Year;
            Fresh.activity = "Vers";
            Fresh.normInSeconds = (viewModel.Fresh);

            Norm Fronting = new Norm();
            Fronting.branchId = user.ManagerOfBranchId.Value;
            Fronting.week = viewModel.Week;
            Fronting.year = viewModel.Year;
            Fronting.activity = "Spiegelen";
            Fronting.normInSeconds = (viewModel.Fronting);

            await _normsRepository.InsertMany(new Norm[] { Coli, Fillshelves, Cashregister, Fresh, Fronting });

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
            var user = await _userManager.GetUserAsync(User);

            List<Norm> norms = await _normsRepository.GetSelectedNorms(user.ManagerOfBranchId, viewModel.Year, viewModel.Week);

            foreach (Norm norm in norms)
            {
                if (norm is null)
                {
                    continue;
                }
                switch (norm.activity)
                {
                    case "Coli uitladen":
                        norm.normInSeconds = viewModel.UnloadColis * 60;
                        break;
                    case "Vakkenvullen":
                        norm.normInSeconds = viewModel.FillShelves * 60;
                        break;
                    case "Kassa":
                        norm.normInSeconds = viewModel.Cashier;
                        break;
                    case "Vers":
                        norm.normInSeconds = viewModel.Fresh;
                        break;
                    case "Spiegelen":
                        norm.normInSeconds = viewModel.Fronting;
                        break;
                    default:
                        break;
                }
            }

            await _normsRepository.UpdateMany(norms);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating norms");
            return View("Error");
        }
    }



}