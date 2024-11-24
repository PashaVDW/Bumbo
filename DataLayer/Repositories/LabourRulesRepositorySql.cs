using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class LabourRulesRepositorySql : ILabourRulesRepository
    {
        private readonly BumboDBContext _context;

        public LabourRulesRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<LabourRules> GetAllLabourRulesForCountry(string countryName)
        {
            return _context.LabourRules.Where(lr => lr.CountryName.Equals(countryName)).ToList();
        }
    }
}
