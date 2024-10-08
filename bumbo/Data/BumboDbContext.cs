using bumbo.Models;
using Microsoft.EntityFrameworkCore;

namespace bumbo.Data
{
    public class BumboDbContext : DbContext
    {
        public BumboDbContext(DbContextOptions<BumboDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
