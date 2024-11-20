using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using bumbo.Components;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            int branchId = user.ManagerOfBranchId.Value;

            var headers = new List<string> { "Naam" };
            var tableBuilder = new TableHtmlBuilder<TemplatesViewModel>();

            List<Template> templates = _templatesRepository
                .GetAllTemplates()
                .Where(template => template.Branch_branchId == branchId)
                .ToList();

            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository
                .GetAllTemplateHasDays()
                .Where(thd => templates.Any(template => template.Id == thd.Templates_id))
                .ToList();

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

            var htmlTable = tableBuilder.GenerateTable("Templates", headers, templateViewModel, "../Templates/Create", item =>
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
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

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
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            bool isSuccess = true;
            bool isNameEmpty = string.IsNullOrEmpty(name);
            bool isTemplateDuplicate = false;
            bool hasCustomerErrors = false;
            bool hasContainerErrors = false;

            if (isNameEmpty)
            {
                isSuccess = false;
            }
            else
            {
                var existingTemplate = await _templatesRepository.GetByNameAndBranchAsync(name, branchId);
                if (existingTemplate != null)
                {
                    isSuccess = false;
                    isTemplateDuplicate = true;
                }
            }

            foreach (var kvp in customerData)
            {
                if (kvp.Value <= 0)
                {
                    isSuccess = false;
                    hasCustomerErrors = true;
                }
            }

            foreach (var kvp in containerData)
            {
                if (kvp.Value <= 0)
                {
                    isSuccess = false;
                    hasContainerErrors = true;
                }
            }

            if (!isSuccess)
            {
                var errorMessages = new List<string>();

                if (isNameEmpty)
                {
                    errorMessages.Add("Please provide a name for the template.");
                }

                if (isTemplateDuplicate)
                {
                    errorMessages.Add("A template with this name already exists.");
                }

                if (hasCustomerErrors && hasContainerErrors)
                {
                    errorMessages.Add("All customers and container amounts must be greater than zero.");
                }
                else
                {
                    if (hasCustomerErrors)
                    {
                        errorMessages.Add("Customer amounts must be greater than zero for all days.");
                    }

                    if (hasContainerErrors)
                    {
                        errorMessages.Add("Container amounts must be greater than zero for all days.");
                    }
                }

                TempData["ToastMessage"] = string.Join(" ", errorMessages);
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                var model = new TemplateCreateViewModel
                {
                    Name = name,
                    Days = customerData.Select(kvp => new DayData
                    {
                        DayName = kvp.Key,
                        CustomerAmount = kvp.Value,
                        ContainerAmount = containerData.ContainsKey(kvp.Key) ? containerData[kvp.Key] : 0
                    }).ToList()
                };

                return View(model);
            }

            var template = new Template
            {
                Name = name,
                Branch_branchId = branchId,
                TemplateHasDays = customerData.Select(kvp => new TemplateHasDays
                {
                    Days_name = kvp.Key,
                    CustomerAmount = kvp.Value,
                    ContainerAmount = containerData[kvp.Key]
                }).ToList()
            };

            await _templatesRepository.Add(template);
            await _templatesRepository.SaveChangesAsync();

            TempData["ToastMessage"] = "Template created successfully!";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "templateToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int templateId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            if (_templatesRepository.GetByIdAndBranch(templateId, branchId) == null)
            {
                TempData["ToastMessage"] = "No template found!";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
                return RedirectToAction(nameof(Index));
            }

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
        public async Task<IActionResult> Update(int templateId, string name, Dictionary<string, int> customerData, Dictionary<string, int> containerData)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            int branchId = user.ManagerOfBranchId.Value;

            bool isSuccess = true;
            bool isNameEmpty = string.IsNullOrEmpty(name);
            bool isTemplateDuplicate = false;
            bool hasCustomerErrors = false;
            bool hasContainerErrors = false;

            if (isNameEmpty)
            {
                isSuccess = false;
            }
            else
            {
                var existingTemplate = await _templatesRepository.GetByNameAndBranchAndIdAsync(name, branchId, templateId);
                if (existingTemplate != null)
                {
                    isSuccess = false;
                    isTemplateDuplicate = true;
                }
            }

            foreach (var kvp in customerData)
            {
                if (kvp.Value <= 0)
                {
                    isSuccess = false;
                    hasCustomerErrors = true;
                }
            }

            foreach (var kvp in containerData)
            {
                if (kvp.Value <= 0)
                {
                    isSuccess = false;
                    hasContainerErrors = true;
                }
            }

            if (!isSuccess)
            {
                var errorMessages = new List<string>();

                if (isNameEmpty)
                {
                    errorMessages.Add("Please provide a name for the template.");
                }

                if (isTemplateDuplicate)
                {
                    errorMessages.Add("A template with this name already exists.");
                }

                if (hasCustomerErrors && hasContainerErrors)
                {
                    errorMessages.Add("All customers and container amounts must be greater than zero.");
                }
                else
                {
                    if (hasCustomerErrors)
                    {
                        errorMessages.Add("Customer amounts must be greater than zero for all days.");
                    }

                    if (hasContainerErrors)
                    {
                        errorMessages.Add("Container amounts must be greater than zero for all days.");
                    }
                }

                TempData["ToastMessage"] = string.Join(" ", errorMessages);
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                var templateViewModel = new TemplatesViewModel
                {
                    TemplateId = templateId,
                    Name = name,
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

            var template = _templatesRepository.GetByIdAndBranch(templateId, branchId);

            if (template == null)
            {
                var templateViewModel = new TemplatesViewModel
                {
                    TemplateId = templateId,
                    Name = name,
                    HasDays = customerData.Select((customer, index) => new TemplateHasDaysViewModel
                    {
                        DayName = customer.Key,
                        CustomerAmount = customer.Value,
                        ContainerAmount = containerData.ContainsKey(customer.Key) ? containerData[customer.Key] : 0
                    }).ToList()
                };

                TempData["ToastMessage"] = "The template doesn't exist";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                ViewBag.Template = templateViewModel;

                return RedirectToAction(nameof(Index));
            }

            template.Name = name;

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

            TempData["ToastMessage"] = "Template updated successfully!";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "templateToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int templateId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var template = await _templatesRepository.GetByIdAsync(templateId);

            if (template == null)
            {
                TempData["ToastMessage"] = "The template doesn't exist";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
                return RedirectToAction(nameof(Index));
            }

            _templatesHasDaysRepository.DeleteByTemplateId(templateId);

            await _templatesRepository.DeleteAsync(template);

            TempData["ToastMessage"] = "Template deleted successfully!";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "templateToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction(nameof(Index));
        }
    }
}
