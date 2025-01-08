﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRegisteredHoursRepository
    {
        public List<RegisteredHours> GetRegisteredHoursFromEmployee(string employeeId);  
        public List<RegisteredHours> GetRegisteredHoursInWeekFromEmployee(string employeeId, int week);  
    }
}
