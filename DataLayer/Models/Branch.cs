using bumbo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

public class Branch
{
    [Key]
    public int BranchId { get; set; }

    [Required, StringLength(50)]
    public string PostalCode { get; set; }

    [Required, StringLength(10)]
    public string HouseNumber { get; set; }


    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required, StringLength(100)]
    public string Street { get; set; }

    [Required, StringLength(50)]
    public string CountryName { get; set; }
    [Required]
    public int ShelfMeeters { get; set; }
    [Required]
    public TimeOnly OpeningTime { get; set; }
    [Required]
    public TimeOnly ClosingTime { get; set; }

    [ForeignKey("CountryName")]
    public Country Country { get; set; }
    public ICollection<BranchHasEmployee> BranchHasEmployees { get; set; }
    public ICollection<BranchRequestsEmployee> BranchRequestsEmployee { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
    public virtual ICollection<Norm> Norm { get; set; }
    public virtual ICollection<RegisteredHours> RegisteredHours { get; set; }

}