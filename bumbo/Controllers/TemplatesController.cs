using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Components;
using static bumbo.Controllers.NormeringController;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bumbo.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        readonly ITemplatesRepository _templatesRepository;
        readonly ITemplateHasDaysRepository _templatesHasDaysRepository;
        readonly IDaysRepository _daysRepository;

        public TemplatesController(UserManager<Employee> userManager, ITemplatesRepository templatesRepository, ITemplateHasDaysRepository templateHasDaysRepository, IDaysRepository daysRepository)
        {
            _userManager = userManager;
            _templatesRepository = templatesRepository;
            _templatesHasDaysRepository = templateHasDaysRepository;
            _daysRepository = daysRepository;
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
            })
            .ToList();

            var htmlTable = tableBuilder.GenerateTable("Templates", headers, templateViewModel, "../Templates/Create", "../#edit", item =>
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

        public IActionResult Create()
        {
            var model = new TemplateCreateViewModel
            {
                Days = new List<DayData>
                {
                    new DayData { DayName = "Monday" },
                    new DayData { DayName = "Tuesday" },
                    new DayData { DayName = "Wednesday" },
                    new DayData { DayName = "Thursday" },
                    new DayData { DayName = "Friday" },
                    new DayData { DayName = "Saturday" },
                    new DayData { DayName = "Sunday" }
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, Dictionary<string, int> customerData, Dictionary<string, int> containerData)
        {
            // Check if a template with the same name already exists
            var existingTemplate = await _templatesRepository.GetByNameAsync(name);
            if (existingTemplate != null)
            {
                ModelState.AddModelError("Name", "A template with this name already exists.");
            }

            // Check that all customer and container amounts are greater than 0
            foreach (var kvp in customerData)
            {
                if (kvp.Value <= 0)
                {
                    ModelState.AddModelError($"customerData[{kvp.Key}]", "Customer amount must be greater than 0.");
                }
            }

            foreach (var kvp in containerData)
            {
                if (kvp.Value <= 0)
                {
                    ModelState.AddModelError($"containerData[{kvp.Key}]", "Container amount must be greater than 0.");
                }
            }

            // If the model state is not valid, return the view with the current data
            if (!ModelState.IsValid)
            {
                // Pass back the current data to the view
                ViewBag.Template = new TemplateCreateViewModel
                {
                    Name = name,
                    Days = customerData.Select(kvp => new DayData
                    {
                        DayName = kvp.Key,
                        CustomerAmount = kvp.Value,
                        ContainerAmount = containerData[kvp.Key]
                    }).ToList()
                };

                return View();
            }

            // Create a new Template
            var template = new Template
            {
                Name = name,
                TemplateHasDays = customerData.Select(kvp => new TemplateHasDays
                {
                    Days_name = kvp.Key,  // Ensure this matches your property name
                    CustomerAmount = kvp.Value,
                    ContainerAmount = containerData[kvp.Key]
                }).ToList()
            };

            // Add the template to the repository and save changes
            await _templatesRepository.AddAsync(template);
            await _templatesRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Update(int templateId)
        {
            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays();
            List<Template> templates = _templatesRepository.GetAllTemplates();

            var dayOrder = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

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
            .OrderBy(thd => Array.IndexOf(dayOrder, thd.DayName))
            .ToList()
            }).ToList();

            ViewBag.Template = templateViewModel.Find(t => t.TemplateId == templateId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int templateId, string Name, Dictionary<string, int> customerData, Dictionary<string, int> containerData)
        {
            var template = _templatesRepository.GetAllTemplates().FirstOrDefault(t => t.Id == templateId);

            if(template == null)
            {
                var templateViewModel = new TemplatesViewModel
                {
                    TemplateId = templateId,
                    Name = Name,
                    HasDays = customerData.Select((customer, index) => new TemplateHasDaysViewModel
                    {
                        DayName = customer.Key,
                        CustomerAmount = customer.Value,
                        ContainerAmount = containerData.ContainsKey(customer.Key) ? containerData[customer.Key] : 0
                    }).ToList()
                };

                ViewBag.Template = templateViewModel;

                return View();
            }

            template.Name = Name;

            var templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays()
                .Where(thd => thd.Templates_id == templateId)
                .ToList();

            foreach (var thd in templateHasDays)
            {
                if (customerData.TryGetValue(thd.Days_name, out int customerAmount))
                {
                    thd.CustomerAmount = customerAmount;
                }

                if (containerData.TryGetValue(thd.Days_name, out int containerAmount))
                {
                    thd.ContainerAmount = containerAmount;
                }

                _templatesHasDaysRepository.Update(thd);
            }

            _templatesRepository.Update(template);
            await _templatesRepository.SaveChangesAsync();
            await _templatesHasDaysRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
