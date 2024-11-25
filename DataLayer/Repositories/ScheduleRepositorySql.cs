
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

        public List<Schedule> GetScheduleForBranchByDay(int branchId, DateOnly dayDate)
        {
            var schedule = _context.Schedule
                .Include(s => s.Employee)
                .Include(s => s.Department)
                .Where(s => s.BranchId == branchId && s.Date == dayDate)
                .ToList();

            return schedule;
        }
        
        public List<string> GetDepartments()
        {
            var departments = _context.Departments
                .Select(d => d.DepartmentName)
                .Distinct()
                .ToList();

            return departments;
        }

        public bool CanRemoveEmployeeFromDay(DateOnly dayDate, string employeeId, int branchId)
        {
            var exists = _context.Schedule
                .Any(s => s.BranchId == branchId && s.Date == dayDate && s.EmployeeId == employeeId);

            return exists;
        }

        public void RemoveEmployeeFromDay(DateOnly dayDate, string employeeId, int branchId)
        {
            var schedule = _context.Schedule
                .FirstOrDefault(s => s.BranchId == branchId && s.Date == dayDate && s.EmployeeId == employeeId);

            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
                _context.SaveChanges();
            }
        }

        public List<Schedule> GetWeekScheduleForEmployee(string employeeId, DateTime monday, DateTime sunday)
        {
            var dateOnlyMonday = DateOnly.FromDateTime(monday);
            var dateOnlySunday = DateOnly.FromDateTime(sunday);
            return _context.Schedule.Where(s => s.EmployeeId == employeeId && s.Date >= dateOnlyMonday && s.Date <= dateOnlySunday).ToList();
        }

        public void UpdateEmployeeDaySchedule(string employeeId, DateTime date, TimeOnly startTime, TimeOnly endTime, int branchId, string departmentName)
        {
            var existingSchedule = _context.Schedule
                .FirstOrDefault(s => s.EmployeeId == employeeId && s.Date == DateOnly.FromDateTime(date));

            if (existingSchedule != null)
            {
                existingSchedule.StartTime = startTime;
                existingSchedule.EndTime = endTime;
                existingSchedule.DepartmentName = departmentName;

                _context.Schedule.Update(existingSchedule);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Schedule entry does not exist for the specified employee and date.");
            }
        }
    }
}
