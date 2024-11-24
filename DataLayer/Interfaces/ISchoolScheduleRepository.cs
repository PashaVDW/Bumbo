using DataLayer.Models;

public interface ISchoolScheduleRepository
{
    void AddSchoolSchedulesForEmployee(string employeeId, List<SchoolSchedule> schedules);
    SchoolSchedule GetEmployeeDaySchoolSchedule(DateTime date, string employeeId);
}
