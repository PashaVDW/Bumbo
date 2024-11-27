using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentName);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    FunctionName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.FunctionName);
                });

            migrationBuilder.CreateTable(
                name: "Norms",
                columns: table => new
                {
                    normId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchId = table.Column<int>(type: "int", nullable: false),
                    week = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    activity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    normInSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Norms", x => x.normId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSystemManager = table.Column<bool>(type: "bit", nullable: false),
                    ManagerOfBranchId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    shelfMeters = table.Column<int>(type: "int", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrognosisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branches_Countries_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Countries",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchHasEmployees",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FunctionName = table.Column<string>(type: "nvarchar(45)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHasEmployees", x => new { x.BranchId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_BranchHasEmployees_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchHasEmployees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchHasEmployees_Functions_FunctionName",
                        column: x => x.FunctionName,
                        principalTable: "Functions",
                        principalColumn: "FunctionName");
                });

            migrationBuilder.CreateTable(
                name: "Prognoses",
                columns: table => new
                {
                    PrognosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNr = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prognoses", x => x.PrognosisId);
                    table.ForeignKey(
                        name: "FK_Prognoses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_branchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Branches_Branch_branchId",
                        column: x => x.Branch_branchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrognosisHasDays",
                columns: table => new
                {
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<int>(type: "int", nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    PackagesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrognosisHasDays", x => new { x.Days_name, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_PrognosisHasDays_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrognosisHasDays_Prognoses_PrognosisId",
                        column: x => x.PrognosisId,
                        principalTable: "Prognoses",
                        principalColumn: "PrognosisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateHasDays",
                columns: table => new
                {
                    Templates_id = table.Column<int>(type: "int", nullable: false),
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    ContainerAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateHasDays", x => new { x.Templates_id, x.Days_name });
                    table.ForeignKey(
                        name: "FK_TemplateHasDays_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateHasDays_Templates_Templates_id",
                        column: x => x.Templates_id,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrognosisHasDaysHasDepartments",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<int>(type: "int", nullable: false),
                    AmountWorkersNeeded = table.Column<int>(type: "int", nullable: false),
                    HoursWorkNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrognosisHasDaysHasDepartments", x => new { x.DepartmentName, x.Days_name, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartments_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartments_Department_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Department",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartments_Prognoses_PrognosisId",
                        column: x => x.PrognosisId,
                        principalTable: "Prognoses",
                        principalColumn: "PrognosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartments_PrognosisHasDays_Days_name_PrognosisId",
                        columns: x => new { x.Days_name, x.PrognosisId },
                        principalTable: "PrognosisHasDays",
                        principalColumns: new[] { "Days_name", "PrognosisId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "6561e367-d580-4ed3-bf97-0f91f0e6624d", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAELMSZlbWGlQnw7iI2Z8q9XhB9j1YzXCLnevgmogPgp0F/HxRbaLvn4kMR3mv7f3e4w==", "+31 6 34567890", false, "8329 SK", "2d3342b2-51d4-4cf4-bf20-cd1400ecde0b", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3b96f1b7-5a37-4bea-b26f-a00a2b99e2e7", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEPG96L1JVapcDPh13mjcDz3HhQli6ha88zgwgNyBmknUVCTdbECDdWidV9xvOrqsTg==", "+31 6 56789012", false, "2933 KJ", "d93f99f5-aebf-4730-9d4d-2e4b70cc1400", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "10ce757b-79e8-4341-bddd-c0967271b831", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDfvHhFkVProMg8ZvZzCZ/3pyNs74wYjpDgXJ/wDqniUMxs9qXfeDV97STKOm8NMZg==", "06-12345678", false, "9271 GB", "986d1f3c-e03e-457c-a2c6-91230f7ae32d", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                column: "Name",
                values: new object[]
                {
                    "Belgium",
                    "Germany",
                    "Netherlands"
                });

            migrationBuilder.InsertData(
                table: "Days",
                column: "Name",
                values: new object[]
                {
                    "Dinsdag",
                    "Donderdag",
                    "Maandag",
                    "Vrijdag",
                    "Woensdag",
                    "Zaterdag",
                    "Zondag"
                });

            migrationBuilder.InsertData(
                table: "Department",
                column: "DepartmentName",
                values: new object[]
                {
                    "Coli uitladen",
                    "Kassa",
                    "Spiegelen ",
                    "Vakkenvullen",
                    "Vers"
                });

            migrationBuilder.InsertData(
                table: "Functions",
                column: "FunctionName",
                values: new object[]
                {
                    "Cashier",
                    "Manager",
                    "Stocker"
                });

            migrationBuilder.InsertData(
                table: "Norms",
                columns: new[] { "normId", "activity", "branchId", "normInSeconds", "week", "year" },
                values: new object[,]
                {
                    { 1, "Coli uitladen", 1, 90, 41, 2024 },
                    { 2, "Vakkenvullen", 1, 33, 41, 2024 },
                    { 3, "Kassa", 1, 3, 41, 2024 },
                    { 4, "Vers", 1, 7, 41, 2024 },
                    { 5, "Spiegelen", 1, 2, 41, 2024 }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "CountryName", "HouseNumber", "Name", "PostalCode", "PrognosisId", "Street", "shelfMeters" },
                values: new object[,]
                {
                    { 1, "Netherlands", "10", "Amsterdam Filiaal", "1012 LG", null, "Damrak", 0 },
                    { 2, "Belgium", "20", "Brussels Filiaal", "1000", null, "Grote Markt", 0 },
                    { 3, "Netherlands", "2", "Alkmaar Filiaal", "1811 KH", null, "Paardenmarkt", 0 },
                    { 4, "Netherlands", "15", "Rotterdam Filiaal", "3011 HE", null, "Botersloot", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "80d67e72-7b9b-48c3-82b3-24938b36f443", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMFN8A/o1KqtxnEwIICkOgED8/3s1bLCx/jbhxGNBU6P9FagTaJtKJmvYQWwstd/LQ==", "+31 6 12345678", false, "2234 AB", "97fd9e82-ee80-43b5-97f3-ec417c962915", new DateTime(2024, 11, 26, 14, 43, 17, 815, DateTimeKind.Local).AddTicks(1230), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "b7f726fa-ce1e-4ad1-9522-3d2a86ff5099", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAENQ9lQ+S8QqiVqV9w4LSzsaUi1aPcZzIa5iVVQcvPuu3fRAeXbN+1k8dlaIGKv2xEA==", "+31 6 87654321", false, "3345 CD", "7681cf69-e1ca-45d9-820b-fa3d15b98e4c", new DateTime(2024, 11, 26, 14, 43, 17, 933, DateTimeKind.Local).AddTicks(8624), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "80b3e474-6a5d-4659-977f-5d1e29f73d54", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAENbqEm+I5B8ripXNQYQY3A+/0s36etg/LBR4MVE5tpmPjv53SdqUJMxhynY6hEREDg==", "+31 6 45678901", false, "3894 HT", "2ba0c8d1-0f03-4362-be9e-e9f8cc86f48b", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "6c260ba8-2b75-48bf-a696-68e9d49318c9", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAECmYTnVZRMtk7LOzOGJ9cNA6yiKKhSZq6SxpJB3HrTHuJN08jYdAahiCWdmL31euwg==", "+31 6 67890123", false, "4293 BF", "bacd9e89-93ec-4faf-9680-6ede1d71e4e4", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "2fb188ee-6304-42bf-838a-3dd99c186b25", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHJm/a76U2JaYhawiC9I6H75EL8z0i+0m8yCqtbQ7diOuTpQSDSze9JMJlDGwat4qA==", "06-9876543", false, "12345", "42b8c9e6-7831-447a-a218-7bacc300fa36", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 3, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Cashier", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", "Stocker", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Prognoses",
                columns: new[] { "PrognosisId", "BranchId", "WeekNr", "Year" },
                values: new object[,]
                {
                    { 1, 1, 40, 2024 },
                    { 2, 1, 20, 2024 }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Branch_branchId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Basic Package" },
                    { 2, 1, "Standard Package" },
                    { 3, 2, "Premium Package" },
                    { 4, 2, "Family Package" },
                    { 5, 1, "Weekly Special" }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 26, 14, 43, 17, 815, DateTimeKind.Local).AddTicks(1230) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 26, 14, 43, 17, 933, DateTimeKind.Local).AddTicks(8624) },
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PrognosisHasDays",
                columns: new[] { "Days_name", "PrognosisId", "CustomerAmount", "PackagesAmount" },
                values: new object[,]
                {
                    { "Dinsdag", 1, 120, 60 },
                    { "Dinsdag", 2, 115, 55 },
                    { "Donderdag", 1, 110, 45 },
                    { "Donderdag", 2, 105, 42 },
                    { "Maandag", 1, 100, 50 },
                    { "Maandag", 2, 90, 40 },
                    { "Vrijdag", 1, 150, 70 },
                    { "Vrijdag", 2, 140, 68 },
                    { "Woensdag", 1, 130, 55 },
                    { "Woensdag", 2, 125, 50 },
                    { "Zaterdag", 1, 160, 80 },
                    { "Zaterdag", 2, 150, 75 },
                    { "Zondag", 1, 140, 65 },
                    { "Zondag", 2, 130, 60 }
                });

            migrationBuilder.InsertData(
                table: "TemplateHasDays",
                columns: new[] { "Days_name", "Templates_id", "ContainerAmount", "CustomerAmount" },
                values: new object[,]
                {
                    { "Dinsdag", 1, 52, 825 },
                    { "Donderdag", 1, 52, 990 },
                    { "Maandag", 1, 41, 989 },
                    { "Vrijdag", 1, 39, 1040 },
                    { "Woensdag", 1, 38, 902 },
                    { "Zaterdag", 1, 43, 953 },
                    { "Zondag", 1, 32, 872 },
                    { "Dinsdag", 2, 38, 912 },
                    { "Donderdag", 2, 45, 940 },
                    { "Maandag", 2, 42, 916 },
                    { "Vrijdag", 2, 47, 816 },
                    { "Woensdag", 2, 32, 902 },
                    { "Zaterdag", 2, 38, 842 },
                    { "Zondag", 2, 45, 885 },
                    { "Dinsdag", 3, 41, 989 },
                    { "Donderdag", 3, 36, 875 },
                    { "Maandag", 3, 53, 872 },
                    { "Vrijdag", 3, 29, 877 },
                    { "Woensdag", 3, 42, 916 },
                    { "Zaterdag", 3, 53, 945 },
                    { "Zondag", 3, 52, 880 },
                    { "Dinsdag", 4, 38, 903 },
                    { "Donderdag", 4, 42, 985 },
                    { "Maandag", 4, 49, 900 },
                    { "Vrijdag", 4, 36, 865 },
                    { "Woensdag", 4, 45, 930 },
                    { "Zaterdag", 4, 43, 950 },
                    { "Zondag", 4, 38, 950 },
                    { "Dinsdag", 5, 49, 935 },
                    { "Donderdag", 5, 41, 989 },
                    { "Maandag", 5, 52, 832 },
                    { "Vrijdag", 5, 32, 872 },
                    { "Woensdag", 5, 29, 877 },
                    { "Zaterdag", 5, 36, 771 },
                    { "Zondag", 5, 52, 885 }
                });

            migrationBuilder.InsertData(
                table: "PrognosisHasDaysHasDepartments",
                columns: new[] { "Days_name", "DepartmentName", "PrognosisId", "AmountWorkersNeeded", "HoursWorkNeeded" },
                values: new object[,]
                {
                    { "Dinsdag", "Coli uitladen", 1, 3, 25 },
                    { "Donderdag", "Coli uitladen", 1, 3, 24 },
                    { "Maandag", "Coli uitladen", 1, 3, 24 },
                    { "Vrijdag", "Coli uitladen", 1, 4, 28 },
                    { "Woensdag", "Coli uitladen", 1, 3, 26 },
                    { "Zaterdag", "Coli uitladen", 1, 4, 30 },
                    { "Zondag", "Coli uitladen", 1, 3, 27 },
                    { "Dinsdag", "Kassa", 1, 5, 35 },
                    { "Donderdag", "Kassa", 1, 5, 31 },
                    { "Maandag", "Kassa", 1, 5, 32 },
                    { "Vrijdag", "Kassa", 1, 6, 36 },
                    { "Woensdag", "Kassa", 1, 5, 34 },
                    { "Zaterdag", "Kassa", 1, 6, 38 },
                    { "Zondag", "Kassa", 1, 5, 34 },
                    { "Dinsdag", "Spiegelen", 1, 3, 22 },
                    { "Donderdag", "Spiegelen", 1, 3, 19 },
                    { "Maandag", "Spiegelen", 1, 3, 20 },
                    { "Vrijdag", "Spiegelen", 1, 4, 24 },
                    { "Woensdag", "Spiegelen", 1, 3, 21 },
                    { "Zaterdag", "Spiegelen", 1, 4, 26 },
                    { "Zondag", "Spiegelen", 1, 3, 22 },
                    { "Dinsdag", "Vakkenvullen", 1, 4, 30 },
                    { "Donderdag", "Vakkenvullen", 1, 4, 27 },
                    { "Maandag", "Vakkenvullen", 1, 4, 28 },
                    { "Vrijdag", "Vakkenvullen", 1, 5, 32 },
                    { "Woensdag", "Vakkenvullen", 1, 4, 29 },
                    { "Zaterdag", "Vakkenvullen", 1, 5, 35 },
                    { "Zondag", "Vakkenvullen", 1, 4, 30 },
                    { "Dinsdag", "Vers", 1, 2, 18 },
                    { "Donderdag", "Vers", 1, 2, 15 },
                    { "Maandag", "Vers", 1, 2, 16 },
                    { "Vrijdag", "Vers", 1, 3, 20 },
                    { "Woensdag", "Vers", 1, 2, 17 },
                    { "Zaterdag", "Vers", 1, 3, 22 },
                    { "Zondag", "Vers", 1, 2, 18 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ManagerOfBranchId",
                table: "AspNetUsers",
                column: "ManagerOfBranchId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CountryName",
                table: "Branches",
                column: "CountryName");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_PrognosisId",
                table: "Branches",
                column: "PrognosisId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHasEmployees_EmployeeId",
                table: "BranchHasEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHasEmployees_FunctionName",
                table: "BranchHasEmployees",
                column: "FunctionName");

            migrationBuilder.CreateIndex(
                name: "IX_Norms_branchId_year_week_activity",
                table: "Norms",
                columns: new[] { "branchId", "year", "week", "activity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prognoses_BranchId",
                table: "Prognoses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PrognosisHasDays_PrognosisId",
                table: "PrognosisHasDays",
                column: "PrognosisId");

            migrationBuilder.CreateIndex(
                name: "IX_PrognosisHasDaysHasDepartments_Days_name_PrognosisId",
                table: "PrognosisHasDaysHasDepartments",
                columns: new[] { "Days_name", "PrognosisId" });

            migrationBuilder.CreateIndex(
                name: "IX_PrognosisHasDaysHasDepartments_PrognosisId",
                table: "PrognosisHasDaysHasDepartments",
                column: "PrognosisId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHasDays_Days_name",
                table: "TemplateHasDays",
                column: "Days_name");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Branch_branchId",
                table: "Templates",
                column: "Branch_branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Branches_ManagerOfBranchId",
                table: "AspNetUsers",
                column: "ManagerOfBranchId",
                principalTable: "Branches",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Prognoses_PrognosisId",
                table: "Branches",
                column: "PrognosisId",
                principalTable: "Prognoses",
                principalColumn: "PrognosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prognoses_Branches_BranchId",
                table: "Prognoses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BranchHasEmployees");

            migrationBuilder.DropTable(
                name: "Norms");

            migrationBuilder.DropTable(
                name: "PrognosisHasDaysHasDepartments");

            migrationBuilder.DropTable(
                name: "TemplateHasDays");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "PrognosisHasDays");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Prognoses");
        }
    }
}
