﻿using bumbo.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Employee : IdentityUser
    {
        public string BID { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(50)]
        public string PostalCode { get; set; }

        public int HouseNumber { get; set; }

        public DateTime StartDate { get; set; }

        public string FunctionName { get; set; }

        public bool IsSystemManager { get; set; }

        // Nullable foreign key
        public int? ManagerOfBranchId { get; set; } // Correct casing

        // Navigation property
        public Branch ManagerOfBranch { get; set; }
    }

}