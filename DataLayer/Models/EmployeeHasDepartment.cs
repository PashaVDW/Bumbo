using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class EmployeeHasDepartment
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required, StringLength(50)]
        public string DepartmentName { get; set; }
        public Employee Employee { get; set; }
        public Department Department { get; set; }
    }
}
