using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using CsvHelper;
using CsvHelper.Configuration;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Globalization;

namespace bumbo.Controllers
{
    public class RegisteredHoursController : Controller
    {
        private readonly IRegisteredHoursRepository _registeredHoursRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;

        public RegisteredHoursController(IRegisteredHoursRepository registeredHoursRepository, IEmployeeRepository employeeRepository, UserManager<Employee> userManager)
        {
            _registeredHoursRepository = registeredHoursRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;

        }
        public IActionResult ButtonView()
        {
            return View();
        }

        [HttpGet("GetPDF")]
        public IActionResult GetPDF()
        {
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Views", "RegisteredHours", "EmployeesHoursOverview.pdf");
            var contentType = "application/pdf";

            return PhysicalFile(filePath, contentType);
        }

        public IActionResult HoursOverview()
        {
            List<RegisteredHours> registeredHours = new List<RegisteredHours>();
            foreach (Employee emp in _employeeRepository.GetAllEmployees())
            {
                foreach (RegisteredHours hour in _registeredHoursRepository.GetRegisteredHoursFromEmployee(emp.Id))
                {
                    registeredHours.Add(hour);
                }
            }

            DrawPDF(registeredHours);
            ViewData["HideLayoutElements"] = true;

            return View();
        }

        private void DrawPDF(List<RegisteredHours> registeredHours)
        {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            string labelText = $"{registeredHours}";
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            document.Draw(@"Views/RegisteredHours/EmployeesHoursOverview.pdf");
        }
    }
}
