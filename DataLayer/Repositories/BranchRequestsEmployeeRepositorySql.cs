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

        public void DeleteRequest(BranchRequestsEmployee request)
        {
            _context.BranchRequestsEmployee.Remove(request);
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public void RejectRequest(BranchRequestsEmployee request)
        {
            request.RequestStatusName = "Afgewezen";
            _context.Entry(request).Property(e => e.RequestStatusName).IsModified = true;
            _context.SaveChanges();
        }

        public List<Employee> GetAllAvailableEmployees()
        {
            List<BranchRequestsEmployee> requests = _context.BranchRequestsEmployee.ToList();
            List<Employee> employees = _context.Employees.ToList();

            var employeeIdsWithRequests = requests.Select(req => req.EmployeeId);

            List<Employee> availableEmployees = employees
                .Where(emp => !employeeIdsWithRequests.Contains(emp.Id))
                .Distinct() 
                .ToList();

            //List<Employee> availableEmployees = new List<Employee>();
            //foreach (var emp in employees)
            //{
            //    foreach (var req in requests) 
            //    {
            //        if (emp.Id == req.EmployeeId && !availableEmployees.Contains(emp))
            //        {
            //            availableEmployees.Add(emp);
            //        }
            //    }
            //}

            return availableEmployees;
        }
    }
}
