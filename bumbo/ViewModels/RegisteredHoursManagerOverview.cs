namespace bumbo.ViewModels
{
    public class RegisteredHoursManagerOverview
    {
        public List<EmployeeRowViewModel> Employees { get; set; }
    }

    public class EmployeeRowViewModel
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public double TotalScheduledHours { get; set; }
        public double TotalWorkedHours { get; set; }
        public double Difference { get; set; }
        public double BonusHours { get; set; }
        public List<RegisteredHoursDetailViewModel> RegisteredHours { get; set; }
    }

    public class RegisteredHoursDetailViewModel
    {
        public DateTime Date { get; set; }
        public TimeSpan ScheduledStartTime { get; set; }
        public TimeSpan ScheduledEndTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Difference { get; set; }
    }
}
