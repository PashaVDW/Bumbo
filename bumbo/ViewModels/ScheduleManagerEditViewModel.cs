namespace bumbo.ViewModels
{
    public class ScheduleManagerEditViewModel
    {
        public DateTime Date { get; set; }
        public List<DepartmentScheduleViewModel> Departments { get; set; } = new List<DepartmentScheduleViewModel>();
    }
}
