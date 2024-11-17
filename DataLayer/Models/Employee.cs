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

    [Required, RegularExpression(@"^[\d\s\-\(\)\+]+$", ErrorMessage = "Het telefoonnummer mag alleen cijfers, spaties, haakjes, streepjes en een '+' teken bevatten.")]
    public string PhoneNumber { get; set; }

    [Required, StringLength(50)]
    public string PostalCode { get; set; }

    public int HouseNumber { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsSystemManager { get; set; }

    public int? ManagerOfBranchId { get; set; }  // Nullable ManagerOfBranchId

    public Branch? ManagerOfBranch { get; set; }  // Nullable ManagerOfBranch

    public ICollection<BranchHasEmployee> BranchEmployees { get; set; }
}
