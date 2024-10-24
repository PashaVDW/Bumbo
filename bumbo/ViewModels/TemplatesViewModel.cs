namespace bumbo.ViewModels
{
    public class TemplatesViewModel
    {
        public string Name { get; set; }

        public int TemplateId { get; set; }

        public List<TemplateHasDaysViewModel> HasDays { get; set; }
    }
}
