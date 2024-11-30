using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bumbo.Models
{
    [PrimaryKey(nameof(BranchId), nameof(EmployeeId), nameof(RequestToBranchId))]
    public class BranchRequestsEmployee
    {
        [Required]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public int RequestToBranchId { get; set; }
        [Required, StringLength(100)]
        public string RequestStatusName { get; set; }
        public RequestStatus RequestStatus { get; set; }
        [StringLength(300)]
        public string Message { get; set; }
        [Required]
        public DateTime DateNeeded { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public string DepartmentName { get; set; }

        [NotMapped]
        public int Id { get; set; }
    }
}