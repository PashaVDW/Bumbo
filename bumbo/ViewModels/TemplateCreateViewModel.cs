namespace bumbo.ViewModels
{
    public class TemplateCreateViewModel
    {
        public string Name { get; set; } = String.Empty;
        public List<DayData> Days { get; set; } = new List<DayData>();
    }

    public class DayData
    {
        public string DayName { get; set; } = String.Empty;
        public int CustomerAmount { get; set; }
        public int ContainerAmount { get; set; }
    }

}
