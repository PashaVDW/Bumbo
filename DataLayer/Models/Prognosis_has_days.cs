using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bumbo.Models;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Days_name), nameof(PrognosisId))]
public class Prognosis_has_days
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
}
