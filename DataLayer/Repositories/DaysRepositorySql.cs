using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DaysRepositorySql : IDaysRepository
    {
        private readonly BumboDBContext _context;

        public DaysRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public async Task<Days> GetByNameAsync(string name)
        {
            return await _context.Days
                .FirstOrDefaultAsync(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
