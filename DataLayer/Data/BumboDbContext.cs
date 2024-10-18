using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bumbo.Models;
using DataLayer.Models;

namespace bumbo.Data
{
    public class BumboDBContext : IdentityDbContext<Employee>
    {
        public BumboDBContext(DbContextOptions<BumboDBContext> options)
            : base(options)
        {
        }
        public DbSet<Days> Days { get; set; }
        public DbSet<Prognosis> Prognoses { get; set; }
        public DbSet<Prognosis_has_days> Prognosis_Has_Days { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Germany" }
            );

            modelBuilder.Entity<Days>().HasData(
                new Days { Name = "Maandag" },
                new Days { Name = "Dinsdag" },
                new Days { Name = "Woensdag" },
                new Days { Name = "Donderdag" },
                new Days { Name = "Vrijdag" },
                new Days { Name = "Zaterdag" },
                new Days { Name = "Zondag" }
            );

            modelBuilder.Entity<Prognosis>().HasData(
                new Prognosis { PrognosisId = "1", WeekNr = 40, Year = 2024, BranchId = 1 }
            );

            modelBuilder.Entity<Prognosis_has_days>().HasData(
                new Prognosis_has_days { Days_name = "Maandag", PrognosisId = "1", CustomerAmount = 100, PackagesAmount = 50 },
                new Prognosis_has_days { Days_name = "Dinsdag", PrognosisId = "1", CustomerAmount = 120, PackagesAmount = 60 },
                new Prognosis_has_days { Days_name = "Woensdag", PrognosisId = "1", CustomerAmount = 130, PackagesAmount = 55 },
                new Prognosis_has_days { Days_name = "Donderdag", PrognosisId = "1", CustomerAmount = 110, PackagesAmount = 45 },
                new Prognosis_has_days { Days_name = "Vrijdag", PrognosisId = "1", CustomerAmount = 150, PackagesAmount = 70 },
                new Prognosis_has_days { Days_name = "Zaterdag", PrognosisId = "1", CustomerAmount = 160, PackagesAmount = 80 },
                new Prognosis_has_days { Days_name = "Zondag", PrognosisId = "1", CustomerAmount = 140, PackagesAmount = 65 }
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
