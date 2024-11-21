using bumbo.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Interfaces;
using System.Text;
using bumbo.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace bumbo.Controllers
{
    public class RequestsController : Controller
    {

        private readonly UserManager<Employee> _userManager;
        private readonly IBranchesRepository _branchesRepository;

        public RequestsController(UserManager<Employee> userManager, IBranchesRepository branchesRepository)
        {
            _userManager = userManager;
            _branchesRepository = branchesRepository;
        }

        public async Task<IActionResult> Index(RequestsViewModel oldModel, string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");
            Employee testNeededEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");

            // TODO repo i.p.v. Testdata
            var requests = new List<Request>()
            {
                GetTestRequest()
            };

            var headers = new List<string> { "Naam aanvrager", "Bericht", "Nodige Medewerker", "Datum Nodige", "Tijd Nodige", "Acties" };
            var tableBuilder = new TableHtmlBuilder<Request>();
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
            var tableBuilderTwo = new TableHtmlBuilder<Request>();
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

        public IActionResult ReadOutgoing(int requestId)
        {
            // var request = _requestsRepository.GetRequestById(requestId)
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

        public IActionResult Update() 
        {
            // var request = _requestsRepository.GetRequestById(requestId)
            // Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("b2c2d2e2-2222-3333-4444-5555abcdefab");

            // TODO remove
            var request = GetTestRequest();
            var branch = _branchesRepository.GetBranch(request.BranchId);

            var viewModel = new RequestsUpdateViewModel()
            {
                Request = request,
                Employee = testEmp,
                Branch = branch,
            };
            return View(viewModel);        
        }

        public IActionResult Create(string empId, int branchId)
        {
            // TODO remove
            var request = new Request();
            bool isTest = false;

            bool hasChosenEmp = false;
            Employee emp = new Employee();
            if (!empId.IsNullOrEmpty())
            {
                emp = _branchesRepository.GetEmployeeById(empId);
                hasChosenEmp = true;
                Console.WriteLine(emp.FirstName);
            } else
            {
                request = GetTestRequest();
                isTest = true;
            }

            if(!isTest)
            {
                request = new Request()
                {
                    RequestStatusName = "Afgehandeld",
                    EmployeeId = emp.Id,
                    Message = "Ik wil op vakantie omdat ik de afgelopen maanden hard heb gewerkt en het gevoel heb dat ik een pauze nodig heb om op te laden. De stress van deadlines en lange werkdagen heeft me uitgeput, en ik wil de kans grijpen om te ontspannen en nieuwe energie op te doen. Bovendien heb ik altijd al de prachtige stranden van Bali willen bezoeken, waar ik kan genieten van de zon, de zee en de lokale cultuur. Het lijkt me heerlijk om even weg te zijn van de dagelijkse sleur en te genieten van een nieuwe omgeving. Daarom kan ik niet werken; ik heb deze tijd voor mezelf nodig om te herstellen en te genieten van het leven",
                    DateNeeded = new DateTime(2024, 12, 22),
                    BranchId = branchId,
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(15, 0),
                };
            }
            
            var branch = _branchesRepository.GetBranch(request.BranchId);

            var viewModel = new RequestsUpdateViewModel()
            {
                Request = request,
                Employee = emp,
                Branch = branch,
                HasChosenEmployee = hasChosenEmp,
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

            var viewModel = new RequestsAddEmployeeViewModel()
            {
                AllBranches = branches,
            };
            return View(viewModel);
        }

        public IActionResult UpdateRequest(RequestsUpdateViewModel model)
        {
            Console.WriteLine(model.Employee == null);
            return Redirect("Index");
        }

        public IActionResult CreateRequest()
        {
            return Redirect("Index");
        }

        //TODO remove
        private Request GetTestRequest()
        {
            return new Request()
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

    }
}
