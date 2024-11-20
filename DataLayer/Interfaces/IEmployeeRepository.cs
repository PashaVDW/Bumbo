using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        List<Employee> SearchEmployees(string searchTerm);
        Employee GetEmployeeById(string employeeId);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(string employeeId);
        List<Employee> GetEmployeesFromBranch(Branch branch);
        Employee GetAManagerOfBranch(Branch branch);
    }
}
