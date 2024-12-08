using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class CalculateViewmodel
    {
        public string PrognosisId { get; set; }
        public List<Days> Days { get; set; }
        public List<int> CassiereHours { get; set; }
        public List<int> VersWorkersHours { get; set; }
        public List<int> StockingHours { get; set; }
        public List<int> CassieresNeeded { get; set; }
        public List<int> VersWorkersNeeded { get; set; }
        public List<int> StockingWorkersNeeded { get; set; }
    }
}
