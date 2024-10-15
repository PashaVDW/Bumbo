using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Components;
using static bumbo.Controllers.NormeringController;
using bumbo.ViewModels;

namespace bumbo.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public List<TemplatesViewModel> templates = new List<TemplatesViewModel>()
            {
                new TemplatesViewModel {
                    Name = "test",
                    TemplateId = 3,
                    HasDays = new List<TemplateHasDaysViewModel>() {
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Tuesday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 },
                        new TemplateHasDaysViewModel { DayName = "Monday", CustmerAmount = 500, ContainerAmount = 42 }
                    }
                }
            };

        public TemplatesController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string searchTerm, int page = 1)
        {
            //var user = await _userManager.GetUserAsync(User);

            //if (user == null || user.ManagerOfBranchId == null)
            //{
            //    return RedirectToAction("AccessDenied", "Home");
            //}

            var headers = new List<string> { "Naam" };
            var tableBuilder = new TableHtmlBuilder<TemplatesViewModel>();

            
            var htmlTable = tableBuilder.GenerateTable("Templates", headers, templates, "../#add", "../#edit", item =>
            {
                return $@"
                    <td class='py-2 px-4'>{item.Name}</td>
                    <td class='py-2 px-4 text-right'>
                    <button onclick=""window.location.href='../Templates/Update?templateId={item.TemplateId}'"">✏️</button> 
                    </td>";
            }, searchTerm, page);

            ViewBag.HtmlTable = htmlTable;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int templateId)
        {
            ViewBag.Template = templates.Find(t => t.TemplateId == templateId);
            return View();
        }
    }
}
