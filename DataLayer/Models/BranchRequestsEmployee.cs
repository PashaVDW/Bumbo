using DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
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
        [Required]
        public string RequestStatusName { get; set; }
        public RequestStatus RequestStatus { get; set; }
        [StringLength(300)]
        public string Message { get; set; }
    }
}