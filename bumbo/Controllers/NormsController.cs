using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using bumbo.Models.ViewModels.Norms;
using Microsoft.VisualBasic;
using System.Globalization;
using bumbo.Components;
using DataLayer.Models.DTOs;
using DataLayer.Interfaces;
using System.ComponentModel;
using System.ComponentModel.Design;
using bumbo.ViewModels.Norms;

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
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        List<ReadNormViewModel> list = new List<ReadNormViewModel>();

        List<NormOverviewDTO> norms = await _normsRepository.GetOverview();
        norms = norms
            .OrderByDescending(dto => dto.Year)
            .ThenByDescending(dto => dto.Week)
            .ToList();

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

    public async Task<IActionResult> Create(bool lastWeek, int? addYear)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        var selectedYear = addYear ?? DateTime.Now.Year;

        var norms = (await _normsRepository.GetNorms())
            .Where(n => n.branchId == user.ManagerOfBranchId && n.year == selectedYear)
            .Select(n => new NormViewModel
            {
                Week = n.week,
                Year = n.year
            })
            .ToList();

        var viewModel = new AddNormViewModel
        {
            Year = selectedYear,
            Norms = norms
        };

        if (lastWeek)
        {
            TempData["ToastId"] = "loadLastNormToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            var lastNorm = await GetLastWeek(user.ManagerOfBranchId.Value);
            if (lastNorm != null)
            {
                viewModel.Week = lastNorm.Week;
                viewModel.UnloadColis = lastNorm.UnloadColis;
                viewModel.FillShelves = lastNorm.FillShelves;
                viewModel.Cashier = lastNorm.Cashier;
                viewModel.Fresh = lastNorm.Fresh;
                viewModel.Fronting = lastNorm.Fronting;

                int week = LastWeek();
                int year = DateAndTime.Now.Year;
                if (week == 0) { week = 52; year--; }

                if (lastNorm.Week == week && year == lastNorm.Year)
                {
                    TempData["ToastMessage"] = "Norm van vorige week succesvol ingeladen!";
                    TempData["ToastType"] = "success";
                }
                else
                {
                    TempData["ToastMessage"] = $"Laatst aanwezige norm van week {lastNorm.Week}, jaar {lastNorm.Year} succesvol ingeladen!";
                    TempData["ToastType"] = "success";
                    TempData["AutoHide"] = "no";
                }
            }
            else
            {
                TempData["ToastMessage"] = "Er zijn nog geen normeringen!";
                TempData["ToastType"] = "error";
            }
        }
        ViewData["ViewModel"] = viewModel;

        return View(viewModel);
    }



    private async Task<AddNormViewModel> GetLastWeek(int branchId)
    {
        int week = LastWeek();
        int year = DateAndTime.Now.Year;

        if (week == 0)
        {
            week = 52;
            year--;
        }

        List<Norm> normList = await _normsRepository.GetNorms();

        IEnumerable<Norm> norms;

        var lastWeekNorm = normList
            .Where(n => n.branchId == branchId && n.week == week && n.year == year).ToList();

        if (lastWeekNorm.Any())
        {
            norms = lastWeekNorm;
        }
        else
        {
            normList = normList.Where(n => n.branchId == branchId).ToList();
            var lastYearInDb = normList.Max(n => n.year);
            var lastWeekInDb = normList.Where(n => n.year == year).Max(n => n.week);
            norms = normList.Where(n => n.week == lastWeekInDb && n.year == lastYearInDb).ToList();
        }
        AddNormViewModel viewModel = new AddNormViewModel();

        viewModel.UnloadColis = norms.ToList()[0].normInSeconds / 60;
        viewModel.FillShelves = norms.ToList()[1].normInSeconds / 60;
        viewModel.Cashier = norms.ToList()[2].normInSeconds;
        viewModel.Fresh = norms.ToList()[3].normInSeconds;
        viewModel.Fronting = norms.ToList()[4].normInSeconds;
        viewModel.Year = norms.ToList()[0].year;
        viewModel.Week = norms.ToList()[0].week;

        var allNorms = await _normsRepository.GetNorms();
        allNorms = allNorms.Where(n => n.branchId == branchId).ToList();
        viewModel.Norms = allNorms.Select(n => new NormViewModel
        {
            Week = n.week,
            Year = n.year
        }).ToList();

        return viewModel;
    }

    public int LastWeek()
    {
        DateTime currentDate = DateTime.Now;

        Calendar calendar = CultureInfo.CurrentCulture.Calendar;

        CalendarWeekRule calendarWeekRule = CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        return calendar.GetWeekOfYear(currentDate, calendarWeekRule, firstDayOfWeek) - 1;
    }

    public async Task<IActionResult> Update([FromQuery(Name = "NormId")] int selectedNormId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        Norm firstNorm = await _normsRepository.GetNorm(selectedNormId);

        List<Norm> selectedNorms = await _normsRepository.GetSelectedNorms(user.ManagerOfBranchId, firstNorm.year, firstNorm.week);

        UpdateNormInputViewModel viewModel = new UpdateNormInputViewModel();

        viewModel.FirstNormId = 1 + 5 * ((selectedNormId - 1) / 5);
        viewModel.BranchId = user.ManagerOfBranchId.Value;
        viewModel.Week = selectedNorms[0].week;
        viewModel.Year = selectedNorms[0].year;
        viewModel.UnloadColis = selectedNorms[0].normInSeconds / 60;
        viewModel.FillShelves = selectedNorms[1].normInSeconds / 60;
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

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

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

            Norm[] norms = new Norm[] { Coli, Fillshelves, Cashregister, Fresh, Fronting };

            List<Norm> normsCheck = await _normsRepository.GetSelectedNorms(norms[0].branchId, norms[0].year, norms[0].week);

            // checks if there are already norms for the selected week. If so, insertion will be interupted to avoid an error
            if (normsCheck.Count > 0)
            {
                TempData["ToastMessage"] = "Normering toevoegen mislukt. Er is al een normering voor deze week.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "insertNormToast";
                TempData["AutoHide"] = "no";

                return RedirectToAction("Create");
            }
            else if (norms[0].normInSeconds < 0 || norms[1].normInSeconds < 0 || norms[2].normInSeconds < 0
                    || norms[3].normInSeconds < 0 || norms[4].normInSeconds < 0)
            {
                TempData["ToastMessage"] = "Normering toevoegen mislukt. Er kunnen geen negatieve getallen worden toegevoegd.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "insertNormToast";
                TempData["AutoHide"] = "no";

                return RedirectToAction("Create");
            }
            else
            {
                await _normsRepository.InsertMany(norms);

                TempData["ToastMessage"] = "Normering successvol toegevoegd!";
                TempData["ToastType"] = "success";

                TempData["ToastId"] = "insertNormToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while inserting norms");
            return View("Error");
        }
    }

    public async Task<IActionResult> InsertUpdate(UpdateNormViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || user.ManagerOfBranchId == null)
        {
            return RedirectToAction("AccessDenied", "Home");
        }

        try
        {
            if (viewModel.UnloadColis < 0 || viewModel.FillShelves < 0 || viewModel.Cashier < 0
                    || viewModel.Fresh < 0 || viewModel.Fronting < 0)
            {
                TempData["ToastMessage"] = "Normering updaten mislukt. Een waarde kan niet worden aangepast naar een negatieve waarde.";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "updateNormToast";
                TempData["AutoHide"] = "no";

                return RedirectToAction("Update", new { NormId = viewModel.FirstNormId });
            }
            else
            {
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

                TempData["ToastMessage"] = "Normering successvol geupdate!";
                TempData["ToastType"] = "success";

                TempData["ToastId"] = "updateNormToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                return RedirectToAction("Index");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating norms");
            return View("Error");
        }
    }
}