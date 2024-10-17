﻿using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IBranchHasEmployeeRepository
    {
        Task AddBranchHasEmployeeAsync(BranchHasEmployee branchHasEmployee);
        List<BranchHasEmployee> GetBranchesForEmployee(string employeeId);
    }
}
