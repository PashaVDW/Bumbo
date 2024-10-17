using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bumbo.Models;

namespace bumbo.Data
{
    public class BumboDBContext : IdentityDbContext<Employee>
    {
        public BumboDBContext(DbContextOptions<BumboDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Norm> Norms { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Norm>()
                .HasIndex(norm => new { norm.branchId, norm.year, norm.week, norm.activity })
                .IsUnique();

            modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Germany" }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    BranchId = 1,
                    PostalCode = "12345",
                    HouseNumber = "10",
                    Name = "Amsterdam Branch",
                    Street = "Damrak",
                    CountryName = "Netherlands"
                },
                new Branch
                {
                    BranchId = 2,
                    PostalCode = "67890",
                    HouseNumber = "20",
                    Name = "Brussels Branch",
                    Street = "Grand Place",
                    CountryName = "Belgium"
                }
            );

            var passwordHasher = new PasswordHasher<Employee>();

            var john = new Employee
            {
                Id = "1",
                BID = "B001",
                FirstName = "John",
                MiddleName = "A.",
                LastName = "Doe",
                BirthDate = new DateTime(1985, 2, 20),
                PostalCode = "12345",
                HouseNumber = 10,
                StartDate = new DateTime(2010, 1, 1),
                FunctionName = "Manager",
                IsSystemManager = true,
                ManagerOfBranchId = 1,
                UserName = "john.doe@example.com",
                NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
                Email = "john.doe@example.com",
                NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                EmailConfirmed = true
            };
            john.PasswordHash = passwordHasher.HashPassword(john, "PassJohn");

            var jane = new Employee
            {
                Id = "2",
                BID = "B002",
                FirstName = "Jane",
                MiddleName = "B.",
                LastName = "Smith",
                BirthDate = new DateTime(1990, 5, 15),
                PostalCode = "54321",
                HouseNumber = 22,
                StartDate = new DateTime(2012, 4, 1),
                FunctionName = "Cashier",
                IsSystemManager = false,
                ManagerOfBranchId = null,  // Jane is not a manager of any branch
                UserName = "jane.smith@example.com",
                NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
                Email = "jane.smith@example.com",
                NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                EmailConfirmed = true
            };
            jane.PasswordHash = passwordHasher.HashPassword(jane, "PassJane");

            // Add employees to the model
            modelBuilder.Entity<Employee>().HasData(john, jane);
        }
    }
}
