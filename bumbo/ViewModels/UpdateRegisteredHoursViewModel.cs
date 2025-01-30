using System;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class UpdateRegisteredHoursViewModel
    {
        [Required]
        public string EmployeeBID { get; set; }

        public string EmployeeName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "De begintijd is verplicht.")]
        public DateTime WorkedStartTime { get; set; }

        public DateTime? WorkedEndTime { get; set; }

        public string? Notes { get; set; }

        public bool IsSick { get; set; }

        // Toegevoegd voor geplande uren
        public string? ScheduledDepartment { get; set; } // Afdeling voor geplande uren
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }

    }
}
