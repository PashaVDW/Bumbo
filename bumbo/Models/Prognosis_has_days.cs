using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumbo.Models
{
    public class Prognosis_has_days
    {
        [Key]
        [StringLength(10)]
        public string Days_name { get; set; }

        [ForeignKey("Days_name")]
        public Days Days { get; set; }

        [Key]
        [StringLength(45)]
        public string PrognosisId { get; set; }

        [ForeignKey("PrognosisId")]
        public Prognosis Prognosis { get; set; }

        [Required]
        public int CustomerAmount { get; set; }

        [Required]
        public int PackagesAmount { get; set; }
    }
}
