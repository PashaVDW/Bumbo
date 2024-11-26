namespace bumbo.ViewModels
{
    public class ScheduleManagerViewModel
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
        public List<DayScheduleViewModel> DaySchedules { get; set; } = new List<DayScheduleViewModel>();
    }

    public class DayScheduleViewModel
    {
        public DateTime Date { get; set; }
        public List<DepartmentScheduleViewModel> Departments { get; set; } = new List<DepartmentScheduleViewModel>();
    }

    public class DepartmentScheduleViewModel
    {
        public string DepartmentName { get; set; }
        public List<EmployeeScheduleViewModel> Employees { get; set; } = new List<EmployeeScheduleViewModel>();
        public double TotalHours { get; set; }
        public double HoursNeeded { get; set; }
    }

    public class EmployeeScheduleViewModel
    {
        public string? EmployeeId { get; set; } // Nullable for gaps
        public string EmployeeName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsSick { get; set; }
        public bool IsGap { get; set; } = false;
        public bool IsFinal { get; set; }
    }
}
