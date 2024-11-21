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

        public List<Prognosis> GetAllPrognosis()
        {
            return _context.Prognoses.ToList();
        }

        public Prognosis GetLatestPrognosis()
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .OrderByDescending(p => p.Year)
                .ThenByDescending(p => p.WeekNr)
                .ToList<Prognosis>();
            return prognosis.First();
        }

        public Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year)
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .Where(p => p.WeekNr == weekNumber && p.Year == year)
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
