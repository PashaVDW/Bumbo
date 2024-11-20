using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class inintial : Migration
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
                    HouseNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", nullable: true)
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
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
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
                name: "Prognosis_Has_Days",
                columns: table => new
                {
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    PackagesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prognosis_Has_Days", x => new { x.Days_name, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_Prognosis_Has_Days_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prognosis_Has_Days_Prognoses_PrognosisId",
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
                name: "prognosis_Has_Days_Has_Departments",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AmountWorkersNeeded = table.Column<int>(type: "int", nullable: false),
                    HoursWorkNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prognosis_Has_Days_Has_Departments", x => new { x.DepartmentName, x.Days_name, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_prognosis_Has_Days_Has_Departments_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prognosis_Has_Days_Has_Departments_Department_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Department",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prognosis_Has_Days_Has_Departments_Prognoses_PrognosisId",
                        column: x => x.PrognosisId,
                        principalTable: "Prognoses",
                        principalColumn: "PrognosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prognosis_Has_Days_Has_Departments_Prognosis_Has_Days_Days_name_PrognosisId",
                        columns: x => new { x.Days_name, x.PrognosisId },
                        principalTable: "Prognosis_Has_Days",
                        principalColumns: new[] { "Days_name", "PrognosisId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "4d0505aa-d35f-4869-89e6-60f9a22479a8", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEGfVYKp+iLBjZRayL40h6pHas3/aboGjYEQ+w060jJYqnHY+KTu+7vJBGS97FM6Wpg==", "+31 6 34567890", false, "8329 SK", "b5d959a4-b880-43a3-9345-f76a079afffc", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "06958d76-036c-4d98-a43f-7e4963dffda8", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAELGswmoZRRRgJrwBRb5IbD5HlpQ1jDMEn0xNUNrcKuYrQ4NIB+c+omxMKkXD9IRMzg==", "+31 6 56789012", false, "2933 KJ", "703f3b3a-20e1-4f0d-81e5-e51ca8be3f60", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "d8b46e38-4f6f-4d37-806a-0942f6822fca", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGIuuQR95qzWthTbIhQQh4WKrwzt83S4CAlz2z6VrZsFoGOP6jpr3MMxDH3EmRwbNA==", "06-12345678", false, "9271 GB", "9cdb3216-6497-4b33-b1b8-30adcf2998da", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
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
                    "Friday",
                    "Maandag",
                    "Monday",
                    "Saturday",
                    "Sunday",
                    "Thursday",
                    "Tuesday",
                    "Vrijdag",
                    "Wednesday",
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
                columns: new[] { "BranchId", "ClosingTime", "CountryName", "HouseNumber", "Name", "OpeningTime", "PostalCode", "PrognosisId", "Street" },
                values: new object[,]
                {
                    { 1, new TimeOnly(18, 0, 0), "Netherlands", "10", "Amsterdam Filiaal", new TimeOnly(9, 0, 0), "1012 LG", null, "Damrak" },
                    { 2, new TimeOnly(17, 0, 0), "Belgium", "20", "Brussels Filiaal", new TimeOnly(8, 0, 0), "1000", null, "Grote Markt" },
                    { 3, new TimeOnly(21, 0, 0), "Netherlands", "2", "Alkmaar Filiaal", new TimeOnly(9, 0, 0), "1811 KH", null, "Paardenmarkt" },
                    { 4, new TimeOnly(17, 0, 0), "Netherlands", "15", "Rotterdam Filiaal", new TimeOnly(9, 0, 0), "3011 HE", null, "Botersloot" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2113b649-5eed-4dc1-91ba-14a5a26c0380", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAENsLeWbH5rt0sNle+0WnnQVIkPM0Wv5pzwdVYQ7ZN/uBRbKLRT5G67VD4MetmaZ4lQ==", "+31 6 12345678", false, "2234 AB", "1acc8b9e-0f14-4358-a690-cf9e4ddf2d1a", new DateTime(2024, 11, 20, 14, 6, 18, 954, DateTimeKind.Local).AddTicks(2445), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "7e147d23-9524-461c-b357-506b059f8931", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPH0v4OMFhITW1UclRSTMdOxmZ0AAVrp9q3I16ytMrh/roPn+5/Cb6DAt1MFIfwsHw==", "+31 6 87654321", false, "3345 CD", "8a7511d3-ccd7-4de0-88e0-032544a47b31", new DateTime(2024, 11, 20, 14, 6, 19, 55, DateTimeKind.Local).AddTicks(4062), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d4a17525-150b-46b6-80b8-941c6a9fcbc0", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEIZlGx6BBNuSpY36U96aoeFCE6ooh/t2RoSCaD2qJ9hGszX4YSlhFNYcr3WnC5oJTQ==", "+31 6 45678901", false, "3894 HT", "9b161a1d-6742-4ff5-b563-f26031802a3e", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3144db76-7457-44ac-b68e-f9ffc37c6fe9", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAELWR/iTpOf0d2ooiX3pz5GpCpcLuV4GpVco4u9d0gnl6BYZ/bf4tEy/y0vbsCu324w==", "+31 6 67890123", false, "4293 BF", "4b1cb97d-32d4-45e9-a20b-cfcd3d43cd30", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ff875910-1569-40f0-9536-23ddea034108", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJW+mb577smmT68ZXJ0W4xTtY5XiZMrm/lVxhYOJrSm6BoGYO1821bRrXkTVAoXI7Q==", "06-9876543", false, "12345", "c74f7547-fde7-4304-927a-4bba28add22b", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
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
                    { "1", 1, 40, 2024 },
                    { "2", 1, 20, 2024 }
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
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 20, 14, 6, 18, 954, DateTimeKind.Local).AddTicks(2445) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 20, 14, 6, 19, 55, DateTimeKind.Local).AddTicks(4062) },
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Prognosis_Has_Days",
                columns: new[] { "Days_name", "PrognosisId", "CustomerAmount", "PackagesAmount" },
                values: new object[,]
                {
                    { "Dinsdag", "1", 120, 60 },
                    { "Dinsdag", "2", 115, 55 },
                    { "Donderdag", "1", 110, 45 },
                    { "Donderdag", "2", 105, 42 },
                    { "Maandag", "1", 100, 50 },
                    { "Maandag", "2", 90, 40 },
                    { "Vrijdag", "1", 150, 70 },
                    { "Vrijdag", "2", 140, 68 },
                    { "Woensdag", "1", 130, 55 },
                    { "Woensdag", "2", 125, 50 },
                    { "Zaterdag", "1", 160, 80 },
                    { "Zaterdag", "2", 150, 75 },
                    { "Zondag", "1", 140, 65 },
                    { "Zondag", "2", 130, 60 }
                });

            migrationBuilder.InsertData(
                table: "TemplateHasDays",
                columns: new[] { "Days_name", "Templates_id", "ContainerAmount", "CustomerAmount" },
                values: new object[,]
                {
                    { "Friday", 1, 39, 1040 },
                    { "Monday", 1, 41, 989 },
                    { "Saturday", 1, 43, 953 },
                    { "Sunday", 1, 32, 872 },
                    { "Thursday", 1, 52, 990 },
                    { "Tuesday", 1, 52, 825 },
                    { "Wednesday", 1, 38, 902 },
                    { "Friday", 2, 47, 816 },
                    { "Monday", 2, 42, 916 },
                    { "Saturday", 2, 38, 842 },
                    { "Sunday", 2, 45, 885 },
                    { "Thursday", 2, 45, 940 },
                    { "Tuesday", 2, 38, 912 },
                    { "Wednesday", 2, 32, 902 },
                    { "Friday", 3, 29, 877 },
                    { "Monday", 3, 53, 872 },
                    { "Saturday", 3, 53, 945 },
                    { "Sunday", 3, 52, 880 },
                    { "Thursday", 3, 36, 875 },
                    { "Tuesday", 3, 41, 989 },
                    { "Wednesday", 3, 42, 916 },
                    { "Friday", 4, 36, 865 },
                    { "Monday", 4, 49, 900 },
                    { "Saturday", 4, 43, 950 },
                    { "Sunday", 4, 38, 950 },
                    { "Thursday", 4, 42, 985 },
                    { "Tuesday", 4, 38, 903 },
                    { "Wednesday", 4, 45, 930 },
                    { "Friday", 5, 32, 872 },
                    { "Monday", 5, 52, 832 },
                    { "Saturday", 5, 36, 771 },
                    { "Sunday", 5, 52, 885 },
                    { "Thursday", 5, 41, 989 },
                    { "Tuesday", 5, 49, 935 },
                    { "Wednesday", 5, 29, 877 }
                });

            migrationBuilder.InsertData(
                table: "prognosis_Has_Days_Has_Departments",
                columns: new[] { "Days_name", "DepartmentName", "PrognosisId", "AmountWorkersNeeded", "HoursWorkNeeded" },
                values: new object[,]
                {
                    { "Dinsdag", "Coli uitladen", "1", 3, 25 },
                    { "Donderdag", "Coli uitladen", "1", 3, 24 },
                    { "Maandag", "Coli uitladen", "1", 3, 24 },
                    { "Vrijdag", "Coli uitladen", "1", 4, 28 },
                    { "Woensdag", "Coli uitladen", "1", 3, 26 },
                    { "Zaterdag", "Coli uitladen", "1", 4, 30 },
                    { "Zondag", "Coli uitladen", "1", 3, 27 },
                    { "Dinsdag", "Kassa", "1", 5, 35 },
                    { "Donderdag", "Kassa", "1", 5, 31 },
                    { "Maandag", "Kassa", "1", 5, 32 },
                    { "Vrijdag", "Kassa", "1", 6, 36 },
                    { "Woensdag", "Kassa", "1", 5, 34 },
                    { "Zaterdag", "Kassa", "1", 6, 38 },
                    { "Zondag", "Kassa", "1", 5, 34 },
                    { "Dinsdag", "Spiegelen", "1", 3, 22 },
                    { "Donderdag", "Spiegelen", "1", 3, 19 },
                    { "Maandag", "Spiegelen", "1", 3, 20 },
                    { "Vrijdag", "Spiegelen", "1", 4, 24 },
                    { "Woensdag", "Spiegelen", "1", 3, 21 },
                    { "Zaterdag", "Spiegelen", "1", 4, 26 },
                    { "Zondag", "Spiegelen", "1", 3, 22 },
                    { "Dinsdag", "Vakkenvullen", "1", 4, 30 },
                    { "Donderdag", "Vakkenvullen", "1", 4, 27 },
                    { "Maandag", "Vakkenvullen", "1", 4, 28 },
                    { "Vrijdag", "Vakkenvullen", "1", 5, 32 },
                    { "Woensdag", "Vakkenvullen", "1", 4, 29 },
                    { "Zaterdag", "Vakkenvullen", "1", 5, 35 },
                    { "Zondag", "Vakkenvullen", "1", 4, 30 },
                    { "Dinsdag", "Vers", "1", 2, 18 },
                    { "Donderdag", "Vers", "1", 2, 15 },
                    { "Maandag", "Vers", "1", 2, 16 },
                    { "Vrijdag", "Vers", "1", 3, 20 },
                    { "Woensdag", "Vers", "1", 2, 17 },
                    { "Zaterdag", "Vers", "1", 3, 22 },
                    { "Zondag", "Vers", "1", 2, 18 }
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
                name: "IX_Prognosis_Has_Days_PrognosisId",
                table: "Prognosis_Has_Days",
                column: "PrognosisId");

            migrationBuilder.CreateIndex(
                name: "IX_prognosis_Has_Days_Has_Departments_Days_name_PrognosisId",
                table: "prognosis_Has_Days_Has_Departments",
                columns: new[] { "Days_name", "PrognosisId" });

            migrationBuilder.CreateIndex(
                name: "IX_prognosis_Has_Days_Has_Departments_PrognosisId",
                table: "prognosis_Has_Days_Has_Departments",
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
                name: "prognosis_Has_Days_Has_Departments");

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
                name: "Prognosis_Has_Days");

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
