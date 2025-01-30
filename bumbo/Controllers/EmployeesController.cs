using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using bumbo.Components;

namespace bumbo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IFunctionRepository _functionRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(UserManager<Employee> userManager, IFunctionRepository functionRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _functionRepository = functionRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 25)
        {
            var headers = new List<string> { "Naam", "Achternaam", "Email", "Telefoonnummer",  };
            var tableBuilder = new TableHtmlBuilder<Employee>();
            var htmlTable = tableBuilder.GenerateTable("", headers, _employeeRepository.GetAllEmployees(), "/medewerkers/aanmaken", item =>
            {
                return $@"
                    <td class='py-2 px-4'>{item.FirstName}</td>
                    <td class='py-2 px-4'>{item.LastName}</td>
                    <td class='py-2 px-4'>{item.Email}</td>
                    <td class='py-2 px-4'>{item.PhoneNumber}</td>
                    <td class='py-2 px-4'><a href='/medewerkers/bewerken?medewerkerId={item.Id}' class=""bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 float-left rounded-xl"" >Bewerken</a><td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

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
                }).ToList(),

                IsSystemManager = user.IsSystemManager
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
                    PhoneNumber = model.PhoneNumber,
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
                            FunctionName = string.IsNullOrEmpty(model.SelectedFunction) ? null : model.SelectedFunction,
                            StartDate = DateTime.Now
                        };

                        await _branchHasEmployeeRepository.AddBranchHasEmployeeAsync(branchHasEmployee);
                    }

                    TempData["SuccessEmployeeAddedMessage"] = "Medewerker succesvol toegevoegd!";

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

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(string medewerkerId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || (user.ManagerOfBranchId == null && !user.IsSystemManager))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var employee = _employeeRepository.GetEmployeeById(medewerkerId);
            if (employee == null)
            {
                TempData["ErrorMessage"] = "De medewerker die u wilt bewerken, bestaat niet." + medewerkerId + "ads";
                return RedirectToAction("Index");
            }

            var branchAssignments = _branchHasEmployeeRepository.GetBranchesForEmployee(employee.Id);
            var userManagerOfBranchAssignment = branchAssignments.FirstOrDefault(b => b.BranchId == user.ManagerOfBranchId);

            var model = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                PostalCode = employee.PostalCode,
                HouseNumber = employee.HouseNumber,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                BID = employee.BID,
                IsSystemManager = employee.IsSystemManager,
                UserManagerOfBranchId = user.ManagerOfBranchId ?? 0,

                SelectedFunction = userManagerOfBranchAssignment?.FunctionName,

                BranchAssignments = branchAssignments.Select(bhe => new BranchAssignmentViewModel
                {
                    BranchId = bhe.BranchId,
                    BranchName = bhe.Branch.Name,
                    FunctionName = bhe.FunctionName,
                    StartDate = bhe.StartDate
                }).ToList(),

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
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || (user.ManagerOfBranchId == null && !user.IsSystemManager))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (!ModelState.IsValid)
            {
                model.Functions = _functionRepository.GetAllFunctions().Select(f => new SelectListItem
                {
                    Value = f.FunctionName,
                    Text = f.FunctionName
                }).ToList();

                var branchAssignments = _branchHasEmployeeRepository.GetBranchesForEmployee(model.Id);
                model.BranchAssignments = branchAssignments.Select(bhe => new BranchAssignmentViewModel
                {
                    BranchId = bhe.BranchId,
                    BranchName = bhe.Branch.Name,
                    FunctionName = bhe.FunctionName,
                    StartDate = bhe.StartDate
                }).ToList();

                return View(model);
            }

            var employee = _employeeRepository.GetEmployeeById(model.Id);
            if (employee == null)
            {
                TempData["ErrorMessage"] = "De medewerker die u wilt bewerken, bestaat niet.";
                return RedirectToAction("Index");
            }

            employee.FirstName = model.FirstName;
            employee.MiddleName = model.MiddleName;
            employee.LastName = model.LastName;
            employee.BirthDate = model.BirthDate;
            employee.PostalCode = model.PostalCode;
            employee.HouseNumber = model.HouseNumber;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Email = model.Email;
            employee.BID = model.BID;
            employee.IsSystemManager = model.IsSystemManager;

            _employeeRepository.UpdateEmployee(employee);

            if (user.ManagerOfBranchId != null)
            {
                var branchAssignment = _branchHasEmployeeRepository.GetBranchAssignment(employee.Id, user.ManagerOfBranchId.Value);

                if (branchAssignment != null && !string.IsNullOrEmpty(model.SelectedFunction))
                {
                    branchAssignment.FunctionName = model.SelectedFunction;
                    _branchHasEmployeeRepository.UpdateBranchAssignment(branchAssignment);
                }
                else if (branchAssignment == null && !string.IsNullOrEmpty(model.SelectedFunction))
                {
                    TempData["ErrorMessage"] = "Functie kan niet ingesteld worden.";
                }
            }

            TempData["SuccessMessage"] = "De medewerker is succesvol bijgewerkt.";

            // Redirect back to the employee overview page
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBranchAssignment(string EmployeeId, int BranchId)
        {
            var branchAssignment = _branchHasEmployeeRepository.GetBranchAssignment(EmployeeId, BranchId);

            if (branchAssignment == null)
            {
                TempData["ErrorMessage"] = "De toewijzing kon niet worden gevonden.";
                return RedirectToAction("Update", new { medewerkerId = EmployeeId });
            }

            // Verwijder de branch-employee relatie
            _branchHasEmployeeRepository.RemoveBranchAssignment(branchAssignment);

            TempData["SuccessMessage"] = "De toewijzing is succesvol verwijderd.";
            return RedirectToAction("Update", new { medewerkerId = EmployeeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployee(string employeeId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);

            if (employee == null)
            {
                TempData["ErrorMessage"] = "De medewerker die u wilt verwijderen, bestaat niet.";
                return RedirectToAction("Index");
            }

            try
            {
                _employeeRepository.DeleteEmployee(employeeId);

                TempData["SuccessMessage"] = "De medewerker is succesvol verwijderd.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Er is een fout opgetreden bij het verwijderen van de medewerker: {ex.Message}";
                return RedirectToAction("Update", new { medewerkerId = employeeId });
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignBranchToEmployee(string employeeId, int branchId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);
            if (employee == null)
            {
                TempData["ErrorMessage"] = "De medewerker bestaat niet.";
                return RedirectToAction("Index");
            }

            var branchAssignment = new BranchHasEmployee
            {
                EmployeeId = employeeId,
                BranchId = branchId,
                FunctionName = null,
                StartDate = DateTime.Now
            };

            await _branchHasEmployeeRepository.AddBranchHasEmployeeAsync(branchAssignment);

            TempData["SuccessMessage"] = "Het filiaal is succesvol toegewezen aan de medewerker.";

            return RedirectToAction("Update", new { medewerkerId = employeeId });
        }



    }
}
