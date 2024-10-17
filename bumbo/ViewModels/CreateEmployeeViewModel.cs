using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bumbo.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(50, ErrorMessage = "Postal code cannot be longer than 50 characters.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "House number is required.")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public string? BID { get; set; }

        public int? ManagerOfBranchId { get; set; } = null;

        public bool IsSystemManager { get; set; } = false;

        [Required(ErrorMessage = "Please select a function.")]
        public string SelectedFunction { get; set; }

        public List<SelectListItem>? Functions { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
