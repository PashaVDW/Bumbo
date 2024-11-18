﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "Prognosis_Has_Days",
                columns: table => new
                {
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<int>(type: "int", nullable: false),
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
                    PrognosisId = table.Column<int>(type: "int", nullable: false),
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
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "b14c3b76-aaef-455c-b123-a1bc83321831", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEHfDOlxSVyy8qfdSyCeWYntsQWLZwZ5T0vs4JAGKRLMnFDxoAzREa0A7qnOUYGV1+g==", "+31 6 34567890", false, "8329 SK", "3738f238-2c7c-467e-a897-efe57c28680f", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0406aa6c-a646-47b8-8408-e8fb535a1750", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEK61PA/9g2kmUBVqoG6rTF2E7L6n+k5Yyql4cqhJwtrfvhXhPrQGqf2KE8WipF/Y3w==", "+31 6 56789012", false, "2933 KJ", "a15761ee-63f4-4c36-bf3b-db49fec4ca7d", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3091f802-37d7-4d31-9d48-7a81af608875", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAKHCGtoU3eBQiMQD8hcc9eCwNN5GY/KK1Xl83kWNUxQJAdXdhf2jvCrxOWwAX4j1A==", "06-12345678", false, "9271 GB", "cd6e0346-da45-49a1-9929-74ad6da25d4a", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
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
                columns: new[] { "BranchId", "CountryName", "HouseNumber", "Name", "PostalCode", "PrognosisId", "Street" },
                values: new object[,]
                {
                    { 1, "Netherlands", "10", "Amsterdam Filiaal", "1012 LG", null, "Damrak" },
                    { 2, "Belgium", "20", "Brussels Filiaal", "1000", null, "Grote Markt" },
                    { 3, "Netherlands", "2", "Alkmaar Filiaal", "1811 KH", null, "Paardenmarkt" },
                    { 4, "Netherlands", "15", "Rotterdam Filiaal", "3011 HE", null, "Botersloot" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "95e5d986-668d-4290-9868-4d3031353a78", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAECBdWrqU0/9gujvb+W+gxZLKC9YWTOtn9FVAtFc626/uKgY/BCvcDvpWJrT2q3OJRg==", "+31 6 12345678", false, "2234 AB", "13f5b639-797c-42bc-a284-11c0c5877125", new DateTime(2024, 11, 18, 16, 1, 41, 666, DateTimeKind.Local).AddTicks(3800), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ec73cba8-dece-4d54-9b37-e2dcddcd60a2", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEP3XmGYYuAOFCVN2xAbc69yh0DNTwV7970zSwW1agLgZtQQgqtYoTqAPRKqAjk0xug==", "+31 6 87654321", false, "3345 CD", "880531e0-4825-4619-9e55-72056617ae58", new DateTime(2024, 11, 18, 16, 1, 41, 734, DateTimeKind.Local).AddTicks(7127), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cf5f010f-afde-4a15-9873-f3bb3cdf2d2b", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEPbP+EqWK7USeFVAH/QbVJnAoI35hOOQnRwiDvfJL++zzuKzyZY8MQh0EfGcFyJRhg==", "+31 6 45678901", false, "3894 HT", "479fe370-c504-482c-b0eb-fae4c1132d62", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "61299a58-3ea7-45c3-8cb6-6bb7a3fb1df5", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEFdmhrfhmrkrNl3s/UrNIZqsZj5eSO0V5m6XStnea7ctvZWqto7gIAqsfwGXEDeC9A==", "+31 6 67890123", false, "4293 BF", "5eea0fe0-e263-464a-ae44-7cd19b65b883", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "fc1bc6fe-2c95-452d-8a47-4e65307e3b74", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEELZi/hQB9VxiJSVlqk279mK17pTcKrkjIZRLnw9IBxvcRWIN1rrOfS2mycS6jTJvw==", "06-9876543", false, "12345", "5b48cb5f-f4b0-4a7d-83d6-2269f3f65648", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
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
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 18, 16, 1, 41, 666, DateTimeKind.Local).AddTicks(3800) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 18, 16, 1, 41, 734, DateTimeKind.Local).AddTicks(7127) },
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Prognosis_Has_Days",
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
                table: "prognosis_Has_Days_Has_Departments",
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