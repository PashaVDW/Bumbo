using bumbo.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Interfaces;

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

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !user.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0");

            // TODO repo i.p.v. Testdata
            var requests = new List<Request>()
            {
                new Request()
                {
                    RequestStatusName = "Afgehandeld",
                    RequestTypeName = "Vakantie",
                    EmployeeId = testEmp.Id,
                    Description = "Ik wil vakantie, want ik vind mijn collega 's vervelend"
                }
            };

            var headers = new List<string> { "Titel", "Naam aanvrager", "Categorie", "Status" };
            var tableBuilder = new TableHtmlBuilder<Request>();
            var htmlTable = tableBuilder.GenerateTable("Aanvragen", headers, requests, "", item =>
            {
                return $@"
                 <td class='py-2 px-4'><strong>Naam</strong></td>
                 <td class='py-2 px-4'>{_branchesRepository.GetEmployeeById(item.EmployeeId).FirstName}</td>
                 <td class='py-2 px-4'>{item.RequestTypeName}</td>
                 <td class='py-2 px-4'>{item.RequestStatusName}</td>
                 <td class='py-2 px-4 text-right'>
                 <button onclick=""window.location.href='../Requests/Read?requestId={item.RequestId}'"">✏️</button> 
                 </td>";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;

            return View();
        }

        public IActionResult Read(int requestId)
        {
            return View();
        }
    }
}
