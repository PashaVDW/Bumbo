using Azure.Core;
using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SwapShiftRequestRepositorySql : ISwapShiftRequestRepository
    {

        private readonly BumboDBContext _context;

        public SwapShiftRequestRepositorySql(BumboDBContext context)
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

        public List<SwitchRequest> GetAllIncomingRequests(string employeeId)
        {
            return _context.SwitchRequest
                .Include(sr => sr.Schedule)
                .Include(sr => sr.Employee)
                .Where(sr => sr.SendToEmployeeId == employeeId)
                .ToList();
        }

        public List<SwitchRequest> GetAllOutgoingRequests(string employeeId)
        {
            return _context.SwitchRequest
                .Include(sr => sr.Schedule)
                .Include(sr => sr.Employee)
                .Where(sr => sr.EmployeeId == employeeId)
                .ToList();
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

            return availableEmployees;
        }
    }
}
