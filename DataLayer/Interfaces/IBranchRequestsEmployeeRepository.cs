using bumbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBranchRequestsEmployeeRepository
    {
        public void AddRequest(BranchRequestsEmployee request);
        public void UpdateRequest(BranchRequestsEmployee request);
        public List<BranchRequestsEmployee> GetAllIncomingRequests(int branchId);
        public List<BranchRequestsEmployee> GetAllOutgoingRequests(int branchId);
    }
}
