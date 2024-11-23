using bumbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBranchesRepository
    {
       
        Branch GetBranch(int id);
        string GetBranchCountryName(int branchId);
        List<Branch> GetAllBranches();
        void AddBranch(Branch branch);
        void UpdateBranch(Branch branch);
        void DeleteBranch(Branch branch);
        void AddBranchManager(string employeeId, Branch branch);
        void DeleteBranchManager(string employeeId);
        List<Employee> GetManagersOfBranch(Branch branch);
        List<Employee> GetEmployeesFromBranch(Branch branch);
        List<Employee> GetAllEmployees();

    }
}