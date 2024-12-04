using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class CalculateViewmodel
    {
        public string PrognosisId { get; set; }
        public Dictionary<Days, int> CassiereHours { get; set; }
        public Dictionary<Days, int> VersWorkersHours { get; set; }
        public Dictionary<Days, int> StockingHours { get; set; }
        public Dictionary<Days, int> CassieresNeeded { get; set; }
        public Dictionary<Days, int> WorkersNeeded { get; set; }
    }
}
