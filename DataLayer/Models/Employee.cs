using bumbo.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Employee : IdentityUser
{
    public string? BID { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required, StringLength(50)]
    public string PostalCode { get; set; }
    [Required]
    public int HouseNumber { get; set; }
    [Required, StringLength(50)]
    public string Email { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required, StringLength(50)]
    public string FunctionName { get; set; }
    public bool IsSystemManager { get; set; }
    public int? ManagerOfBranchId { get; set; }  // Nullable ManagerOfBranchId
    public int? WorksAtBranchId { get; set; }  // Nullable WorksAtBranchId

    public Branch? ManagerOfBranch { get; set; }  // Nullable ManagerOfBranch
    public Branch? WorksAtBranch { get; set; }  // Nullable WorksAtBranch
}
