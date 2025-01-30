using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Department
    {
        [Key, StringLength(50)]
        public string DepartmentName { get; set; }
        public virtual ICollection<EmployeeHasDepartment> EmployeeHasDepartment { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<PrognosisHasDaysHasDepartment> PrognosisHasDaysHasDepartment { get; set; }
    }
}
