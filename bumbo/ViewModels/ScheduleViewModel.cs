namespace bumbo.ViewModels
{
    public class ScheduleViewModel
    {
        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DepartmentName { get; set; }
        public int isSick { get; set; }
    }
}
