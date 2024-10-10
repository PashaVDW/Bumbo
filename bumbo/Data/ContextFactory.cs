using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace bumbo.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<BumboDbContext>
    {
        public ContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
        .Build();

        public BumboDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BumboDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("jules"));

            return new BumboDbContext(builder.Options);
        }
    }
}
