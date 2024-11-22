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
            // Verkrijg alle datums uit de nieuwe schema's
            var scheduleDates = schedules.Select(ns => ns.Date).ToList();

            // Vind de bestaande schema's op basis van deze datums
            List<SchoolSchedule> existingSchedules = _context.SchoolSchedule
                .Where(s => s.EmployeeId == employeeId && scheduleDates.Contains(s.Date))
                .ToList();

            // Verwijder de bestaande schema's
            _context.SchoolSchedule.RemoveRange(existingSchedules);

            // Voeg de nieuwe schema's toe
            _context.SchoolSchedule.AddRange(schedules);

            // Sla wijzigingen op
            _context.SaveChanges();
        }


    }
}
