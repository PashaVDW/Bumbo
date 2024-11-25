using bumbo.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [PrimaryKey("DepartmentName", "Days_name", "PrognosisId")]
    public class PrognosisHasDaysHasDepartment
    {
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [ForeignKey("DepartmentName")]
        public Department Department { get; set; }
        [StringLength(10)]
        public string Days_name { get; set; }
        [ForeignKey("Days_name")]
        public Days Days { get; set; }
        [Required]
        public int PrognosisId { get; set; }
        [ForeignKey("PrognosisId")]
        public Prognosis Prognosis { get; set; }
        public int AmountWorkersNeeded { get; set; }
        public int HoursWorkNeeded { get; set; }
        public PrognosisHasDays Prognosis_Has_Days { get; set; }
    }
}
