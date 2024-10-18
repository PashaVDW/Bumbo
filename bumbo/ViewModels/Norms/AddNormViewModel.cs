using Microsoft.AspNetCore.Mvc;

namespace bumbo.Models.ViewModels.Norms
{
    public class AddNormViewModel
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public int UnloadColis { get; set; }
        public int FillShelves { get; set; }
        public int Cashier { get; set; }
        public int Fresh { get; set; }
        public int Fronting { get; set; }
    }
}
