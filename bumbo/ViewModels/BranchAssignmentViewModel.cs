using System;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class BranchAssignmentViewModel
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Functie is verplicht.")]
        public string FunctionName { get; set; }

        [Required(ErrorMessage = "Startdatum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
