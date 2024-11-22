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

        public DbSet<Availability> Availability { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchHasEmployee> BranchHasEmployees { get; set; }
        public DbSet<BranchRequestsEmployee> BranchRequestsEmployee { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeHasDepartment> EmployeeHasDepartment { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<LabourRules> LabourRules { get; set; }
        public DbSet<Norm> Norms { get; set; }
        public DbSet<Prognosis> Prognoses { get; set; }
        public DbSet<PrognosisHasDays> PrognosisHasDays { get; set; }
        public DbSet<PrognosisHasDaysHasDepartment> PrognosisHasDaysHasDepartment { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<SchoolSchedule> SchoolSchedule { get; set; }
        public DbSet<SwitchRequest> SwitchRequest { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateHasDays> TemplateHasDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TemplateHasDays>()
               .HasKey(t => new { t.TemplatesId, t.DaysName });
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

            modelBuilder.Entity<PrognosisHasDays>().HasData(
                new PrognosisHasDays { DayName = "Maandag", PrognosisId = "1", CustomerAmount = 100, PackagesAmount = 50 },
                new PrognosisHasDays { DayName = "Dinsdag", PrognosisId = "1", CustomerAmount = 120, PackagesAmount = 60 },
                new PrognosisHasDays { DayName = "Woensdag", PrognosisId = "1", CustomerAmount = 130, PackagesAmount = 55 },
                new PrognosisHasDays { DayName = "Donderdag", PrognosisId = "1", CustomerAmount = 110, PackagesAmount = 45 },
                new PrognosisHasDays { DayName = "Vrijdag", PrognosisId = "1", CustomerAmount = 150, PackagesAmount = 70 },
                new PrognosisHasDays { DayName = "Zaterdag", PrognosisId = "1", CustomerAmount = 160, PackagesAmount = 80 },
                new PrognosisHasDays { DayName = "Zondag", PrognosisId = "1", CustomerAmount = 140, PackagesAmount = 65 },

                new PrognosisHasDays { DayName = "Maandag", PrognosisId = "2", CustomerAmount = 90, PackagesAmount = 40 },
                new PrognosisHasDays { DayName = "Dinsdag", PrognosisId = "2", CustomerAmount = 115, PackagesAmount = 55 },
                new PrognosisHasDays { DayName = "Woensdag", PrognosisId = "2", CustomerAmount = 125, PackagesAmount = 50 },
                new PrognosisHasDays { DayName = "Donderdag", PrognosisId = "2", CustomerAmount = 105, PackagesAmount = 42 },
                new PrognosisHasDays { DayName = "Vrijdag", PrognosisId = "2", CustomerAmount = 140, PackagesAmount = 68 },
                new PrognosisHasDays { DayName = "Zaterdag", PrognosisId = "2", CustomerAmount = 150, PackagesAmount = 75 },
                new PrognosisHasDays { DayName = "Zondag", PrognosisId = "2", CustomerAmount = 130, PackagesAmount = 60 }
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
                    BranchBranchId = 1
                },
                new Template
                {
                    Id = 2,
                    Name = "Standard Package",
                    BranchBranchId = 1
                },
                new Template
                {
                    Id = 3,
                    Name = "Premium Package",
                    BranchBranchId = 2
                },
                new Template
                {
                    Id = 4,
                    Name = "Family Package",
                    BranchBranchId = 2
                },
                new Template
                {
                    Id = 5,
                    Name = "Weekly Special",
                    BranchBranchId = 1
                }
            );

            // Seed data for Template_has_days
            modelBuilder.Entity<TemplateHasDays>().HasData(
                new TemplateHasDays { TemplatesId = 1, DaysName = "Monday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Tuesday", CustomerAmount = 825, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Wednesday", CustomerAmount = 902, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Thursday", CustomerAmount = 990, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Friday", CustomerAmount = 1040, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Saturday", CustomerAmount = 953, ContainerAmount = 43 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Sunday", CustomerAmount = 872, ContainerAmount = 32 },

                new TemplateHasDays { TemplatesId = 2, DaysName = "Monday", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Tuesday", CustomerAmount = 912, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Wednesday", CustomerAmount = 902, ContainerAmount = 32 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Thursday", CustomerAmount = 940, ContainerAmount = 45 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Friday", CustomerAmount = 816, ContainerAmount = 47 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Saturday", CustomerAmount = 842, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Sunday", CustomerAmount = 885, ContainerAmount = 45 },

                new TemplateHasDays { TemplatesId = 3, DaysName = "Monday", CustomerAmount = 872, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Tuesday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Wednesday", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Thursday", CustomerAmount = 875, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Friday", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Saturday", CustomerAmount = 945, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Sunday", CustomerAmount = 880, ContainerAmount = 52 },

                new TemplateHasDays { TemplatesId = 4, DaysName = "Monday", CustomerAmount = 900, ContainerAmount = 49 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Tuesday", CustomerAmount = 903, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Wednesday", CustomerAmount = 930, ContainerAmount = 45 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Thursday", CustomerAmount = 985, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Friday", CustomerAmount = 865, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Saturday", CustomerAmount = 950, ContainerAmount = 43 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Sunday", CustomerAmount = 950, ContainerAmount = 38 },

                new TemplateHasDays { TemplatesId = 5, DaysName = "Monday", CustomerAmount = 832, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Tuesday", CustomerAmount = 935, ContainerAmount = 49 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Wednesday", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Thursday", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Friday", CustomerAmount = 872, ContainerAmount = 32 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Saturday", CustomerAmount = 771, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Sunday", CustomerAmount = 885, ContainerAmount = 52 }
            );

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdd => phdd.PrognosisHasDays)
                .WithMany(phd => phd.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdd => new { phdd.DayName, phdd.PrognosisId })
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

            modelBuilder.Entity<RequestStatus>().HasData(
                new RequestStatus() { RequestStatusName = "In Afwachting" },
                new RequestStatus() { RequestStatusName = "Afgewezen" },
                new RequestStatus() { RequestStatusName = "Geaccepteerd" }
            );

            modelBuilder.Entity<BranchRequestsEmployee>().HasData(
                new BranchRequestsEmployee
                {
                    BranchId = 1,
                    EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab",
                    RequestToBranchId = 2,
                    RequestStatusName = "In Afwachting",
                    Message = "Overplaatsing nodig vanwege projectdeadline.",
                    DateNeeded = DateTime.Now.AddDays(7),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(17, 0),
                },
                new BranchRequestsEmployee
                {
                    BranchId = 2,
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    RequestToBranchId = 1,
                    RequestStatusName = "In Afwachting",
                    Message = "Terugkeer naar oorspronkelijke vestiging.",
                    DateNeeded = DateTime.Now.AddDays(14),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(16, 0),
                }
            );

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

            modelBuilder.Entity<EmployeeHasDepartment>()
                .HasKey(ehd => new { ehd.DepartmentName, ehd.EmployeeId });

            modelBuilder.Entity<EmployeeHasDepartment>()
                .HasOne(ehd => ehd.Department)
                .WithMany(d => d.EmployeeHasDepartment)
                .HasForeignKey(ehd => ehd.DepartmentName);

            modelBuilder.Entity<EmployeeHasDepartment>()
                .HasOne(ehd => ehd.Employee)
                .WithMany(e => e.EmployeeHasDepartment)
                .HasForeignKey(ehd => ehd.EmployeeId);

            modelBuilder.Entity<Availability>()
                .HasKey(a => new { a.Date, a.EmployeeId });

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Availabilitys)
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<SchoolSchedule>()
                .HasKey(ss => new { ss.Date, ss.EmployeeId });

            modelBuilder.Entity<SchoolSchedule>()
                .HasOne(ss => ss.Employee)
                .WithMany(e => e.SchoolSchedules)
                .HasForeignKey(ss => ss.EmployeeId);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.Date, s.EmployeeId, s.BranchId, s.DepartmentName });

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Schedules)
                .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.EmployeeId, s.BranchId, s.Date });

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Schedules)
                .HasForeignKey(s => s.DepartmentName);


            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Schedules)
                .HasForeignKey(s => s.BranchId);

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasKey(phdhd => new { phdhd.DepartmentName, phdhd.DayName, phdhd.PrognosisId });

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdhd => phdhd.Department)
                .WithMany(dn => dn.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdhd => phdhd.DepartmentName);

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdhd => phdhd.PrognosisHasDays)
                .WithMany(phd => phd.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdhd => new { phdhd.DayName, phdhd.PrognosisId } );

            modelBuilder.Entity<LabourRules>()
                .HasOne(lr => lr.Country)
                .WithMany(c => c.LabourRules)
                .HasForeignKey(lr => lr.CountryName);

            modelBuilder.Entity<Norm>()
                .HasOne(n => n.Branch)
                .WithMany(b => b.Norm)
                .HasForeignKey(n => n.branchId);

            modelBuilder.Entity<LabourRules>()
                .HasKey(lr => lr.CountryName);

            modelBuilder.Entity<SwitchRequest>()
                .HasOne(sr => sr.Employee)
                .WithMany(e => e.SwitchRequests)
                .HasForeignKey(sr => sr.SendToEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SwitchRequest>()
                .HasOne(sr => sr.Schedule)
                .WithMany(s => s.SwitchRequests)
                .HasForeignKey(sr => new { sr.EmployeeId, sr.BranchId, sr.Date })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BranchRequestsEmployee>()
                .HasOne(bre => bre.Employee)
                .WithMany(e => e.BranchRequestsEmployee)
                .HasForeignKey(bre => bre.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BranchRequestsEmployee>()
                .HasOne(bre => bre.Branch)
                .WithMany(b => b.BranchRequestsEmployee)
                .HasForeignKey(bre => bre.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BranchRequestsEmployee>()
                .HasOne(bre => bre.Branch)
                .WithMany(b => b.BranchRequestsEmployee)
                .HasForeignKey(bre => bre.RequestToBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BranchRequestsEmployee>()
                .HasOne(bre => bre.RequestStatus)
                .WithMany(rs => rs.BranchRequestsEmployee)
                .HasForeignKey(bre => bre.RequestStatusName)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
