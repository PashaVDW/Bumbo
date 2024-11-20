using bumbo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PrognosisHasDaysHasDepartment
    {
        [Required, StringLength(50)]
        public string DepartmentName { get; set; }
        [Required, StringLength(10)]
        public string dayName { get; set; }
        [Required, StringLength(45)]
        public string PrognosisId { get; set; }
        [Required]
        public int AmountOfWorkersNeeded { get; set; }
        [Required]
        public int HoursOfWorkNeeded { get; set; }
        public Department Department { get; set; }
        public PrognosisHasDays PrognosisHasDays { get; set; }
    }
}
