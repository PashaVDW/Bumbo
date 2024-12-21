using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class EmployeeRegisterHoursViewModel
    {
        public DateTime? ClockedInTime { get; set; }
        public DateTime Today { get; set; }
        public string DayName { get; set; }
        public Schedule FirstShift { get; set; }
        public List<Schedule> WeekSchedule { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public List<Schedule> RegisteredHoursPlanned { get; set; }
        public List<RegisteredHours> RegisteredHoursSchedule { get; set; }
        public List<RegisteredHours> ExtraRegisteredHoursSchedule { get; set; }
    }
}
