using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class AvailabilityRepositorySql : IAvailabilityRepository
    {
        private readonly BumboDBContext _context;

        public AvailabilityRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public void AddAvailabilities(List<Availability> availabilities, DateTime periodStart, DateTime periodEnd)
        {
            if (availabilities != null && availabilities.Any())
            {
                var employeeId = availabilities.First().EmployeeId;

                var periodStartDateOnly = DateOnly.FromDateTime(periodStart);
                var periodEndDateOnly = DateOnly.FromDateTime(periodEnd);

                var existingAvailabilities = _context.Availability
                   .Where(a => a.EmployeeId == employeeId
                               && a.Date >= periodStartDateOnly
                               && a.Date <= periodEndDateOnly)
                   .ToList();

                if (existingAvailabilities.Any())
                {
                    _context.Availability.RemoveRange(existingAvailabilities);
                    _context.SaveChanges();
                }

                foreach (var a in availabilities)
                {
                    Console.WriteLine($"id: {a.EmployeeId}, date: {a.Date}, start: {a.StartTime}, eind: {a.EndTime}");
                }

                _context.Availability.AddRange(availabilities);
                _context.SaveChanges();
            }
        }


        public List<Availability> GetAvailabilitiesBetweenDates(DateTime firsteDate, DateTime lastDate, string employeeId)
        {
            DateOnly firstDateOfWeek = DateOnly.FromDateTime(firsteDate);
            DateOnly lastDateOfWeek = DateOnly.FromDateTime(lastDate);

            return _context.Availability
                .Where(a => a.Date >= firstDateOfWeek && a.Date <= lastDateOfWeek && a.EmployeeId == employeeId)
                .OrderBy(a => a.Date)
                .ToList();
        }

        public Availability GetEmployeeDayAvailability(DateTime date, string employeeId)
        {
            var dateOnly = DateOnly.FromDateTime(date);
            return _context.Availability.SingleOrDefault(a => a.Date == dateOnly && a.EmployeeId == employeeId);
        }
    }
}
