namespace bumbo.ViewModels
{
    public class EmployeeDayScheduleViewModel
    {
        public DateOnly Date { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; }
    }
}
