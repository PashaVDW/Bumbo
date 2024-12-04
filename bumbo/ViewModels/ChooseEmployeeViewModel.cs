namespace bumbo.ViewModels
{
    public class ChooseEmployeeViewModel
    {
        public DateOnly ScheduleDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeViewModel> AvailableEmployees { get; set; }
    }

    public class EmployeeViewModel
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
