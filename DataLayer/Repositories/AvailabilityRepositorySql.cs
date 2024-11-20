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

        public void AddAvailabilities(List<Availability> availabilities)
        {
            if (availabilities != null && availabilities.Any())
            {
                _context.Availability.AddRange(availabilities);
                _context.SaveChanges();
            }
        }


        public List<Availability> GetAvailabilitiesBetweenDates(DateTime firsteDate, DateTime lastDate)
        {
            DateOnly firstDateOfWeek = DateOnly.FromDateTime(firsteDate);
            DateOnly lastDateOfWeek = DateOnly.FromDateTime(lastDate);

            return _context.Availability
                .Where(a => a.Date >= firstDateOfWeek && a.Date <= lastDateOfWeek)
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}
