﻿using bumbo.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Interfaces;
using System.Text;
using bumbo.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using bumbo.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace bumbo.Controllers
{
    public class RequestsController : Controller
    {

        private readonly UserManager<Employee> _userManager;
        private readonly IBranchesRepository _branchesRepository;
        private readonly IBranchRequestsEmployeeRepository _branchRequestsEmployeeRepository;

        public RequestsController(UserManager<Employee> userManager, IBranchesRepository branchesRepository, IBranchRequestsEmployeeRepository branchRequestsEmployeeRepository)
        {
            _userManager = userManager;
            _branchesRepository = branchesRepository;
            _branchRequestsEmployeeRepository = branchRequestsEmployeeRepository;
    }

        public async Task<IActionResult> Index(RequestsViewModel oldModel, string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");
            Employee testNeededEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");

            // TODO repo i.p.v. Testdata
            var requests = new List<BranchRequestsEmployee>()
            {
                GetTestRequest()
            };

            var headers = new List<string> { "Naam aanvrager", "Bericht", "Nodige Medewerker", "Datum Nodige", "Tijd Nodige", "Acties" };
            var tableBuilder = new TableHtmlBuilder<BranchRequestsEmployee>();
            var htmlTable = tableBuilder.GenerateTable("Inkomende Aanvragen", headers, requests, "", item =>
            {
                var emp = _branchesRepository.GetEmployeeById(item.EmployeeId);
                var messageFirstPart = item.Message.Substring(0, 50) + "...";
                return $@"
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{messageFirstPart}</td>
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{item.DateNeeded.Day}-{item.DateNeeded.Month}-{item.DateNeeded.Year}</td>
                 <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                 <td class='py-2 px-4 text-right'>
                 <div class='gap-4 flex w-11/12 justify-end'>
                    <button class='bg-red-600 hover:bg-red-500 text-white font-semibold py-2 px-6 rounded-xl'>Weigeren</button>
                    <button class='bg-green-500 hover:bg-green-400 text-white font-semibold py-2 px-6 rounded-xl'>Accepteren</button>
                 </div>
                 </td>
                ";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            var headersTwo = new List<string> { "Nodige Medewerker", "Van Filiaal", "Bericht", "Datum Nodige", "Tijd Nodige", "Status", "Acties" };
            var tableBuilderTwo = new TableHtmlBuilder<BranchRequestsEmployee>();
            var htmlTableTwo = tableBuilderTwo.GenerateTable("Uitgaande Aanvragen", headersTwo, requests, "../Requests/Create", item =>
            {
                var emp = _branchesRepository.GetEmployeeById(item.EmployeeId);
                var messageFirstPart = item.Message.Substring(0, 50) + "...";
                var branch = _branchesRepository.GetBranch(item.BranchId);
                return $@"
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{branch.Name}</td>
                 <td class='py-2 px-4'>{messageFirstPart}</td>
                 <td class='py-2 px-4'>{item.DateNeeded.Day}-{item.DateNeeded.Month}-{item.DateNeeded.Year}</td>
                 <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                 <td class='py-2 px-4'>{item.RequestStatusName}</td>
                 <td class='py-2 px-4 text-right'>
                 <button onclick=""window.location.href='../Requests/ReadOutgoing?requestId={item.RequestToBranchId}'"">✏️</button> 
                 </td>";

            }, searchTerm, page);

            ViewBag.HtmlTableTwo = htmlTableTwo;

            var viewModel = new RequestsViewModel()
            {
                IncomingRequests = requests,
                OutgoingRequests = requests
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ReadOutgoing(int requestId)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // var request = _branchRequestsEmployeeRepository.GetRequestById(requestId)
            // Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");

            // TODO remove
            var request = GetTestRequest();

            var viewModel = new RequestsReadViewModel()
            {
                Request = request,
                Employee = testEmp
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Update(string empId, int branchId) 
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // var request = _requestsRepository.GetRequestById(requestId)
            // Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            // TODO remove
            var request = GetTestRequest();

            bool hasChosenEmp = false;
            Employee emp = new Employee();

            var branch = new Branch();

            if (!empId.IsNullOrEmpty())
            {
                emp = _branchesRepository.GetEmployeeById(empId);
                branch = _branchesRepository.GetBranch(branchId);
                hasChosenEmp = true;

                // TODO remove
                request.BranchId = branchId;
            } else
            {
                emp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");
            }
            
            request.EmployeeId = emp.Id;


            var viewModel = new RequestsUpdateViewModel()
            {
                Employee = emp,
                Branch = branch,
                HasChosenEmployee = hasChosenEmp,
                Request = request
            };
            return View(viewModel);        
        }

        public async Task<IActionResult> Create(string empId, int branchId)
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            Console.WriteLine(user.ManagerOfBranchId.Value);

            bool hasChosenEmp = false;
            Employee emp = new Employee();
            if (!empId.IsNullOrEmpty())
            {
                emp = _branchesRepository.GetEmployeeById(empId);
                hasChosenEmp = true;
            }

            var branch = _branchesRepository.GetBranch(branchId);

            var viewModel = new RequestsUpdateViewModel()
            {
                Employee = emp,
                Branch = branch,
                HasChosenEmployee = hasChosenEmp,
                EmployeeId = empId,
                BranchId = branchId,
            };
            return View(viewModel);
        }

        public IActionResult AddEmployee() 
        {
            var branches = _branchesRepository.GetAllBranches();
            foreach (var br in branches)
            {
                br.Employees = _branchesRepository.GetEmployeesFromBranch(br);
            }

            var viewModel = new RequestsAddEmployeeViewModel() { AllBranches = branches };
            return View(viewModel);
        }

        public IActionResult AddEmployeeUpdate()
        {
            var branches = _branchesRepository.GetAllBranches();
            foreach (var br in branches)
            {
                br.Employees = _branchesRepository.GetEmployeesFromBranch(br);
            }

            var viewModel = new RequestsAddEmployeeViewModel() { AllBranches = branches };
            return View(viewModel);
        }

        public IActionResult UpdateRequest(RequestsUpdateViewModel model)
        {
            Console.WriteLine(model.Employee == null);
            return Redirect("Index");
        }

        public async Task<IActionResult> CreateRequest(RequestsUpdateViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (model.EmployeeId == null)
            {
                SetTempDataForToast("addEmployeeFail");
                TempData["ToastMessage"] = "Je moet een medewerker toevoegen";
                TempData["ToastType"] = "error";
                return View("Create", model);
            }

            model.Employee = _branchesRepository.GetEmployeeById(model.EmployeeId);
            model.Branch = _branchesRepository.GetBranch(model.BranchId);
            int thisBranchId = user.ManagerOfBranchId.Value;


            BranchRequestsEmployee request = new BranchRequestsEmployee()
            {
                Branch = model.Branch,
                BranchId = thisBranchId,
                Employee = model.Employee,
                EmployeeId = model.Employee.Id,

                Message = model.Request.Message,
                RequestStatusName = "In Afwachting",
                RequestToBranchId = model.Branch.BranchId,

                DateNeeded = model.Request.DateNeeded,
                Department = model.Request.Department,
                StartTime = model.Request.StartTime,
                EndTime = model.Request.EndTime,
            };

            _branchRequestsEmployeeRepository.AddRequest(request);

            SetTempDataForToast("createRequest");
            TempData["ToastMessage"] = "Verzoek aangemaakt";
            TempData["ToastType"] = "succes";

            return Redirect("Index");
        }

        public IActionResult SearchForAvailableEmployees(string searchTerm)
        {
            var branches = _branchesRepository.GetAllBranches();
            if (searchTerm.IsNullOrEmpty())
            {
                foreach (var br in branches)
                {
                    br.Employees = _branchesRepository.GetEmployeesFromBranch(br);
                }
            } else
            {
                foreach (var br in branches)
                {
                    br.Employees = _branchesRepository.GetEmployeesFromBranch(br).Where(e =>
                        (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(e.MiddleName) && e.MiddleName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(e.LastName) && e.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }
                branches = branches.Where(b => b.Employees.Count > 0).ToList();
            }
            
            var viewModel = new RequestsAddEmployeeViewModel()
            {
                AllBranches = branches,
            };
            return View("AddEmployee", viewModel);
        }

        //TODO remove
        private BranchRequestsEmployee GetTestRequest()
        {
            return new BranchRequestsEmployee()
            {
                RequestStatusName = "Afgehandeld",
                EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab",
                Message = "Ik wil op vakantie omdat ik de afgelopen maanden hard heb gewerkt en het gevoel heb dat ik een pauze nodig heb om op te laden. De stress van deadlines en lange werkdagen heeft me uitgeput, en ik wil de kans grijpen om te ontspannen en nieuwe energie op te doen. Bovendien heb ik altijd al de prachtige stranden van Bali willen bezoeken, waar ik kan genieten van de zon, de zee en de lokale cultuur. Het lijkt me heerlijk om even weg te zijn van de dagelijkse sleur en te genieten van een nieuwe omgeving. Daarom kan ik niet werken; ik heb deze tijd voor mezelf nodig om te herstellen en te genieten van het leven",
                DateNeeded = new DateTime(2024, 12, 22),
                BranchId = 4,
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(15, 0),
            };
        }

        private void SetTempDataForToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
        }

    }
}
