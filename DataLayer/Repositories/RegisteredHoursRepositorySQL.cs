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
    public class RegisteredHoursRepositorySQL : IRegisteredHoursRepository
    {
        readonly BumboDBContext _context;

        public RegisteredHoursRepositorySQL(BumboDBContext context)
        {
            _context = context;
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

    }
}
