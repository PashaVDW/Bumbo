using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class PrognosisHasDaysRepositorySql : IPrognosisHasDaysRepository
    {
        readonly BumboDBContext _context;

        public PrognosisHasDaysRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<PrognosisHasDays> GetPrognosisHasDays()
        {
            return _context.PrognosisHasDays.ToList();
        }

        public List<PrognosisHasDays> GetLatestPrognosis_has_days()
        {
            var latestPrognosisId = _context.PrognosisHasDays.Max(p => p.PrognosisId);
            return _context.PrognosisHasDays
                .Where(p => p.PrognosisId == latestPrognosisId)
                .ToList();
        }

        public List<PrognosisHasDays> GetPrognosisHasDaysByPrognosisId(string prognosisId)
        {
            return _context.PrognosisHasDays
                .Where(p => p.PrognosisId == prognosisId)
                .ToList();
        }

        public void UpdatePrognosisHasDays(List<PrognosisHasDays> prognosisDayList)
        {
            _context.PrognosisHasDays.UpdateRange(prognosisDayList);

            _context.SaveChanges();
        }
    }
}
