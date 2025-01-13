using bumbo.Components;
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
using Azure.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System;

namespace bumbo.Controllers
{
    public class RequestsController : Controller
    {

        private readonly UserManager<Employee> _userManager;
        private readonly IBranchesRepository _branchesRepository;
        private readonly IBranchRequestsEmployeeRepository _branchRequestsEmployeeRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public RequestsController(UserManager<Employee> userManager, IBranchesRepository branchesRepository, 
            IBranchRequestsEmployeeRepository branchRequestsEmployeeRepository, IScheduleRepository scheduleRepository)
        {
            _userManager = userManager;
            _branchesRepository = branchesRepository;
            _branchRequestsEmployeeRepository = branchRequestsEmployeeRepository;
            _scheduleRepository = scheduleRepository;
    }

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            List<BranchRequestsEmployee> incomingRequests = _branchRequestsEmployeeRepository.GetAllIncomingRequests(branchId);
            List<BranchRequestsEmployee> outgoingRequests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(branchId);

            var headers = new List<string> { "Naam aanvrager", "Filiaal", "Bericht", "Nodige Medewerker", "Datum Nodige", "Tijd Nodige", "Acties" };
            var tableBuilder = new TableHtmlBuilder<BranchRequestsEmployee>();
            var htmlTable = tableBuilder.GenerateTable("Inkomende Aanvragen", headers, incomingRequests, "", item =>
            {
                var emp = _branchesRepository.GetEmployeeById(item.EmployeeId);
                var messageFirstPart = item.Message;
                if (messageFirstPart.Length > 20)
                {
                    messageFirstPart = item.Message.Substring(0, 20) + "...";
                }
                return $@"
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{item.BranchId}</td>
                 <td class='py-2 px-4'>{messageFirstPart}</td>
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{item.DateNeeded.Day}-{item.DateNeeded.Month}-{item.DateNeeded.Year}</td>
                 <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                 <td class='py-2 px-4 text-right'>

                 <div class='gap-3 flex'>
                     <form method=""post"" action='/Requests/RejectRequest'>
                         <input type=""hidden"" name='RequestId' value='{item.Id}'/>
                         <button class='py-2 px-2 font-semibold'>Weigeren</button>
                     </form>
                     <form method=""post"" action='/Requests/AcceptRequest'>
                         <input type=""hidden"" name='RequestId' value='{item.Id}'/>
                         <input type=""hidden"" name='Request.RequestToBranchId' value='{item.RequestToBranchId}'/>
                         <input type=""hidden"" name='DepartmentName' value='{item.DepartmentName}'/>
                         <input type=""hidden"" name='Request.EmployeeId' value='{item.EmployeeId}'/>
                         <input type=""hidden"" name='Request.DateNeeded' value='{item.DateNeeded}'/>
                         <input type=""hidden"" name='Request.StartTime' value='{item.StartTime}'/>
                         <input type=""hidden"" name='Request.EndTime' value='{item.EndTime}'/>
                         <button class='bg-green-500 text-white py-2 px-6 rounded-xl font-semibold hover:bg-green-400'>Accepteren</button>
                     </form>
                 
                    <button class='text-xl' onclick=""window.location.href='../Requests/ReadIncoming?id={item.Id}'"">→</button> 
                 </div>
                 </td>
                ";

            }, searchTerm, page);

            string tempHtmlTable = htmlTable.Replace("<button onclick = \"window.location.href='" + "" + "';\" class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl '>Nieuwe inkomende aanvragen </button>", "");
            tempHtmlTable = tempHtmlTable.Replace("<button onclick=\"window.location.href='';\" class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl'>Nieuwe inkomende aanvragen</button>", "");
            string newHtmlTable = tempHtmlTable.Replace("text-4xl", "text-3xl");
            
            ViewBag.HtmlTable = newHtmlTable;

