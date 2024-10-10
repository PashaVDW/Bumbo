using bumbo.Interfaces;
using bumbo.Models;

namespace bumbo.Services
{
    public class WeekOverviewService : IWeekOverviewService
    {
        public WeekOverview GetWeekOverzicht(int weekNummer)
        {
            List<DayOverview> days = new List<DayOverview>
            {
                new DayOverview { Id = 1, dayName = "Ma", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 2, dayName = "Di", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 3, dayName = "Wo", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 4, dayName = "Do", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 5, dayName = "Vr", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 6, dayName = "Za", customerAmount = 200, packagesAmount = 10},
                new DayOverview { Id = 7, dayName = "Zo", customerAmount = 200, packagesAmount = 10}
            };

            return new WeekOverview{ Id = 1, weekNumber = weekNummer, days = days};
        }
    }
}
