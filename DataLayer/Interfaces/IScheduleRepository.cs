using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IScheduleRepository
    {
        List<Schedule> GetSchedulesForBranchByWeek(int branchId, List<DateOnly> weekDates);
        List<string> GetDepartments();
    }
}
