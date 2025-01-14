﻿using bumbo.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ISwapShiftRequestRepository
    {
        public List<SwitchRequest> GetAllIncomingRequests(string employeeId);
        public List<SwitchRequest> GetAllOutgoingRequests(string employeeId);

        void AddSwitchRequest(SwitchRequest switchRequest);
        SwitchRequest GetSwitchRequest(string sendToEmployeeId, string employeeId, int branchId, DateOnly date);
        void UpdateSwitchRequest(SwitchRequest switchRequest);
        void RemoveSwitchRequest(SwitchRequest switchRequest);
    }
}
