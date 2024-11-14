using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class DaysRepositorySQL : IDaysRepositorySQL
    {
        private readonly BumboDBContext _context;

        public DaysRepositorySQL(BumboDBContext context)
        {
            _context = context;
        }

        public List<Days> getAllDays()
        {
            return _context.Days
                .OrderBy(day =>
                    day.Name == "Maandag" ? 1 :
                    day.Name == "Dinsdag" ? 2 :
                    day.Name == "Woensdag" ? 3 :
                    day.Name == "Donderdag" ? 4 :
                    day.Name == "Vrijdag" ? 5 :
                    day.Name == "Zaterdag" ? 6 :
                    day.Name == "Zondag" ? 7 : 8)
                .ToList();
        }
    }
}
