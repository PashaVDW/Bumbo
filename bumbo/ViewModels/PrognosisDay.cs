using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class PrognosisDay
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public List<PrognosisHasDaysHasDepartment> DepartmentList { get; set; }
        public int TotalWorkersNeeded { get; set; }
        public int TotalHoursWorkNeeded { get; set; }
    }
}
