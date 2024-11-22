using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class BranchRequestsEmployeeRepositorySql : IBranchRequestsEmployeeRepository
    {

        private readonly BumboDBContext _context;

        public BranchRequestsEmployeeRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public void AddRequest(BranchRequestsEmployee request)
        {
            _context.BranchRequestsEmployee.Add(request);
            _context.SaveChanges();
        }

        public void UpdateRequest(BranchRequestsEmployee request)
        {
            _context.BranchRequestsEmployee.Update(request);
            _context.SaveChanges();
        }

        public List<BranchRequestsEmployee> GetAllIncomingRequests(int branchId)
        {
            return _context.BranchRequestsEmployee.ToList();
        }
        public List<BranchRequestsEmployee> GetAllOutgoingRequests(int branchId)
        {
            return _context.BranchRequestsEmployee.ToList();
        }
    }
}
