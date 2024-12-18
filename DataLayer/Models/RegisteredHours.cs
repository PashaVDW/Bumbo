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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int RegistrationNumber { get; set; }
        [Required]
        public string EmployeeBID { get; set; }
        [Required]
        public string EmployeeId { get; set; }

        //public Employee Employee { get; set; }

    }
}
