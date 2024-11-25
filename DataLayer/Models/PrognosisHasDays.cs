using System.ComponentModel.DataAnnotations;
﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using bumbo.Models;

[PrimaryKey(nameof(Days_name), nameof(PrognosisId))]
public class PrognosisHasDays
{
    [StringLength(10)]
    public string Days_name { get; set; }

    [ForeignKey("Days_name")]
    public Days Days { get; set; }

    public int PrognosisId { get; set; }

    [ForeignKey("PrognosisId")]
    public Prognosis Prognosis { get; set; }

    [Required]
    public int CustomerAmount { get; set; }

    [Required]
    public int PackagesAmount { get; set; }
    public ICollection<PrognosisHasDaysHasDepartment> PrognosisHasDaysHasDepartment { get; set; }
}

