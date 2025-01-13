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
        public void DeleteRequest(BranchRequestsEmployee request);
        public List<BranchRequestsEmployee> GetAllIncomingRequests(int branchId);
        public List<BranchRequestsEmployee> GetAllOutgoingRequests(int branchId);
        public void AcceptRequest(BranchRequestsEmployee request);
        public void RejectRequest(BranchRequestsEmployee request);
        public List<Employee> GetAllAvailableEmployees();
        void RemoveOutgoingRequest(BranchRequestsEmployee request);
    }
}
