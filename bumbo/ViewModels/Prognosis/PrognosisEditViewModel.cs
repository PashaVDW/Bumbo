using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class PrognosisEditViewModel
    {
        public int PrognosisId { get; set; }
        public List<string> Days_name { get; set; }
        public List<int> CustomerAmount { get; set; }
        public List<int> PackagesAmount { get; set; }
        public int CurrentWeek { get; set; }
        public int CurrentYear { get; set; }
        public int WeekNr { get; set; }
        public int Year { get; set; }
    }
}
