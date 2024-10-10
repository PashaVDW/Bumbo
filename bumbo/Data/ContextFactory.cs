using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace bumbo.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<BumboDBContext>
    {
        public ContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
        .Build();

        public BumboDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BumboDBContext>();
            var connectionString = Configuration.GetConnectionString("bumbo");
            builder.UseSqlServer(connectionString);

            return new BumboDBContext(builder.Options);
        }
    }
}
