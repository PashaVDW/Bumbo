using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bumbo.Models
{
    public class Template
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Branch_branchId { get; set; }

        [ForeignKey("Branch_branchId")]
        public Branch Branch { get; set; }

        [Required, StringLength(64)]
        public string Name { get; set; }

        public ICollection<Template_has_days> TemplateHasDays { get; set; }
    }
}
