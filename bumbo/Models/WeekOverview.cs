using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class WeekOverview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int weekNumber { get; set; }

        [Required]
        public List<DayOverview> days { get; set; }
    }
}
