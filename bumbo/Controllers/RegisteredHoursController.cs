﻿using ceTe.DynamicPDF;
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

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            AddText(page, registeredHours, employees);

            document.Draw(@"Views/PDF/EmployeesHoursOverview.pdf");
        }

        private void AddText(Page page, List<RegisteredHours> registeredHours, List<Employee> employees)
        {
            int width = 500;
            int height = 100;
            int fontSize = 18;
            int x = 0;
            int multiplier = 40;

            int y = 0;
            foreach (RegisteredHours hour in registeredHours)
            {
                Employee emp = GetEmployeeForHour(hour, employees);
                if (emp == null) 
                { 
                    return; 
                }
                string text = $"{emp.FirstName} {emp.MiddleName} {emp.LastName}: {hour.StartTime} - {hour.EndTime}";
                Label label = new Label(text, x, y * multiplier, width, height, Font.Helvetica, fontSize, TextAlign.Center);
                page.Elements.Add(label);

                y++;
            }
        }

        private Employee GetEmployeeForHour(RegisteredHours hour, List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                if (employee.Id == hour.EmployeeId)
                {
                    return employee;
                }
            }
            return null;
        }
    }
}
