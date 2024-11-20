using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Request
    {
        public int RequestToBranchId;
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
        public string Message { get; set; }
        public string RequestStatusName { get; set; }
        public DateTime DateNeeded { get; set; }
        
    }
}
