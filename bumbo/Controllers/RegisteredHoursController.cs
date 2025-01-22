using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicHoliday;
using System.Data;
using System.Globalization;


namespace bumbo.Controllers
{
    public class RegisteredHoursController : Controller
    {
        private readonly IRegisteredHoursRepository _registeredHoursRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILabourRulesRepository _labourRulesRepository;
        private readonly IScheduleRepository _scheduleRulesRepository;
        private readonly ISchoolScheduleRepository _schoolScheduleRulesRepository;

        private readonly UserManager<Employee> _userManager;

        private List<int> _weeksEmployeeOverworkedIn;

        public RegisteredHoursController(IRegisteredHoursRepository registeredHoursRepository, IEmployeeRepository employeeRepository, 
            UserManager<Employee> userManager, ILabourRulesRepository labourRulesRepository, IScheduleRepository scheduleRulesRepository
            , ISchoolScheduleRepository schoolScheduleRulesRepository)
        {
            _registeredHoursRepository = registeredHoursRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _labourRulesRepository = labourRulesRepository;
            _scheduleRulesRepository = scheduleRulesRepository;
            _schoolScheduleRulesRepository = schoolScheduleRulesRepository;

            _weeksEmployeeOverworkedIn = new List<int>();
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

        public async Task<IActionResult> HoursOverview(string activeCountry)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.ManagerOfBranch != null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                TempData["ActiveCountry"] = activeCountry;

                List<Employee> employees = _employeeRepository.GetAllEmployees();
                List<RegisteredHours> registeredHours = FillRegisteredHoursList(employees);

                DrawPDF(registeredHours, employees);
                ViewData["HideLayoutElements"] = true;

                return View();
            } catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
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
                if (_registeredHoursRepository.GetRegisteredHoursFromEmployee(employee.Id).Count == 0)
                {
                    continue;
                }
                string text = $"{employee.FirstName} {employee.MiddleName} {employee.LastName} ({employee.Email}): ";
                Label employeeName = new Label(text, x, y * multiplier, width, height, Font.Helvetica, fontSize, TextAlign.Left);
                page.Elements.Add(employeeName);
                counter = 0;
                _weeksEmployeeOverworkedIn = new List<int>();

