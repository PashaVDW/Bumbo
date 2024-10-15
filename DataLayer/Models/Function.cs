using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Function
    {
        [Key]
        [StringLength(45)]
        public string FunctionName { get; set; }
    }
}
