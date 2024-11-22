using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bumbo.Models;
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

        public List<SchoolSchedule> getSchedulesBetweenDates(DateTime startDate, DateTime endDate, string employeeId)
        {
            DateOnly firstDateOfWeek = DateOnly.FromDateTime(startDate);
            DateOnly lastDateOfWeek = DateOnly.FromDateTime(endDate);

            return _context.SchoolSchedule
                .Where(a => a.Date >= firstDateOfWeek && a.Date <= lastDateOfWeek && a.EmployeeId == employeeId)
                .OrderBy(a => a.Date)
                .ToList();
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
