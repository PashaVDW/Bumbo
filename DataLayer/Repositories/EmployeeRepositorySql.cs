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
            return _context.Users.ToList();
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
    }
}
