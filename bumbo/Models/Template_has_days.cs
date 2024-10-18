using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumbo.Models
{
    public class Template_has_days
    {
        [Key, Required]
        public int Templates_id { get; set; }

        [ForeignKey("Templates_id")]
        public Template Template { get; set; }

        [Required, StringLength(10)]
        public string Days_name { get; set; }

        [ForeignKey("Days_name")]
        public Days Days { get; set; }

        [Required]
        public int CustomerAmount { get; set; }

        [Required]
        public int ContainerAmount { get; set; }
    }
}
