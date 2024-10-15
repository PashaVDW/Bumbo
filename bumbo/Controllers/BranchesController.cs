using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using bumbo.Components;
using static bumbo.Controllers.NormeringController;
using bumbo.ViewModels;

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
                branch.Employees = GetEmployees(branch);
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

            return View(branches);
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
            var viewModel = new ReadFilialenViewModel() { BranchId = branchId, 
                CountryName = branch.CountryName, HouseNumber = branch.HouseNumber, 
                Name = branch.Name, PostalCode = branch.PostalCode, Street = branch.Street, 
                Employees = GetEmployees(branch), Managers = GetManagersOfBranch(branch)};
            
            return View(viewModel);
        }

        public IActionResult CreateBranchManagerView(string searchTerm, int page = 1)
        {
            int branchId = int.Parse(HttpContext.Request.Query["branchId"]);
            var newBranch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            newBranch.Employees = GetEmployees(newBranch).Where(e => e.ManagerOfBranchId == 0).ToList();

            var employees = newBranch.Employees.ToList();

            var headers = new List<string> { "Naam", "Functie", "Filiaal nummer" };
            var tableBuilder = new TableHtmlBuilder<Employee>();
            var htmlTable = tableBuilder.GenerateTable("", headers, employees, "", item =>
            {
            return $@"
                 <td class='py-2 px-4'>{item.FirstName + item.LastName}</td>
                 <td class='py-2 px-4'>{item.FunctionName}</td>
                 <td class='py-2 px-4'>{newBranch.BranchId}</td>";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View(newBranch);
        }

        public IActionResult AddBranch(Branch branch)
        {

            _context.Branches.Add(branch);
            _context.SaveChanges();

            return RedirectToAction("BranchesView");
        }

        [HttpPost]
        public IActionResult UpdateBranch(Branch branch)
        {

            _context.Branches.Update(branch);
            _context.SaveChanges();

            return RedirectToAction("BranchesView");
        }

        [HttpPost]
        public IActionResult DeleteBranch(int branchId)
        {
            var newBranch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);

            if(newBranch == null)
            {
                return View("UpdateBranchView", branchId);
            }

            _context.Branches.Remove(newBranch);
            _context.SaveChanges();

            return RedirectToAction("BranchesView");
        }

        private List<Employee> GetManagersOfBranch(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .Where(e => e.ManagerOfBranchId == branch.BranchId)
                .ToList();
            return employees;
        }

        private List<Employee> GetEmployees(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .ToList();
            employees.Add(GetTestEmployee());
            employees.Add(GetTestEmployee());
            employees.Add(GetTestEmployee());
            employees.Add(GetTestEmployee());
            employees.Add(GetTestEmployee());
            return employees;
        }

        public Employee GetTestEmployee()
        {
            Employee emp = new Employee
            {
                Id = "3",
                BID = "B003",
                FirstName = "Cane",
                MiddleName = "A.",
                LastName = "Peterson",
                BirthDate = new DateTime(1985, 2, 20),
                PostalCode = "7812 CD",
                HouseNumber = 18,
                StartDate = new DateTime(2009, 1, 1),
                FunctionName = "Vakkenvuller",
                IsSystemManager = false,
                ManagerOfBranchId = 0,
                UserName = "cane.peterson@hotmail.com",
                NormalizedUserName = "CANE.PETERSON@HOTMAIL.COM",
                Email = "cane.doe@example.com",
                NormalizedEmail = "CANE.PETERSON@HOTMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "hashedpassword123"
            };
            return emp;
        }
    }
}
