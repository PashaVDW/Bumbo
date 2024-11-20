namespace bumbo.ViewModels
{
    public class EmployeeSchedulesViewModel
    {
        public List<DayScheduleViewModel> Schedules { get; set; }
        public DateOnly Today { get; set; }
        public DateOnly StartOfWeek { get; set; }
        public int SelectedWeek { get; set; }
    }
}
