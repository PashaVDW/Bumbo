﻿using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class EmployeeRepositorySql : IEmployeeRepository
    {
        private readonly BumboDBContext _context;

        public EmployeeRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Users.ToList().OrderBy(u => u.FirstName).ToList();
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            return _context.Users
                .Where(e => e.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            e.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            e.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Employee GetEmployeeById(string employeeId)
        {
            return _context.Users
                           .FirstOrDefault(e => e.Id == employeeId);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Users.Update(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(string employeeId)
        {
            var employee = _context.Users.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                _context.Users.Remove(employee);
                _context.SaveChanges();
            }
        }

        public List<Employee> GetAvailableEmployees(DateOnly date, TimeOnly startTime, TimeOnly endTime, int branchId, string departmentName)
        {
            return _context.Availability
                .Include(a => a.Employee)
                    .ThenInclude(e => e.BranchEmployees)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.EmployeeHasDepartment)
                .Where(a => a.Date == date &&
                            a.StartTime <= startTime &&
                            a.EndTime >= endTime &&
                            a.Employee.BranchEmployees.Any(be => be.BranchId == branchId))
                .Select(a => a.Employee)
                .ToList();
        }

        public async Task<List<Employee>> GetEmployeesOfBranch(int? branchId)
        {
            return _context.Users
                     .Join(_context.BranchHasEmployees,
                           e => e.Id,
                           bhe => bhe.EmployeeId,
                           (e, bhe) => new { e, bhe })
                     .Where(joined => joined.bhe.BranchId == branchId)
                     .Select(joined => joined.e)
                     .ToList();
        }
    }
}
