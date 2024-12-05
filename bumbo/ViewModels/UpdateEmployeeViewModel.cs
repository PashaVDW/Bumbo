using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class UpdateEmployeeViewModel
    {
        [Required]
        public string Id { get; set; } // Employee ID

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht.")]
        [StringLength(50)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht.")]
        [RegularExpression(@"^\d+\s*([a-zA-Z]?)$", ErrorMessage = "Voer een geldig huisnummer in zoals 17 of 17b of 17 b")]
        public string HouseNumber { get; set; }


        [Required(ErrorMessage = "Telefoonnummer is verplicht.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        public string? BID { get; set; }

        public string? SelectedFunction { get; set; }

        public List<SelectListItem>? Functions { get; set; }

        public bool IsSystemManager { get; set; }

        public List<BranchAssignmentViewModel> BranchAssignments { get; set; } = new List<BranchAssignmentViewModel>();
        public int UserManagerOfBranchId { get; set; }
    }
}
