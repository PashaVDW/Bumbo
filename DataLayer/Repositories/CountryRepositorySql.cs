using bumbo.Data;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class CountryRepositorySql : ICountryRepository
    {
        private readonly BumboDBContext _context;
        public CountryRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<string> GetCountries()
        {
            return _context.Countries.Select(c => c.Name).ToList();
        }
    }
}
