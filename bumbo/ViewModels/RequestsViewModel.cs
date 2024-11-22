using bumbo.Models;

namespace bumbo.ViewModels
{
    public class RequestsViewModel
    {
        public List<BranchRequestsEmployee> IncomingRequests { get; set; }
        public List<BranchRequestsEmployee> OutgoingRequests { get; set; }

    }
}
