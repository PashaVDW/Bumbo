namespace bumbo.ViewModels
{
    public class WeekOverviewViewModel
    {
        public int Year { get; set; }
        public int WeekNr { get; set; }
        public List<DayOverviewViewModel> Days { get; set; }
    }
}
