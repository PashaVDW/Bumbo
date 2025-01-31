using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BranchHasEmployeeRepositorySql : IBranchHasEmployeeRepository
    {
        private readonly BumboDBContext _context;

        public BranchHasEmployeeRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public async Task AddBranchHasEmployeeAsync(BranchHasEmployee branchHasEmployee)
        {
            _context.BranchHasEmployees.Add(branchHasEmployee);
            await _context.SaveChangesAsync();
        }

        public List<BranchHasEmployee> GetBranchesForEmployee(string employeeId)
        {
            return _context.BranchHasEmployees
                           .Include(bhe => bhe.Branch)
                           .Where(bhe => bhe.EmployeeId == employeeId)
                           .ToList();
        }

        public BranchHasEmployee GetBranchAssignment(string employeeId, int branchId)
        {
            return _context.BranchHasEmployees
                           .FirstOrDefault(bhe => bhe.EmployeeId == employeeId && bhe.BranchId == branchId);
        }

        public BranchHasEmployee GetBranchNameByEmployee(string employeeId)
        {
            return _context.BranchHasEmployees.FirstOrDefault(bhe => bhe.EmployeeId == employeeId);
        }

        public void RemoveBranchAssignment(BranchHasEmployee branchAssignment)
        {
            _context.BranchHasEmployees.Remove(branchAssignment);
            _context.SaveChanges();
        }

        public void UpdateBranchAssignment(BranchHasEmployee branchAssignment)
        {
            _context.BranchHasEmployees.Update(branchAssignment);
            _context.SaveChanges();
        }

    }
}
