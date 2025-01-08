using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RegisteredHoursRepositorySQL : IRegisteredHoursRepository
    {
        private readonly BumboDBContext _context;

        public RegisteredHoursRepositorySQL(BumboDBContext context)
        {
            _context = context;
        }
        public List<RegisteredHours> GetRegisteredHoursFromEmployee(string employeeId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            return _context.RegisteredHours.Where(r => r.EmployeeId.Equals(employeeId) 
                                                        && r.StartTime.Month == today.Month
                                                        && r.EndTime.Month == today.Month).ToList();
        }

        public List<RegisteredHours> GetRegisteredHoursInWeekFromEmployee(string employeeId, int week)
        {
            return _context.RegisteredHours
                .Where(r => r.EmployeeId.Equals(employeeId))
                .AsEnumerable()
                .Where(r => ISOWeek.GetWeekOfYear(r.StartTime) == week
                        && ISOWeek.GetWeekOfYear(r.EndTime) == week).ToList();
        }
    }
}
