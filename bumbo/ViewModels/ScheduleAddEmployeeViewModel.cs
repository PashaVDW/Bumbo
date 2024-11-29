﻿
namespace bumbo.ViewModels
{
    public class ScheduleAddEmployeeViewModel
    {

        
        public List<ScheduleAddEmployeeSingleViewModel> Employees { get; set; } = new List<ScheduleAddEmployeeSingleViewModel>();
    }

    public class ScheduleAddEmployeeSingleViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly Date { get; set; }
        public double PlannedHours { get; set; }
        public TimeOnly ToPlanHours { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}