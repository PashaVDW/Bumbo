using DataLayer.Models;
using System.Collections.Generic;

namespace bumbo.ViewModels
{
    public class UpdateSwapShiftViewModel
    {
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
        public List<UpdateSwapShiftScheduleViewModel> Schedules { get; set; }
        public SwitchRequest SelectedUitgaandeDienstRuil { get; set; }
    }

    public class UpdateSwapShiftScheduleViewModel
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DepartmentName { get; set; }
        public bool IsSelected { get; set; }
    }
}
