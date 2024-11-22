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

        [Required(ErrorMessage = "Startuur is verplicht.")]
        [Range(0, 23, ErrorMessage = "Startuur moet tussen 0 en 23 liggen.")]
        public int? StartHour { get; set; } = 0;

        [Required(ErrorMessage = "Startminuten zijn verplicht.")]
        [Range(0, 59, ErrorMessage = "Startminuten moeten tussen 0 en 59 liggen.")]
        public int? StartMinute { get; set; } = 0;

        [Required(ErrorMessage = "Einduur is verplicht.")]
        [Range(0, 23, ErrorMessage = "Einduur moet tussen 0 en 23 liggen.")]
        public int? EndHour { get; set; } = 0;

        [Required(ErrorMessage = "Eindminuten zijn verplicht.")]
        [Range(0, 59, ErrorMessage = "Eindminuten moeten tussen 0 en 59 liggen.")]
        public int? EndMinute { get; set; } = 0;
    }

}
