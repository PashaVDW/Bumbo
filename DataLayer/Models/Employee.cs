using bumbo.Models;
using DataLayer.Models;
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

    [Required, StringLength(10)]
    public string HouseNumber { get; set; }
    [Required, StringLength(50)]
    public string Email { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsSystemManager { get; set; }

    public int? ManagerOfBranchId { get; set; }  // Nullable ManagerOfBranchId

    public Branch? ManagerOfBranch { get; set; }  // Nullable ManagerOfBranch


    public ICollection<BranchHasEmployee> BranchEmployees { get; set; }
    public virtual ICollection<EmployeeHasDepartment> EmployeeHasDepartment { get; set; }
    public virtual ICollection<Availability> Availabilitys { get; set; }
    public virtual ICollection<SchoolSchedule> SchoolSchedules { get; set; }
    public virtual ICollection<SwitchRequest> SwitchRequests { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
    public virtual ICollection<BranchRequestsEmployee> BranchRequestsEmployee { get; set; }
    public virtual ICollection<RegisteredHours> RegisteredHours { get; set; }
}
