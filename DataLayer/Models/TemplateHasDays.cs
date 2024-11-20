using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumbo.Models
{
    public class TemplateHasDays
    {
        [Key, Required]
        public int TemplatesId { get; set; }

        [ForeignKey("Templates_id")]
        public Template Template { get; set; }

        [Required, StringLength(10)]
        public string DaysName { get; set; }

        [ForeignKey("Days_name")]
        public Days Days { get; set; }

        [Required]
        public int CustomerAmount { get; set; }

        [Required]
        public int ContainerAmount { get; set; }
    }
}
