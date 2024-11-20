using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace bumbo.Models
{
    public class Template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BranchBranchId { get; set; }

        [ForeignKey("Branch_branchId")]
        public Branch Branch { get; set; }

        [Required, StringLength(64)]
        public string Name { get; set; }

        public ICollection<TemplateHasDays> TemplateHasDays { get; set; }
    }
}
