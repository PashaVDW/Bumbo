using Azure.Core;
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
            List<BranchRequestsEmployee> requests =
                _context.BranchRequestsEmployee
                .Where(r => r.BranchId != branchId)
                .Where(r => r.RequestStatusName.Equals("In Afwachting"))
                .ToList();
            int id = 0;
            foreach (var request in requests)
            {
                id++;
                request.Id = id;
            }
            return requests;
        }
        public List<BranchRequestsEmployee> GetAllOutgoingRequests(int branchId)
        {
            List<BranchRequestsEmployee> requests =
                 _context.BranchRequestsEmployee
                .Where(r => r.BranchId == branchId)
                .ToList();
            int id = 0;
            foreach (var request in requests) {
                id++;
                request.Id = id;
            }
            return requests;
        }

        public void AcceptRequest(BranchRequestsEmployee request)
        {
            request.RequestStatusName = "Geaccepteerd";
            _context.Entry(request).Property(e => e.RequestStatusName).IsModified = true;
            //_context.Update(request);
            _context.SaveChanges();
        }

        public void RejectRequest(BranchRequestsEmployee request)
        {
            request.RequestStatusName = "Afgewezen";
            _context.Entry(request).Property(e => e.RequestStatusName).IsModified = true;
            _context.SaveChanges();
        }
    }
}
