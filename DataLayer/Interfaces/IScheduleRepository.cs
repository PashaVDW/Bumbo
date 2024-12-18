using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataLayer.Interfaces
{
    public interface IScheduleRepository
    {
        List<Schedule> GetSchedulesForBranchByWeek(int branchId, List<DateOnly> weekDates);
        List<Schedule> GetScheduleForBranchByDay(int branchId, DateOnly dayDate);
        List<Schedule> GetSchedulesForEmployeeByDay(string employeeId, DateOnly date);
        List<Schedule> SetSchedulesSick(List<Schedule> sickSchedules);
        List<Schedule> SetSchedulesBetter(List<Schedule> sickSchedules);
        List<Schedule> GetSchedulesForEmployeeByWeek(string employeeId, List<DateOnly> weekDates);
        List<string> GetDepartments();
        bool CanRemoveEmployeeFromDay(DateOnly dayDate, string employeeId, int branchId);
        void RemoveEmployeeFromDay(DateOnly dayDate, string employeeId, int branchId);
        List<Schedule> GetWeekScheduleForEmployee(string? employeeId, DateTime monday, DateTime sunday);
        public List<Schedule> GetMonthScheduleForEmployee(string employeeId, DateTime firstDay, DateTime lastDay);
        public List<Schedule> GetRegisteredHoursInMonthScheduleForEmployee(string employeeId, DateTime firstDay, DateTime lastDay);
        void UpdateEmployeeDaySchedule(string employeeId, DateTime date, TimeOnly startTime, TimeOnly endTime, int branchId, string departmentName);
        void FinalizeSchedules(int branchId, List<DateOnly> weekDates);
        List<Schedule> GetSchedulesForEmployee(string employeeId);
        Schedule GetScheduleByEmployeeBranchDate(string employeeId, int branchId, DateOnly date);
        void UpdateSchedule(Schedule schedule);
        void RemoveSchedule(Schedule schedule);
        void AddSchedule(Schedule updatedSchedule);
        void AddHelpEmployeeToDay(Schedule schedule);
        void AddEmployee(string employeeId, int branchId, DateOnly date, string departmentName, TimeOnly startTime, TimeOnly endTime);
        bool EmployeeIsAlreadyPlanned(DateTime date, string employeeId);
    }
}
