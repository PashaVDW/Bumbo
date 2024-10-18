using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Days
    {
        [Key, StringLength(10)]
        public string Name { get; set; }

        public ICollection<bumbo.Models.Prognosis_has_days> Prognosis_Has_Days { get; set; }
    }
}
