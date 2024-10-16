using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Components;
using static bumbo.Controllers.NormeringController;
using bumbo.ViewModels;
using DataLayer.Interfaces;

namespace bumbo.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        readonly ITemplatesRepository _templatesRepository;
        readonly ITemplateHasDaysRepository _templatesHasDaysRepository;

        public TemplatesController(UserManager<Employee> userManager, ITemplatesRepository templatesRepository, ITemplateHasDaysRepository templateHasDaysRepository)
        {
            _userManager = userManager;
            _templatesRepository = templatesRepository;
            _templatesHasDaysRepository = templateHasDaysRepository;
        }

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var headers = new List<string> { "Naam" };
            var tableBuilder = new TableHtmlBuilder<TemplatesViewModel>();

            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays();
            List<Template> templates = _templatesRepository.GetAllTemplates();

            List<TemplatesViewModel> templateViewModel = templates.Select(template => new TemplatesViewModel
            {
                TemplateId = template.Id,
                Name = template.Name,
                HasDays = templateHasDays
            .Where(thd => thd.Templates_id == template.Id)
            .Select(thd => new TemplateHasDaysViewModel
                {
                    DayName = thd.Days_name,
                    CustomerAmount = thd.CustomerAmount,
                    ContainerAmount = thd.ContainerAmount
                })
            .ToList()
            }).ToList();

            var htmlTable = tableBuilder.GenerateTable("Templates", headers, templateViewModel, "../#add", "../#edit", item =>
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

        [HttpGet]
        public async Task<IActionResult> Update(int templateId)
        {
            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays();
            List<Template> templates = _templatesRepository.GetAllTemplates();

            List<TemplatesViewModel> templateViewModel = templates.Select(template => new TemplatesViewModel
            {
                TemplateId = template.Id,
                Name = template.Name,
                HasDays = templateHasDays
            .Where(thd => thd.Templates_id == template.Id)
            .Select(thd => new TemplateHasDaysViewModel
                {
                    DayName = thd.Days_name,
                    CustomerAmount = thd.CustomerAmount,
                    ContainerAmount = thd.ContainerAmount
                })
            .ToList()
            }).ToList();

            ViewBag.Template = templateViewModel.Find(t => t.TemplateId == templateId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int templateId, TemplateHasDays templateHasDays)
        {
            return View(templateHasDays);
        }
    }
}
