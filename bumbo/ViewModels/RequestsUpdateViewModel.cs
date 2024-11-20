using DataLayer.Models;

namespace bumbo.ViewModels
{
    public class RequestsUpdateViewModel
    {
        public Request Request { get; set; }
        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public Department Department { get; set; }
    }
}
