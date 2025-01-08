namespace bumbo.ViewModels
{
    public class RegisteredHoursManagerOverview
    {
        public List<EmployeeRowViewModel> Employees { get; set; }
            = new List<EmployeeRowViewModel>();

        public int DisplayMonth { get; set; }
        public int DisplayYear { get; set; }
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
            = new List<RegisteredHoursDetailViewModel>();
    }

    public class RegisteredHoursDetailViewModel
    {
        public DateOnly Date { get; set; }

        public TimeOnly? ScheduledStartTime { get; set; }
        public TimeOnly? ScheduledEndTime { get; set; }
        public TimeOnly? WorkedStartTime { get; set; }
        public TimeOnly? WorkedEndTime { get; set; }

        public double ScheduledHoursDay { get; set; }
        public double WorkedHoursDay { get; set; }
        public double Difference { get; set; }
        public string Notes { get; set; }
        public double OverworkHours { get; internal set; }
        public double OverworkPay { get; internal set; }
        public double BonusHours { get; internal set; }
    }

}
