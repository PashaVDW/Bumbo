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

        public List<Prognosis_has_days> GetPrognosis_has_days()
        {
            return _context.Prognosis_Has_Days.ToList();
        }
    }
}