            var headersTwo = new List<string> { "Nodige Medewerker", "Van Filiaal", "Bericht", "Datum Nodige", "Tijd Nodige", "Status", "Acties" };
            var tableBuilderTwo = new TableHtmlBuilder<BranchRequestsEmployee>();
            var htmlTableTwo = tableBuilderTwo.GenerateTable("Aanvragen", headersTwo, outgoingRequests, "../Requests/Create", item =>
            {
                var emp = _branchesRepository.GetEmployeeById(item.EmployeeId);
                var messageFirstPart = item.Message;
                if (messageFirstPart != null && messageFirstPart.Length > 20)
                {
                    messageFirstPart = item.Message.Substring(0, 20) + "...";
                }
                else if(messageFirstPart == null)
                {
                    messageFirstPart = "";
                }
                var branch = _branchesRepository.GetBranch(item.RequestToBranchId);
                return $@"
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{branch.Name}</td>
                 <td class='py-2 px-4'>{messageFirstPart}</td>
                 <td class='py-2 px-4'>{item.DateNeeded.Day}-{item.DateNeeded.Month}-{item.DateNeeded.Year}</td>
                 <td class='py-2 px-4'>{item.StartTime} - {item.EndTime}</td>
                 <td class='py-2 px-4'>{item.RequestStatusName}</td>
                 <td class='py-2 px-4 text-right'>
                 <button onclick=""window.location.href='../Requests/ReadOutgoing?id={item.Id}'"">✏</button> 
                 </td>";

            }, searchTerm, page);

            string newHtmlTableTwo = htmlTableTwo.Replace("text-4xl", "text-3xl");

            ViewBag.HtmlTableTwo = newHtmlTableTwo;

            return View();
        }

