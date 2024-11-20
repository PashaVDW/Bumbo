using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
    }
}