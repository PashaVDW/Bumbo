using bumbo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bumbo.ViewModels
{
    public class ReadBranchViewModel
    {

        public int BranchId { get; set; }

        public string PostalCode { get; set; }

        public string HouseNumber { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string CountryName { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Employee> Managers { get; set; }

    }
}
