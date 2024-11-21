namespace bumbo.ViewModels
{
    public class ScheduleManagerViewModel
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
    }
}
