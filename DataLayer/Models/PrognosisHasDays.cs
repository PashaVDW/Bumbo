using System.ComponentModel.DataAnnotations;
﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bumbo.Models;

[PrimaryKey(nameof(Days_name), nameof(PrognosisId))]
public class Prognosis_has_days
{
    [PrimaryKey(nameof(DayName), nameof(PrognosisId))]
    public class PrognosisHasDays
    {
        [StringLength(10)]
        public string DayName { get; set; }

        [ForeignKey(nameof(DayName))]
        public Days Days { get; set; }

    public int PrognosisId { get; set; }

        [ForeignKey(nameof(PrognosisId))]
        public Prognosis Prognosis { get; set; }

    [Required]
    public int CustomerAmount { get; set; }

        [Required]
        public int PackagesAmount { get; set; }
        public virtual ICollection<PrognosisHasDaysHasDepartment> PrognosisHasDaysHasDepartment { get; set; }
    }
}

