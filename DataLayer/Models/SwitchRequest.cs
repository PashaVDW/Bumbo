using bumbo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [PrimaryKey(nameof(SendToEmployeeId), nameof(EmployeeId), nameof(BranchId), nameof(Date))]
    public class SwitchRequest
    {
        [Required]
        public string SendToEmployeeId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public bool Declined { get; set; } = false;
        public bool IsAccepted { get; set; } = false;
        public Employee Employee { get; set; }
        public Schedule Schedule { get; set; }
    }
}
