using bumbo.ViewModels;
using DataLayer.Interfaces;
using bumbo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bumbo.Controllers
{
    public class RegisteredHoursManagerController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public RegisteredHoursManagerController(UserManager<Employee> userManager, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Haal de ingelogde gebruiker op
            Employee user = await _userManager.GetUserAsync(User);

            // Controleer of de gebruiker is ingelogd en rechten heeft
            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // Haal alle medewerkers van de branch op
            List<Employee> employees = await _employeeRepository.GetEmployeesOfBranch(user.ManagerOfBranchId);

            List<EmployeeRowViewModel> employeeRows = employees.Select(employee => new EmployeeRowViewModel
            {
                EmployeeId = employee.UserName, // Unieke identificatie van de medewerker
                FullName = $"{employee.FirstName} {employee.MiddleName} {employee.LastName}",
                TotalScheduledHours = employee.RegisteredHours?.Sum(hours => (hours.EndTime - hours.StartTime).TotalHours) ?? 0,
                TotalWorkedHours = employee.RegisteredHours?.Sum(hours => (hours.EndTime - hours.StartTime).TotalHours) ?? 0,
                Difference = employee.RegisteredHours?.Sum(hours => (hours.EndTime - hours.StartTime).TotalHours) ?? 0, // Placeholder
                BonusHours = employee.RegisteredHours?.Where(hours => (hours.EndTime - hours.StartTime).TotalHours > 8)
                .Sum(hours => (hours.EndTime - hours.StartTime).TotalHours - 8) ?? 0,
                RegisteredHours = employee.RegisteredHours?.Select(hours => new RegisteredHoursDetailViewModel
                {
                    Date = hours.StartTime.Date,
                    ScheduledStartTime = hours.StartTime.TimeOfDay,
                    ScheduledEndTime = hours.EndTime.TimeOfDay,
                    StartTime = hours.StartTime.TimeOfDay,
                    EndTime = hours.EndTime.TimeOfDay,
                    Difference = (hours.EndTime - hours.StartTime).TotalHours
                }).ToList() ?? new List<RegisteredHoursDetailViewModel>()
            }).ToList();


            RegisteredHoursManagerOverview model = new RegisteredHoursManagerOverview
            {
                Employees = employeeRows
            };

            return View(model);

        }
    }
}
