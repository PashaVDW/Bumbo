using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataLayer.Interfaces;
using DataLayer.Models;
using bumbo.Data;

namespace DataLayer.Repositories
{
    public class ScheduleRepositorySql : IScheduleRepository
    {
        private readonly BumboDBContext _context;

        public ScheduleRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Schedule> GetSchedulesForBranchByWeek(int branchId, List<DateOnly> weekDates)
        {
            var schedules = _context.Schedule
                .Include(s => s.Employee)
                .Include(s => s.Department)
                .Where(s => s.BranchId == branchId && weekDates.Contains(s.Date))
                .ToList();

            return schedules;
        }
        public List<Schedule> GetSchedulesForEmployeeByDay(string employeeId, DateOnly date)
        {
            var schedules = _context.Schedule
                .Include(s => s.Employee)
                .Where(s => s.EmployeeId == employeeId && s.Date == date)
                .ToList();

            return schedules;
        }

        public List<Schedule> SetSchedulesSick(List<Schedule> sickSchedules)
        {
            sickSchedules.ForEach(s => s.IsSick = true);
            _context.SaveChanges();
            return sickSchedules;
        }
        public List<Schedule> SetSchedulesBetter(List<Schedule> sickSchedules)
        {
            sickSchedules.ForEach(s => s.IsSick = false);
            _context.SaveChanges();
            return sickSchedules;
        }
        public List<Schedule> GetSchedulesForEmployeeByWeek(string employeeId, List<DateOnly> weekDates)
        {
            var schedules = _context.Schedule
                .Include(s => s.Employee)
                .Include(s => s.Department)
                .Where(s => s.EmployeeId == employeeId && weekDates.Contains(s.Date))
                .ToList();

            return schedules;
        }
        public List<string> GetDepartments()
        {
            var departments = _context.Departments
                .Select(d => d.DepartmentName)
                .Distinct()
                .ToList();

            return departments;
        }

        public void FinalizeSchedules(int branchId, List<DateOnly> weekDates)
        {
            var schedules = _context.Schedule
                .Where(s => s.BranchId == branchId && weekDates.Contains(s.Date))
                .ToList();

            if (schedules.Any())
            {
                schedules.ForEach(s => s.IsFinal = true);
                _context.SaveChanges();
            }
        }


    }
}
