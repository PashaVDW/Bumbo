using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [PrimaryKey(nameof(EmployeeId), nameof(BranchId), nameof(Date))]
    public class Schedule
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Key]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required, StringLength(50)]
        public string DepartmentName { get; set; }

        public bool IsSick { get; set; }

        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public Department Department { get; set; }

        public virtual ICollection<SwitchRequest> SwitchRequests { get; set; }
    }

}
