using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class PrognosisDay
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public List<Prognosis_has_days_has_Department> DepartmentList { get; set; }
        public int TotalWorkersNeeded { get; set; }
        public int TotalHoursWorkNeeded { get; set; }
    }
}
