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
        public DbSet<Prognosis> Prognoses { get; set; }
        public DbSet<Prognosis_has_days> Prognosis_Has_Days { get; set; }
        public DbSet<Norm> Norms { get; set; }
        public DbSet<BranchHasEmployee> BranchHasEmployees { get; set; }
        public DbSet<Function> Functions { get; set; }
        
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TemplateHasDays>()
               .HasKey(t => new { t.Templates_id, t.Days_name });
            modelBuilder.Entity<Norm>()
                .HasIndex(norm => new { norm.branchId, norm.year, norm.week, norm.activity })
                .IsUnique();

            var weekFourtyOneColi = new Norm
            {
                normId = 1,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 90
            };

            var weekFourtyOneShelve = new Norm
            {
                normId = 2,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 33
            };

            var weekFourtyOneCashier = new Norm
            {
                normId = 3,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 3
            };

            var weekFourtyOneFresh = new Norm
            {
                normId = 4,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Vers",
                normInSeconds = 7
            };

            var weekFourtyOneFronting = new Norm
            {
                normId = 5,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 2
            };

            modelBuilder.Entity<Norm>().HasData(
                weekFourtyOneColi,
                weekFourtyOneShelve,
                weekFourtyOneCashier,
                weekFourtyOneFresh,
                weekFourtyOneFronting);

            modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Germany" }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    BranchId = 1,
                    PostalCode = "1012 LG",
                    HouseNumber = "10",
                    Name = "Amsterdam Filiaal",
                    Street = "Damrak",
                    CountryName = "Netherlands"
                },
                new Branch
                {
                    BranchId = 2,
                    PostalCode = "1000",
                    HouseNumber = "20",
                    Name = "Brussels Filiaal",
                    Street = "Grote Markt",
                    CountryName = "Belgium"
                },
                new Branch
                {
                    BranchId = 3,
                    PostalCode = "1811 KH",
                    HouseNumber = "2",
                    Name = "Alkmaar Filiaal",
                    Street = "Paardenmarkt",
                    CountryName = "Netherlands"
                },
                new Branch
                {
                    BranchId = 4,
                    PostalCode = "3011 HE",
                    HouseNumber = "15",
                    Name = "Rotterdam Filiaal",
                    Street = "Botersloot",
                    CountryName = "Netherlands"
                }
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
                new Prognosis { PrognosisId = "1", WeekNr = 40, Year = 2024, BranchId = 1 },
                new Prognosis { PrognosisId = "2", WeekNr = 20, Year = 2024, BranchId = 1 }
            );

            modelBuilder.Entity<Prognosis_has_days>().HasData(
                new Prognosis_has_days { Days_name = "Maandag", PrognosisId = "1", CustomerAmount = 100, PackagesAmount = 50 },
                new Prognosis_has_days { Days_name = "Dinsdag", PrognosisId = "1", CustomerAmount = 120, PackagesAmount = 60 },
                new Prognosis_has_days { Days_name = "Woensdag", PrognosisId = "1", CustomerAmount = 130, PackagesAmount = 55 },
                new Prognosis_has_days { Days_name = "Donderdag", PrognosisId = "1", CustomerAmount = 110, PackagesAmount = 45 },
                new Prognosis_has_days { Days_name = "Vrijdag", PrognosisId = "1", CustomerAmount = 150, PackagesAmount = 70 },
                new Prognosis_has_days { Days_name = "Zaterdag", PrognosisId = "1", CustomerAmount = 160, PackagesAmount = 80 },
                new Prognosis_has_days { Days_name = "Zondag", PrognosisId = "1", CustomerAmount = 140, PackagesAmount = 65 },

                new Prognosis_has_days { Days_name = "Maandag", PrognosisId = "2", CustomerAmount = 90, PackagesAmount = 40 },
                new Prognosis_has_days { Days_name = "Dinsdag", PrognosisId = "2", CustomerAmount = 115, PackagesAmount = 55 },
                new Prognosis_has_days { Days_name = "Woensdag", PrognosisId = "2", CustomerAmount = 125, PackagesAmount = 50 },
                new Prognosis_has_days { Days_name = "Donderdag", PrognosisId = "2", CustomerAmount = 105, PackagesAmount = 42 },
                new Prognosis_has_days { Days_name = "Vrijdag", PrognosisId = "2", CustomerAmount = 140, PackagesAmount = 68 },
                new Prognosis_has_days { Days_name = "Zaterdag", PrognosisId = "2", CustomerAmount = 150, PackagesAmount = 75 },
                new Prognosis_has_days { Days_name = "Zondag", PrognosisId = "2", CustomerAmount = 130, PackagesAmount = 60 }
            );

            modelBuilder.Entity<Function>().HasData(
                new Function { FunctionName = "Cashier" },
                new Function { FunctionName = "Stocker" },
                new Function { FunctionName = "Manager" });

            var passwordHasher = new PasswordHasher<Employee>();

            var john = new Employee
            {
                Id = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // If this gives errors, make it '1' and DONT DELETE HIM FROM BRANCH
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
                Id = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                BID = "B002",
                FirstName = "Jane",
                MiddleName = "B.",
                LastName = "Smith",
                BirthDate = new DateTime(1990, 5, 15),
                PostalCode = "9271 GB",
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

            var anna = new Employee
            {
                Id = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                BID = "B003",
                FirstName = "Anna",
                MiddleName = "",
                LastName = "van Dijk",
                BirthDate = new DateTime(1992, 2, 14),
                PostalCode = "8329 SK",
                HouseNumber = 5,
                StartDate = new DateTime(2018, 6, 20),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 34567890",
                UserName = "anna.vandijk@hotmail.com",
                NormalizedUserName = "ANNA.VANDIJK@HOTMAIL.COM",
                Email = "anna.vandijk@hotmail.com",
                NormalizedEmail = "ANNA.VANDIJK@HOTMAIL.COM",
                EmailConfirmed = false
            };
            anna.PasswordHash = passwordHasher.HashPassword(anna, "PassAnna");

            var michael = new Employee
            {
                Id = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9",
                BID = "B004",
                FirstName = "Michael",
                MiddleName = "",
                LastName = "Bakker",
                BirthDate = new DateTime(1980, 12, 1),
                PostalCode = "3894 HT",
                HouseNumber = 15,
                StartDate = new DateTime(2010, 9, 5),
                IsSystemManager = false,
                ManagerOfBranchId = 3,
                PhoneNumber = "+31 6 45678901",
                UserName = "michael.bakker@gmail.com",
                NormalizedUserName = "MICHAEL.BAKKER@GMAIL.COM",
                Email = "michael.bakker@gmail.com",
                NormalizedEmail = "MICHAEL.BAKKER@GMAIL.COM",
                EmailConfirmed = false
            };
            michael.PasswordHash = passwordHasher.HashPassword(michael, "PassMicheal");

            var sarah = new Employee
            {
                Id = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0",
                BID = "B005",
                FirstName = "Sarah",
                MiddleName = "",
                LastName = "van der Ven",
                BirthDate = new DateTime(1988, 4, 10),
                PostalCode = "2933 KJ",
                HouseNumber = 8,
                StartDate = new DateTime(2017, 3, 15),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 56789012",
                UserName = "sarah.vanderven@hotmail.com",
                NormalizedUserName = "SARAH.VANDERVEN@HOTMAIL.COM",
                Email = "sarah.vanderven@hotmail.com",
                NormalizedEmail = "SARAH.VANDERVEN@HOTMAIL.COM",
                EmailConfirmed = false
            };
            sarah.PasswordHash = passwordHasher.HashPassword(sarah, "PassSarah");

            var david = new Employee
            {
                Id = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1",
                BID = "B006",
                FirstName = "David",
                MiddleName = "",
                LastName = "den Boer",
                BirthDate = new DateTime(1995, 7, 20),
                PostalCode = "4293 BF",
                HouseNumber = 30,
                StartDate = new DateTime(2020, 11, 1),
                IsSystemManager = false,
                ManagerOfBranchId = 2,
                PhoneNumber = "+31 6 67890123",
                UserName = "david.denboer@gmail.com",
                NormalizedUserName = "DAVID.DENBOER@GMAIL.COM",
                Email = "david.denboer@gmail.com",
                NormalizedEmail = "DAVID.DENBOER@GMAIL.COM",
                EmailConfirmed = false
            };
            david.PasswordHash = passwordHasher.HashPassword(david, "PassDavid");

            // Add employees to the model

            modelBuilder.Entity<Employee>().HasData(john, jane, anna, michael, sarah, david);
            
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

            var branchHasEmployeeOne = new BranchHasEmployee
            {
                BranchId = 2,
                EmployeeId = david.Id,
                StartDate = david.StartDate,
                FunctionName = "Manager"
            };
            var branchHasEmployeeTwo = new BranchHasEmployee
            {
                BranchId = 3,
                EmployeeId = michael.Id,
                StartDate = michael.StartDate,
                FunctionName = "Manager"
            };
            var branchHasEmployeeThree = new BranchHasEmployee
            {
                BranchId = 4,
                EmployeeId = anna.Id,
                StartDate = anna.StartDate,
                FunctionName = "Stocker"
            };
            var branchHasEmployeeFour = new BranchHasEmployee
            {
                BranchId = 3,
                EmployeeId = sarah.Id,
                StartDate = sarah.StartDate,
                FunctionName = "Cashier"
            };
            var branchHasEmployeeFive = new BranchHasEmployee
            {
                BranchId = 1,
                EmployeeId = jane.Id,
                StartDate = jane.StartDate,
                FunctionName = "Cashier"
            };
            modelBuilder.Entity<BranchHasEmployee>().HasData(
                branchHasEmployeeOne, 
                branchHasEmployeeTwo, 
                branchHasEmployeeThree, 
                branchHasEmployeeFour,
                branchHasEmployeeFive
            );
        }
    }
}
