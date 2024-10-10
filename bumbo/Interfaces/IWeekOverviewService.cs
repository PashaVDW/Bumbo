using bumbo.Models;

namespace bumbo.Interfaces
{
    public interface IWeekOverviewService
    {
        WeekOverview GetWeekOverview(int weekNummer);
    }
}
