using bumbo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class LabourRules
    {
        [Required, StringLength(50)]
        public string CountryName { get; set; }
        public Country Country { get; set; }
    }
}
