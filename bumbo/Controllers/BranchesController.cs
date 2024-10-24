using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using bumbo.Components;
using static bumbo.Controllers.NormeringController;
using bumbo.ViewModels;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using DataLayer.Interfaces;

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
                 <td class='py-2 px-4'>
                 <button onclick=""window.location.href='../Branches/ReadBranchView?branchId={item.BranchId}'"">✏️</button> 
                 </td>";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View();
        }

        public IActionResult CreateBranchView()
        {
            return View();
        }

        public IActionResult UpdateBranchView(int branchId)
        {
            var branch = _branchesRepository.GetBranch(branchId);
            return View(branch);
        }

        public IActionResult ReadBranchView(int branchId)
        {
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

        public IActionResult CreateBranchManagerView(int branchId, string searchTerm, int page = 1)
        {

            var newBranch = _branchesRepository.GetBranch(branchId);
            newBranch.Employees = _branchesRepository.GetEmployeesFromBranch(newBranch).Where(e => e.ManagerOfBranchId == null).ToList();

            var viewModel = new CreateBranchManagerViewModel() { 
                BranchId = branchId, Employees = newBranch.Employees.ToList() 
            };

            var employees = newBranch.Employees.ToList();

            var headers = new List<string> { "Naam", "Filiaal nummer" };
            var tableBuilder = new TableHtmlBuilder<Employee>();
            var htmlTable = tableBuilder.GenerateTable("", headers, employees, "", item =>
            {
            return $@"
                 <td class='py-2 px-4'>{item.FirstName + " " + item.LastName}</td>
                 <td class='py-2 px-4'>{newBranch.BranchId}</td>
                 <td class='py-2 px-4'><a href='/Branches/AddBranchManager?branchId={newBranch.BranchId}&amp;employeeId={item.Id}' class=""bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 float-left rounded-xl"" >Kiezen</a><td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View(viewModel);
        }

        public IActionResult AddBranch(Branch branch)
        {
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
        public IActionResult UpdateBranch(Branch branch)
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

        [HttpPost]
        public IActionResult DeleteBranch(int branchId)
        {
            SetTempDataForToast("updateBranchToast");

            var newBranch = _branchesRepository.GetBranch(branchId);

            if(newBranch == null)
            {
                TempData["ToastMessage"] = "Filiaal verwijderen mislukt";
                TempData["ToastType"] = "error";

                return View("UpdateBranchView", branchId);
            }

            _branchesRepository.DeleteBranch(newBranch);

            TempData["ToastMessage"] = "Filiaal is verwijderd";
            TempData["ToastType"] = "success";

            return RedirectToAction("BranchesView");
        }

        public IActionResult AddBranchManager(string employeeId, int branchId)
        {

            var branch = _branchesRepository.GetBranch(branchId);

            _branchesRepository.AddBranchManager(employeeId, branch);

            var viewModel = GetReadBranchViewModel(branch);

            SetTempDataForToast("addBranchManagerToast");
            TempData["ToastMessage"] = "Filiaalmanager is toegevoegd";
            TempData["ToastType"] = "success";

            return View("ReadBranchView", viewModel);
        }

        public IActionResult DeleteBranchManager(string employeeId, int branchId)
        {

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
