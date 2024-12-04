using System.Collections.Generic;

namespace bumbo.ViewModels
{
    public class CreateSwapShiftViewModel
    {
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
        public List<SwapShiftScheduleViewModel> Schedules { get; set; }
    }

    public class SwapShiftScheduleViewModel
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DepartmentName { get; set; }
    }
}
