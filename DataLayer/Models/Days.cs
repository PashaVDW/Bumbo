using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Days
    {
        [Key, StringLength(10)]
        public string Name { get; set; }

        public ICollection<TemplateHasDays> TemplateHasDays { get; set; }
        public ICollection<PrognosisHasDays> PrognosisHasDays { get; set; }
    }
}
