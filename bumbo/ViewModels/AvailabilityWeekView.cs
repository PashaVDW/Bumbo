using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class AvailabilityWeekView
    {
        public List<AvailabilityDayOverview> Days { get; set; } = new List<AvailabilityDayOverview>();
        public int Year { get; set; }
        public string Month { get; set; }
        public int Week { get; set; }
    }

    public class AvailabilityDayOverview
    {
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public class AvailabilityInputViewModel
    {
        public int StartWeek { get; set; }
        public int StartYear { get; set; }
        public int EndWeek { get; set; }
        public int EndYear { get; set; }
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
        public List<AvailabilityDayInputViewModel> Days { get; set; } = new List<AvailabilityDayInputViewModel>();
    }


    public class AvailabilityDayInputViewModel
    {
        public string DayName { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int DayNumber { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public bool AllDay { get; set; } = false;
    }


}
