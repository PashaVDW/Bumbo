using bumbo.Models;

namespace bumbo.ViewModels.Prognosis
{
    public class AddTemplateViewModel
    {
        public List<Template> Templates{ get; set; }
        public int? WeekNr { get; set; }
        public int? YearNr { get; set; }
    }
}
