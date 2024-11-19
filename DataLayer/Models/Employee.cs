using bumbo.Models;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public bool IsSystemManager { get; set; }

    public int? ManagerOfBranchId { get; set; }  // Nullable ManagerOfBranchId
    public int? WorksAtBranchId { get; set; } // Nullable WorksAtBranchId

    public Branch? Branch { get; set; }  // Nullable ManagerOfBranch
    public virtual ICollection<EmployeeHasDepartment> EmployeeHasDepartment { get; set; }
    public virtual ICollection<Availability> Availabilitys { get; set; }
    public virtual ICollection<SchoolSchedule> SchoolSchedules { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}
