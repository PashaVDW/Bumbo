using DataLayer.Models;

public interface ISchoolScheduleRepository
{
    void AddSchoolSchedulesForEmployee(string employeeId, List<SchoolSchedule> schedules);
}
