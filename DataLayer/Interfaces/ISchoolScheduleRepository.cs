using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ISchoolScheduleRepository
    {
        void AddSchoolSchedulesForEmployee(string employeeId, List<SchoolSchedule> schedules);
        List<SchoolSchedule> getSchedulesBetweenDates(DateTime startDate, DateTime endDate, string employeeId);
        SchoolSchedule GetEmployeeDaySchoolSchedule(DateTime date, string employeeId);
        List<SchoolSchedule> GetEmployeeSchoolSchedule(string employeeId);
    }
}
