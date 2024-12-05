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

        public string AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year, int managerOfBranchId)
        {
            var prognosis = new Prognosis
            {
                PrognosisId = "prognosis_week_"+week+"_"+year,
                BranchId = managerOfBranchId,
                WeekNr = week,
                Year = year
            };

            _context.Prognoses.Add(prognosis);
            _context.SaveChanges();

            if (days.Count == CustomerAmount.Count && days.Count == PackagesAmount.Count)
            {
                foreach (var day in days)
                {
                    if (!_context.Days.Any(d => d.Name == day.Name))
                    {
                        throw new InvalidOperationException($"Day '{day.Name}' does not exist in the Days table.");
                    }

                    var prognosisHasDays = new PrognosisHasDays
                    {
                        DayName = day.Name,
                        PrognosisId = prognosis.PrognosisId,
                        CustomerAmount = CustomerAmount[days.IndexOf(day)],
                        PackagesAmount = PackagesAmount[days.IndexOf(day)]
                    };

                    _context.PrognosisHasDays.Add(prognosisHasDays);
                    _context.SaveChanges();
                }
                return prognosis.PrognosisId;
            }
            else
            {
                return null;
            }
        }

        public List<Prognosis> GetAllPrognosis()
        {
            return _context.Prognoses.ToList();
        }

        public Prognosis GetLatestPrognosis(int branchId)
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .Where(p => p.BranchId == branchId)
                .OrderByDescending(p => p.Year)
                .ThenByDescending(p => p.WeekNr)
                .ToList<Prognosis>();
            return prognosis.First();
        }

        public Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year, int branchId)
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .Where(p => p.WeekNr == weekNumber && p.Year == year && p.BranchId == branchId)
                .ToList();
            return prognosis.FirstOrDefault();
        }

        public List<PrognosisHasDaysHasDepartment> GetPrognosisDetailsByBranchWeekAndYear(int branchId, int weekNumber, int year)
        {
            return _context.PrognosisHasDaysHasDepartment
                .Include(phdd => phdd.PrognosisHasDays)
                    .ThenInclude(phd => phd.Prognosis)
                .Where(phdd => phdd.PrognosisHasDays.Prognosis.BranchId == branchId
                            && phdd.PrognosisHasDays.Prognosis.WeekNr == weekNumber
                            && phdd.PrognosisHasDays.Prognosis.Year == year)
                .ToList();
        }
    }
}
