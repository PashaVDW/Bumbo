using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRegisteredHoursRepository
    {
        void AddShift(RegisteredHours newShift);
        bool ClockOut(string employeeId, DateTime endTime);
        DateTime? GetClockedInTime(string employeeId);
    }
}
