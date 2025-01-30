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
                .GetAllTemplatesFromBranch(user.ManagerOfBranchId.Value)
                .Where(template => template.BranchBranchId == branchId)
                .ToList();

            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository
                .GetAllTemplateHasDays()
                .Where(thd => templates.Any(template => template.Id == thd.TemplatesId))
                .ToList();

            List<TemplatesViewModel> templateViewModel = templates.Select(template => new TemplatesViewModel
            {
                TemplateId = template.Id,
                Name = template.Name,
                HasDays = templateHasDays
                .Where(thd => thd.TemplatesId == template.Id)
                .Select(thd => new TemplateHasDaysViewModel
                {
                    DayName = thd.DaysName,
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
                    new DayData { DayName = "Maandag" },
                    new DayData { DayName = "Dinsdag" },
                    new DayData { DayName = "Woensdag" },
                    new DayData { DayName = "Donderdag" },
                    new DayData { DayName = "Vrijdag" },
                    new DayData { DayName = "Zaterdag" },
                    new DayData { DayName = "Zondag" }
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
                    errorMessages.Add("Voer een geldige naam in voor de template.");
                }

                if (isTemplateDuplicate)
                {
                    errorMessages.Add("Een template met deze naam bestaat al.");
                }

                if (hasCustomerErrors && hasContainerErrors)
                {
                    errorMessages.Add("Alle klant en coli gegevens moeten meer dan nul zijn.");
                }
                else
                {
                    if (hasCustomerErrors)
                    {
                        errorMessages.Add("Aantal klanten moeten voor alle dagen groter zijn dan nul.");
                    }

                    if (hasContainerErrors)
                    {
                        errorMessages.Add("Aantal coli moet voor alle dagen groter zijn dan nul.");
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
                BranchBranchId = branchId,
                TemplateHasDays = customerData.Select(kvp => new TemplateHasDays
                {
                    DaysName = kvp.Key,
                    CustomerAmount = kvp.Value,
                    ContainerAmount = containerData[kvp.Key]
                }).ToList()
            };

            await _templatesRepository.Add(template);
            await _templatesRepository.SaveChangesAsync();

            TempData["ToastMessage"] = "Template succesvol aangemaakt!";
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
                TempData["ToastMessage"] = "Geen template gevonden!";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
                return RedirectToAction(nameof(Index));
            }

            List<TemplateHasDays> templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays();
            List<Template> templates = _templatesRepository.GetAllTemplatesFromBranch(user.ManagerOfBranchId.Value);

            var dayOrder = new[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" };

            List<TemplatesViewModel> templateViewModel = templates.Select(template => new TemplatesViewModel
            {
                TemplateId = template.Id,
                Name = template.Name,
                HasDays = templateHasDays
            .Where(thd => thd.TemplatesId == template.Id)
            .Select(thd => new TemplateHasDaysViewModel
            {
                DayName = thd.DaysName,
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
                    errorMessages.Add("Geef een naam op voor de template.");
                }

                if (isTemplateDuplicate)
                {
                    errorMessages.Add("Een template met deze naam bestaat al.");
                }

                if (hasCustomerErrors && hasContainerErrors)
                {
                    errorMessages.Add("Alle klant en coli gegevens moeten meer dan nul zijn.");
                }
                else
                {
                    if (hasCustomerErrors)
                    {
                        errorMessages.Add("Aantal klanten moeten voor alle dagen groter zijn dan nul.");
                    }

                    if (hasContainerErrors)
                    {
                        errorMessages.Add("Aantal coli moet voor alle dagen groter zijn dan nul.");
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

                TempData["ToastMessage"] = "De template bestaat niet";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;

                ViewBag.Template = templateViewModel;

                return RedirectToAction(nameof(Index));
            }

            template.Name = name;

            var templateHasDays = _templatesHasDaysRepository.GetAllTemplateHasDays()
                .Where(thd => thd.TemplatesId == templateId)
                .ToList();

            foreach (var thd in templateHasDays)
            {
                if (customerData.TryGetValue(thd.DaysName, out int customerAmount))
                {
                    thd.CustomerAmount = customerAmount;
                }

                if (containerData.TryGetValue(thd.DaysName, out int containerAmount))
                {
                    thd.ContainerAmount = containerAmount;
                }

                _templatesHasDaysRepository.Update(thd);
            }

            _templatesRepository.Update(template);
            await _templatesRepository.SaveChangesAsync();
            await _templatesHasDaysRepository.SaveChangesAsync();

            TempData["ToastMessage"] = "Template succesvol bijgewerkt!";
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
                TempData["ToastMessage"] = "Dit template bestaat niet";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "templateToast";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
                return RedirectToAction(nameof(Index));
            }

            _templatesHasDaysRepository.DeleteByTemplateId(templateId);

            await _templatesRepository.DeleteAsync(template);

            TempData["ToastMessage"] = "Template succesvol verwijderd!";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "templateToast";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction(nameof(Index));
        }
    }
}
