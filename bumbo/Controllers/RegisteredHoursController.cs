using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicHoliday;


namespace bumbo.Controllers
{
    public class RegisteredHoursController : Controller
    {
        private readonly IRegisteredHoursRepository _registeredHoursRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILabourRulesRepository _labourRulesRepository;

        private readonly UserManager<Employee> _userManager;

        public RegisteredHoursController(IRegisteredHoursRepository registeredHoursRepository, IEmployeeRepository employeeRepository, 
            UserManager<Employee> userManager, ILabourRulesRepository labourRulesRepository)
        {
            _registeredHoursRepository = registeredHoursRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _labourRulesRepository = labourRulesRepository;

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
            List<Employee> employees = _employeeRepository.GetAllEmployees();
            List<RegisteredHours> registeredHours = FillRegisteredHoursList(employees);

            DrawPDF(registeredHours, employees);
            ViewData["HideLayoutElements"] = true;

            return View();
        }

        private List<RegisteredHours> FillRegisteredHoursList(List<Employee> employees)
        {
            List<RegisteredHours> registeredHours = new List<RegisteredHours>();
            foreach (Employee emp in employees)
            {
                foreach (RegisteredHours hour in _registeredHoursRepository.GetRegisteredHoursFromEmployee(emp.Id))
                {
                    registeredHours.Add(hour);
                }
            }
            return registeredHours;
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
                    int amountOfMinutes = hour.EndTime.Minute - hour.StartTime.Minute;
                    amountOfMinutes = GoThroughLabourRules(hour, amountOfMinutes/60, employee);
                    text = GetToManyHoursWorked(hour, amountOfMinutes / 60, text, employee);
                    text += $"{hour.EndTime.Minute - hour.StartTime.Minute} minuten | ";
                }
                else
                {
                    int amountOfHours = hour.EndTime.Hour - hour.StartTime.Hour;
                    amountOfHours = GoThroughLabourRules(hour, amountOfHours, employee);
                    text = GetToManyHoursWorked(hour, amountOfHours, text, employee);
                    text += $"{amountOfHours} uur | ";
                }
            }
            return text;
        }

        // Toeslagen
        private int GoThroughLabourRules(RegisteredHours hour, int amountOfHours, Employee employee)
        {
            foreach (Schedule schedule in employee.Schedules.Where(s => s.Date.Month == hour.EndTime.Month))
            {
                if (schedule.Date == DateOnly.FromDateTime(hour.EndTime) && schedule.IsSick)
                {
                    amountOfHours *= (int)0.7;
                    return amountOfHours;
                }
            }

            if ((TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.MinValue
                && TimeOnly.FromTimeSpan(hour.EndTime.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(6, 0, 0)))
                && (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(21, 0, 0))
                && TimeOnly.FromTimeSpan(hour.EndTime.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(24, 0, 0)))
                && (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(18, 0, 0))
                && TimeOnly.FromTimeSpan(hour.EndTime.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(24, 0, 0)) 
                && hour.StartTime.DayOfWeek == DayOfWeek.Saturday && hour.EndTime.DayOfWeek == DayOfWeek.Saturday))
            {
                amountOfHours = (int)(amountOfHours * 1.5);
            }
            if (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(20, 0, 0))
               && TimeOnly.FromTimeSpan(hour.EndTime.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(21, 0, 0)))
            {
                amountOfHours = (int)(amountOfHours * 1.333);
            }
            if (hour.StartTime.DayOfWeek == DayOfWeek.Sunday && hour.EndTime.DayOfWeek == DayOfWeek.Sunday)
            {
                amountOfHours *= 2;
            }

            DutchPublicHoliday dutchHolidays = new DutchPublicHoliday();
            List<DateTime> holidays = dutchHolidays.PublicHolidays(2025).ToList();
            foreach (DateTime holiday in holidays)
            {
                if (hour.StartTime.Equals(holiday) && hour.EndTime.Equals(holiday))
                {
                    amountOfHours *= 2;
                }
            }

            return amountOfHours;
        }

        // Regels algemeen
        private string GetToManyHoursWorked(RegisteredHours hour, int amountOfHours, string text, Employee employee)
        {
            string activeCountry = "Netherlands";
            List<LabourRules> labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(activeCountry).ToList();
            if (!labourRules.Any())
            {
                labourRules = _labourRulesRepository.CreateDefaultLabourRulesForCountry(activeCountry);
            }

            foreach (LabourRules rule in labourRules)
            {
                if (activeCountry.Equals("Netherlands"))
                {
                    if (rule.AgeGroup.Equals("<16"))
                    {
                        if (Employee16OrLessHasWorkedToMuch(employee, hour))
                        {
                            text += "teveel gewerkt";
                            return text;
                        }
                    }
                    else if (rule.AgeGroup.Equals("16-17"))
                    {
                        if (Employee16Or17HasWorkedToMuch(employee, hour))
                        {
                            text += "teveel gewerkt";
                            return text;
                        }
                    }
                }
            }

            if (amountOfHours > 12)
            {
                text += $"({amountOfHours - 12} Teveel) ";
            }

            List<RegisteredHours> hoursThisWeek = _registeredHoursRepository.GetRegisteredHoursInWeekFromEmployee(employee.Id, hour.StartTime.GetWeekOfYear());
            int count = 0;

            foreach (RegisteredHours hourThisWeek in hoursThisWeek)
            {
                count += hourThisWeek.EndTime.Hour - hourThisWeek.StartTime.Hour;
            }

            if (count > 60)
            {
                text += $"({count - 60} teveel in week) ";
            } 

            return text;
        }

        // < 16 jaar
        private bool Employee16OrLessHasWorkedToMuch(Employee employee, RegisteredHours registeredHour)
        {
            int hoursWorkedInWeek = 0;
            List<RegisteredHours> registeredHours = _registeredHoursRepository.GetRegisteredHoursInWeekFromEmployee(employee.Id, registeredHour.StartTime.GetWeekOfYear());
            List<DayOfWeek> daysWorked = new List<DayOfWeek>();
            foreach (RegisteredHours hour in registeredHours)
            {
                hoursWorkedInWeek += hour.EndTime.Hour - hour.StartTime.Hour;
                if (!daysWorked.Contains(hour.EndTime.DayOfWeek))
                {
                    daysWorked.Add(hour.EndTime.DayOfWeek);
                }
            }

            int hoursWorked = registeredHour.EndTime.Hour - registeredHour.StartTime.Hour;
            int hoursOnSchoolInWeek = 0;
            foreach (SchoolSchedule schoolSchedule in employee.SchoolSchedules.Where(s => s.Date.Month == registeredHour.EndTime.Month))
            {
                if (schoolSchedule.Date == DateOnly.FromDateTime(registeredHour.EndTime))
                {
                    hoursWorked += schoolSchedule.EndTime.Hour - schoolSchedule.StartTime.Hour;
                }
                hoursOnSchoolInWeek += schoolSchedule.EndTime.Hour - schoolSchedule.StartTime.Hour;
            }

            return hoursWorkedInWeek > 40 || hoursWorked > 8 || registeredHour.EndTime.Hour > 19
                || hoursWorkedInWeek - hoursOnSchoolInWeek > 12 || daysWorked.Count > 5;
        }

        // 16-17 jaar
        private bool Employee16Or17HasWorkedToMuch(Employee employee, RegisteredHours registeredHour)
        {
            int hoursWorkedInWeek = 0;
            List<RegisteredHours> registeredHours = _registeredHoursRepository.GetRegisteredHoursFromEmployee(employee.Id);
            foreach (RegisteredHours hour in registeredHours)
            {
                hoursWorkedInWeek += hour.EndTime.Hour - hour.StartTime.Hour;
            }

            int hoursWorked = registeredHour.EndTime.Hour - registeredHour.StartTime.Hour;
            foreach (SchoolSchedule schoolSchedule in employee.SchoolSchedules)
            {
                if (schoolSchedule.Date == DateOnly.FromDateTime(registeredHour.EndTime))
                {
                    hoursWorked += schoolSchedule.EndTime.Hour - schoolSchedule.StartTime.Hour;
                }
            }
            
            return (hoursWorkedInWeek/4) > 40 || hoursWorked > 9;
        }

        private Page NextPage(Page page, Document document)
        {
            Page nextPage = new Page(PageSize.A4, PageOrientation.Portrait, 54.0f);
            page = nextPage;
            document.Pages.Add(nextPage);

            return page;
        }

        private List<DayOfWeek> GetDaysOfWeek()
        {
            return new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, 
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday }; ;
        }
    }
}
