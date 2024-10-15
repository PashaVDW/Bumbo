using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class NormsRepositorySql : INormsRepository
    {
        readonly BumboDBContext _context;
        public List<Norm> GetNorms()
        {
            return _context.Norms.ToList();
        }
    }
}
