using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bumbo.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Voornaam mag niet langer zijn dan 50 tekens.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(50, ErrorMessage = "Achternaam mag niet langer zijn dan 50 tekens.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [DataType(DataType.Date, ErrorMessage = "Voer een geldige datum in.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht.")]
        [StringLength(50, ErrorMessage = "Postcode mag niet langer zijn dan 50 tekens.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht.")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Startdatum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        public string? BID { get; set; }

        public int? ManagerOfBranchId { get; set; } = null;

        public bool IsSystemManager { get; set; } = false;

        public string? SelectedFunction { get; set; }

        public List<SelectListItem>? Functions { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
