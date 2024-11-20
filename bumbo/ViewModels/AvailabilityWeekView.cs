using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class AvailabilityWeekView
    {
        public List<DayOverview> Days { get; set; } = new List<DayOverview>();
        public int Year { get; set; }
        public string Month { get; set; }
        public int Week { get; set; }
    }

    public class DayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public class AvailabilityInputViewModel
    {
        [Required]
        public int Week { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public List<DayInputViewModel> Days { get; set; } = new List<DayInputViewModel>();
    }

    public class DayInputViewModel
    {
        public string DayName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }


}
