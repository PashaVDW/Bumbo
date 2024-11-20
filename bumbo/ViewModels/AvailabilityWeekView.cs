namespace bumbo.ViewModels
{
    public class AvailabilityWeekView
    {
        public List<DayOverview> Days { get; set; } = new List<DayOverview>();
    }

    public class DayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
