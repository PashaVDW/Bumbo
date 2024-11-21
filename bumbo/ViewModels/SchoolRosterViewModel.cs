namespace bumbo.ViewModels
{
    public class SchoolRosterViewModel
    {
        public List<SchoolRosterDayOverview> Days { get; set; } = new List<SchoolRosterDayOverview>();
        public int Year { get; set; }
        public string Month { get; set; }
        public int Week { get; set; }
        public bool Edit { get; set; } = false;
    }

    public class SchoolRosterDayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
