namespace bumbo.ViewModels
{
    public class DayScheduleViewModel
    {
        public DateOnly Date { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; }
    }
}
