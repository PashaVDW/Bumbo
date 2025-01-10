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

        public void UpdateSwitchRequest(SwitchRequest request)
        {
            var existingRequest = _context.SwitchRequest.FirstOrDefault(r =>
                r.SendToEmployeeId == request.SendToEmployeeId &&
                r.EmployeeId == request.EmployeeId &&
                r.BranchId == request.BranchId &&
                r.Date == request.Date);

            if (existingRequest != null)
            {
                existingRequest.Date = request.Date;
                _context.SaveChanges();
            }
        }

        public List<SwitchRequest> GetAllIncomingRequests(string employeeId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            return _context.SwitchRequest
                .Include(sr => sr.Schedule)
                    .ThenInclude(s => s.Department)
                .Include(sr => sr.Employee)
                .Where(sr => sr.SendToEmployeeId == employeeId && !sr.Declined && sr.Date > today)
                .ToList();
        }

        public List<SwitchRequest> GetAllOutgoingRequests(string employeeId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            return _context.SwitchRequest
                .Include(sr => sr.Schedule)
                    .ThenInclude(s => s.Department)
                .Include(sr => sr.Employee)
                .Where(sr => sr.EmployeeId == employeeId && sr.Date > today)
                .ToList();
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

        public void AddSwitchRequest(SwitchRequest switchRequest)
        {
            _context.SwitchRequest.Add(switchRequest);
            _context.SaveChanges();
        }

        public SwitchRequest GetSwitchRequest(string sendToEmployeeId, string employeeId, int branchId, DateOnly date)
        {
            return _context.SwitchRequest
                .Include(sr => sr.Schedule)
                .Include(sr => sr.Employee)
                .FirstOrDefault(sr =>
                    sr.SendToEmployeeId == sendToEmployeeId &&
                    sr.EmployeeId == employeeId &&
                    sr.BranchId == branchId &&
                    sr.Date == date);
        }

        public void RemoveSwitchRequest(SwitchRequest switchRequest)
        {
            _context.SwitchRequest.Remove(switchRequest);
            _context.SaveChanges();
        }
    }
}
