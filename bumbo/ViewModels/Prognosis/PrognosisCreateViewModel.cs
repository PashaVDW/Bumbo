using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class PrognosisCreateViewModel
    {
        public List<Days> Days { get; set; }
        public List<int> CustomerAmount { get; set; }
        public List<int> PackagesAmount { get; set; }
        public int WeekNr { get; set; }
        public int Year { get; set; }
    }
}
