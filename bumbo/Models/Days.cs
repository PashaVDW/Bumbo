using System.ComponentModel.DataAnnotations;

namespace bumbo.Models
{
    public class Days
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }


    }
}
