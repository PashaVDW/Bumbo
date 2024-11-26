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

        public void CreateDefaultLabourRulesForCountry(string activeCountry)
        {
            var defaultLabourRules = _context.LabourRules.Where(r => r.CountryName.Equals("Default")).ToList();
            defaultLabourRules.ForEach(r => r.CountryName = activeCountry);
            _context.LabourRules.AddRange(defaultLabourRules);
            _context.SaveChanges();
        }
    }
}
