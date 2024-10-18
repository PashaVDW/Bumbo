using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Onthoud mij?")]
        public bool RememberMe { get; set; }
    }
}
