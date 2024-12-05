using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class InputCalculateViewModel
    {
        public string prognosisId {  get; set; }
        public int Year { get; set; }
        public int WeekNr { get; set; }
        public List<int> CustomerAmount { get; set; }
        public List<int> PackagesAmount { get; set; }
    }
}
