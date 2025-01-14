﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace bumbo.Models
{
    public class Prognosis
    {
        [Key]
        [StringLength(45)]
        public string PrognosisId { get; set; }

        [Required, Range(0, 99)]
        public int WeekNr { get; set; }

        [Required, Range(0, 9999)]
        public int Year { get; set; }

        [Required]
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public ICollection<Branch> Branches { get; set; }

        public ICollection<PrognosisHasDays> PrognosisHasDays{ get; set; }
    }
}
