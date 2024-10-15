using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Country
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        // Een Country kan meerdere branches hebben
        public ICollection<Branch> Branches { get; set; }
    }
}
