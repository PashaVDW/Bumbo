using bumbo.Models;
using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class RequestsUpdateViewModel
    {
        public BranchRequestsEmployee Request { get; set; }
        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public bool HasChosenEmployee { get; set; }
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
    }
}
