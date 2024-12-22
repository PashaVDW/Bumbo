using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using CsvHelper;
using CsvHelper.Configuration;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
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

        [HttpGet("RegisteredHoursPDF")]
        public IActionResult RegisteredHoursPDF()
        {
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Views", "PDF", "EmployeesHoursOverview.pdf");
            var contentType = "application/pdf";

            return PhysicalFile(filePath, contentType);
        }

        public IActionResult HoursOverview()
        {
            List<RegisteredHours> registeredHours = new List<RegisteredHours>();
            List<Employee> employees = _employeeRepository.GetAllEmployees();
            foreach (Employee emp in employees)
            {
                foreach (RegisteredHours hour in _registeredHoursRepository.GetRegisteredHoursFromEmployee(emp.Id))
                {
                    registeredHours.Add(hour);
                }
            }

            DrawPDF(registeredHours, employees);
            ViewData["HideLayoutElements"] = true;

            return View();
        }

        private void DrawPDF(List<RegisteredHours> registeredHours, List<Employee> employees)
        {
            Document document = new Document();

            Page page = new Page(PageSize.A4, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            AddText(page, registeredHours, employees, document);

            document.Draw(@"Views/PDF/EmployeesHoursOverview.pdf");
        }

        private void AddText(Page page, List<RegisteredHours> registeredHours, List<Employee> employees, Document document)
        {
            int pageHeight = 700;

            int width = 500;
            int height = 100;
            int fontSize = 18;
            int x = 0;
            int multiplier = 40;

            int y = 0;
            int counter = 0;
            foreach (Employee employee in employees) 
            {
                string text = $"{employee.FirstName} {employee.LastName} ({employee.Email}): ";
                Label employeeName = new Label(text, x, y * multiplier, width, height, Font.Helvetica, fontSize, TextAlign.Left);
                page.Elements.Add(employeeName);
                counter = 0;
                foreach (RegisteredHours hour in registeredHours)
                {
                    text = SetLabelText(text, hour, counter, employee);

                    counter++;
                }
                if (!text.Equals($"{employee.FirstName} {employee.LastName} ({employee.Email}): "))
                {
                    y++;
                    if (y * multiplier >= pageHeight)
                    {
                        page = NextPage(page, document);
                        y = 0;
                    }

                    Label hoursText = new Label(text, x, y * multiplier, width, height, Font.Helvetica, fontSize, TextAlign.Left);
                    page.Elements.Add(hoursText);
                    y++;
                }
                y++;
                if (y * multiplier >= pageHeight)
                {
                    page = NextPage(page, document);
                    y = 0;
                }
            }
        }
        private string SetLabelText(string text, RegisteredHours hour, int counter, Employee employee)
        {
            if (employee.Id == hour.EmployeeId &&
                        !(hour.EndTime.Minute - hour.StartTime.Minute == 0 && hour.EndTime.Hour - hour.StartTime.Hour == 0))
            {
                if (counter == 0)
                {
                    text = $"";
                }
                if (hour.EndTime.Hour - hour.StartTime.Hour == 0)
                {
                    text += $" {hour.EndTime.Minute - hour.StartTime.Minute} minuten";
                }
                else
                {
                    text += $" {hour.EndTime.Hour - hour.StartTime.Hour} uur";
                }
            }
            return text;
        }

        private Page NextPage(Page page, Document document)
        {
            Page nextPage = new Page(PageSize.A4, PageOrientation.Portrait, 54.0f);
            page = nextPage;
            document.Pages.Add(nextPage);

            return page;
        }
    }
}
