using bumbo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class LabourRules
    {
        [Required, StringLength(50)]
        public string CountryName { get; set; }

        [Key, StringLength(10)]
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

        public Country Country { get; set; }
    }
}