                foreach (RegisteredHours hour in registeredHours)
                {
                    text = SetLabelText(text, hour, counter, employee);

                    counter++;
                }
                if (!text.Equals(""))
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
                    if (_weeksEmployeeOverworkedIn.Count > 0)
                    {
                        string weeks = WriteOverworkedHours();
                        Label weeksLabel = new Label(weeks, x, y * multiplier, width, height, Font.Helvetica, fontSize, TextAlign.Left);
                        page.Elements.Add(weeksLabel);
                        y++;
                    }
                }
                y++;
                if (y * multiplier >= pageHeight)
                {
                    page = NextPage(page, document);
                    y = 0;
                }
            }
        }

        private string WriteOverworkedHours()
        {
            string weeks = "Weken teveel gewerkt: ";
            int i = 0;
            foreach (int week in _weeksEmployeeOverworkedIn)
            {
                if (!weeks.Contains(week.ToString()))
                {
                    if (i == 0)
                    {
                        weeks += $"{week}";
                    }
                    else
                    {
                        weeks += $", {week}";
                    }
                    i++;
                }
            }
            return weeks;
        }

        private Page NextPage(Page page, Document document)
        {
            Page nextPage = new Page(PageSize.A4, PageOrientation.Portrait, 54.0f);
            page = nextPage;
            document.Pages.Add(nextPage);

            return page;
        }

        private string SetLabelText(string text, RegisteredHours hour, int counter, Employee employee)
        {
            if (counter == 0)
            {
                text = $"";
            }
            if (employee.Id == hour.EmployeeId &&
                        !(hour.EndTime.Value.Minute - hour.StartTime.Minute == 0 && hour.EndTime.Value.Hour - hour.StartTime.Hour == 0))
            {
                int amountOfHours = GetHours(hour);
                if (amountOfHours == 0)
                {
                    double amountOfMinutes = (hour.EndTime.Value.Minute - hour.StartTime.Minute)/60;
                    if(amountOfMinutes >= 0.5)
                        text += $"1 uur | ";
                }
                else
                {
                    amountOfHours = GoThroughLabourRules(hour, amountOfHours, employee);
                    text = GetToManyHoursWorked(hour, amountOfHours, text, employee);
                    text += $"{amountOfHours} uur | ";
                }
            }
            return text;
        }

        // -------------------- Labour Rules --------------------

        // Toeslagen
        private int GoThroughLabourRules(RegisteredHours hour, int amountOfHours, Employee employee)
        {
            employee.Schedules = _scheduleRulesRepository.GetSchedulesForEmployee(employee.Id);
            employee.SchoolSchedules = _schoolScheduleRulesRepository.GetEmployeeSchoolSchedule(employee.Id);

            foreach (Schedule schedule in employee.Schedules.Where(s => s.Date.Month == hour.EndTime.Value.Month))
            {
                if (schedule.Date == DateOnly.FromDateTime(hour.EndTime.Value) && schedule.IsSick)
                {
                    amountOfHours *= (int)0.7;
                    return amountOfHours;
                }
            }

            if ((TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.MinValue
                && TimeOnly.FromTimeSpan(hour.EndTime.Value.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(6, 0, 0)))
                || (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(21, 0, 0))
                && TimeOnly.FromTimeSpan(hour.EndTime.Value.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(24, 0, 0)))
                || (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(18, 0, 0))
                && TimeOnly.FromTimeSpan(hour.EndTime.Value.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(24, 0, 0)) 
                && hour.StartTime.DayOfWeek == DayOfWeek.Saturday && hour.EndTime.Value.DayOfWeek == DayOfWeek.Saturday))
            {
                amountOfHours = (int)(amountOfHours * 1.5);
            }
            if (TimeOnly.FromTimeSpan(hour.StartTime.TimeOfDay) >= TimeOnly.FromTimeSpan(new TimeSpan(20, 0, 0))
               && TimeOnly.FromTimeSpan(hour.EndTime.Value.TimeOfDay) <= TimeOnly.FromTimeSpan(new TimeSpan(21, 0, 0)))
            {
                amountOfHours = (int)(amountOfHours * 1.333);
            }

            if (hour.StartTime.DayOfWeek == DayOfWeek.Sunday && hour.EndTime.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                amountOfHours *= 2;
            } else
            {
                DutchPublicHoliday dutchHolidays = new DutchPublicHoliday();
                List<DateTime> holidays = dutchHolidays.PublicHolidays(2025).ToList();
                foreach (DateTime holiday in holidays)
                {
                    if (hour.StartTime.DayOfYear == holiday.DayOfYear && hour.EndTime.Value.DayOfYear == holiday.DayOfYear)
                    {
                        amountOfHours *= 2;
                    }
                }
            }

            return amountOfHours;
        }

        // Regels
        private string GetToManyHoursWorked(RegisteredHours hour, int amountOfHours, string text, Employee employee)
        {
            string activeCountry = TempData["ActiveCountry"].ToString();
            List<LabourRules> labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(activeCountry).ToList();
            if (!labourRules.Any())
            {
                labourRules = _labourRulesRepository.CreateDefaultLabourRulesForCountry(activeCountry);
            }

            foreach (LabourRules rule in labourRules)
            {
                if (IsMinor(rule, employee))
                {
                    if (EmployeeMinorHasWorkedToMuch(employee, hour, rule))
                    {
                        text += "teveel gewerkt ";
                        return text;
                    }
                }
                else if (rule.AgeGroup.Equals(">17"))
                {
                    text = CheckIfEmployeeHasWorkedToMuch(text, rule, amountOfHours,employee, hour);
                }
            }

            return text;
        }

        private bool IsMinor(LabourRules rule, Employee employee)
        {
            return (rule.AgeGroup.Equals("<16") && employee.BirthDate.Year > DateTime.Now.Year - 16) ||
                (rule.AgeGroup.Equals("16-17") && (employee.BirthDate.Year == DateTime.Now.Year - 16
                || employee.BirthDate.Year == DateTime.Now.Year - 17));
        }

        private string CheckIfEmployeeHasWorkedToMuch(string text, LabourRules rule, int amountOfHours, Employee employee, RegisteredHours hour)
        {
            int maxHoursPerDay = rule.MaxHoursPerDay;
            if (amountOfHours > maxHoursPerDay)
            {
                text += $"({amountOfHours - maxHoursPerDay} Teveel) ";
            }

            List<RegisteredHours> hoursThisWeek = _registeredHoursRepository.GetRegisteredHoursInWeekFromEmployee(employee.Id, hour.StartTime.GetWeekOfYear());
            int count = 0;

            foreach (RegisteredHours hourThisWeek in hoursThisWeek)
            {
                count += hourThisWeek.EndTime.Value.Hour - hourThisWeek.StartTime.Hour;
            }

            int maxHoursPerWeek = rule.MaxHoursPerWeek;
            if (count > maxHoursPerWeek)
            {
                _weeksEmployeeOverworkedIn.Add(ISOWeek.GetWeekOfYear(hour.EndTime.Value));
            }
            return text;
        }

        private bool EmployeeMinorHasWorkedToMuch(Employee employee, RegisteredHours registeredHour, LabourRules labourRule)
        {

            int hoursWorkedOnDay = GetHours(registeredHour);
            int hoursOnSchoolInWeek = 0;
            foreach (SchoolSchedule schoolSchedule in employee.SchoolSchedules.Where(s => s.Date.Month == registeredHour.EndTime.Value.Month))
            {
                if (schoolSchedule.Date == DateOnly.FromDateTime(registeredHour.EndTime.Value))
                {
                    hoursWorkedOnDay += GetHours(schoolSchedule);
                }
                hoursOnSchoolInWeek += GetHours(schoolSchedule);
            }

            if (labourRule.AgeGroup.Equals("<16"))
            {
                return CheckForLessThanSixteen(labourRule, employee, hoursWorkedOnDay, registeredHour, hoursOnSchoolInWeek);
            }
            if (labourRule.AgeGroup.Equals("16-17"))
            {
                return CheckForSixteenOrSeventeen(labourRule, employee, hoursWorkedOnDay);
            }

            return false;
        }

        private bool CheckForLessThanSixteen(LabourRules labourRule, Employee employee, int hoursWorkedOnDay, 
            RegisteredHours registeredHour, int hoursOnSchoolInWeek)
        {
            List<RegisteredHours> registeredHours = _registeredHoursRepository.GetRegisteredHoursInWeekFromEmployee(employee.Id, registeredHour.StartTime.GetWeekOfYear());
            List<DayOfWeek> daysWorked = new List<DayOfWeek>();

            int hoursWorkedInWeek = 0;
            foreach (RegisteredHours hour in registeredHours)
            {
                hoursWorkedInWeek += GetHours(hour);
                if (!daysWorked.Contains(hour.EndTime.Value.DayOfWeek))
                {
                    daysWorked.Add(hour.EndTime.Value.DayOfWeek);
                }
            }

            return hoursWorkedInWeek > labourRule.MaxHoursPerWeek
                || hoursWorkedOnDay > labourRule.MaxHoursPerDay
                || registeredHour.EndTime.Value.Hour > labourRule.MaxEndTime.Hours
                || hoursWorkedInWeek - hoursOnSchoolInWeek > labourRule.MaxHoursWithSchool
                || daysWorked.Count > labourRule.MaxWorkDaysPerWeek;
        }

        private bool CheckForSixteenOrSeventeen(LabourRules labourRule, Employee employee, int hoursWorkedOnDay)
        {
            int hoursWorkedInMonth = GetHoursWorkedInMonth(employee);
            return (hoursWorkedInMonth / 4) > labourRule.MaxHoursPerWeek
                || hoursWorkedOnDay > labourRule.MaxHoursPerDay;
        }

        private int GetHoursWorkedInMonth(Employee employee)
        {
            int hoursWorkedInMonth = 0;
            List<RegisteredHours> registeredHours = _registeredHoursRepository.GetRegisteredHoursFromEmployee(employee.Id);
            foreach (RegisteredHours hour in registeredHours)
            {
                hoursWorkedInMonth += GetHours(hour);
            }
            return hoursWorkedInMonth;
        }

        private int GetHours(RegisteredHours hour)
        {
            TimeOnly time = TimeOnly.FromTimeSpan(hour.EndTime.Value - hour.StartTime);
            double amountOfHours = (double) (time.Hour + (double)time.Minute/60);
            amountOfHours = Math.Round(amountOfHours, 0, MidpointRounding.AwayFromZero);

            return (int) amountOfHours;
        }
        private int GetHours(SchoolSchedule schoolSchedule)
        {
            TimeOnly time = TimeOnly.FromTimeSpan(schoolSchedule.EndTime - schoolSchedule.StartTime);
            double amountOfHours = (double)(time.Hour + (double)time.Minute / 60);
            amountOfHours = Math.Round(amountOfHours, 0, MidpointRounding.AwayFromZero);

            return (int)amountOfHours;
        }
    }
}
