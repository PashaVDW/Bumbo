using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IBranchHasEmployeeRepository
    {
        Task AddBranchHasEmployeeAsync(BranchHasEmployee branchHasEmployee);
        List<BranchHasEmployee> GetBranchesForEmployee(string employeeId);
        BranchHasEmployee GetBranchAssignment(string employeeId, int branchId);
        void RemoveBranchAssignment(BranchHasEmployee branchAssignment);
    }
}
