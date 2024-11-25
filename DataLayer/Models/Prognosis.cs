using bumbo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Prognosis
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PrognosisId { get; set; }

    [Required, Range(0, 99)]
    public int WeekNr { get; set; }

    [Required, Range(0, 9999)]
    public int Year { get; set; }

    [Required]
    public int BranchId { get; set; }

    [ForeignKey("BranchId")]
    public Branch Branch { get; set; }

    public ICollection<Branch> Branches { get; set; }

    public ICollection<PrognosisHasDays> Prognosis_Has_Days { get; set; }
}
