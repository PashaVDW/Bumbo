﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                name: "Departments",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentName);
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
                name: "RequestStatus",
                columns: table => new
                {
                    RequestStatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.RequestStatusName);
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
                name: "LabourRules",
                columns: table => new
                {
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaxHoursPerDay = table.Column<int>(type: "int", nullable: false),
                    MaxEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaxHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    MaxWorkDaysPerWeek = table.Column<int>(type: "int", nullable: false),
                    MinRestDaysPerWeek = table.Column<int>(type: "int", nullable: false),
                    NumHoursWorkedBeforeBreak = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SickPayPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    OvertimePayPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    MinutesOfBreak = table.Column<int>(type: "int", nullable: false),
                    MaxHoursWithSchool = table.Column<int>(type: "int", nullable: false),
                    MinRestHoursBetweenShifts = table.Column<int>(type: "int", nullable: false),
                    MaxShiftDuration = table.Column<int>(type: "int", nullable: false),
                    MaxOvertimeHoursPerWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourRules", x => new { x.CountryName, x.AgeGroup });
                    table.ForeignKey(
                        name: "FK_LabourRules_Countries_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Countries",
                        principalColumn: "Name",
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
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSystemManager = table.Column<bool>(type: "bit", nullable: false),
                    ManagerOfBranchId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                name: "Availability",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => new { x.Date, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_Availability_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHasDepartment",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHasDepartment", x => new { x.DepartmentName, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_EmployeeHasDepartment_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHasDepartment_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSchedule",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSchedule", x => new { x.Date, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_SchoolSchedule_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
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
                    ShelfMeeters = table.Column<int>(type: "int", nullable: false),
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
                name: "BranchRequestsEmployee",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestToBranchId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchRequestsEmployee", x => new { x.BranchId, x.EmployeeId, x.RequestToBranchId });
                    table.ForeignKey(
                        name: "FK_BranchRequestsEmployee_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchRequestsEmployee_Branches_RequestToBranchId",
                        column: x => x.RequestToBranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchRequestsEmployee_RequestStatus_RequestStatusName",
                        column: x => x.RequestStatusName,
                        principalTable: "RequestStatus",
                        principalColumn: "RequestStatusName",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Norms_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
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
                    BranchBranchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Branches_BranchBranchId",
                        column: x => x.BranchBranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrognosisHasDays",
                columns: table => new
                {
                    DayName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    PackagesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrognosisHasDays", x => new { x.DayName, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_PrognosisHasDays_Days_DayName",
                        column: x => x.DayName,
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
                name: "Schedule",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSick = table.Column<bool>(type: "bit", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => new { x.EmployeeId, x.BranchId, x.Date });
                    table.ForeignKey(
                        name: "FK_Schedule_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemplateHasDays",
                columns: table => new
                {
                    TemplatesId = table.Column<int>(type: "int", nullable: false),
                    DaysName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    ContainerAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateHasDays", x => new { x.TemplatesId, x.DaysName });
                    table.ForeignKey(
                        name: "FK_TemplateHasDays_Days_DaysName",
                        column: x => x.DaysName,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateHasDays_Templates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrognosisHasDaysHasDepartment",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DayName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AmountOfWorkersNeeded = table.Column<int>(type: "int", nullable: false),
                    HoursOfWorkNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrognosisHasDaysHasDepartment", x => new { x.DepartmentName, x.DayName, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartment_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrognosisHasDaysHasDepartment_PrognosisHasDays_DayName_PrognosisId",
                        columns: x => new { x.DayName, x.PrognosisId },
                        principalTable: "PrognosisHasDays",
                        principalColumns: new[] { "DayName", "PrognosisId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SwitchRequest",
                columns: table => new
                {
                    SendToEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Declined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchRequest", x => new { x.SendToEmployeeId, x.EmployeeId, x.BranchId, x.Date });
                    table.ForeignKey(
                        name: "FK_SwitchRequest_AspNetUsers_SendToEmployeeId",
                        column: x => x.SendToEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SwitchRequest_Schedule_EmployeeId_BranchId_Date",
                        columns: x => new { x.EmployeeId, x.BranchId, x.Date },
                        principalTable: "Schedule",
                        principalColumns: new[] { "EmployeeId", "BranchId", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "e6fbc6bb-1a6b-492e-a935-1f3921477327", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFx1OeoNd0mky/eWZDRBFVVwyp2FcMtQihyI/vxTjOtdh289d13qB0JD+qRH/xD62Q==", "+31 6 34567890", false, "8329 SK", "2e31945a-0bf6-4ead-8587-d99bf08575b4", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "79d39b20-a82d-404d-b172-1eeb68460496", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOF/TsxWyJ3HtEJcJj5BN8z7fQ2+i1348L+9I6WWQ5FHRHulsIYUCMQVYK/tYqmH8A==", "+31 6 56789012", false, "2933 KJ", "1e248dca-e756-4b41-98a7-c3a809647996", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0ffe281a-4e8b-4086-ae93-ef927b16b7e7", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAENYC19O9ePwNOfrhmMmmm68dFc1v/cbkYcwB3kMl0lm5U3KvEy4bY6nL6Ex68Eg11w==", "06-12345678", false, "9271 GB", "c5c61f16-80e6-4df3-a472-0385f0746933", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
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
                table: "Departments",
                column: "DepartmentName",
                values: new object[]
                {
                    "Kassa",
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
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 19), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 20), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 20), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 21), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 22), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 22), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 23), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 23), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 23), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 24), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 24), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 24), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 26), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 27), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 28), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 29), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 29), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 30), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 30), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 30), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 1), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 12, 1), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 12, 1), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "ClosingTime", "CountryName", "HouseNumber", "Name", "OpeningTime", "PostalCode", "PrognosisId", "ShelfMeeters", "Street" },
                values: new object[,]
                {
                    { 1, new TimeOnly(18, 0, 0), "Netherlands", "10", "Amsterdam Filiaal", new TimeOnly(9, 0, 0), "1012 LG", null, 0, "Damrak" },
                    { 2, new TimeOnly(17, 0, 0), "Belgium", "20", "Brussels Filiaal", new TimeOnly(8, 0, 0), "1000", null, 0, "Grote Markt" },
                    { 3, new TimeOnly(21, 0, 0), "Netherlands", "2", "Alkmaar Filiaal", new TimeOnly(9, 0, 0), "1811 KH", null, 0, "Paardenmarkt" },
                    { 4, new TimeOnly(17, 0, 0), "Netherlands", "15", "Rotterdam Filiaal", new TimeOnly(9, 0, 0), "3011 HE", null, 0, "Botersloot" }
                });

            migrationBuilder.InsertData(
                table: "LabourRules",
                columns: new[] { "AgeGroup", "CountryName", "MaxEndTime", "MaxHoursPerDay", "MaxHoursPerWeek", "MaxHoursWithSchool", "MaxOvertimeHoursPerWeek", "MaxShiftDuration", "MaxWorkDaysPerWeek", "MinRestDaysPerWeek", "MinRestHoursBetweenShifts", "MinutesOfBreak", "NumHoursWorkedBeforeBreak", "OvertimePayPercentage", "SickPayPercentage" },
                values: new object[,]
                {
                    { "<16", "Netherlands", new TimeSpan(0, 19, 0, 0, 0), 8, 40, 12, 0, 8, 5, 2, 12, 30, 4m, 0m, 70m },
                    { ">17", "Netherlands", new TimeSpan(1, 0, 0, 0, 0), 12, 60, 0, 20, 12, 6, 1, 11, 30, 4m, 150m, 70m },
                    { "16-17", "Netherlands", new TimeSpan(0, 22, 0, 0, 0), 9, 40, 40, 0, 9, 5, 2, 12, 30, 4m, 0m, 70m }
                });

            migrationBuilder.InsertData(
                table: "SchoolSchedule",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 19), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 20), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 22), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 27), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 29), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "7964484f-7645-44e4-8453-c9674b73593a", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH3Wlb4XVoIAOBvIliOVg9T8eXclyaNf9dEomZUhIydV5TwTqaa9ETNEYTn2Biqx9g==", "+31 6 12345678", false, "2234 AB", "78a4d56a-be0f-4825-aa60-7d92d6153ee0", new DateTime(2024, 11, 27, 10, 56, 35, 171, DateTimeKind.Local).AddTicks(5405), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "8a94be4e-028b-4e03-a12a-298d11fae01c", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEF8CI7jcpfcbjVc6oqzNfbu0sZblENc0a7V8x3FxHaaX4aT/gxBjuJXb8Yt1sX0I5w==", "+31 6 87654321", false, "3345 CD", "63a4d67a-8e2c-438e-9c9a-b6fa39250d83", new DateTime(2024, 11, 27, 10, 56, 35, 246, DateTimeKind.Local).AddTicks(65), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e188b0eb-e541-46ff-a00e-bd57a752ca37", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEBYje7lXytXyvhzJPWVkreAaFjwxxbELTem+f5kAhCoONGCEYKHubGV/mBNmVPEfzw==", "+31 6 45678901", false, "3894 HT", "2f87087c-61f6-408a-a597-f28012f8d28a", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "90e4e64f-a4b5-4d1d-bec2-3d8bf34f4c74", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEAJ4FhA2YOq5jK5RHBlVZkin5z184aKWKVeQEbY1zVZkEF9nkGh6jwO78WeI+LcBBA==", "+31 6 67890123", false, "4293 BF", "b4f1150e-8830-4b1c-80ae-884183387dc8", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "96362ec5-cfdd-4cfb-8b36-7e36a57f5ef2", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDJtt1Xzu/jmrFImYOvFqRtDaganBSGpWFiKKB8KHAJrw2OlWriooiu+BGDnjvM5JQ==", "06-9876543", false, "12345", "6ef5c085-859d-4917-beaa-089382d30480", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
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
                table: "Prognoses",
                columns: new[] { "PrognosisId", "BranchId", "WeekNr", "Year" },
                values: new object[,]
                {
                    { "1", 1, 40, 2024 },
                    { "2", 1, 20, 2024 },
                    { "prognosis_week_47_2024", 1, 47, 2024 }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "BranchId", "Date", "EmployeeId", "DepartmentName", "EndTime", "IsSick", "StartTime", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(17, 0, 0), false, new TimeOnly(13, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(20, 0, 0), true, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(12, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(17, 0, 0), false, new TimeOnly(9, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "BranchBranchId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Basic Package" },
                    { 2, 1, "Standard Package" },
                    { 3, 2, "Premium Package" },
                    { 4, 2, "Family Package" },
                    { 5, 1, "Weekly Special" }
                });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 20), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(14, 30, 0), new TimeOnly(10, 30, 0) },
                    { new DateOnly(2024, 11, 20), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 21), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 22), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 23), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 23), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 24), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 25), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 27), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(14, 30, 0), new TimeOnly(10, 30, 0) },
                    { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 28), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 29), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 30), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 30), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 12, 1), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 27, 10, 56, 35, 171, DateTimeKind.Local).AddTicks(5405) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 27, 10, 56, 35, 246, DateTimeKind.Local).AddTicks(65) },
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PrognosisHasDays",
                columns: new[] { "DayName", "PrognosisId", "CustomerAmount", "PackagesAmount" },
                values: new object[,]
                {
                    { "Dinsdag", "1", 120, 60 },
                    { "Dinsdag", "2", 115, 55 },
                    { "Donderdag", "1", 110, 45 },
                    { "Donderdag", "2", 105, 42 },
                    { "Friday", "prognosis_week_47_2024", 210, 290 },
                    { "Maandag", "1", 100, 50 },
                    { "Maandag", "2", 90, 40 },
                    { "Monday", "prognosis_week_47_2024", 200, 300 },
                    { "Saturday", "prognosis_week_47_2024", 250, 320 },
                    { "Sunday", "prognosis_week_47_2024", 180, 260 },
                    { "Thursday", "prognosis_week_47_2024", 190, 270 },
                    { "Tuesday", "prognosis_week_47_2024", 150, 250 },
                    { "Vrijdag", "1", 150, 70 },
                    { "Vrijdag", "2", 140, 68 },
                    { "Wednesday", "prognosis_week_47_2024", 220, 280 },
                    { "Woensdag", "1", 130, 55 },
                    { "Woensdag", "2", 125, 50 },
                    { "Zaterdag", "1", 160, 80 },
                    { "Zaterdag", "2", 150, 75 },
                    { "Zondag", "1", 140, 65 },
                    { "Zondag", "2", 130, 60 }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "BranchId", "Date", "EmployeeId", "DepartmentName", "EndTime", "IsSick", "StartTime", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Kassa", new TimeOnly(13, 0, 0), false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Kassa", new TimeOnly(15, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), false, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(21, 30, 0), false, new TimeOnly(16, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Kassa", new TimeOnly(16, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(18, 0, 0), false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(12, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(18, 0, 0), false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(17, 0, 0), false, new TimeOnly(9, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "SchoolSchedule",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 22), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 29), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "TemplateHasDays",
                columns: new[] { "DaysName", "TemplatesId", "ContainerAmount", "CustomerAmount" },
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
                table: "PrognosisHasDaysHasDepartment",
                columns: new[] { "DayName", "DepartmentName", "PrognosisId", "AmountOfWorkersNeeded", "HoursOfWorkNeeded" },
                values: new object[,]
                {
                    { "Monday", "Kassa", "prognosis_week_47_2024", 3, 24 },
                    { "Tuesday", "Kassa", "prognosis_week_47_2024", 2, 16 },
                    { "Monday", "Vakkenvullen", "prognosis_week_47_2024", 4, 32 },
                    { "Tuesday", "Vakkenvullen", "prognosis_week_47_2024", 3, 24 },
                    { "Monday", "Vers", "prognosis_week_47_2024", 2, 16 }
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
                name: "IX_Availability_EmployeeId",
                table: "Availability",
                column: "EmployeeId");

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
                name: "IX_BranchRequestsEmployee_EmployeeId",
                table: "BranchRequestsEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchRequestsEmployee_RequestStatusName",
                table: "BranchRequestsEmployee",
                column: "RequestStatusName");

            migrationBuilder.CreateIndex(
                name: "IX_BranchRequestsEmployee_RequestToBranchId",
                table: "BranchRequestsEmployee",
                column: "RequestToBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHasDepartment_EmployeeId",
                table: "EmployeeHasDepartment",
                column: "EmployeeId");

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
                name: "IX_PrognosisHasDaysHasDepartment_DayName_PrognosisId",
                table: "PrognosisHasDaysHasDepartment",
                columns: new[] { "DayName", "PrognosisId" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_BranchId",
                table: "Schedule",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DepartmentName",
                table: "Schedule",
                column: "DepartmentName");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TemplateId",
                table: "Schedule",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSchedule_EmployeeId",
                table: "SchoolSchedule",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SwitchRequest_EmployeeId_BranchId_Date",
                table: "SwitchRequest",
                columns: new[] { "EmployeeId", "BranchId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHasDays_DaysName",
                table: "TemplateHasDays",
                column: "DaysName");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_BranchBranchId",
                table: "Templates",
                column: "BranchBranchId");

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
                name: "Availability");

            migrationBuilder.DropTable(
                name: "BranchHasEmployees");

            migrationBuilder.DropTable(
                name: "BranchRequestsEmployee");

            migrationBuilder.DropTable(
                name: "EmployeeHasDepartment");

            migrationBuilder.DropTable(
                name: "LabourRules");

            migrationBuilder.DropTable(
                name: "Norms");

            migrationBuilder.DropTable(
                name: "PrognosisHasDaysHasDepartment");

            migrationBuilder.DropTable(
                name: "SchoolSchedule");

            migrationBuilder.DropTable(
                name: "SwitchRequest");

            migrationBuilder.DropTable(
                name: "TemplateHasDays");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "PrognosisHasDays");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Prognoses");
        }
    }
}