using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }

        [StringLength(36)]
        public string BID { get; set; }

        [Required, StringLength(50)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string middleName { get; set; }

        [Required, StringLength(50)]
        public string lastName { get; set; }

        [Required]
        public DateTime birthDate { get; set; }

        [Required, StringLength(50)]
        public string postalCode { get; set; }

        public int houseNumber { get; set; }

        [Required]
        public int phoneNumber { get; set; }

        [Required, StringLength(50)]
        public string email { get; set; }

        public DateTime startDate { get; set; }

        [StringLength(50)]
        public string functionName { get; set; }

        public int managerOfBranchId { get; set; }

        public string password { get; set; }

        public bool isSystemManager { get; set; }
    }
}
