using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class RegisteredHours
    {
        [Key]
        public int RegistrationNumber { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int BranchId { get; set; }
        
        [Required]
        public string EmployeeBID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        public bool IsDefenitive { get; set; }
    }
}
