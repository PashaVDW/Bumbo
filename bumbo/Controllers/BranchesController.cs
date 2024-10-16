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

namespace bumbo.Controllers
{
    public class BranchesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private BumboDBContext _context;

        //TODO Verwijderen
        private List<Employee> _testEmployees;

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
            var viewModel = GetReadBranchViewModel(branch);

            return View(viewModel);
        }

        public IActionResult CreateBranchManagerView(string searchTerm, int page = 1)
        {
            int branchId = int.Parse(HttpContext.Request.Query["branchId"]);

            var newBranch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            newBranch.Employees = GetEmployees(newBranch).Where(e => e.ManagerOfBranchId == 0).ToList();

            var viewModel = new CreateBranchManagerViewModel() { 
                BranchId = branchId, Employees = newBranch.Employees.ToList() 
            };

            var employees = newBranch.Employees.ToList();

            var headers = new List<string> { "Naam", "Functie", "Filiaal nummer" };
            var tableBuilder = new TableHtmlBuilder<Employee>();
            var htmlTable = tableBuilder.GenerateTable("", headers, employees, "", item =>
            {
            return $@"
                 <td class='py-2 px-4'>{item.FirstName + " " + item.LastName}</td>
                 <td class='py-2 px-4'>{item.FunctionName}</td>
                 <td class='py-2 px-4'>{newBranch.BranchId}</td>
                 <td class='py-2 px-4'><a href='/Branches/AddBranchManager?branchId={newBranch.BranchId}&amp;employeeId={item.Id}' class=""bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 float-left rounded-xl"" >Kiezen</a><td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View(viewModel);
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

        public IActionResult AddBranchManager(int employeeId, int branchId)
        {

            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);

            // TODO doen wanneer er medewerkers in de database staan
            //var employee = _context.Employees.SingleOrDefault(e => e.Id.Equals(employeeId.ToString()));
            //employee.ManagerOfBranch = branch;
            //employee.ManagerOfBranchId = branchId;
            //_context.SaveChanges();

            // TODO verwijderen wanneer er medewerkers in de database staan
            branch.Employees = GetManagersOfBranch(branch);
            var employee = branch.Employees.SingleOrDefault(e => int.Parse(e.Id) == employeeId);
            employee.ManagerOfBranch = branch;
            employee.ManagerOfBranchId = branchId;
            Console.WriteLine(employee.FirstName);
            Console.WriteLine(employee.ManagerOfBranchId);
            // Tot hier

            var viewModel = GetReadBranchViewModel(branch);

            return View("ReadBranchView", viewModel);
        }

        public IActionResult DeleteBranchManager(int employeeId, int branchId)
        {

            Console.WriteLine("Delete ---");
            Console.WriteLine(employeeId + " is in branch " + branchId);
            Console.WriteLine(employeeId + " is in branch " + branchId);
            Console.WriteLine(employeeId + " is in branch " + branchId);
            Console.WriteLine(employeeId + " is in branch " + branchId);
            Console.WriteLine(employeeId + " is in branch " + branchId);

            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            var viewModel = GetReadBranchViewModel(branch);

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
                Employees = GetEmployees(branch),
                Managers = GetManagersOfBranch(branch)
            };
            return viewModel;
        }

        private List<Employee> GetManagersOfBranch(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .Where(e => e.ManagerOfBranchId == branch.BranchId)
                .ToList();

            //TODO verwijderen
            if (_testEmployees == null)
            {
                _testEmployees = new List<Employee>();
                _testEmployees.Add(GetTestEmployee(3, "Cane"));
                _testEmployees.Add(GetTestEmployee(4, "Mike"));
                _testEmployees.Add(GetTestEmployee(5, "Frank"));
                _testEmployees.Add(GetTestEmployee(6, "Jack"));
                _testEmployees.Add(GetTestEmployee(7, "Harry"));

                foreach (var emp in _testEmployees)
                {
                    employees.Add(emp);
                }
            }
            var testManagers = _testEmployees.Where(e => e.ManagerOfBranchId == branch.BranchId);
            foreach (var emp in testManagers)
            {
                employees.Add(emp);
            }
            // Tot hier

            return employees;
        }

        private List<Employee> GetEmployees(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .ToList();

            // TODO Verwijderen
            if(_testEmployees == null)
            {
                _testEmployees = new List<Employee>();
                _testEmployees.Add(GetTestEmployee(3, "Cane"));
                _testEmployees.Add(GetTestEmployee(4, "Mike"));
                _testEmployees.Add(GetTestEmployee(5, "Frank"));
                _testEmployees.Add(GetTestEmployee(6, "Jack"));
                _testEmployees.Add(GetTestEmployee(7, "Harry"));

                foreach (var emp in _testEmployees)
                {
                    employees.Add(emp);
                }
            }
            // Tot hier

            return employees;
        }

        //TODO Verwijderen
        public Employee GetTestEmployee(int id, string FirstName)
        {
            Employee emp = new Employee
            {
                Id = "" + id,
                BID = "B003",
                FirstName = FirstName,
                MiddleName = "A.",
                LastName = "Peterson",
                BirthDate = new DateTime(1985, 2, 20),
                PostalCode = "7812 CD",
                HouseNumber = 18,
                StartDate = new DateTime(2009, 1, 1),
                FunctionName = "Vakkenvuller",
                IsSystemManager = false,
                ManagerOfBranchId = 0,
                UserName = FirstName.ToLower() + ".peterson@hotmail.com",
                NormalizedUserName = FirstName.ToUpper() + ".PETERSON@HOTMAIL.COM",
                Email = FirstName.ToLower() + ".peterson@example.com",
                NormalizedEmail = FirstName.ToUpper() + ".PETERSON@HOTMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "hashedpassword123"
            };
            return emp;
        }
    }
}
