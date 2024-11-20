using bumbo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

    [ForeignKey("CountryName")]
    public Country Country { get; set; }

    public ICollection<Employee> Employees { get; set; }

}