namespace bumbo.ViewModels
{
    public class AvailabilityWeekView
    {
        public List<DayOverview> Days { get; set; } = new List<DayOverview>();
        public int Year { get; set; }
        public string Month { get; set; }
        public int Week { get; set; }
    }

    public class DayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
