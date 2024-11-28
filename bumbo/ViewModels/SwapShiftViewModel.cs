using System;
using System.Collections.Generic;

namespace bumbo.ViewModels
{
    public class SwapShiftViewModel
    {
        public List<SwitchRequestViewModel> IncomingRequests { get; set; } = new List<SwitchRequestViewModel>();
        public List<SwitchRequestViewModel> OutgoingRequests { get; set; } = new List<SwitchRequestViewModel>();
    }

    public class SwitchRequestViewModel
    {
        public string RequesterName { get; set; }
        public string ReceiverName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
    }
}
