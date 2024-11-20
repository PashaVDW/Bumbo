using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BranchesRepositorySql : IBranchesRepository
    {

        private readonly BumboDBContext _context;

        public BranchesRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public void AddBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            _context.SaveChanges();
        }

        public void UpdateBranch(Branch branch)
        {
            _context.Branches.Update(branch);
            _context.SaveChanges();
        }

        public void DeleteBranch(Branch branch)
        {
            _context.Branches.Remove(branch);
            _context.SaveChanges();
        }

        public Branch GetBranch(int branchId)
        {
            var branch = _context.Branches.SingleOrDefault(p => p.BranchId == branchId);
            return branch;
        }

        public List<Branch> GetAllBranches() 
        {
            return _context.Branches.ToList();
        }

        public void AddBranchManager(string employeeId, Branch branch)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id.Equals(employeeId.ToString()));
            employee.ManagerOfBranch = branch;
            employee.ManagerOfBranchId = branch.BranchId;
            _context.SaveChanges();
        }

        public void DeleteBranchManager(string employeeId)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id.Equals(employeeId.ToString()));
            employee.ManagerOfBranch = null;
            employee.ManagerOfBranchId = null;
            _context.SaveChanges();
        }

        public List<Employee> GetManagersOfBranch(Branch branch)
        {
            List<Employee> employees = _context
                .Employees
                .Where(e => e.ManagerOfBranchId == branch.BranchId)
                .ToList();
            return employees;
        }

        public List<Employee> GetEmployeesFromBranch(Branch branch)
        {
            List<BranchHasEmployee> branchHasEmployees = _context
                .BranchHasEmployees
                .Where(e => e.BranchId == branch.BranchId)
                .ToList();


            List<Employee> employeesInDatabase = _context
                .Employees
                .ToList();

            List<Employee> employeesInBranch = new List<Employee>();

            foreach (var emp in employeesInDatabase)
            {
                foreach (var branchEmp in branchHasEmployees)
                {
                    if (branchEmp.EmployeeId == emp.Id)
                    {
                        employeesInBranch.Add(emp);
                    }
                }
            }

            return employeesInBranch;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee GetEmployeeById(string id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id.Equals(id));
        }
    }
}
