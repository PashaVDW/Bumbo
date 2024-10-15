using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PostalCode { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        public string FunctionName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? BID { get; set; }

        public int? ManagerOfBranchId { get; set; } = null;

        public bool IsSystemManager { get; set; } = false;
    }
}
