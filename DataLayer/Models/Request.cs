using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Request
    {
        [Key]
        public int RequestId;
        public string RequestStatusName { get; set; }
        public string RequestTypeName { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
    }
}
