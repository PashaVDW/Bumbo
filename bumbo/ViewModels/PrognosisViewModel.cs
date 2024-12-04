namespace bumbo.ViewModels
{
    public class PrognosisViewModel
    {
        public string PrognosisId { get; set; }
        public int currentYear { get; set; }
        public int currentWeek { get; set; }
        public int Year { get; set; }
        public int WeekNr { get; set; }
        public List<PrognosisDay> Days { get; set; }
    }
}