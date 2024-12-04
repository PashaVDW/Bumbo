using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class InputCalculateViewModel
    {
        public int Year { get; set; }
        public int WeekNr { get; set; }
        public List<int> CustomerAmount { get; set; }
        public List<int> PackagesAmount { get; set; }
    }
}
