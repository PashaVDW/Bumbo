using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bumbo.Models;
using DataLayer.Models;
using System.Reflection.Emit;
using System.Xml;

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
        public DbSet<Department> Departments { get; set; }
        public DbSet<RegisteredHours> RegisteredHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TemplateHasDays>()
               .HasKey(t => new { t.TemplatesId, t.DaysName });
            modelBuilder.Entity<Norm>()
                .HasIndex(norm => new { norm.branchId, norm.year, norm.week, norm.activity })
                .IsUnique();

            //seeddata
            var weekFourtyOneColi = new Norm
            {
                normId = 1,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 300
            };

            var weekFourtyOneShelve = new Norm
            {
                normId = 2,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 240
            };

            var weekFourtyOneCashier = new Norm
            {
                normId = 3,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyOneFresh = new Norm
            {
                normId = 4,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyOneFronting = new Norm
            {
                normId = 5,
                branchId = 1,
                week = 41,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 30
            };

            var weekFourtyTwoColi = new Norm
            {
                normId = 6,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 320
            };

            var weekFourtyTwoShelve = new Norm
            {
                normId = 7,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 260
            };

            var weekFourtyTwoCashier = new Norm
            {
                normId = 8,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 2
            };

            var weekFourtyTwoFresh = new Norm
            {
                normId = 9,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Vers",
                normInSeconds = 2
            };

            var weekFourtyTwoFronting = new Norm
            {
                normId = 10,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 35
            };

            var weekFourtyThreeColi = new Norm
            {
                normId = 11,
                branchId = 1,
                week = 43,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 310
            };

            var weekFourtyThreeShelve = new Norm
            {
                normId = 12,
                branchId = 1,
                week = 43,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 250
            };

            var weekFourtyThreeCashier = new Norm
            {
                normId = 13,
                branchId = 1,
                week = 43,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyThreeFresh = new Norm
            {
                normId = 14,
                branchId = 1,
                week = 43,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyThreeFronting = new Norm
            {
                normId = 15,
                branchId = 1,
                week = 43,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 40
            };

            var weekFourtyFourColi = new Norm
            {
                normId = 16,
                branchId = 1,
                week = 44,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 300
            };

            var weekFourtyFourShelve = new Norm
            {
                normId = 17,
                branchId = 1,
                week = 44,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 240
            };

            var weekFourtyFourCashier = new Norm
            {
                normId = 18,
                branchId = 1,
                week = 44,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyFourFresh = new Norm
            {
                normId = 19,
                branchId = 1,
                week = 44,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyFourFronting = new Norm
            {
                normId = 20,
                branchId = 1,
                week = 44,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 30
            };

            var weekFourtyFiveColi = new Norm
            {
                normId = 21,
                branchId = 1,
                week = 45,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 290
            };

            var weekFourtyFiveShelve = new Norm
            {
                normId = 22,
                branchId = 1,
                week = 45,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 230
            };

            var weekFourtyFiveCashier = new Norm
            {
                normId = 23,
                branchId = 1,
                week = 45,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyFiveFresh = new Norm
            {
                normId = 24,
                branchId = 1,
                week = 45,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyFiveFronting = new Norm
            {
                normId = 25,
                branchId = 1,
                week = 45,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 25
            };

            var weekFourtySixColi = new Norm
            {
                normId = 26,
                branchId = 1,
                week = 46,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 305
            };

            var weekFourtySixShelve = new Norm
            {
                normId = 27,
                branchId = 1,
                week = 46,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 245
            };

            var weekFourtySixCashier = new Norm
            {
                normId = 28,
                branchId = 1,
                week = 46,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtySixFresh = new Norm
            {
                normId = 29,
                branchId = 1,
                week = 46,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtySixFronting = new Norm
            {
                normId = 30,
                branchId = 1,
                week = 46,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 33
            };

            var weekFourtySevenColi = new Norm
            {
                normId = 31,
                branchId = 1,
                week = 47,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 310
            };

            var weekFourtySevenShelve = new Norm
            {
                normId = 32,
                branchId = 1,
                week = 47,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 250
            };

            var weekFourtySevenCashier = new Norm
            {
                normId = 33,
                branchId = 1,
                week = 47,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtySevenFresh = new Norm
            {
                normId = 34,
                branchId = 1,
                week = 47,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtySevenFronting = new Norm
            {
                normId = 35,
                branchId = 1,
                week = 47,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 30
            };

            var weekFourtyEightColi = new Norm
            {
                normId = 36,
                branchId = 1,
                week = 48,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 280
            };

            var weekFourtyEightShelve = new Norm
            {
                normId = 37,
                branchId = 1,
                week = 48,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 220
            };

            var weekFourtyEightCashier = new Norm
            {
                normId = 38,
                branchId = 1,
                week = 48,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyEightFresh = new Norm
            {
                normId = 39,
                branchId = 1,
                week = 48,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyEightFronting = new Norm
            {
                normId = 40,
                branchId = 1,
                week = 48,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 28
            };

            var weekFourtyNineColi = new Norm
            {
                normId = 41,
                branchId = 1,
                week = 49,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 295
            };

            var weekFourtyNineShelve = new Norm
            {
                normId = 42,
                branchId = 1,
                week = 49,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 235
            };

            var weekFourtyNineCashier = new Norm
            {
                normId = 43,
                branchId = 1,
                week = 49,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFourtyNineFresh = new Norm
            {
                normId = 44,
                branchId = 1,
                week = 49,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFourtyNineFronting = new Norm
            {
                normId = 45,
                branchId = 1,
                week = 49,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 32
            };

            var weekFiftyColi = new Norm
            {
                normId = 46,
                branchId = 1,
                week = 50,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 300
            };

            var weekFiftyShelve = new Norm
            {
                normId = 47,
                branchId = 1,
                week = 50,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 240
            };

            var weekFiftyCashier = new Norm
            {
                normId = 48,
                branchId = 1,
                week = 50,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFiftyFresh = new Norm
            {
                normId = 49,
                branchId = 1,
                week = 50,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFiftyFronting = new Norm
            {
                normId = 50,
                branchId = 1,
                week = 50,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 30
            };

            var weekFiftyOneColi = new Norm
            {
                normId = 51,
                branchId = 1,
                week = 51,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 290
            };

            var weekFiftyOneShelve = new Norm
            {
                normId = 52,
                branchId = 1,
                week = 51,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 230
            };

            var weekFiftyOneCashier = new Norm
            {
                normId = 53,
                branchId = 1,
                week = 51,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFiftyOneFresh = new Norm
            {
                normId = 54,
                branchId = 1,
                week = 51,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFiftyOneFronting = new Norm
            {
                normId = 55,
                branchId = 1,
                week = 51,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 28
            };

            var weekFiftyTwoColi = new Norm
            {
                normId = 56,
                branchId = 1,
                week = 52,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 310
            };

            var weekFiftyTwoShelve = new Norm
            {
                normId = 57,
                branchId = 1,
                week = 52,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 250
            };

            var weekFiftyTwoCashier = new Norm
            {
                normId = 58,
                branchId = 1,
                week = 52,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFiftyTwoFresh = new Norm
            {
                normId = 59,
                branchId = 1,
                week = 52,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFiftyTwoFronting = new Norm
            {
                normId = 60,
                branchId = 1,
                week = 52,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 32
            };

            var weekFiftyThreeColi = new Norm
            {
                normId = 61,
                branchId = 1,
                week = 53,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 300
            };

            var weekFiftyThreeShelve = new Norm
            {
                normId = 62,
                branchId = 1,
                week = 53,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 240
            };

            var weekFiftyThreeCashier = new Norm
            {
                normId = 63,
                branchId = 1,
                week = 53,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 1
            };

            var weekFiftyThreeFresh = new Norm
            {
                normId = 64,
                branchId = 1,
                week = 53,
                year = 2024,
                activity = "Vers",
                normInSeconds = 1
            };

            var weekFiftyThreeFronting = new Norm
            {
                normId = 65,
                branchId = 1,
                week = 53,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 30
            };

            modelBuilder.Entity<Norm>().HasData(
                weekFourtyOneColi,
                weekFourtyOneShelve,
                weekFourtyOneCashier,
                weekFourtyOneFresh,
                weekFourtyOneFronting,
                weekFourtyTwoColi,
                weekFourtyTwoShelve,
                weekFourtyTwoCashier,
                weekFourtyTwoFresh,
                weekFourtyTwoFronting,
                weekFourtyThreeColi,
                weekFourtyThreeShelve,
                weekFourtyThreeCashier,
                weekFourtyThreeFresh,
                weekFourtyThreeFronting,
                weekFourtyFourColi,
                weekFourtyFourShelve,
                weekFourtyFourCashier,
                weekFourtyFourFresh,
                weekFourtyFourFronting,
                weekFourtyFiveColi,
                weekFourtyFiveShelve,
                weekFourtyFiveCashier,
                weekFourtyFiveFresh,
                weekFourtyFiveFronting,
                weekFourtySixColi,
                weekFourtySixShelve,
                weekFourtySixCashier,
                weekFourtySixFresh,
                weekFourtySixFronting,
                weekFourtySevenColi,
                weekFourtySevenShelve,
                weekFourtySevenCashier,
                weekFourtySevenFresh,
                weekFourtySevenFronting,
                weekFourtyEightColi,
                weekFourtyEightShelve,
                weekFourtyEightCashier,
                weekFourtyEightFresh,
                weekFourtyEightFronting,
                weekFourtyNineColi,
                weekFourtyNineShelve,
                weekFourtyNineCashier,
                weekFourtyNineFresh,
                weekFourtyNineFronting,
                weekFiftyColi,
                weekFiftyShelve,
                weekFiftyCashier,
                weekFiftyFresh,
                weekFiftyFronting,
                weekFiftyOneColi,
                weekFiftyOneShelve,
                weekFiftyOneCashier,
                weekFiftyOneFresh,
                weekFiftyOneFronting,
                weekFiftyTwoColi,
                weekFiftyTwoShelve,
                weekFiftyTwoCashier,
                weekFiftyTwoFresh,
                weekFiftyTwoFronting,
                weekFiftyThreeColi,
                weekFiftyThreeShelve,
                weekFiftyThreeCashier,
                weekFiftyThreeFresh,
                weekFiftyThreeFronting);

            modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Germany" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentName = "Vakkenvullen" },
                new Department { DepartmentName = "Kassa" },
                new Department { DepartmentName = "Vers" }
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
                new Prognosis { PrognosisId = "prognosis_week_40_2024", WeekNr = 40, Year = 2024, BranchId = 1 },
                new Prognosis { PrognosisId = "prognosis_week_20_2024", WeekNr = 20, Year = 2024, BranchId = 1 }
            );

            modelBuilder.Entity<PrognosisHasDays>().HasData(
                new PrognosisHasDays { DayName = "Maandag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 100, PackagesAmount = 50 },
                new PrognosisHasDays { DayName = "Dinsdag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 120, PackagesAmount = 60 },
                new PrognosisHasDays { DayName = "Woensdag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 130, PackagesAmount = 55 },
                new PrognosisHasDays { DayName = "Donderdag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 110, PackagesAmount = 45 },
                new PrognosisHasDays { DayName = "Vrijdag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 150, PackagesAmount = 70 },
                new PrognosisHasDays { DayName = "Zaterdag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 160, PackagesAmount = 80 },
                new PrognosisHasDays { DayName = "Zondag", PrognosisId = "prognosis_week_40_2024", CustomerAmount = 140, PackagesAmount = 65 },

                new PrognosisHasDays { DayName = "Maandag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 90, PackagesAmount = 40 },
                new PrognosisHasDays { DayName = "Dinsdag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 115, PackagesAmount = 55 },
                new PrognosisHasDays { DayName = "Woensdag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 125, PackagesAmount = 50 },
                new PrognosisHasDays { DayName = "Donderdag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 105, PackagesAmount = 42 },
                new PrognosisHasDays { DayName = "Vrijdag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 140, PackagesAmount = 68 },
                new PrognosisHasDays { DayName = "Zaterdag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 150, PackagesAmount = 75 },
                new PrognosisHasDays { DayName = "Zondag", PrognosisId = "prognosis_week_20_2024", CustomerAmount = 130, PackagesAmount = 60 }
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
                HouseNumber = "10",
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
                HouseNumber = "22",
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
                HouseNumber = "5",
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
                HouseNumber = "15",
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
                HouseNumber = "8",
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
                HouseNumber = "30",
                StartDate = new DateTime(2020, 11, 1),
                IsSystemManager = false,
                ManagerOfBranchId = null,
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
                HouseNumber = "7",
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
                HouseNumber = "12",
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

            modelBuilder.Entity<EmployeeHasDepartment>().HasData(
                
                new EmployeeHasDepartment() 
                { 
                    DepartmentName = "Vakkenvullen",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Kassa", 
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vers",
                    EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Kassa",
                    EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vers",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vakkenvullen",
                    EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Kassa",
                    EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1"
                }

                );

            modelBuilder.Entity<Template>().HasData(
                new Template
                {
                    Id = 1,
                    Name = "Groot bijbestellen",
                    BranchBranchId = 1
                },
                new Template
                {
                    Id = 2,
                    Name = "Standaard week",
                    BranchBranchId = 1
                },
                new Template
                {
                    Id = 3,
                    Name = "vakantie week",
                    BranchBranchId = 2
                },
                new Template
                {
                    Id = 4,
                    Name = "rustige week",
                    BranchBranchId = 2
                },
                new Template
                {
                    Id = 5,
                    Name = "aanbiedingen ombouw week",
                    BranchBranchId = 1
                }
            );

            // Seed data for Template_has_days
            modelBuilder.Entity<TemplateHasDays>().HasData(
                new TemplateHasDays { TemplatesId = 1, DaysName = "Maandag", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Dinsdag", CustomerAmount = 825, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Woensdag", CustomerAmount = 902, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Donderdag", CustomerAmount = 990, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Vrijdag", CustomerAmount = 1040, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Zaterdag", CustomerAmount = 953, ContainerAmount = 43 },
                new TemplateHasDays { TemplatesId = 1, DaysName = "Zondag", CustomerAmount = 872, ContainerAmount = 32 },

                new TemplateHasDays { TemplatesId = 2, DaysName = "Maandag", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Dinsdag", CustomerAmount = 912, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Woensdag", CustomerAmount = 902, ContainerAmount = 32 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Donderdag", CustomerAmount = 940, ContainerAmount = 45 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Vrijdag", CustomerAmount = 816, ContainerAmount = 47 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Zaterdag", CustomerAmount = 842, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 2, DaysName = "Zondag", CustomerAmount = 885, ContainerAmount = 45 },

                new TemplateHasDays { TemplatesId = 3, DaysName = "Maandag", CustomerAmount = 872, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Dinsdag", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Woensdag", CustomerAmount = 916, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Donderdag", CustomerAmount = 875, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Vrijdag", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Zaterdag", CustomerAmount = 945, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 3, DaysName = "Zondag", CustomerAmount = 880, ContainerAmount = 52 },

                new TemplateHasDays { TemplatesId = 4, DaysName = "Maandag", CustomerAmount = 900, ContainerAmount = 49 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Dinsdag", CustomerAmount = 903, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Woensdag", CustomerAmount = 930, ContainerAmount = 45 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Donderdag", CustomerAmount = 985, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Vrijdag", CustomerAmount = 865, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Zaterdag", CustomerAmount = 950, ContainerAmount = 43 },
                new TemplateHasDays { TemplatesId = 4, DaysName = "Zondag", CustomerAmount = 950, ContainerAmount = 38 },

                new TemplateHasDays { TemplatesId = 5, DaysName = "Maandag", CustomerAmount = 832, ContainerAmount = 52 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Dinsdag", CustomerAmount = 935, ContainerAmount = 49 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Woensdag", CustomerAmount = 877, ContainerAmount = 29 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Donderdag", CustomerAmount = 989, ContainerAmount = 41 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Vrijdag", CustomerAmount = 872, ContainerAmount = 32 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Zaterdag", CustomerAmount = 771, ContainerAmount = 36 },
                new TemplateHasDays { TemplatesId = 5, DaysName = "Zondag", CustomerAmount = 885, ContainerAmount = 52 }
            );


            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdd => phdd.PrognosisHasDays)
                .WithMany(phd => phd.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdd => new { phdd.DaysName, phdd.PrognosisId })
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

            var janeBranchAssignment = new BranchHasEmployee
            {
                BranchId = 1,
                EmployeeId = jane.Id,
                StartDate = new DateTime(2012, 4, 1),
                FunctionName = "Stocker"
            };

            modelBuilder.Entity<BranchHasEmployee>().HasData(
                branchHasEmployeeOne,
                branchHasEmployeeTwo,
                branchHasEmployeeThree,
                branchHasEmployeeFour,
                branchHasEmployeeFive,
                branchHasEmployeeSix,
                janeBranchAssignment
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
                    DepartmentName = "Vers"
                },
                new BranchRequestsEmployee
                {
                    BranchId = 2,
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    RequestToBranchId = 1,
                    RequestStatusName = "In Afwachting",
                    Message = "Er zijn te weinig medewerkers op deze datum beschikbaar.",
                    DateNeeded = DateTime.Now.AddDays(14),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vakkenvullen"
                },
                new BranchRequestsEmployee
                {
                    BranchId = 3,
                    EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                    RequestToBranchId = 4,
                    RequestStatusName = "Afgewezen",
                    Message = "Hulp nodig vanwege ziekte van een collega.",
                    DateNeeded = DateTime.Now.AddDays(10),
                    StartTime = new TimeOnly(8, 30),
                    EndTime = new TimeOnly(17, 30),
                    DepartmentName = "Vakkenvullen"
                },
                new BranchRequestsEmployee
                {
                    BranchId = 1,
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0",
                    RequestToBranchId = 3,
                    RequestStatusName = "Geaccepteerd",
                    Message = "Overplaatsing voor trainingssessies.",
                    DateNeeded = DateTime.Now.AddDays(20),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Kassa"
                }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony Ross
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(13, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", // Sarah van der Ven
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(12, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = true
                },
                new Schedule
                {
                    EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", // Douwe Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", // Pasha Bakker
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(16, 0),
                    EndTime = new TimeOnly(21, 30),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 29),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 5),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", // David den Boer
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(12, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony Ross
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Kassa",
                    IsFinal = false,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", // Sarah van der Ven
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(20, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = true
                },

                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", // Pasha Bakker
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 24),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 8),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(12, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                }, 
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 9),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                }, 
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 12),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                }, 
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 14),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 12, 21),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 1, 5),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(12, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 1, 12),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 1, 22),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 1, 24),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 2, 3),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 2, 7),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2025, 2, 9),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                }
            );

            // Seeddata for Prognosis
            var prognosisId = "prognosis_week_47_2024"; // Unique ID for the prognosis
            modelBuilder.Entity<Prognosis>().HasData(
                new Prognosis
                {
                    PrognosisId = prognosisId,
                    WeekNr = 47,
                    Year = 2024,
                    BranchId = 1 // Assuming branch 1
                }
            );

            // Seeddata for PrognosisHasDays
            modelBuilder.Entity<PrognosisHasDays>().HasData(
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Maandag",
                    CustomerAmount = 200,
                    PackagesAmount = 300
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Dinsdag",
                    CustomerAmount = 150,
                    PackagesAmount = 250
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Woensdag",
                    CustomerAmount = 220,
                    PackagesAmount = 280
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Donderdag",
                    CustomerAmount = 190,
                    PackagesAmount = 270
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Vrijdag",
                    CustomerAmount = 210,
                    PackagesAmount = 290
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Zaterdag",
                    CustomerAmount = 250,
                    PackagesAmount = 320
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Zondag",
                    CustomerAmount = 180,
                    PackagesAmount = 260
                }
            );



modelBuilder.Entity<Schedule>().HasData(
    new Schedule
    {
        EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony
        BranchId = 1,
        Date = new DateOnly(2025, 1, 6), // Maandag
        StartTime = new TimeOnly(9, 0),
        EndTime = new TimeOnly(17, 0),
        DepartmentName = "Vakkenvullen",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony
        BranchId = 1,
        Date = new DateOnly(2025, 1, 7), // Dinsdag
        StartTime = new TimeOnly(9, 0),
        EndTime = new TimeOnly(17, 0),
        DepartmentName = "Vakkenvullen",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony
        BranchId = 1,
        Date = new DateOnly(2025, 1, 8), // Woensdag
        StartTime = new TimeOnly(9, 0),
        EndTime = new TimeOnly(17, 0),
        DepartmentName = "Kassa",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony
        BranchId = 1,
        Date = new DateOnly(2025, 1, 9), // Donderdag
        StartTime = new TimeOnly(10, 0),
        EndTime = new TimeOnly(16, 0),
        DepartmentName = "Vakkenvullen",
        IsFinal = true,
        IsSick = true
    },
    new Schedule
    {
        EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", // Anthony
        BranchId = 1,
        Date = new DateOnly(2025, 1, 10), // Vrijdag
        StartTime = new TimeOnly(9, 30),
        EndTime = new TimeOnly(17, 30),
        DepartmentName = "Kassa",
        IsFinal = true,
        IsSick = false
    },

    new Schedule
    {
        EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane
        BranchId = 1,
        Date = new DateOnly(2025, 1, 13), // Maandag
        StartTime = new TimeOnly(8, 0),
        EndTime = new TimeOnly(14, 0),
        DepartmentName = "Vers",
        IsFinal = true,
        IsSick = true
    },
    new Schedule
    {
        EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane
        BranchId = 1,
        Date = new DateOnly(2025, 1, 14), // Dinsdag
        StartTime = new TimeOnly(8, 0),
        EndTime = new TimeOnly(14, 0),
        DepartmentName = "Vers",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane
        BranchId = 1,
        Date = new DateOnly(2025, 1, 15), // Woensdag
        StartTime = new TimeOnly(8, 30),
        EndTime = new TimeOnly(15, 0),
        DepartmentName = "Vakkenvullen",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane
        BranchId = 1,
        Date = new DateOnly(2025, 1, 16), // Donderdag
        StartTime = new TimeOnly(9, 0),
        EndTime = new TimeOnly(17, 0),
        DepartmentName = "Kassa",
        IsFinal = true,
        IsSick = false
    },
    new Schedule
    {
        EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane
        BranchId = 1,
        Date = new DateOnly(2025, 1, 17), // Vrijdag
        StartTime = new TimeOnly(10, 0),
        EndTime = new TimeOnly(14, 0),
        DepartmentName = "Vers",
        IsFinal = true,
        IsSick = false
    }
);


            modelBuilder.Entity<RegisteredHours>().HasData(
                // == Anthony (BID = "B012") ==
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 6, 9, 5, 0),
                    EndTime = new DateTime(2025, 1, 6, 16, 55, 0),
                    EmployeeBID = "B012",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 7, 9, 15, 0),
                    EndTime = new DateTime(2025, 1, 7, 17, 5, 0),
                    EmployeeBID = "B012",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 8, 9, 0, 0),
                    EndTime = new DateTime(2025, 1, 8, 17, 10, 0),
                    EmployeeBID = "B012",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 9, 10, 5, 0),
                    EndTime = new DateTime(2025, 1, 9, 15, 58, 0),
                    EmployeeBID = "B012",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 10, 9, 35, 0),
                    EndTime = new DateTime(2025, 1, 10, 17, 25, 0),
                    EmployeeBID = "B012",
                    EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                    BranchId = 1,
                    IsDefenitive = true
                },

                // == Jane (BID = "B002") ==
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 13, 8, 5, 0),
                    EndTime = new DateTime(2025, 1, 13, 13, 50, 0),
                    EmployeeBID = "B002",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 14, 8, 10, 0),
                    EndTime = new DateTime(2025, 1, 14, 14, 5, 0),
                    EmployeeBID = "B002",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 15, 8, 35, 0),
                    EndTime = new DateTime(2025, 1, 15, 14, 55, 0),
                    EmployeeBID = "B002",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 16, 9, 5, 0),
                    EndTime = new DateTime(2025, 1, 16, 17, 2, 0),
                    EmployeeBID = "B002",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    BranchId = 1,
                    IsDefenitive = true
                },
                new RegisteredHours
                {
                    StartTime = new DateTime(2025, 1, 17, 10, 2, 0),
                    EndTime = new DateTime(2025, 1, 17, 14, 1, 0),
                    EmployeeBID = "B002",
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    BranchId = 1,
                    IsDefenitive = true
                }
            );



            modelBuilder.Entity<PrognosisHasDaysHasDepartment>().HasData(
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Maandag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 4, HoursOfWorkNeeded = 28 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Maandag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 32 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Maandag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 2, HoursOfWorkNeeded = 16 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Dinsdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 4, HoursOfWorkNeeded = 30 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Dinsdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 35 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Dinsdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 2, HoursOfWorkNeeded = 18 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Woensdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 4, HoursOfWorkNeeded = 29 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Woensdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 34 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Woensdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 2, HoursOfWorkNeeded = 17 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Donderdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 4, HoursOfWorkNeeded = 27 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Donderdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 31 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Donderdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 2, HoursOfWorkNeeded = 15 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Vrijdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 32 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Vrijdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 6, HoursOfWorkNeeded = 36 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Vrijdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 3, HoursOfWorkNeeded = 20 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Zaterdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 35 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Zaterdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 6, HoursOfWorkNeeded = 38 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Zaterdag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 3, HoursOfWorkNeeded = 22 },

                new PrognosisHasDaysHasDepartment { DepartmentName = "Vakkenvullen", DaysName = "Zondag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 4, HoursOfWorkNeeded = 30 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Kassa", DaysName = "Zondag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 5, HoursOfWorkNeeded = 34 },
                new PrognosisHasDaysHasDepartment { DepartmentName = "Vers", DaysName = "Zondag", PrognosisId = "prognosis_week_47_2024", AmountOfWorkersNeeded = 2, HoursOfWorkNeeded = 18 }
            );
            modelBuilder.Entity<LabourRules>().HasData(
              new LabourRules
              {
                  CountryName = "Netherlands",
                  AgeGroup = "<16",
                  MaxHoursPerDay = 8,
                  MaxEndTime = new TimeSpan(19, 0, 0),
                  MaxHoursPerWeek = 40,
                  MaxWorkDaysPerWeek = 5,
                  MaxHoursWithSchool = 12,
                  MinRestDaysPerWeek = 2,
                  NumHoursWorkedBeforeBreak = 4,
                  MinutesOfBreak = 30,
                  SickPayPercentage = 70m,
                  OvertimePayPercentage = 0m,
                  MinRestHoursBetweenShifts = 12,
                  MaxShiftDuration = 8,
                  MaxOvertimeHoursPerWeek = 0
              },
              new LabourRules
              {
                  CountryName = "Netherlands",
                  AgeGroup = "16-17",
                  MaxHoursPerDay = 9,
                  MaxEndTime = new TimeSpan(22, 0, 0),
                  MaxHoursPerWeek = 40,
                  MaxWorkDaysPerWeek = 5,
                  MaxHoursWithSchool = 40,
                  MinRestDaysPerWeek = 2,
                  NumHoursWorkedBeforeBreak = 4,
                  MinutesOfBreak = 30,
                  SickPayPercentage = 70m,
                  OvertimePayPercentage = 0m,
                  MinRestHoursBetweenShifts = 12,
                  MaxShiftDuration = 9,
                  MaxOvertimeHoursPerWeek = 0
              },
              new LabourRules
              {
                  CountryName = "Netherlands",
                  AgeGroup = ">17",
                  MaxHoursPerDay = 12,
                  MaxEndTime = new TimeSpan(24, 0, 0),
                  MaxHoursPerWeek = 60,
                  MaxWorkDaysPerWeek = 6,
                  MaxHoursWithSchool = 0,
                  MinRestDaysPerWeek = 1,
                  NumHoursWorkedBeforeBreak = 4,
                  MinutesOfBreak = 30,
                  SickPayPercentage = 70m,
                  OvertimePayPercentage = 150m,
                  MinRestHoursBetweenShifts = 11,
                  MaxShiftDuration = 12,
                  MaxOvertimeHoursPerWeek = 20
              }
          );
            modelBuilder.Entity<Availability>().HasData(
                // John Doe
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },

                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },
                new Availability { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },

                // Jane Smith
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },

                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },

                // Darlon van Dijk
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },

                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },

                // Pasha Bakker
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(10, 30), EndTime = new TimeOnly(14, 30) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(10, 30), EndTime = new TimeOnly(14, 30) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                // Sarah van der Ven
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(9, 30), EndTime = new TimeOnly(13, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(11, 30), EndTime = new TimeOnly(15, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },

                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(9, 30), EndTime = new TimeOnly(13, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(11, 30), EndTime = new TimeOnly(15, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },
                new Availability { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },

                // David den Boer
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                // Anthony Ross
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(11, 30), EndTime = new TimeOnly(15, 30) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(9, 30), EndTime = new TimeOnly(13, 30) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },

                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(11, 30), EndTime = new TimeOnly(15, 30) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(9, 30), EndTime = new TimeOnly(13, 30) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(12, 30) },

                // Douwe Jansen
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 23), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 24), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(12, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(15, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(16, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 30), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) },

                new Availability { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 12, 5), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(21, 0) }




            );

            modelBuilder.Entity<SchoolSchedule>().HasData(
                // John Doe
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                // Jane Smith
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                // Darlon van Dijk
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                // Pasha Bakker
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                // Sarah van der Ven
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                // David den Boer
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                // Anthony van Vliet
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(18, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(19, 0) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "a1b1c1d1-1111-2222-3333-4444abcdabcd", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0) },

                // Douwe Jansen
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 18), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 19), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 20), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 21), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 22), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },

                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 25), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 26), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(18, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 27), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 28), StartTime = new TimeOnly(12, 30), EndTime = new TimeOnly(15, 30) },
                new SchoolSchedule { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 11, 29), StartTime = new TimeOnly(14, 30), EndTime = new TimeOnly(17, 30) }
            );

            modelBuilder.Entity<RegisteredHours>().HasData(
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 1,
                    StartTime = new DateTime(2024, 12, 5, 8, 1, 12),
                    EndTime = new DateTime(2024, 12, 5, 14, 2, 27),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 2,
                    StartTime = new DateTime(2024, 12, 16, 8, 58, 52),
                    EndTime = new DateTime(2024, 12, 16, 12, 0, 44),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 3,
                    StartTime = new DateTime(2024, 12, 17, 13, 2, 42),
                    EndTime = new DateTime(2024, 12, 17, 17, 1, 25),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 4,
                    StartTime = new DateTime(2024, 12, 24, 7, 59, 33),
                    EndTime = new DateTime(2024, 12, 24, 16, 1, 26),
                    BranchId = 1,
                    IsDefenitive = true,
                },


                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 5,
                    StartTime = new DateTime(2025, 1, 5, 9, 1, 12),
                    EndTime = new DateTime(2025, 1, 5, 12, 2, 27),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 6,
                    StartTime = new DateTime(2025, 1, 8, 11, 58, 52),
                    EndTime = new DateTime(2025, 1, 8, 16, 0, 44),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 8,
                    StartTime = new DateTime(2025, 1, 16, 7, 59, 33),
                    EndTime = new DateTime(2025, 1, 16, 16, 1, 26),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 9,
                    StartTime = new DateTime(2025, 1, 22, 11, 1, 12),
                    EndTime = new DateTime(2025, 1, 22, 17, 2, 27),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 10,
                    StartTime = new DateTime(2025, 1, 24, 8, 58, 52),
                    EndTime = new DateTime(2025, 1, 24, 15, 0, 44),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 11,
                    StartTime = new DateTime(2025, 2, 3, 9, 2, 42),
                    EndTime = new DateTime(2025, 2, 3, 17, 1, 25),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 12,
                    StartTime = new DateTime(2025, 2, 7, 10, 59, 33),
                    EndTime = new DateTime(2025, 2, 7, 17, 1, 26),
                    BranchId = 1,
                    IsDefenitive = true,
                },
                new RegisteredHours()
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                    EmployeeBID = "B002",
                    RegistrationNumber = 13,
                    StartTime = new DateTime(2025, 2, 9, 8, 59, 33),
                    EndTime = new DateTime(2025, 2, 9, 14, 59, 46),
                    BranchId = 1,
                    IsDefenitive = true,
                }
            );


            // Relations
            modelBuilder.Entity<BranchHasEmployee>()
                .HasKey(bhw => new { bhw.BranchId, bhw.EmployeeId });

            modelBuilder.Entity<RegisteredHours>()
                .HasKey(rh => new { rh.EmployeeId, rh.RegistrationNumber });

            modelBuilder.Entity<RegisteredHours>()
                .Property(e => e.RegistrationNumber)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RegisteredHours>()
                .HasOne<Employee>()
                .WithMany(e => e.RegisteredHours)
                .HasForeignKey(rh => rh.EmployeeBID)
                .HasPrincipalKey(e => e.BID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegisteredHours>()
                .HasOne<Branch>()
                .WithMany(e => e.RegisteredHours)
                .HasForeignKey(rh => rh.BranchId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.BID)
                .IsUnique();


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
                .HasKey(phdhd => new { phdhd.DepartmentName, phdhd.DaysName, phdhd.PrognosisId });

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdhd => phdhd.Department)
                .WithMany(dn => dn.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdhd => phdhd.DepartmentName);

            modelBuilder.Entity<PrognosisHasDaysHasDepartment>()
                .HasOne(phdhd => phdhd.PrognosisHasDays)
                .WithMany(phd => phd.PrognosisHasDaysHasDepartment)
                .HasForeignKey(phdhd => new { phdhd.DaysName, phdhd.PrognosisId } );

            modelBuilder.Entity<LabourRules>()
                .HasKey(l => new { l.CountryName, l.AgeGroup });

            modelBuilder.Entity<LabourRules>()
                .HasOne(l => l.Country)
                .WithMany(c => c.LabourRules)
                .HasForeignKey(l => l.CountryName);

            modelBuilder.Entity<LabourRules>()
               .Property(lr => lr.SickPayPercentage)
               .HasPrecision(5, 2);

            modelBuilder.Entity<LabourRules>()
                .Property(lr => lr.OvertimePayPercentage)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Norm>()
                .HasOne(n => n.Branch)
                .WithMany(b => b.Norm)
                .HasForeignKey(n => n.branchId);

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

            modelBuilder.Entity<RegisteredHours>()
                .HasKey(rh => new { rh.EmployeeBID, rh.StartTime });

            modelBuilder.Entity<RegisteredHours>()
                .HasOne(rh => rh.Employee)
                .WithMany(e => e.RegisteredHours)
                .HasForeignKey(rh => rh.EmployeeBID)
                .HasPrincipalKey(e => e.BID)    // <---- cruciaal!
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.BID)
                .IsUnique(); // BID moet uniek zijn


        }
    }
}
