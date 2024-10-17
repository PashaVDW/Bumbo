using DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class BranchHasEmployee
    {

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string FunctionName { get; set; }
        public Function Function { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}