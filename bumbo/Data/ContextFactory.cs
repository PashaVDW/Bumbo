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
<<<<<<< HEAD
            var builder = new DbContextOptionsBuilder<BumboDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("jules"));
=======
            var builder = new DbContextOptionsBuilder<BumboDBContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("anthony"));
>>>>>>> 8e5722d6ee676189c31dc400999d4f2fe6df2b54

            return new BumboDBContext(builder.Options);
        }
    }
}
