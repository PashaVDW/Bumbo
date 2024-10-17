using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.ViewModels;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bumbo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IFunctionRepository _functionRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;

        public EmployeesController(UserManager<Employee> userManager, IFunctionRepository functionRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository)
        {
            _userManager = userManager;
            _functionRepository = functionRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
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

            var model = new CreateEmployeeViewModel
            {
                Functions = _functionRepository.GetAllFunctions().Select(f => new SelectListItem
                {
                    Value = f.FunctionName,
                    Text = f.FunctionName
                }).ToList()
            };

            return View(model);
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
                    Email = model.Email,
                    UserName = model.Email,
                    IsSystemManager = model.IsSystemManager,
                    ManagerOfBranchId = model.ManagerOfBranchId
                };

                var result = await _userManager.CreateAsync(employee, "DefaultPassword123!");

                if (result.Succeeded)
                {
                    if (user.ManagerOfBranchId != null)
                    {
                        var branchHasEmployee = new BranchHasEmployee
                        {
                            EmployeeId = employee.Id,
                            BranchId = user.ManagerOfBranchId.Value,
                            FunctionName = model.SelectedFunction,
                            StartDate = DateTime.Now
                        };

                        await _branchHasEmployeeRepository.AddBranchHasEmployeeAsync(branchHasEmployee);
                    }

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                // Log the validation errors
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                }
            }

            model.Functions = _functionRepository.GetAllFunctions().Select(f => new SelectListItem
            {
                Value = f.FunctionName,
                Text = f.FunctionName
            }).ToList();

            return View(model);
        }





    }
}
