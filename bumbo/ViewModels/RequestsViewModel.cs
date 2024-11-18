using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class RequestsViewModel
    {
        public List<Request> Requests { get; set; }
        public string SelectedType { get; set; }
        public bool ShowFinishedRequests { get; set; }

    }
}
