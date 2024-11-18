using bumbo.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Interfaces;
using System.Text;
using bumbo.ViewModels;

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
            Employee testEmp = _branchesRepository.GetEmployeeById("289594c0-1d21-4276-b98c-3a7a9d18cb2b");

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

            MakeHtmlTable(requests, searchTerm, page);

            var viewModel = new RequestsViewModel()
            {
                Requests = requests,
                SelectedType = oldModel.SelectedType,
                ShowFinishedRequests = oldModel.ShowFinishedRequests
            };

            return View(viewModel);
        }

        public IActionResult Read(int requestId)
        {
            // var request = _requestsRepository.GetRequestById(requestId)
            // Employee emp = _branchesRepository.GetEmployeeById(request.EmployeeId);

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("289594c0-1d21-4276-b98c-3a7a9d18cb2b");

            var request = new Request()
            {
                RequestStatusName = "Afgehandeld",
                RequestTypeName = "Vakantie",
                EmployeeId = testEmp.Id,
                Description = "Ik wil op vakantie omdat ik de afgelopen maanden hard heb gewerkt en het gevoel heb dat ik een pauze nodig heb om op te laden. De stress van deadlines en lange werkdagen heeft me uitgeput, en ik wil de kans grijpen om te ontspannen en nieuwe energie op te doen. Bovendien heb ik altijd al de prachtige stranden van Bali willen bezoeken, waar ik kan genieten van de zon, de zee en de lokale cultuur. Het lijkt me heerlijk om even weg te zijn van de dagelijkse sleur en te genieten van een nieuwe omgeving. Daarom kan ik niet werken; ik heb deze tijd voor mezelf nodig om te herstellen en te genieten van het leven."
            };

            var viewModel = new RequestsReadViewModel() { 
                Request = request,
                Employee = testEmp
            };

            return View(viewModel);
        }

        public IActionResult FilterType(RequestsViewModel oldModel, string searchTerm, int page = 1)
        {
            Console.WriteLine("Categorie: " + oldModel.SelectedType);
            Console.WriteLine("Status: " + oldModel.ShowFinishedRequests);

            // TODO remove
            Employee testEmp = _branchesRepository.GetEmployeeById("289594c0-1d21-4276-b98c-3a7a9d18cb2b");

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

            MakeHtmlTable(requests, searchTerm, page);

            var viewModel = new RequestsViewModel()
            {
                Requests = requests,
                SelectedType = oldModel.SelectedType,
                ShowFinishedRequests = oldModel.ShowFinishedRequests
            };

            return View("Index", viewModel);
        }

        private void MakeHtmlTable(List<Request> requests, string searchTerm, int page)
        {
            var headers = new List<string> { "Titel", "Naam aanvrager", "Categorie", "Status" };
            var tableBuilder = new TableHtmlBuilderRequests<Request>();
            var htmlTable = tableBuilder.GenerateTable("Aanvragen", headers, requests, item =>
            {
                var emp = _branchesRepository.GetEmployeeById(item.EmployeeId);
                return $@"
                 <td class='py-2 px-4'><strong>Naam</strong></td>
                 <td class='py-2 px-4'>{emp.FirstName} {emp.MiddleName} {emp.LastName}</td>
                 <td class='py-2 px-4'>{item.RequestTypeName}</td>
                 <td class='py-2 px-4'>{item.RequestStatusName}</td>
                 <td class='py-2 px-4 text-right'>
                 <button onclick=""window.location.href='../Requests/Read?requestId={item.RequestId}'"">✏️</button> 
                 </td>";

            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;
        }
    }


    
    class TableHtmlBuilderRequests<TItem>
    {
        public string GenerateTable(string title, List<string> headers, List<TItem> items, Func<TItem, string> rowTemplate, string searchTerm = null, int currentPage = 1, int pageSize = 10)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                items = items.Where(item =>
                {
                    var properties = typeof(TItem).GetProperties();
                    return properties.Any(prop => prop.GetValue(item)?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
                }).ToList();
            }

            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            var pagedItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<div class='container mx-auto p-10'>" +
                                   "<div class='flex justify-between items-center mb-4 mx-8'>" +
                                   "<h2 class='mb-4 text-4xl font-bold leading-none tracking-tight text-gray-900'>" + title + "</h2>" +
                                   "<form method='get' class='flex items-center space-x-4'>" +
                                   "<input type='text' name='searchTerm' value='" + searchTerm + "' placeholder='Zoek naar medewerkers' class='w-full border border-gray-300 rounded-full py-2 px-6 focus:outline-none focus:ring-2 focus:ring-yellow-400' />" +
                                   "<button type='submit' class='bg-gray-200 text-gray-700 py-2 px-6 rounded-full hover:bg-gray-300'>Zoeken</button>" +
                                   "</form>" +
                                   "</div>"
                                   );
            htmlBuilder.AppendLine("<div class='w-full p-6'>");
            htmlBuilder.AppendLine("<div class='overflow-x-auto w-full'>");
            htmlBuilder.AppendLine("<table class='min-w-full table-auto border-collapse'>");

            htmlBuilder.AppendLine(
                "<thead>" +
                "<tr class='text-left text-gray-600 font-bold'>"
            );
            foreach (var header in headers)
            {
                htmlBuilder.AppendLine($"<th class='py-2 px-4'>{header}</th>");
            }
            htmlBuilder.AppendLine(
                "</tr>" +
                "</thead>"
            );
            htmlBuilder.AppendLine("<tbody>");
            foreach (var item in pagedItems)
            {
                htmlBuilder.AppendLine("<tr class='border-b hover:bg-gray-50'>");
                htmlBuilder.AppendLine(rowTemplate(item));
                htmlBuilder.AppendLine("</tr>");
            }
            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("<div class='flex justify-center items-center mt-4 space-x-2'>");

            if (currentPage > 1)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage - 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Vorige</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Vorige</span>");
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    htmlBuilder.AppendLine($"<span class='py-2 px-4 bg-yellow-400 text-white rounded-lg'>{i}</span>");
                }
                else
                {
                    htmlBuilder.AppendLine($"<a href='?page={i}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300'>{i}</a>");
                }
            }
            if (currentPage < totalPages)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage + 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Volgende</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Volgende</span>");
            }
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("</div>");

            return htmlBuilder.ToString();
        }
    }
}
