using bumbo.Models;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Department
    {
        [Key, StringLength(50)]
        public string DepartmentName { get; set; }
        public ICollection<PrognosisHasDaysHasDepartment> Prognosis { get; set; }
    }
}
