using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class AvailabilityRepositorySql : IAvailabilityRepository
    {
        readonly BumboDBContext _context;

        public AvailabilityRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public Task<List<Availability>> GetAvailabilityOfEmployee(string employeeId)
        {
            return _context.Availability
                    .Where(a => a.EmployeeId == employeeId)
                    .ToListAsync();
        }
    }
}
