using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class UpdateEmployeeViewModel
    {
        [Required(ErrorMessage = "Het ID is verplicht.")]
        public string Id { get; set; } // Employee ID

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Voornaam mag niet langer zijn dan 50 tekens.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Achternaam mag niet langer zijn dan 50 tekens.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht.")]
        [StringLength(50, ErrorMessage = "Postcode mag niet langer zijn dan 50 tekens.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht.")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht.")]
        [RegularExpression(@"^[\d\s\-\(\)\+]+$", ErrorMessage = "Het telefoonnummer mag alleen cijfers, spaties, haakjes, streepjes en een '+' teken bevatten.")]
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
