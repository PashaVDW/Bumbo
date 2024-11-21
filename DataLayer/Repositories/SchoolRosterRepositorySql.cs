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
    public class SchoolRosterRepositorySql : ISchoolRosterRepository
    {
        private readonly BumboDBContext _context;

        public SchoolRosterRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<SchoolSchedule> getSchedulesBetweenDates(DateTime startDate, DateTime endDate, string employeeId)
        {
            DateOnly firstDateOfWeek = DateOnly.FromDateTime(startDate);
            DateOnly lastDateOfWeek = DateOnly.FromDateTime(endDate);

            return _context.SchoolSchedule
                .Where(a => a.Date >= firstDateOfWeek && a.Date <= lastDateOfWeek && a.EmployeeId == employeeId)
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}
