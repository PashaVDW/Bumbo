namespace bumbo.ViewModels
{
    public class LabourRulesGroupViewModel
    {
        public List<LabourRulesViewModel> GeneralLabourRules { get; set; }
        public List<LabourRulesViewModel> AgeLabourRules { get; set; }
        public List<String> Countries { get; set; }
        public string ActiveCountry { get; set; }

    }
}
