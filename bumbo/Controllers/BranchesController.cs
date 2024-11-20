using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using bumbo.Components;
using bumbo.ViewModels;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using DataLayer.Interfaces;
using System.Text;
using Microsoft.Data.SqlClient;

namespace bumbo.Controllers
{
    public class BranchesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IBranchesRepository _branchesRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;

        public BranchesController(UserManager<Employee> userManager, IBranchesRepository branchesRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository)
        {
            _userManager = userManager;
            _branchesRepository = branchesRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
        }

        public async Task<IActionResult> BranchesView(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var branches = _branchesRepository.GetAllBranches();
            foreach (var branch in branches)
            {
                branch.Employees = _branchesRepository.GetEmployeesFromBranch(branch);
            }

            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var headers = new List<string> { "Naam", "Filiaal nummer", "Aantal medewerkers", "Straatnaam + huisnummer" };
            var tableBuilder = new TableHtmlBuilder<Branch>();
            var htmlTable = tableBuilder.GenerateTable("Filialen", headers, branches, "../Branches/CreateBranchView", item =>
            {
                return $@"
                 <td class='py-2 px-4'>{item.Name}</td>
                 <td class='py-2 px-4'>{item.BranchId}</td>
                 <td class='py-2 px-4'>{item.Employees.Count} medewerkers</td>
                 <td class='py-2 px-4'>{item.Street + " " + item.HouseNumber}</td>
                 <td class='py-2 px-4 text-right'>
                 <button onclick=""window.location.href='../Branches/ReadBranchView?branchId={item.BranchId}'"">✏️</button> 
                 </td>";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View();
        }

        public async Task<IActionResult> CreateBranchView()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        public async Task<IActionResult> UpdateBranchView(int branchId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branch = _branchesRepository.GetBranch(branchId);
            return View(branch);
        }

        public async Task<IActionResult> ReadBranchView(int branchId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branch = _branchesRepository.GetBranch(branchId);
            var viewModel = GetReadBranchViewModel(branch);

            if (viewModel.Managers.Count == 0) 
            {
                SetTempDataForToast("branchManagerAmountToast");
                TempData["ToastMessage"] = "Er zijn op dit moment geen filiaalmanagers";
                TempData["ToastType"] = "info";
                TempData["MilSecHide"] = 5000;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> CreateBranchManagerView(int branchId, string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var newBranch = _branchesRepository.GetBranch(branchId);
            newBranch.Employees = _branchesRepository.GetAllEmployees().Where(e => e.ManagerOfBranchId == null).ToList();

            var viewModel = new CreateBranchManagerViewModel() { 
                BranchId = branchId, Employees = newBranch.Employees.ToList() 
            };

            var employees = newBranch.Employees.ToList();

            var headers = new List<string> { "Naam", "Filiaal nummer" };
            var tableBuilder = new TableHtmlBuilderAddBranchManager<Employee>();
            var htmlTable = tableBuilder.GenerateTable(headers, employees, item =>
            {
            return $@"
                 <td class='py-2 px-4'>{item.FirstName + " " + item.LastName}</td>
                 <td class='py-2 px-4'>{newBranch.BranchId}</td>
                 <td class='py-2 px-4 flex justify-end'><a href='/Branches/AddBranchManager?branchId={newBranch.BranchId}&amp;employeeId={item.Id}' class=""bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 float-left rounded-xl"" >Kiezen</a><td>";
            }, branchId, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View(viewModel);
        }

        public async Task<IActionResult> AddBranch(Branch branch)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            SetTempDataForToast("createBranchToast");
            try
            {
                _branchesRepository.AddBranch(branch);

                TempData["ToastMessage"] = "Filiaal is aangemaakt";
                TempData["ToastType"] = "success";

                return RedirectToAction("BranchesView");
            }
            catch (Exception ex) 
            {
                TempData["ToastMessage"] = "Filiaal aanmaken mislukt";
                TempData["ToastType"] = "error";

                return View("CreateBranchView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBranch(Branch branch)
        {
            SetTempDataForToast("updateBranchToast");
            try
            {
                _branchesRepository.UpdateBranch(branch);

                TempData["ToastMessage"] = "Filiaal is geüpdatet";
                TempData["ToastType"] = "success";

                return RedirectToAction("BranchesView");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "Filiaal updaten mislukt";
                TempData["ToastType"] = "error";

                return View("UpdateBranchView", branch);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                SetTempDataForToast("deleteBranchToast");

                var newBranch = _branchesRepository.GetBranch(branchId);

                if (newBranch == null)
                {
                    TempData["ToastMessage"] = "Filiaal verwijderen mislukt";
                    TempData["ToastType"] = "error";

                    return View("UpdateBranchView", newBranch);
                }

                _branchesRepository.DeleteBranch(newBranch);

                TempData["ToastMessage"] = "Filiaal is verwijderd";
                TempData["ToastType"] = "success";

                return RedirectToAction("BranchesView");
            }
            catch (DbUpdateException)
            {
                TempData["ToastMessage"] = "Filiaal heeft nog een of meerdere filiaalmanager(s)";
                TempData["ToastType"] = "error";

                var newBranch = _branchesRepository.GetBranch(branchId);
                return View("UpdateBranchView", newBranch);
            }
            
        }

        public async Task<IActionResult> AddBranchManager(string employeeId, int branchId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branch = _branchesRepository.GetBranch(branchId);

            _branchesRepository.AddBranchManager(employeeId, branch);

            var viewModel = GetReadBranchViewModel(branch);

            SetTempDataForToast("addBranchManagerToast");
            TempData["ToastMessage"] = "Filiaalmanager is toegevoegd";
            TempData["ToastType"] = "success";

            return View("ReadBranchView", viewModel);
        }

        public async Task<IActionResult> DeleteBranchManager(string employeeId, int branchId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branch = _branchesRepository.GetBranch(branchId);

            _branchesRepository.DeleteBranchManager(employeeId);

            var viewModel = GetReadBranchViewModel(branch);

            SetTempDataForToast("deleteBranchManagerToast");
            TempData["ToastMessage"] = "Filiaalmanager is verwijderd";
            TempData["ToastType"] = "success";

            return View("ReadBranchView", viewModel);
        }

        public ReadBranchViewModel GetReadBranchViewModel(Branch branch)
        {
            var viewModel = new ReadBranchViewModel()
            {
                BranchId = branch.BranchId,
                CountryName = branch.CountryName,
                HouseNumber = branch.HouseNumber,
                Name = branch.Name,
                PostalCode = branch.PostalCode,
                Street = branch.Street,
                Employees = _branchesRepository.GetEmployeesFromBranch(branch),
                Managers = _branchesRepository.GetManagersOfBranch(branch)
            };
            viewModel.CountryName = CountryNameToDutch(viewModel.CountryName);
            return viewModel;
        }

        private string CountryNameToDutch(string countryName)
        {
            switch (countryName) 
            {
                case "Netherlands":
                    return "Nederland";
                case "Belgium":
                    return "België";
                case "Germany":
                    return "Duitsland";
                default:
                    return "";
            }
        }

        private void SetTempDataForToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
        }
    }
}

class TableHtmlBuilderAddBranchManager<TItem>
{
    public string GenerateTable(List<string> headers, List<TItem> items, Func<TItem, string> rowTemplate, int branchId, string searchTerm = null, int currentPage = 1, int pageSize = 10)
    {
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            items = items.Where(item =>
            {
                var properties = typeof(TItem).GetProperties();
                return properties.Any(prop => prop.GetValue(item)?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
            }).ToList();
        }

        var totalItems = items.Count;
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

        var pagedItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        var htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<div class='container mx-auto p-10'>" +
                               "<div class='flex justify-center items-center mb-4'>" +
                               "<form method='get' class='flex items-center space-x-4'>" +
                               "<input type='hidden' name='branchId' value=" + branchId + " />" +
                               "<input type='text' name='searchTerm' value='" + searchTerm + "' placeholder='Zoek naar medewerkers' class='w-full border border-gray-300 rounded-full py-2 px-6 focus:outline-none focus:ring-2 focus:ring-yellow-400' />" +
                               "<button type='submit' class='bg-gray-200 text-gray-700 py-2 px-6 rounded-full hover:bg-gray-300'>Zoeken</button>" +
                               "</form>" +
                               "</div>"
                               );
        htmlBuilder.AppendLine("<div class='w-full p-6'>");
        htmlBuilder.AppendLine("<div class='overflow-x-auto w-full'>");
        htmlBuilder.AppendLine("<table class='min-w-full table-auto border-collapse'>");

        htmlBuilder.AppendLine(
            "<thead>" +
            "<tr class='text-left text-gray-600 font-bold'>"
        );
        foreach (var header in headers)
        {
            htmlBuilder.AppendLine($"<th class='py-2 px-4'>{header}</th>");
        }
        htmlBuilder.AppendLine(
            "</tr>" +
            "</thead>"
        );
        htmlBuilder.AppendLine("<tbody>");
        foreach (var item in pagedItems)
        {
            htmlBuilder.AppendLine("<tr class='border-b hover:bg-gray-50'>");
            htmlBuilder.AppendLine(rowTemplate(item));
            htmlBuilder.AppendLine("</tr>");
        }
        htmlBuilder.AppendLine("</tbody>");
        htmlBuilder.AppendLine("</table>");
        htmlBuilder.AppendLine("</div>");

        htmlBuilder.AppendLine("<div class='flex justify-center items-center mt-4 space-x-2'>");

        if (currentPage > 1)
        {
            htmlBuilder.AppendLine($"<a href='?page={currentPage - 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Vorige</a>");
        }
        else
        {
            htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Vorige</span>");
        }

        for (int i = 1; i <= totalPages; i++)
        {
            if (i == currentPage)
            {
                htmlBuilder.AppendLine($"<span class='py-2 px-4 bg-yellow-400 text-white rounded-lg'>{i}</span>");
            }
            else
            {
                htmlBuilder.AppendLine($"<a href='?page={i}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300'>{i}</a>");
            }
        }
        if (currentPage < totalPages)
        {
            htmlBuilder.AppendLine($"<a href='?page={currentPage + 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Volgende</a>");
        }
        else
        {
            htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Volgende</span>");
        }
        htmlBuilder.AppendLine("</div>");

        htmlBuilder.AppendLine("</div>");

        return htmlBuilder.ToString();
    }


}