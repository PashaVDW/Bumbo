using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PrognosisRepositorySql : IPrognosisRepository
    {
        readonly BumboDBContext _context;

        public PrognosisRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public async Task<Prognosis> GetPrognosisForCurrentWeekAsync(int branchId)
        {
            var currentDate = DateTime.Now;

            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
            int currentWeek = currentCulture.Calendar.GetWeekOfYear(
                currentDate,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek
            );
            int currentYear = currentDate.Year;

            var prognosis = await _context.Prognoses
                .Include(p => p.Prognosis_Has_Days)
                .Include(p => p.Branch)
                .Where(p => p.WeekNr == currentWeek && p.Year == currentYear && p.BranchId == branchId)
                .FirstOrDefaultAsync();

            return prognosis;
        }

        public void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year)
        {
            var prognosis = new Prognosis
            {
                BranchId = 1,
                WeekNr = week,
                Year = year
            };

            _context.Prognoses.Add(prognosis);
            _context.SaveChanges();

            if (days.Count == CustomerAmount.Count && days.Count == PackagesAmount.Count)
            {
                foreach (var day in days)
                {
                    // Check if the day exists in the Days table
                    if (!_context.Days.Any(d => d.Name == day.Name))
                    {
                        throw new InvalidOperationException($"Day '{day.Name}' does not exist in the Days table.");
                    }

                    var prognosisHasDays = new Prognosis_has_days
                    {
                        Days_name = day.Name,
                        PrognosisId = prognosis.PrognosisId,
                        CustomerAmount = CustomerAmount[days.IndexOf(day)],
                        PackagesAmount = PackagesAmount[days.IndexOf(day)]
                    };

                    _context.Prognosis_Has_Days.Add(prognosisHasDays);
                }

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Days, CustomerAmount, and PackagesAmount lists must have the same number of elements.");
            }
        }
    }
}