        public async Task<IActionResult> ReadOutgoing(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;

            var requests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(branchId);
            var request = requests.Where(r => r.Id == id).SingleOrDefault();
            Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            var viewModel = new RequestsReadViewModel()
            {
                Request = request,
                Employee = emp,
                RequestId = id,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ReadIncoming(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;

            var requests = _branchRequestsEmployeeRepository.GetAllIncomingRequests(branchId);
            var request = requests.Where(r => r.Id == id).SingleOrDefault();
            Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            var viewModel = new RequestsReadViewModel()
            {
                Request = request,
                Employee = emp,
                RequestId = id,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Update(int requestId, string empId, int branchId) 
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var thisBranchId = user.ManagerOfBranchId.Value;
            var requests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(thisBranchId);
            var request = requests.Where(r => r.Id == requestId).SingleOrDefault();

            var model = new RequestsUpdateViewModel();
            if (TempData["UpdateVMUpdate"] != null)
            {
                model = JsonConvert.DeserializeObject<RequestsUpdateViewModel>((string)TempData["UpdateVMUpdate"]);
                request = model.Request;
            }

            Employee emp = new Employee();
            if (!empId.IsNullOrEmpty())
            {
                emp = _branchesRepository.GetEmployeeById(empId);
                request.EmployeeId = empId;
                request.Employee = emp;
                request.RequestToBranchId = branchId;
            } else
            {
                emp = _branchesRepository.GetEmployeeById(request.EmployeeId);
                empId = request.EmployeeId;
                branchId = request.RequestToBranchId;
            }

            var branch = _branchesRepository.GetBranch(branchId);

            var viewModel = new RequestsUpdateViewModel()
            {
                Employee = emp,
                Branch = branch,
                EmployeeId = empId,
                BranchId = branchId,
                Request = request,
                DepartmentName = model.DepartmentName
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Create(string empId, int branchId)
        {
            var model = new RequestsUpdateViewModel();
            if (TempData["UpdateVMCreate"] != null)
            {
                model = JsonConvert.DeserializeObject<RequestsUpdateViewModel>((string)TempData["UpdateVMCreate"]);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

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
                Request = model.Request,
                DepartmentName = model.DepartmentName
            };
            return View(viewModel);
        }

        public async Task<IActionResult> AddEmployee(string previousPage, int requestId) 
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;

            var branches = _branchesRepository.GetAllBranches()
                            .Where(b => b.BranchId != branchId)
                            .ToList();
            foreach (var br in branches)
            {
                br.Employees = _branchesRepository.GetEmployeesFromBranch(br);
            }

            List<Employee> employees = _branchRequestsEmployeeRepository.GetAllAvailableEmployees();
            List<Employee> availableEmployees = new List<Employee>();

            foreach (var br in branches)
            {
                br.Employees = br.Employees
                .Where(e => employees.Contains(e))
                .ToList();
                br.CountryName = CountryNameToDutch(br.CountryName);
            }

            var viewModel = new RequestsAddEmployeeViewModel() {
                AllBranches = branches,
                PreviousPage = previousPage,
                RequestId = requestId,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> UpdateRequest(RequestsUpdateViewModel model, string action)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            model.Employee = _branchesRepository.GetEmployeeById(model.EmployeeId);
            model.Branch = _branchesRepository.GetBranch(model.BranchId);

            if (action.Equals("addEmployee"))
            {
                TempData["UpdateVMUpdate"] = JsonConvert.SerializeObject(model,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return RedirectToAction("AddEmployee", new { previousPage = "Update" });
            }

            if (model.Request.DateNeeded == DateTime.MinValue ||
                model.Request.StartTime == TimeOnly.MinValue || model.Request.EndTime == TimeOnly.MinValue)
            {
                SetTempDataForToast("formFail");
                TempData["ToastMessage"] = "Nog niet alles is ingevuld";
                TempData["ToastType"] = "error";
                return View("Update", model);
            }

            if (model.Request.EndTime <= model.Request.StartTime)
            {
                SetTempDataForToast("endTimeLater");
                TempData["ToastMessage"] = "Begintijd moet later zijn dan eindtijd";
                TempData["ToastType"] = "error";

                return View("Update", model);
            }

            var branchId = user.ManagerOfBranchId.Value;

            var allRequests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(branchId);
            var request = allRequests.Where(r => r.Id == model.Request.Id).SingleOrDefault();

            var newRequest = new BranchRequestsEmployee
            {
                StartTime = model.Request.StartTime,
                EndTime = model.Request.EndTime,
                BranchId = branchId,
                DateNeeded = model.Request.DateNeeded,
                Message = model.Request.Message,
                EmployeeId = model.Request.EmployeeId,
                RequestStatusName = model.Request.RequestStatusName,
                RequestToBranchId = model.Request.RequestToBranchId,
                DepartmentName = model.DepartmentName,
            };

            _branchRequestsEmployeeRepository.DeleteRequest(request);
            _branchRequestsEmployeeRepository.AddRequest(newRequest);
            return Redirect("Index");
        }

        public async Task<IActionResult> CreateRequest(RequestsUpdateViewModel model, string action)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            model.Employee = _branchesRepository.GetEmployeeById(model.EmployeeId);
            model.Branch = _branchesRepository.GetBranch(model.BranchId);

            if (action.Equals("addEmployee"))
            {
                TempData["UpdateVMCreate"] = JsonConvert.SerializeObject(model, 
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return RedirectToAction("AddEmployee",  new { previousPage = "Create" });
            }

            if (model.Request.DateNeeded == DateTime.MinValue || 
                model.Request.StartTime == TimeOnly.MinValue || model.Request.EndTime == TimeOnly.MinValue)
            {
                SetTempDataForToast("formFail");
                TempData["ToastMessage"] = "Nog niet alles is ingevuld";
                TempData["ToastType"] = "error";
                return View("Create", model);
            }

            if (model.EmployeeId == null)
            {
                SetTempDataForToast("addEmployeeFail");
                TempData["ToastMessage"] = "Je moet een medewerker toevoegen";
                TempData["ToastType"] = "error";
                return View("Create", model);
            }

            if (model.Request.EndTime <= model.Request.StartTime)
            {
                SetTempDataForToast("endTimeLater");
                TempData["ToastMessage"] = "Begintijd moet later zijn dan eindtijd";
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
                StartTime = model.Request.StartTime,
                EndTime = model.Request.EndTime,
                DepartmentName = model.DepartmentName,
            };

            _branchRequestsEmployeeRepository.AddRequest(request);

            SetTempDataForToast("createRequest");
            TempData["ToastMessage"] = "Verzoek aangemaakt";
            TempData["ToastType"] = "succes";

            return Redirect("Index");
        }

        public async Task<IActionResult> SearchForAvailableEmployees(string searchTerm, string previousPage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            int thisBranchId = user.ManagerOfBranchId.Value;

            var branches = _branchesRepository.GetAllBranches();
            if (searchTerm.IsNullOrEmpty())
            {
                foreach (var br in branches)
                {
                    br.Employees = _branchesRepository.GetEmployeesFromBranch(br);
                }
                branches = branches.Where(b => b.Employees.Count > 0).Where(b => b.BranchId != thisBranchId).ToList();
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
                branches = branches.Where(b => b.Employees.Count > 0).Where(b => b.BranchId != thisBranchId).ToList();
            }
            
            var viewModel = new RequestsAddEmployeeViewModel()
            {
                AllBranches = branches,
                PreviousPage = previousPage
            };
            return View("AddEmployee", viewModel);
        }

        public async Task<IActionResult> AcceptRequest(RequestsReadViewModel model)
        {
            int id = model.RequestId;
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;

            var requests = _branchRequestsEmployeeRepository.GetAllIncomingRequests(branchId);
            var request = requests.Where(r => r.Id == id).SingleOrDefault();

            Employee emp = _branchesRepository.GetEmployeeWithDepartmentById(model.Request.EmployeeId);
            string departmentName = null;
            
            foreach (var dep in emp.EmployeeHasDepartment)
            {
                if (dep.DepartmentName.Equals(model.DepartmentName))
                {
                    departmentName = dep.DepartmentName;
                }
            }

            Schedule schedule = new Schedule()
            {
                BranchId = model.Request.RequestToBranchId,
                EmployeeId = model.Request.EmployeeId,
                Date = DateOnly.FromDateTime(model.Request.DateNeeded),
                StartTime = model.Request.StartTime,
                EndTime = model.Request.EndTime,
                DepartmentName = departmentName,
            };

            _branchRequestsEmployeeRepository.AcceptRequest(request);
            _scheduleRepository.AddHelpEmployeeToDay(schedule);

            return Redirect("Index");
        }

        public async Task<IActionResult> RejectRequest(RequestsReadViewModel model)
        {
            int id = model.RequestId;
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;

            var requests = _branchRequestsEmployeeRepository.GetAllIncomingRequests(branchId);
            var request = requests.Where(r => r.Id == id).SingleOrDefault();

            _branchRequestsEmployeeRepository.RejectRequest(request);

            return Redirect("Index");
        }

        public async Task<IActionResult> RemoveRequest(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var branchId = user.ManagerOfBranchId.Value;
            var requests = _branchRequestsEmployeeRepository.GetAllOutgoingRequests(branchId);
            var request = requests.Where(r => r.Id == id).SingleOrDefault();

            SetTempDataForToast("createRequest");

            if (request == null)
            {
                TempData["ToastMessage"] = "Uitgaand verzoek bestaat niet!";
                TempData["ToastType"] = "error";
            }
            else
            {
                _branchRequestsEmployeeRepository.RemoveOutgoingRequest(request);
                TempData["ToastMessage"] = "Uitgaand verzoek succesvol verwijdered!";
                TempData["ToastType"] = "error";
            }


            return Redirect("Index");
        }

        // Private methodes

        private void SetTempDataForToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
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

    }
}
