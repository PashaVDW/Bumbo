namespace bumbo.ViewModels
{
    public class SchoolScheduleViewModel
    {
        public List<SchoolScheduleDayOverview> Days { get; set; } = new List<SchoolScheduleDayOverview>();
        public int Year { get; set; }
        public string Month { get; set; }
        public int Week { get; set; }
        public bool Edit { get; set; } = false;
    }

    public class SchoolScheduleDayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
