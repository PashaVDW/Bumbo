namespace bumbo.ViewModels
{
    public class ScheduleManagerEditViewModel
    {
        public string Date { get; set; }
        public string titleDate { get; set; }
        public List<DepartmentScheduleEditViewModel> Departments { get; set; } = new List<DepartmentScheduleEditViewModel>();
    }

    public class DepartmentScheduleEditViewModel
    {
        public string DepartmentName { get; set; }
        public List<EmployeeScheduleEditViewModel> Employees { get; set; } = new List<EmployeeScheduleEditViewModel>();
        public double TotalHours { get; set; }
        public double HoursNeeded { get; set; }
    }

    public class EmployeeScheduleEditViewModel
    {
        public string EmployeeId { get; set; } 
        public string EmployeeName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DepartmentName { get; set; }

        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
