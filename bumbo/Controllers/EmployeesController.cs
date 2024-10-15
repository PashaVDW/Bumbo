using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.ViewModels;
using System.Threading.Tasks;

namespace bumbo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeesController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || (user.ManagerOfBranchId == null && !user.IsSystemManager))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || (user.ManagerOfBranchId == null && !user.IsSystemManager))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || (user.ManagerOfBranchId == null && !user.IsSystemManager))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    PostalCode = model.PostalCode,
                    HouseNumber = model.HouseNumber,
                    StartDate = model.StartDate,
                    FunctionName = model.FunctionName,
                    Email = model.Email,
                    UserName = model.Email,
                    IsSystemManager = model.IsSystemManager,
                    ManagerOfBranchId = model.ManagerOfBranchId
                };

                var result = await _userManager.CreateAsync(employee, "DefaultPassword123!");

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

    }
}
