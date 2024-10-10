using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class DayOverview
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string dayName { get; set; }

        [Required]
        public int customerAmount { get; set; }

        [Required]
        public int packagesAmount { get; set; }
    }
}
