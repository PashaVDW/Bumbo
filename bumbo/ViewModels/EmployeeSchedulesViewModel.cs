namespace bumbo.ViewModels
{
    public class EmployeeSchedulesViewModel
    {
        public List<EmployeeDayScheduleViewModel> Schedules { get; set; }
        public DateOnly Today { get; set; }
        public DateOnly StartOfWeek { get; set; }
        public int SelectedWeek { get; set; }
        public int Year { get; set; }
    }
}
