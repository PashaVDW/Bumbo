using bumbo.Models;

namespace bumbo.Interfaces
{
    public interface IWeekOverviewService
    {
        WeekOverview GetWeekOverzicht(int weekNummer);
    }
}
