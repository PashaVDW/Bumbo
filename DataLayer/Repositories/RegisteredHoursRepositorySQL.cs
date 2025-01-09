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
                                                        && r.EndTime != null
                                                        && r.StartTime.Month == today.Month
                                                        && r.EndTime.Value.Month == today.Month).ToList();
        }

        public void AddShift(RegisteredHours newShift)
        {
            _context.RegisteredHours.Add(newShift);
            _context.SaveChanges();
        }

        public bool IsClockedIn(string employeeId)
        {
            return _context.RegisteredHours
                .Any(rh => rh.EmployeeId == employeeId && rh.EndTime == null);
        }

        public bool ClockOut(string employeeId, DateTime endTime)
        {
            var activeShift = _context.RegisteredHours
                .FirstOrDefault(rh => rh.EmployeeId == employeeId && rh.EndTime == null);

            if (activeShift == null)
            {
                return false;
            }

            activeShift.EndTime = endTime;

            _context.SaveChanges();
            return true;
        }

        public DateTime? GetClockedInTime(string employeeId)
        {
            var activeShift = _context.RegisteredHours
                .FirstOrDefault(rh => rh.EmployeeId == employeeId && rh.EndTime == null);

            if (activeShift == null)
            {
                return null;
            };

            return activeShift.StartTime;
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
