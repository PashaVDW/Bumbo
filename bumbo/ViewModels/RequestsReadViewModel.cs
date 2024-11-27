using bumbo.Models;
using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class RequestsReadViewModel
    {
        public BranchRequestsEmployee Request { get; set; }
        public Employee Employee { get; set; }
        public int RequestId { get; set; }
        public string DepartmentName { get; set; }
    }
}
