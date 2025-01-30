namespace bumbo.Models.ViewModels.Norms
{
    public class UpdateNormInputViewModel
    {
        public int FirstNormId { get; set; }
        public int BranchId { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
        public int UnloadColis { get; set; }
        public int FillShelves { get; set; }
        public int Cashier { get; set; }
        public int Fresh { get; set; }
        public int Fronting { get; set; }
    }
}
