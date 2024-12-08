using System.ComponentModel.DataAnnotations;
namespace bumbo.ViewModels
{
    public class AddSchoolScheduleViewModel
    {
        [Required(ErrorMessage = "Start week is verplicht.")]
        [Range(1, 53, ErrorMessage = "De start week moet tussen 1 en 53 zijn.")]
        public int StartWeek { get; set; }

        [Required(ErrorMessage = "Start jaar is verplicht.")]
        public int StartYear { get; set; }

        [Required(ErrorMessage = "Eind week is verplicht.")]
        [Range(1, 53, ErrorMessage = "De eind week moet tussen 1 en 53 zijn.")]
        public int EndWeek { get; set; }

        [Required(ErrorMessage = "Eind jaar is verplicht.")]
        public int EndYear { get; set; }

        public List<DaySchoolScheduleViewModel> Days { get; set; } = new List<DaySchoolScheduleViewModel>();
    }

    public class DaySchoolScheduleViewModel
    {
        [Required]
        public string DayName { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int DayNumber { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }

}
