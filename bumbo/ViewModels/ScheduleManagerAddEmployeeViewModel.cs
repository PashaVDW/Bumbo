namespace bumbo.ViewModels
{
    public class ScheduleManagerAddEmployeeViewModel
    {
        public DateTime Date { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeOnly EmployeeAvailableStartTime { get; set; }
        public TimeOnly EmployeeAvailableEndTime { get; set; }
        public DayScheduleAddEmployeeViewModel DaySchedule { get; set; }
    }

    public class DayScheduleAddEmployeeViewModel
    {
        public DateTime Date { get; set; }
        public List<DepartmentScheduleAddEmployeeViewModel> Departments { get; set; } = new List<DepartmentScheduleAddEmployeeViewModel>();
    }

    public class DepartmentScheduleAddEmployeeViewModel
    {
        public string DepartmentName { get; set; }
        public List<EmployeeScheduleAddEmployeeViewModel> Employees { get; set; } = new List<EmployeeScheduleAddEmployeeViewModel>();
        public double TotalHours { get; set; }
        public double HoursNeeded { get; set; }
    }

    public class EmployeeScheduleAddEmployeeViewModel
    {
        public string? EmployeeId { get; set; } // Nullable for gaps
        public string EmployeeName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsSick { get; set; }
        public bool IsGap { get; set; } = false;
    }
}
