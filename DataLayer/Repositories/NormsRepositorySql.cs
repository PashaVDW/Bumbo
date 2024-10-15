using bumbo.Data;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class NormsRepositorySql : INormsRepository
    {
        readonly BumboDBContext _context;
        public List<ReadNormViewModel> readNorms()
        {
            var query = _context.Norms
    .Where(coli => coli.activity == "Coli uitladen")
    .Join(_context.Norms.Where(shelve => shelve.activity == "Vakkenvullen"),
          coli => new { coli.branchId, coli.year, coli.week },
          shelve => new { shelve.branchId, shelve.year, shelve.week },
          (coli, shelve) => new { coli, shelve })
    .Join(_context.Norms.Where(cashier => cashier.activity == "Kassa"),
          result => new { result.coli.branchId, result.coli.year, result.coli.week },
          cashier => new { cashier.branchId, cashier.year, cashier.week },
          (result, cashier) => new { result.coli, result.shelve, cashier })
    .Join(_context.Norms.Where(fresh => fresh.activity == "Vers"),
          result => new { result.coli.branchId, result.coli.year, result.coli.week },
          fresh => new { fresh.branchId, fresh.year, fresh.week },
          (result, fresh) => new { result.coli, result.shelve, result.cashier, fresh })
    .Join(_context.Norms.Where(front => front.activity == "Spiegelen"),
          result => new { result.coli.branchId, result.coli.year, result.coli.week },
          front => new { front.branchId, front.year, front.week },
          (result, front) => new
          {
              Week = result.coli.week,
              ColiNorm = result.coli.normInSeconds,
              ShelveNorm = result.shelve.normInSeconds,
              CashierNorm = result.cashier.normInSeconds,
              FreshNorm = result.fresh.normInSeconds,
              FrontNorm = front.normInSeconds
          });

            var norms = query.ToList();
            return norms;
        }
    }
}
