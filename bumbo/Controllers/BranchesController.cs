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

namespace bumbo.Controllers
{
    public class BranchesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private BumboDBContext _context;

        public BranchesController(UserManager<Employee> userManager, BumboDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> BranchesView(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var branches = _context.Branches.ToList();
            foreach (var branch in branches)
            {
                branch.Employees = GetEmployeesFromBranch(branch);
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
            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            return View(branch);
        }

        public IActionResult ReadBranchView(int branchId)
        {
            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
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

            var newBranch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            newBranch.Employees = GetEmployeesFromBranch(newBranch).Where(e => e.ManagerOfBranchId == null).ToList();

            var viewModel = new CreateBranchManagerViewModel() { 
                BranchId = branchId, Employees = newBranch.Employees.ToList() 
            };

            var employees = newBranch.Employees.ToList();

            var branchHasEmployees = _context.BranchHasEmployees.Where(e => e.BranchId == branchId).ToList();

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
                _context.Branches.Add(branch);
                _context.SaveChanges();

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
                _context.Branches.Update(branch);
                _context.SaveChanges();

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

            var newBranch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);

            if(newBranch == null)
            {
                TempData["ToastMessage"] = "Filiaal verwijderen mislukt";
                TempData["ToastType"] = "error";

                return View("UpdateBranchView", branchId);
            }

            _context.Branches.Remove(newBranch);
            _context.SaveChanges();

            TempData["ToastMessage"] = "Filiaal is verwijderd";
            TempData["ToastType"] = "success";

            return RedirectToAction("BranchesView");
        }

        public IActionResult AddBranchManager(string employeeId, int branchId)
        {

            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);

            var employee = _context.Employees.SingleOrDefault(e => e.Id.Equals(employeeId.ToString()));
            employee.ManagerOfBranch = branch;
            employee.ManagerOfBranchId = branchId;
            _context.SaveChanges();

            var viewModel = GetReadBranchViewModel(branch);

            SetTempDataForToast("addBranchManagerToast");
            TempData["ToastMessage"] = "Filiaalmanager is toegevoegd";
            TempData["ToastType"] = "success";

            return View("ReadBranchView", viewModel);
        }

        public IActionResult DeleteBranchManager(string employeeId, int branchId)
        {

            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);

            var employee = _context.Employees.SingleOrDefault(e => e.Id.Equals(employeeId.ToString()));
            employee.ManagerOfBranch = null;
            employee.ManagerOfBranchId = null;
            _context.SaveChanges();

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
                Employees = GetEmployeesFromBranch(branch),
                Managers = GetManagersOfBranch(branch)
            };
            viewModel.CountryName = CountryNameToDutch(viewModel.CountryName);
            return viewModel;
        }

        private List<Employee> GetManagersOfBranch(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .Where(e => e.ManagerOfBranchId == branch.BranchId)
                .ToList();
            return employees;
        }

        private List<Employee> GetEmployeesFromBranch(Branch branch)
        {
            List<BranchHasEmployee> branchHasEmployees = _context
                .BranchHasEmployees
                .Where(e => e.BranchId == branch.BranchId)
                .ToList();


            List<Employee> employeesInDatabase = _context
                .Employees
                .ToList();

            List<Employee> employeesInBranch = new List<Employee>();

            foreach (var emp in employeesInDatabase)
            {
                foreach (var branchEmp in branchHasEmployees) 
                {
                    if (branchEmp.EmployeeId == emp.Id)
                    {
                        employeesInBranch.Add(emp);
                    }
                }
            }

            return employeesInBranch;
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
