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
                    day.Name == "Monday" ? 1 :
                    day.Name == "Tuesday" ? 2 :
                    day.Name == "Wednesday" ? 3 :
                    day.Name == "Thursday" ? 4 :
                    day.Name == "Friday" ? 5 :
                    day.Name == "Saturday" ? 6 :
                    day.Name == "Sunday" ? 7 : 8)
                .ToList();
        }
    }
}
