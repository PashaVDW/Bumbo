using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bumbo.Models;

namespace bumbo.Data
{
    public class BumboDBContext : IdentityDbContext<Employee>
    {
        public BumboDBContext(DbContextOptions<BumboDBContext> options)
            : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Template_has_days> Template_Has_Days { get; set; }
        public DbSet<Days> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Template_has_days>()
               .HasKey(t => new { t.Templates_id, t.Days_name });

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

            modelBuilder.Entity<Template>().HasData(
                new Template
                {

                }    
            );
            
            modelBuilder.Entity<Days>().HasData(
                new Days()
                {
                    Name = "Monday"
                },
                new Days()
                {
                    Name = "Tuesday"
                },
                new Days()
                {
                    Name = "Wednesday"
                },
                new Days()
                {
                    Name = "Thursday"
                },
                new Days()
                {
                    Name = "Friday"
                },
                new Days()
                {
                    Name = "Saturday"
                },
                new Days()
                {
                    Name = "Sunday"
                }
            );

            modelBuilder.Entity<Template>().HasData(
                new Template {
                    Id = 1,
                    Name = "Basic Package",
                    Branch_branchId = 1
                },
                new Template {
                    Id = 2,
                    Name = "Standard Package",
                    Branch_branchId = 1
                },
                new Template {
                    Id = 3,
                    Name = "Premium Package",
                    Branch_branchId = 2
                },
                new Template {
                    Id = 4,
                    Name = "Family Package",
                    Branch_branchId = 2
                },
                new Template {
                    Id = 5,
                    Name = "Weekly Special",
                    Branch_branchId = 1
                }
            );

            // Seed data for Template_has_days
            modelBuilder.Entity<Template_has_days>().HasData(
                new Template_has_days {
                    Templates_id = 1,
                    Days_name = "Monday",
                    CustomerAmount = 10,
                    ContainerAmount = 5
                },
                new Template_has_days { Templates_id = 1, Days_name = "Tuesday", CustomerAmount = 12, ContainerAmount = 6 },
                new Template_has_days { Templates_id = 1, Days_name = "Wednesday", CustomerAmount = 14, ContainerAmount = 7 },
                new Template_has_days { Templates_id = 1, Days_name = "Thursday", CustomerAmount = 11, ContainerAmount = 4 },
                new Template_has_days { Templates_id = 1, Days_name = "Friday", CustomerAmount = 13, ContainerAmount = 5 },
                new Template_has_days { Templates_id = 1, Days_name = "Saturday", CustomerAmount = 15, ContainerAmount = 8 },
                new Template_has_days { Templates_id = 1, Days_name = "Sunday", CustomerAmount = 9, ContainerAmount = 3 },

                new Template_has_days { Templates_id = 2, Days_name = "Monday", CustomerAmount = 20, ContainerAmount = 10 },
                new Template_has_days { Templates_id = 2, Days_name = "Tuesday", CustomerAmount = 18, ContainerAmount = 9 },
                new Template_has_days { Templates_id = 2, Days_name = "Wednesday", CustomerAmount = 22, ContainerAmount = 12 },
                new Template_has_days { Templates_id = 2, Days_name = "Thursday", CustomerAmount = 19, ContainerAmount = 8 },
                new Template_has_days { Templates_id = 2, Days_name = "Friday", CustomerAmount = 21, ContainerAmount = 11 },
                new Template_has_days { Templates_id = 2, Days_name = "Saturday", CustomerAmount = 17, ContainerAmount = 7 },
                new Template_has_days { Templates_id = 2, Days_name = "Sunday", CustomerAmount = 15, ContainerAmount = 6 },

                new Template_has_days { Templates_id = 3, Days_name = "Monday", CustomerAmount = 30, ContainerAmount = 15 },
                new Template_has_days { Templates_id = 3, Days_name = "Tuesday", CustomerAmount = 25, ContainerAmount = 12 },
                new Template_has_days { Templates_id = 3, Days_name = "Wednesday", CustomerAmount = 28, ContainerAmount = 14 },
                new Template_has_days { Templates_id = 3, Days_name = "Thursday", CustomerAmount = 26, ContainerAmount = 13 },
                new Template_has_days { Templates_id = 3, Days_name = "Friday", CustomerAmount = 27, ContainerAmount = 12 },
                new Template_has_days { Templates_id = 3, Days_name = "Saturday", CustomerAmount = 24, ContainerAmount = 11 },
                new Template_has_days { Templates_id = 3, Days_name = "Sunday", CustomerAmount = 23, ContainerAmount = 10 },

                new Template_has_days { Templates_id = 4, Days_name = "Monday", CustomerAmount = 15, ContainerAmount = 7 },
                new Template_has_days { Templates_id = 4, Days_name = "Tuesday", CustomerAmount = 12, ContainerAmount = 5 },
                new Template_has_days { Templates_id = 4, Days_name = "Wednesday", CustomerAmount = 14, ContainerAmount = 6 },
                new Template_has_days { Templates_id = 4, Days_name = "Thursday", CustomerAmount = 13, ContainerAmount = 5 },
                new Template_has_days { Templates_id = 4, Days_name = "Friday", CustomerAmount = 11, ContainerAmount = 4 },
                new Template_has_days { Templates_id = 4, Days_name = "Saturday", CustomerAmount = 10, ContainerAmount = 3 },
                new Template_has_days { Templates_id = 4, Days_name = "Sunday", CustomerAmount = 9, ContainerAmount = 2 },

                new Template_has_days { Templates_id = 5, Days_name = "Monday", CustomerAmount = 5, ContainerAmount = 3 },
                new Template_has_days { Templates_id = 5, Days_name = "Tuesday", CustomerAmount = 6, ContainerAmount = 2 },
                new Template_has_days { Templates_id = 5, Days_name = "Wednesday", CustomerAmount = 7, ContainerAmount = 4 },
                new Template_has_days { Templates_id = 5, Days_name = "Thursday", CustomerAmount = 8, ContainerAmount = 5 },
                new Template_has_days { Templates_id = 5, Days_name = "Friday", CustomerAmount = 10, ContainerAmount = 6 },
                new Template_has_days { Templates_id = 5, Days_name = "Saturday", CustomerAmount = 9, ContainerAmount = 5 },
                new Template_has_days { Templates_id = 5, Days_name = "Sunday", CustomerAmount = 8, ContainerAmount = 4 }
            );

        }
    }
}
