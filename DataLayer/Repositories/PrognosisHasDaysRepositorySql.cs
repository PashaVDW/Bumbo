using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class PrognosisHasDaysRepositorySql : IPrognosisHasDaysRepository
    {
        readonly BumboDBContext _context;

        public PrognosisHasDaysRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Prognosis_has_days> GetPrognosisHasDays()
        {
            return _context.Prognosis_Has_Days.ToList();
        }

        public List<Prognosis_has_days> GetLatestPrognosis_has_days()
        {
            var latestPrognosisId = _context.Prognosis_Has_Days.Max(p => p.PrognosisId);
            return _context.Prognosis_Has_Days
                .Where(p => p.PrognosisId == latestPrognosisId)
                .ToList();
        }

        public List<Prognosis_has_days> GetPrognosisHasDaysByPrognosisId(int prognosisId)
        {
            return _context.Prognosis_Has_Days
                .Where(p => p.PrognosisId == prognosisId)
                .ToList();
        }
    }
}
