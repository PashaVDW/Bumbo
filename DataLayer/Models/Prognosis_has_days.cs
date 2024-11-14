using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumbo.Models
{
    [PrimaryKey("Days_name", "PrognosisId")]
    public class Prognosis_has_days
    {
        [StringLength(10)]
        public string Days_name { get; set; }

        [ForeignKey("Days_name")]
        public Days Days { get; set; }

        [StringLength(45)]
        public string PrognosisId { get; set; }

        [ForeignKey("PrognosisId")]
        public Prognosis Prognosis { get; set; }

        [Required]
        public int CustomerAmount { get; set; }

        [Required]
        public int PackagesAmount { get; set; }
        public ICollection<Prognosis_has_days_has_Department> prognosis_Has_Days_Has_Department { get; set; }
    }
}
