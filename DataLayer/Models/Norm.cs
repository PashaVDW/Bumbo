using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Norm
    {
        [Key]
        public int normId { get; set; }

        [Required]
        public int branchId { get; set; }

        [Required]
        public int week { get; set; }

        [Required]
        public int year { get; set; }

        [Required, StringLength(100)]
        public string activity { get; set; }
        
        [Required]
        public int normInSeconds { get; set; }
        public Branch Branch { get; set; }
    }
}
