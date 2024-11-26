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
        List<Schedule> GetSchedulesForEmployeeByDay(string employeeId, DateOnly date);
        List<Schedule> SetSchedulesSick(List<Schedule> sickSchedules);
        List<Schedule> SetSchedulesBetter(List<Schedule> sickSchedules);
        List<Schedule> GetSchedulesForEmployeeByWeek(string employeeId, List<DateOnly> weekDates);
        List<string> GetDepartments();
        void FinalizeSchedules(int branchId, List<DateOnly> weekDates);
    }
}
