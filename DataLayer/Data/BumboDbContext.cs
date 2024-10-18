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

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateHasDays> TemplateHasDays { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<BranchHasEmployee> BranchHasEmployees { get; set; }
        public DbSet<Function> Functions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TemplateHasDays>()
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

            // Seeding Functions
            modelBuilder.Entity<Function>().HasData(
                new Function { FunctionName = "Cashier" },
                new Function { FunctionName = "Stocker" },
                new Function { FunctionName = "Manager" }
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
                IsSystemManager = true,
                ManagerOfBranchId = 1,
                PhoneNumber = "06-9876543",
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
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "06-12345678",
                UserName = "jane.smith@example.com",
                NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
                Email = "jane.smith@example.com",
                NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                EmailConfirmed = true
            };
            jane.PasswordHash = passwordHasher.HashPassword(jane, "PassJane");

            // Add employees to the model
            modelBuilder.Entity<Employee>().HasData(john, jane);
            
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
            modelBuilder.Entity<TemplateHasDays>().HasData(
                new TemplateHasDays { Templates_id = 1, Days_name = "Monday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Tuesday", CustomerAmount = 825, ContainerAmount = 52 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Wednesday", CustomerAmount = 902, ContainerAmount = 38 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Thursday", CustomerAmount = 990, ContainerAmount = 52 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Friday", CustomerAmount = 1040, ContainerAmount = 39 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Saturday", CustomerAmount = 953, ContainerAmount = 43 },
                new TemplateHasDays { Templates_id = 1, Days_name = "Sunday", CustomerAmount = 872, ContainerAmount = 32 },

                new TemplateHasDays { Templates_id = 2, Days_name = "Monday", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Tuesday", CustomerAmount = 912, ContainerAmount = 38 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Wednesday", CustomerAmount = 902, ContainerAmount = 32 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Thursday", CustomerAmount = 940, ContainerAmount = 45 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Friday", CustomerAmount = 816, ContainerAmount = 47 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Saturday", CustomerAmount = 842, ContainerAmount = 38 },
                new TemplateHasDays { Templates_id = 2, Days_name = "Sunday", CustomerAmount = 885, ContainerAmount = 45 },

                new TemplateHasDays { Templates_id = 3, Days_name = "Monday", CustomerAmount = 872, ContainerAmount = 53 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Tuesday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Wednesday", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Thursday", CustomerAmount = 875, ContainerAmount = 36 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Friday", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Saturday", CustomerAmount = 945, ContainerAmount = 53 },
                new TemplateHasDays { Templates_id = 3, Days_name = "Sunday", CustomerAmount = 880, ContainerAmount = 52 },

                new TemplateHasDays { Templates_id = 4, Days_name = "Monday", CustomerAmount = 900, ContainerAmount = 49 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Tuesday", CustomerAmount = 903, ContainerAmount = 38 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Wednesday", CustomerAmount = 930, ContainerAmount = 45 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Thursday", CustomerAmount = 985, ContainerAmount = 42 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Friday", CustomerAmount = 865, ContainerAmount = 36 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Saturday", CustomerAmount = 950, ContainerAmount = 43 },
                new TemplateHasDays { Templates_id = 4, Days_name = "Sunday", CustomerAmount = 950, ContainerAmount = 38 },

                new TemplateHasDays { Templates_id = 5, Days_name = "Monday", CustomerAmount = 832, ContainerAmount = 52 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Tuesday", CustomerAmount = 935, ContainerAmount = 49 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Wednesday", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Thursday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Friday", CustomerAmount = 872, ContainerAmount = 32 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Saturday", CustomerAmount = 771, ContainerAmount = 36 },
                new TemplateHasDays { Templates_id = 5, Days_name = "Sunday", CustomerAmount = 885, ContainerAmount = 52 }
            );

            //Relations
            // Relations
            modelBuilder.Entity<BranchHasEmployee>()
                .HasKey(bhw => new { bhw.BranchId, bhw.EmployeeId });

            modelBuilder.Entity<BranchHasEmployee>()
                .HasOne(bhw => bhw.Branch)
                .WithMany(b => b.BranchHasEmployees)
                .HasForeignKey(bhw => bhw.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BranchHasEmployee>()
                .HasOne(bhw => bhw.Employee)
                .WithMany(e => e.BranchEmployees)
                .HasForeignKey(bhw => bhw.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BranchHasEmployee>()
                .HasOne(bhw => bhw.Function)
                .WithMany()
                .HasForeignKey(bhw => bhw.FunctionName)
                .HasPrincipalKey(f => f.FunctionName)
                .IsRequired(false);



        }
    }
}
