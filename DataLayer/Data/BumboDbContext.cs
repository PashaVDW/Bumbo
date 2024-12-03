using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bumbo.Models;
using DataLayer.Models;
using System;
using Azure.Core;

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

            var weekFourtyTwoColi = new Norm
            {
                normId = 6,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 95
            };

            var weekFourtyTwoShelve = new Norm
            {
                normId = 7,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 35
            };

            var weekFourtyTwoCashier = new Norm
            {
                normId = 8,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 3
            };

            var weekFourtyTwoFresh = new Norm
            {
                normId = 9,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Vers",
                normInSeconds = 8
            };

            var weekFourtyTwoFronting = new Norm
            {
                normId = 10,
                branchId = 1,
                week = 42,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 3
            };

            var weekFourtyOneColiBranchTwo = new Norm
            {
                normId = 11,
                branchId = 2,
                week = 41,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 96
            };

            var weekFourtyOneShelveBranchTwo = new Norm
            {
                normId = 12,
                branchId = 2,
                week = 41,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 32
            };

            var weekFourtyOneCashierBranchTwo = new Norm
            {
                normId = 13,
                branchId = 2,
                week = 41,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 3
            };

            var weekFourtyOneFreshBranchTwo = new Norm
            {
                normId = 14,
                branchId = 2,
                week = 41,
                year = 2024,
                activity = "Vers",
                normInSeconds = 7
            };

            var weekFourtyOneFrontingBranchTwo = new Norm
            {
                normId = 15,
                branchId = 2,
                week = 41,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 2
            };

            var weekFourtyTwoColiBranchTwo = new Norm
            {
                normId = 16,
                branchId = 2,
                week = 42,
                year = 2024,
                activity = "Coli uitladen",
                normInSeconds = 91
            };

            var weekFourtyTwoShelveBranchTwo = new Norm
            {
                normId = 17,
                branchId = 2,
                week = 42,
                year = 2024,
                activity = "Vakkenvullen",
                normInSeconds = 32
            };

            var weekFourtyTwoCashierBranchTwo = new Norm
            {
                normId = 18,
                branchId = 2,
                week = 42,
                year = 2024,
                activity = "Kassa",
                normInSeconds = 4
            };

            var weekFourtyTwoFreshBranchTwo = new Norm
            {
                normId = 19,
                branchId = 2,
                week = 42,
                year = 2024,
                activity = "Vers",
                normInSeconds = 7
            };

            var weekFourtyTwoFrontingBranchTwo = new Norm
            {
                normId = 20,
                branchId = 2,
                week = 42,
                year = 2024,
                activity = "Spiegelen",
                normInSeconds = 2
            };

            modelBuilder.Entity<Norm>().HasData(
                weekFourtyOneColi, weekFourtyOneShelve, weekFourtyOneCashier, weekFourtyOneFresh, weekFourtyOneFronting,
                weekFourtyTwoColi, weekFourtyTwoShelve, weekFourtyTwoCashier, weekFourtyTwoFresh, weekFourtyTwoFronting,
                weekFourtyOneColiBranchTwo, weekFourtyOneShelveBranchTwo, weekFourtyOneCashierBranchTwo, weekFourtyOneFreshBranchTwo, weekFourtyOneFrontingBranchTwo,
                weekFourtyTwoColiBranchTwo, weekFourtyTwoShelveBranchTwo, weekFourtyTwoCashierBranchTwo, weekFourtyTwoFreshBranchTwo, weekFourtyTwoFrontingBranchTwo);

            modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Germany" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentName = "Kassa" },
                new Department { DepartmentName = "Vakkenvullen" },
                new Department { DepartmentName = "Vers" }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    BranchId = 1,
                    PostalCode = "1012LG",
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
                    PostalCode = "1811KH",
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
                    PostalCode = "3011HE",
                    HouseNumber = "15",
                    Name = "Rotterdam Filiaal",
                    Street = "Botersloot",
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(9, 0, 0),
                    ClosingTime = new TimeOnly(17, 0, 0)
                },
                new Branch
                {
                    BranchId = 5,
                    PostalCode = "2511AG",
                    HouseNumber = "22",
                    Name = "Den Haag Filiaal",
                    Street = "Binnenhof",
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(9, 0, 0),
                    ClosingTime = new TimeOnly(22, 0, 0)
                },
                new Branch
                {
                    BranchId = 6,
                    PostalCode = "2311GP",
                    HouseNumber = "18",
                    Name = "Leiden Filiaal",
                    Street = "Breestraat",
                    CountryName = "Netherlands",
                    OpeningTime = new TimeOnly(8, 0, 0),
                    ClosingTime = new TimeOnly(18, 0, 0)
                },
                new Branch
                {
                    BranchId = 7,
                    PostalCode = "9000",
                    HouseNumber = "56",
                    Name = "Gent Filiaal",
                    Street = "Veldstraat",
                    CountryName = "Belgium",
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

            var jan = new Employee
            {
                Id = "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9",
                BID = "B004",
                FirstName = "Jan",
                MiddleName = "",
                LastName = "de Vries",
                BirthDate = new DateTime(1985, 11, 10),
                PostalCode = "1234 AB",
                HouseNumber = 12,
                StartDate = new DateTime(2017, 3, 15),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 12345678",
                UserName = "jan.devries@example.com",
                NormalizedUserName = "JAN.DEVRIES@EXAMPLE.COM",
                Email = "jan.devries@example.com",
                NormalizedEmail = "JAN.DEVRIES@EXAMPLE.COM",
                EmailConfirmed = true
            };
            jan.PasswordHash = passwordHasher.HashPassword(jan, "PassJan");

            var sofie = new Employee
            {
                Id = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1",
                BID = "B005",
                FirstName = "Sofie",
                MiddleName = "",
                LastName = "Jansen",
                BirthDate = new DateTime(1990, 7, 22),
                PostalCode = "9876 ZX",
                HouseNumber = 30,
                StartDate = new DateTime(2020, 5, 10),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 23456789",
                UserName = "sofie.jansen@example.com",
                NormalizedUserName = "SOFIE.JANSEN@EXAMPLE.COM",
                Email = "sofie.jansen@example.com",
                NormalizedEmail = "SOFIE.JANSEN@EXAMPLE.COM",
                EmailConfirmed = true
            };
            sofie.PasswordHash = passwordHasher.HashPassword(sofie, "PassSofie");

            var tom = new Employee
            {
                Id = "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2",
                BID = "B006",
                FirstName = "Tom",
                MiddleName = "",
                LastName = "Koster",
                BirthDate = new DateTime(1988, 3, 3),
                PostalCode = "6543 BC",
                HouseNumber = 15,
                StartDate = new DateTime(2019, 9, 1),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 34567890",
                UserName = "tom.koster@example.com",
                NormalizedUserName = "TOM.KOSTER@EXAMPLE.COM",
                Email = "tom.koster@example.com",
                NormalizedEmail = "TOM.KOSTER@EXAMPLE.COM",
                EmailConfirmed = true
            };
            tom.PasswordHash = passwordHasher.HashPassword(tom, "PassTom");

            var lisa = new Employee
            {
                Id = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3",
                BID = "B007",
                FirstName = "Lisa",
                MiddleName = "",
                LastName = "Hendriks",
                BirthDate = new DateTime(1995, 9, 18),
                PostalCode = "2345 PQ",
                HouseNumber = 8,
                StartDate = new DateTime(2021, 1, 10),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 98765432",
                UserName = "lisa.hendriks@example.com",
                NormalizedUserName = "LISA.HENDRIKS@EXAMPLE.COM",
                Email = "lisa.hendriks@example.com",
                NormalizedEmail = "LISA.HENDRIKS@EXAMPLE.COM",
                EmailConfirmed = true
            };
            lisa.PasswordHash = passwordHasher.HashPassword(lisa, "PassLisa");

            var mark = new Employee
            {
                Id = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4",
                BID = "B008",
                FirstName = "Mark",
                MiddleName = "",
                LastName = "Willems",
                BirthDate = new DateTime(1983, 12, 5),
                PostalCode = "5678 MN",
                HouseNumber = 25,
                StartDate = new DateTime(2016, 11, 1),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 12341234",
                UserName = "mark.willems@example.com",
                NormalizedUserName = "MARK.WILLEMS@EXAMPLE.COM",
                Email = "mark.willems@example.com",
                NormalizedEmail = "MARK.WILLEMS@EXAMPLE.COM",
                EmailConfirmed = true
            };
            mark.PasswordHash = passwordHasher.HashPassword(mark, "PassMark");

            var eva = new Employee
            {
                Id = "f9e8d7c6-1234-5678-90ab-cdef12345678",
                BID = "B009",
                FirstName = "Eva",
                MiddleName = "",
                LastName = "Smit",
                BirthDate = new DateTime(1994, 5, 25),
                PostalCode = "5678 KL",
                HouseNumber = 10,
                StartDate = new DateTime(2022, 4, 15),
                IsSystemManager = false,
                ManagerOfBranchId = null,
                PhoneNumber = "+31 6 87654321",
                UserName = "eva.smit@example.com",
                NormalizedUserName = "EVA.SMIR@EXAMPLE.COM",
                Email = "eva.smit@example.com",
                NormalizedEmail = "EVA.SMIR@EXAMPLE.COM",
                EmailConfirmed = true
            };
            eva.PasswordHash = passwordHasher.HashPassword(eva, "PassEva");

            // Add employees to the model

            modelBuilder.Entity<Employee>().HasData(john, jane, darlon, pasha, sarah, david, anthony, douwe, jan, 
                sofie, tom, lisa, mark, eva);

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
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vers",
                    EmployeeId = "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Kassa",
                    EmployeeId = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vakkenvullen",
                    EmployeeId = "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vers",
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Vakkenvullen",
                    EmployeeId = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4"
                },
                new EmployeeHasDepartment()
                {
                    DepartmentName = "Kassa",
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678"
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
                },
                new Template
                {
                    Id = 6,
                    Name = "Basic Package",
                    BranchBranchId = 3
                },
                new Template
                {
                    Id = 7,
                    Name = "Weekly Special",
                    BranchBranchId = 3
                },
                 new Template
                 {
                     Id = 8,
                     Name = "Basic Package",
                     BranchBranchId = 4
                 },
                new Template
                {
                    Id = 9,
                    Name = "Weekly Special",
                    BranchBranchId = 4
                },
                 new Template
                 {
                     Id = 10,
                     Name = "Basic Package",
                     BranchBranchId = 5
                 },
                new Template
                {
                    Id = 11,
                    Name = "Weekly Special",
                    BranchBranchId = 5
                },
                 new Template
                 {
                     Id = 12,
                     Name = "Basic Package",
                     BranchBranchId = 6
                 },
                new Template
                {
                    Id = 13,
                    Name = "Weekly Special",
                    BranchBranchId = 6
                },
                 new Template
                 {
                     Id = 14,
                     Name = "Basic Package",
                     BranchBranchId = 7
                 },
                new Template
                {
                    Id = 15,
                    Name = "Weekly Special",
                    BranchBranchId = 7
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
                new TemplateHasDays { TemplatesId = 5, DaysName = "Sunday", CustomerAmount = 885, ContainerAmount = 52 },

                new TemplateHasDays { TemplatesId = 6, DaysName = "Monday", CustomerAmount = 987, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Tuesday", CustomerAmount = 827, ContainerAmount = 51 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Wednesday", CustomerAmount = 901, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Thursday", CustomerAmount = 992, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Friday", CustomerAmount = 1045, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Saturday", CustomerAmount = 957, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 6, DaysName = "Sunday", CustomerAmount = 874, ContainerAmount = 33 },
                 
                new TemplateHasDays { TemplatesId = 7, DaysName = "Monday", CustomerAmount = 835, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Tuesday", CustomerAmount = 934, ContainerAmount = 50 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Wednesday", CustomerAmount = 879, ContainerAmount = 27 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Thursday", CustomerAmount = 983, ContainerAmount = 40 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Friday", CustomerAmount = 871, ContainerAmount = 33 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Saturday", CustomerAmount = 761, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 7, DaysName = "Sunday", CustomerAmount = 889, ContainerAmount = 51 },

                new TemplateHasDays { TemplatesId = 8, DaysName = "Monday", CustomerAmount = 987, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Tuesday", CustomerAmount = 827, ContainerAmount = 51 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Wednesday", CustomerAmount = 901, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Thursday", CustomerAmount = 992, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Friday", CustomerAmount = 1045, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Saturday", CustomerAmount = 957, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 8, DaysName = "Sunday", CustomerAmount = 874, ContainerAmount = 33 },

                new TemplateHasDays { TemplatesId = 9, DaysName = "Monday", CustomerAmount = 835, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Tuesday", CustomerAmount = 934, ContainerAmount = 50 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Wednesday", CustomerAmount = 879, ContainerAmount = 27 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Thursday", CustomerAmount = 983, ContainerAmount = 40 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Friday", CustomerAmount = 871, ContainerAmount = 33 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Saturday", CustomerAmount = 761, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 9, DaysName = "Sunday", CustomerAmount = 889, ContainerAmount = 51 },

                new TemplateHasDays { TemplatesId = 10, DaysName = "Monday", CustomerAmount = 987, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Tuesday", CustomerAmount = 827, ContainerAmount = 51 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Wednesday", CustomerAmount = 901, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Thursday", CustomerAmount = 992, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Friday", CustomerAmount = 1045, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Saturday", CustomerAmount = 957, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 10, DaysName = "Sunday", CustomerAmount = 874, ContainerAmount = 33 },

                new TemplateHasDays { TemplatesId = 11, DaysName = "Monday", CustomerAmount = 835, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Tuesday", CustomerAmount = 934, ContainerAmount = 50 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Wednesday", CustomerAmount = 879, ContainerAmount = 27 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Thursday", CustomerAmount = 983, ContainerAmount = 40 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Friday", CustomerAmount = 871, ContainerAmount = 33 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Saturday", CustomerAmount = 761, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 11, DaysName = "Sunday", CustomerAmount = 889, ContainerAmount = 51 },

                new TemplateHasDays { TemplatesId = 12, DaysName = "Monday", CustomerAmount = 987, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Tuesday", CustomerAmount = 827, ContainerAmount = 51 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Wednesday", CustomerAmount = 901, ContainerAmount = 39 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Thursday", CustomerAmount = 992, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Friday", CustomerAmount = 1045, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Saturday", CustomerAmount = 957, ContainerAmount = 42 },
                new TemplateHasDays { TemplatesId = 12, DaysName = "Sunday", CustomerAmount = 874, ContainerAmount = 33 },

                new TemplateHasDays { TemplatesId = 13, DaysName = "Monday", CustomerAmount = 835, ContainerAmount = 53 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Tuesday", CustomerAmount = 934, ContainerAmount = 50 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Wednesday", CustomerAmount = 879, ContainerAmount = 27 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Thursday", CustomerAmount = 983, ContainerAmount = 40 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Friday", CustomerAmount = 871, ContainerAmount = 33 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Saturday", CustomerAmount = 761, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 13, DaysName = "Sunday", CustomerAmount = 889, ContainerAmount = 51 },

                new TemplateHasDays { TemplatesId = 14, DaysName = "Monday", CustomerAmount = 989, ContainerAmount = 44 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Tuesday", CustomerAmount = 817, ContainerAmount = 49 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Wednesday", CustomerAmount = 924, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Thursday", CustomerAmount = 988, ContainerAmount = 55 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Friday", CustomerAmount = 1041, ContainerAmount = 37 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Saturday", CustomerAmount = 947, ContainerAmount = 44 },
                new TemplateHasDays { TemplatesId = 14, DaysName = "Sunday", CustomerAmount = 879, ContainerAmount = 32 },

                new TemplateHasDays { TemplatesId = 15, DaysName = "Monday", CustomerAmount = 825, ContainerAmount = 57 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Tuesday", CustomerAmount = 914, ContainerAmount = 54 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Wednesday", CustomerAmount = 868, ContainerAmount = 25 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Thursday", CustomerAmount = 994, ContainerAmount = 38 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Friday", CustomerAmount = 842, ContainerAmount = 35 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Saturday", CustomerAmount = 790, ContainerAmount = 37 },
                new TemplateHasDays { TemplatesId = 15, DaysName = "Sunday", CustomerAmount = 878, ContainerAmount = 52 }
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
            var branchHasEmployeeSeven = new BranchHasEmployee
            {
                BranchId = 4,
                EmployeeId = jan.Id,
                StartDate = jan.StartDate,
                FunctionName = "Manager"
            };
            var branchHasEmployeeEight = new BranchHasEmployee
            {
                BranchId = 6,
                EmployeeId = sofie.Id,
                StartDate = sofie.StartDate,
                FunctionName = "Stocker"
            };
            var branchHasEmployeeNine = new BranchHasEmployee
            {
                BranchId = 5,
                EmployeeId = tom.Id,
                StartDate = tom.StartDate,
                FunctionName = "Cashier"
            };
            var branchHasEmployeeTen = new BranchHasEmployee
            {
                BranchId = 6,
                EmployeeId = lisa.Id,
                StartDate = lisa.StartDate,
                FunctionName = "Cashier"
            };
            var branchHasEmployeeEleven = new BranchHasEmployee
            {
                BranchId = 7,
                EmployeeId = mark.Id,
                StartDate = mark.StartDate,
                FunctionName = "Stocker"
            };
            var branchHasEmployeeTwelve = new BranchHasEmployee
            {
                BranchId = 7,
                EmployeeId = eva.Id,
                StartDate = eva.StartDate,
                FunctionName = "Stocker"
            };

            modelBuilder.Entity<BranchHasEmployee>().HasData(
                branchHasEmployeeOne, branchHasEmployeeTwo, branchHasEmployeeThree, branchHasEmployeeFour,
                branchHasEmployeeFive, branchHasEmployeeSix, branchHasEmployeeSeven, branchHasEmployeeEight,
                branchHasEmployeeNine, branchHasEmployeeTen, branchHasEmployeeEleven, branchHasEmployeeTwelve
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
                },
                new BranchRequestsEmployee
                {
                    BranchId = 2,
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678",
                    RequestToBranchId = 1,
                    RequestStatusName = "In Afwachting",
                    Message = "Overplaatsing wegens tijdelijke afwezigheid van manager.",
                    DateNeeded = DateTime.Now.AddDays(10),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa"
                },

                new BranchRequestsEmployee
                {
                    BranchId = 3,
                    EmployeeId = "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9",
                    RequestToBranchId = 4,
                    RequestStatusName = "Afgewezen",
                    Message = "Verzoek om overplaatsing vanwege persoonlijke redenen, afgewezen.",
                    DateNeeded = DateTime.Now.AddDays(14),
                    StartTime = new TimeOnly(8, 30),
                    EndTime = new TimeOnly(16, 30),
                    DepartmentName = "Vers"
                },

                new BranchRequestsEmployee
                {
                    BranchId = 4,
                    EmployeeId = "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2",
                    RequestToBranchId = 5,
                    RequestStatusName = "In Afwachting",
                    Message = "Overplaatsing gewenst door nieuwe strategische focus in het bedrijf.",
                    DateNeeded = DateTime.Now.AddDays(5),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Vakkenvullen"
                },

                new BranchRequestsEmployee
                {
                    BranchId = 5,
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3",
                    RequestToBranchId = 6,
                    RequestStatusName = "In Afwachting",
                    Message = "Verzoek om overplaatsing vanwege training in nieuwe technologie.",
                    DateNeeded = DateTime.Now.AddDays(20),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vers"
                }
            );

            modelBuilder.Entity<Schedule>().HasData(
                // Maandag - Kassa
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

                // Maandag - Vakkenvullen
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

                // Maandag - Vers
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 18),
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

                // Dinsdag - Kassa
                new Schedule
                {
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678", // Eva Smit
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", // Sofie Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Dinsdag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                new Schedule
                {
                    EmployeeId = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", // Mark Willems
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Dinsdag - Vers
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
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", // Lisa Hendriks
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 19),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                // Woensdag - Kassa
                new Schedule
                {
                    EmployeeId = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", // Mark Willems
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = true
                },
                new Schedule
                {
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678", // Eva Smit
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", // Sofie Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Woensdag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9", // Jan de Vries
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", // Pasha Bakker
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(22, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Woensdag - Vers
                new Schedule
                {
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", // Lisa Hendriks
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 20),
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
                    Date = new DateOnly(2024, 11, 20),
                    StartTime = new TimeOnly(15, 0),
                    EndTime = new TimeOnly(20, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                // Donderdag - Kassa
                new Schedule
                {
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", // Sarah van der Ven
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678", // Eva Smit
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(15, 0),
                    EndTime = new TimeOnly(21, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Donderdag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(13, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", // Mark Willems
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(20, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Donderdag - Vers
                new Schedule
                {
                    EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", // David den Boer
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(15, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", // Lisa Hendriks
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 21),
                    StartTime = new TimeOnly(15, 0),
                    EndTime = new TimeOnly(20, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                // Vrijdag - Kassa
                new Schedule
                {
                    EmployeeId = "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", // Sarah van der Ven
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(13, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678", // Eva Smit
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Vrijdag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", // Pasha Bakker
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(12, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", // Douwe Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Vrijdag - Vers
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", // Lisa Hendriks
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 22),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                // Zaterdag - Kassa
                new Schedule
                {
                    EmployeeId = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", // Sofie Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
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
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(17, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Zaterdag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", // John Doe
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(12, 00),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", // Mark Willems
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Zaterdag - Vers
                new Schedule
                {
                    EmployeeId = "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", // David den Boer
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", // Lisa Hendriks
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 23),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Vers",
                    IsFinal = true,
                    IsSick = false
                },

                // Zondag - Kassa
                new Schedule
                {
                    EmployeeId = "f9e8d7c6-1234-5678-90ab-cdef12345678", // Eva Smit
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 24),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(14, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },
                new Schedule
                {
                    EmployeeId = "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", // Sofie Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 24),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
                    DepartmentName = "Kassa",
                    IsFinal = true,
                    IsSick = false
                },

                // Zondag - Vakkenvullen
                new Schedule
                {
                    EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", // Douwe Jansen
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 24),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(16, 0),
                    DepartmentName = "Vakkenvullen",
                    IsFinal = true,
                    IsSick = false
                },

                // Zondag - Vers
                new Schedule
                {
                    EmployeeId = "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", // Jane Smith
                    BranchId = 1,
                    Date = new DateOnly(2024, 11, 24),
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
                    Date = new DateOnly(2024, 11, 24),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(18, 0),
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
                    DayName = "Monday",
                    CustomerAmount = 200,
                    PackagesAmount = 300
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Tuesday",
                    CustomerAmount = 150,
                    PackagesAmount = 250
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Wednesday",
                    CustomerAmount = 220,
                    PackagesAmount = 280
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Thursday",
                    CustomerAmount = 190,
                    PackagesAmount = 270
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Friday",
                    CustomerAmount = 210,
                    PackagesAmount = 290
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Saturday",
                    CustomerAmount = 250,
                    PackagesAmount = 320
                },
                new PrognosisHasDays
                {
                    PrognosisId = prognosisId,
                    DayName = "Sunday",
                    CustomerAmount = 180,
                    PackagesAmount = 260
                }
            );

            // Seeddata for PrognosisHasDaysHasDepartment
            modelBuilder.Entity<PrognosisHasDaysHasDepartment>().HasData(
                new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DayName = "Monday",
                    DepartmentName = "Kassa",
                    AmountOfWorkersNeeded = 3,
                    HoursOfWorkNeeded = 24
                },
                new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DayName = "Monday",
                    DepartmentName = "Vakkenvullen",
                    AmountOfWorkersNeeded = 4,
                    HoursOfWorkNeeded = 32
                },
                new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DayName = "Monday",
                    DepartmentName = "Vers",
                    AmountOfWorkersNeeded = 2,
                    HoursOfWorkNeeded = 16
                },
                // Repeat for other days and departments as necessary
                new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DayName = "Tuesday",
                    DepartmentName = "Kassa",
                    AmountOfWorkersNeeded = 2,
                    HoursOfWorkNeeded = 16
                },
                new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DayName = "Tuesday",
                    DepartmentName = "Vakkenvullen",
                    AmountOfWorkersNeeded = 3,
                    HoursOfWorkNeeded = 24
                }
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
                new Availability { EmployeeId = "b2c2d2e2-2222-3333-4444-5555abcdefab", Date = new DateOnly(2024, 12, 1), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(18, 0) }
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
        }
    }
}
