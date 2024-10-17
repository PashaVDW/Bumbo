﻿using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        List<Employee> SearchEmployees(string searchTerm);
    }
}
