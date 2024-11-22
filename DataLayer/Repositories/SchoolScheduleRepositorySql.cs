using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Repositories
{
    public class SchoolScheduleRepositorySql : ISchoolScheduleRepository
    {

        readonly BumboDBContext _context;

        public SchoolScheduleRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public void AddSchoolSchedulesForEmployee(string employeeId, List<SchoolSchedule> schedules)
        {
            var validSchedules = schedules
                .Where(s => s.StartTime != s.EndTime)
                .ToList();

            var scheduleDates = validSchedules.Select(ns => ns.Date).ToList();

            List<SchoolSchedule> existingSchedules = _context.SchoolSchedule
                .Where(s => s.EmployeeId == employeeId && scheduleDates.Contains(s.Date))
                .ToList();

            _context.SchoolSchedule.RemoveRange(existingSchedules);

            _context.SchoolSchedule.AddRange(validSchedules);

            _context.SaveChanges();
        }
    }
}
