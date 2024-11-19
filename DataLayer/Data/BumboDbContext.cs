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
        public DbSet<Prognosis_has_days_has_Department> prognosis_Has_Days_Has_Departments { get; set; }
        public DbSet<Department> Department { get; set; }
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
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(9, 0, 0),
                    ClosingTime = new TimeOnly(18, 0, 0)
                },
                new Branch
                {
                    BranchId = 2,
                    PostalCode = "1000",
                    HouseNumber = "20",
                    Name = "Brussels Filiaal",
                    Street = "Grote Markt",
                    CountryName = "Belgium",
                    OpeningTime = new TimeOnly(8, 0, 0),
                    ClosingTime = new TimeOnly(17, 0, 0)
                },
                new Branch
                {
                    BranchId = 3,
                    PostalCode = "1811 KH",
                    HouseNumber = "2",
                    Name = "Alkmaar Filiaal",
                    Street = "Paardenmarkt",
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(9, 0, 0),
                    ClosingTime = new TimeOnly(21, 0, 0)
                },
                new Branch
                {
                    BranchId = 4,
                    PostalCode = "3011 HE",
                    HouseNumber = "15",
                    Name = "Rotterdam Filiaal",
                    Street = "Botersloot",
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(9, 0, 0),
                    ClosingTime = new TimeOnly(17, 0, 0)
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
                Id = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3",
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

            var darlon = new Employee
            {
                Id = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                BID = "B003",
                FirstName = "Darlon",
                MiddleName = "",
                LastName = "van Dijk",
                BirthDate = new DateTime(1992, 2, 14),
                PostalCode = "8329 SK",
                HouseNumber = 5,
                StartDate = new DateTime(2018, 6, 20),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 34567890",
                UserName = "darlon.vandijk@hotmail.com",
                NormalizedUserName = "DARLON.VANDIJK@HOTMAIL.COM",
                Email = "darlon.vandijk@hotmail.com",
                NormalizedEmail = "DARLON.VANDIJK@HOTMAIL.COM",
                EmailConfirmed = true
            };
            darlon.PasswordHash = passwordHasher.HashPassword(darlon, "PassDarlon");

            var pasha = new Employee
            {
                Id = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9",
                BID = "B004",
                FirstName = "Pasha",
                MiddleName = "",
                LastName = "Bakker",
                BirthDate = new DateTime(1980, 12, 1),
                PostalCode = "3894 HT",
                HouseNumber = 15,
                StartDate = new DateTime(2010, 9, 5),
                IsSystemManager = false,
                ManagerOfBranchId = 3,
                PhoneNumber = "+31 6 45678901",
                UserName = "pasha.bakker@gmail.com",
                NormalizedUserName = "PASHA.BAKKER@GMAIL.COM",
                Email = "pasha.bakker@gmail.com",
                NormalizedEmail = "PASHA.BAKKER@GMAIL.COM",
                EmailConfirmed = false
            };
            pasha.PasswordHash = passwordHasher.HashPassword(pasha, "PassPasha");

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

            var anthony = new Employee
            {
                Id = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                BID = "B012",
                FirstName = "Anthony",
                MiddleName = "",
                LastName = "Ross",
                BirthDate = new DateTime(1993, 3, 5),
                PostalCode = "2234 AB",
                HouseNumber = 7,
                StartDate = DateTime.Now,
                IsSystemManager = false,
                ManagerOfBranchId = 1,
                PhoneNumber = "+31 6 12345678",
                UserName = "anthony.ross@example.com",
                NormalizedUserName = "ANTHONY.ROSS@EXAMPLE.COM",
                Email = "anthony.ross@example.com",
                NormalizedEmail = "ANTHONY.ROSS@EXAMPLE.COM",
                EmailConfirmed = true
            };
            anthony.PasswordHash = passwordHasher.HashPassword(anthony, "PassAnthony");

            var douwe = new Employee
            {
                Id = "b2c2d2e2-2222-3333-4444-5555abcdefab",
                BID = "B013",
                FirstName = "Douwe",
                MiddleName = "",
                LastName = "Jansen",
                BirthDate = new DateTime(1987, 9, 10),
                PostalCode = "3345 CD",
                HouseNumber = 12,
                StartDate = DateTime.Now,
                IsSystemManager = false,
                ManagerOfBranchId = 2,
                PhoneNumber = "+31 6 87654321",
                UserName = "douwe.jansen@example.com",
                NormalizedUserName = "DOUWE.JANSEN@EXAMPLE.COM",
                Email = "douwe.jansen@example.com",
                NormalizedEmail = "DOUWE.JANSEN@EXAMPLE.COM",
                EmailConfirmed = true
            };
            douwe.PasswordHash = passwordHasher.HashPassword(douwe, "PassDouwe");

            // Add employees to the model

            modelBuilder.Entity<Employee>().HasData(john, jane, darlon, pasha, sarah, david, anthony, douwe);

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
                new Template
                {
                    Id = 1,
                    Name = "Basic Package",
                    Branch_branchId = 1
                },
                new Template
                {
                    Id = 2,
                    Name = "Standard Package",
                    Branch_branchId = 1
                },
                new Template
                {
                    Id = 3,
                    Name = "Premium Package",
                    Branch_branchId = 2
                },
                new Template
                {
                    Id = 4,
                    Name = "Family Package",
                    Branch_branchId = 2
                },
                new Template
                {
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

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentName = "Coli uitladen" },
                new Department { DepartmentName = "Vakkenvullen" },
                new Department { DepartmentName = "Kassa" },
                new Department { DepartmentName = "Vers" },
                new Department { DepartmentName = "Spiegelen " });

            modelBuilder.Entity<Prognosis_has_days_has_Department>().HasData(
                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Maandag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 24 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Maandag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 28 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Maandag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 32 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Maandag", PrognosisId = "1", AmountWorkersNeeded = 2, HoursWorkNeeded = 16 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Maandag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 20 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Dinsdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 25 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Dinsdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 30 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Dinsdag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 35 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Dinsdag", PrognosisId = "1", AmountWorkersNeeded = 2, HoursWorkNeeded = 18 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Dinsdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 22 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Woensdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 26 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Woensdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 29 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Woensdag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 34 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Woensdag", PrognosisId = "1", AmountWorkersNeeded = 2, HoursWorkNeeded = 17 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Woensdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 21 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Donderdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 24 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Donderdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 27 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Donderdag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 31 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Donderdag", PrognosisId = "1", AmountWorkersNeeded = 2, HoursWorkNeeded = 15 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Donderdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 19 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Vrijdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 28 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Vrijdag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 32 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Vrijdag", PrognosisId = "1", AmountWorkersNeeded = 6, HoursWorkNeeded = 36 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Vrijdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 20 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Vrijdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 24 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Zaterdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 30 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Zaterdag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 35 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Zaterdag", PrognosisId = "1", AmountWorkersNeeded = 6, HoursWorkNeeded = 38 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Zaterdag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 22 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Zaterdag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 26 },

                new Prognosis_has_days_has_Department { DepartmentName = "Coli uitladen", Days_name = "Zondag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 27 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vakkenvullen", Days_name = "Zondag", PrognosisId = "1", AmountWorkersNeeded = 4, HoursWorkNeeded = 30 },
                new Prognosis_has_days_has_Department { DepartmentName = "Kassa", Days_name = "Zondag", PrognosisId = "1", AmountWorkersNeeded = 5, HoursWorkNeeded = 34 },
                new Prognosis_has_days_has_Department { DepartmentName = "Vers", Days_name = "Zondag", PrognosisId = "1", AmountWorkersNeeded = 2, HoursWorkNeeded = 18 },
                new Prognosis_has_days_has_Department { DepartmentName = "Spiegelen", Days_name = "Zondag", PrognosisId = "1", AmountWorkersNeeded = 3, HoursWorkNeeded = 22 }
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

            modelBuilder.Entity<Prognosis_has_days_has_Department>()
                .HasOne(phdd => phdd.Prognosis_Has_Days)
                .WithMany(phd => phd.Prognosis_Has_Days_Has_Department)
                .HasForeignKey(phdd => new { phdd.Days_name, phdd.PrognosisId })
                .OnDelete(DeleteBehavior.Restrict);

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
                EmployeeId = pasha.Id,
                StartDate = pasha.StartDate,
                FunctionName = "Manager"
            };
            var branchHasEmployeeThree = new BranchHasEmployee
            {
                BranchId = 4,
                EmployeeId = darlon.Id,
                StartDate = darlon.StartDate,
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
                EmployeeId = anthony.Id,
                StartDate = anthony.StartDate,
                FunctionName = "Cashier"
            };
            var branchHasEmployeeSix = new BranchHasEmployee
            {
                BranchId = 2,
                EmployeeId = douwe.Id,
                StartDate = douwe.StartDate,
                FunctionName = "Stocker"
            };

            modelBuilder.Entity<BranchHasEmployee>().HasData(
                branchHasEmployeeOne,
                branchHasEmployeeTwo,
                branchHasEmployeeThree,
                branchHasEmployeeFour,
                branchHasEmployeeFive,
                branchHasEmployeeSix
            );

        }
    }
}
