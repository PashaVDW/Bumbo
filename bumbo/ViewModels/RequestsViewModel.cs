using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class RequestsViewModel
    {
        public List<Request> IncomingRequests { get; set; }
        public List<Request> OutgoingRequests { get; set; }

    }
}
