﻿
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
    }
}
