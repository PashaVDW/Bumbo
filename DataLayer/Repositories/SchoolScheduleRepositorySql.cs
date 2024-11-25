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
    public class SchoolScheduleRepositorySql : ISchoolScheduleRepository
    {

        readonly BumboDBContext _context;

        public SchoolScheduleRepositorySql(BumboDBContext contxext)
        {
            _context = contxext;
        }

        public Task<List<SchoolSchedule>> GetSchoolScheduleOfEmployee(string employeeId)
        {
            return _context.SchoolSchedule
                    .Where(ss => ss.EmployeeId == employeeId)
                    .ToListAsync();
        }
    }
}
