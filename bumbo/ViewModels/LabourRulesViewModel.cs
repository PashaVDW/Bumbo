namespace bumbo.ViewModels
{
    public class LabourRulesViewModel
    {
        public string AgeGroup { get; set; }
        public int MaxHoursPerDay { get; set; }
        public TimeSpan MaxEndTime { get; set; }
        public int MaxHoursPerWeek { get; set; }
        public int MaxWorkDaysPerWeek { get; set; }
        public int MinRestDaysPerWeek { get; set; }
        public decimal NumHoursWorkedBeforeBreak { get; set; }
        public decimal SickPayPercentage { get; set; }
        public decimal OvertimePayPercentage { get; set; }
        public int MinutesOfBreak { get; set; }
        public int MaxHoursWithSchool { get; set; }
        public int MinRestHoursBetweenShifts { get; set; }
        public int MaxShiftDuration { get; set; }
        public int MaxOvertimeHoursPerWeek { get; set; }
        public string CountryName { get; set; }
    }
}
