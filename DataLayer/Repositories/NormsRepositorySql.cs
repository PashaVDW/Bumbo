using bumbo.Data;
using DataLayer.Interfaces;
using DataLayer.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DataLayer.Repositories
{
    public class NormsRepositorySql : INormsRepository
    {
        readonly BumboDBContext _context;

        public NormsRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public async Task<List<Norm>> GetNorms()
        {
            return await _context.Norms.ToListAsync();
        }

        public async Task<List<NormOverviewDTO>> GetOverview()
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
                      (result, front) => new NormOverviewDTO(
                          result.coli.normId,
                          result.coli.year,
                          result.coli.week,
                          result.coli.normInSeconds/60,
                          result.shelve.normInSeconds/60,
                          result.cashier.normInSeconds,
                          result.fresh.normInSeconds,
                          front.normInSeconds
                      ));

            return await query.ToListAsync();
        }

        public async Task InsertMany(Norm[] norms)
        {
            _context.Norms.AddRange(norms);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMany(List<Norm> norms)
        {
            _context.Norms.UpdateRange(norms);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Norm>> GetSelectedNorms(int? branchId, int year, int week)
        {
            return await _context.Norms.Where(norm =>
                norm.branchId == branchId &&
                norm.year == year &&
                norm.week == week)
                .ToListAsync();
        }

        public async Task<Norm> GetNorm(int selectedNormId) 
        {
            return await _context.Norms.FirstOrDefaultAsync(norm => norm.normId == selectedNormId);
        }
    }
}
