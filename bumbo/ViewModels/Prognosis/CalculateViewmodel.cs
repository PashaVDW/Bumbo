using bumbo.Models;

public class CalculateViewmodel
{
    public string PrognosisId { get; set; }
    public List<Days> Days { get; set; }
    public List<double> CassiereHours { get; set; } // Should be double
    public List<double> VersWorkersHours { get; set; } // Should be double
    public List<double> StockingHours { get; set; } // Should be double
    public List<int> CassieresNeeded { get; set; }
    public List<int> VersWorkersNeeded { get; set; }
    public List<int> StockingWorkersNeeded { get; set; }
}
