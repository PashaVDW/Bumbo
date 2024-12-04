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
        List<Employee> GetAvailableEmployees(DateOnly date, TimeOnly startTime, TimeOnly endTime, int branchId, string departmentName);
        Task<List<Employee>> GetEmployeesOfBranch(int? branchId);
    }
}
