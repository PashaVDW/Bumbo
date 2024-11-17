using bumbo.Models;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Department
    {
        [Key, StringLength(50)]
        public string DepartmentName { get; set; }
        public ICollection<Prognosis_has_days_has_Department> Prognosis { get; set; }
    }
}
