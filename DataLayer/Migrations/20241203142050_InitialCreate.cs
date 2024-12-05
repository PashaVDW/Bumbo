using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
    public partial class firstCreate : Migration
========
    public partial class InitialCreate : Migration
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
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
                    HouseNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DateNeeded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IsFinal = table.Column<bool>(type: "bit", nullable: false),
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
                    Declined = table.Column<bool>(type: "bit", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
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
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "5c15cb4b-7ab7-464e-9eeb-2c7543f219d7", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEIfuQqssZSDuntIg4x3NLCtbWBUTCGYFI4sg583XT5wrxNxO6tlDx2BB5bH2gshxhw==", "+31 6 34567890", false, "8329 SK", "e0efda38-2236-4014-a69b-9039990f88dc", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9", 0, "B004", new DateTime(1985, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0205ca0c-14cf-49d8-ad00-0471141437fb", "jan.devries@example.com", true, "Jan", 12, false, "de Vries", false, null, null, "", "JAN.DEVRIES@EXAMPLE.COM", "JAN.DEVRIES@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAMpCfzlJS63wEDt58+UY/qQ0R1g385P+BB7/sy5ZEb3pglgAPhxPFnaxc/OZGpqRw==", "+31 6 12345678", false, "1234 AB", "cc1e7bee-87a0-4138-b9f3-cd1851589134", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jan.devries@example.com" },
                    { "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", 0, "B005", new DateTime(1990, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "60e14d38-95b9-403c-8cd9-17ccc3de6e8c", "sofie.jansen@example.com", true, "Sofie", 30, false, "Jansen", false, null, null, "", "SOFIE.JANSEN@EXAMPLE.COM", "SOFIE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEITIORtul/jxnoLMVzo+ga/KjTlHzFC3QqAcec8F9+VqfCASY6YSU6ZlT0S9HaGk/A==", "+31 6 23456789", false, "9876 ZX", "1ae23c71-194d-4c77-8369-81850abed3c9", new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sofie.jansen@example.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "a83cfd57-467d-4c1b-b32a-d0128a543e19", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEGEa6Gd0PH9r64OFDX4TN36JSgeC1kULv+ODXJ5rw8yhBqmexbuRXlZmnYGUTWyAfg==", "+31 6 56789012", false, "2933 KJ", "4ce795e9-a981-4a8d-8636-a336f5dcd556", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2", 0, "B006", new DateTime(1988, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "82c053ff-0228-407d-9f85-28196c46a107", "tom.koster@example.com", true, "Tom", 15, false, "Koster", false, null, null, "", "TOM.KOSTER@EXAMPLE.COM", "TOM.KOSTER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFsoRomvIF4ctHWoPhwP0zFzwvpWL9waFeqQ6jIAw6CSSBNdYtvdbDz0C8xUxxWq2A==", "+31 6 34567890", false, "6543 BC", "76453309-3784-44ec-957a-5da1bafb24fc", new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "tom.koster@example.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "949ee553-9aa8-45bf-b055-444a51e60577", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, null, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEBPwsU/t9/fqtH2feaX1Mz4XLm3507spzzA0RIIYwGS1/MJEkQEYhw82Cbafo6B/jg==", "+31 6 67890123", false, "4293 BF", "37949469-496b-4d18-a224-043117827204", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", 0, "B007", new DateTime(1995, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "264d26cf-11f1-4b2f-bd61-2329c2548afd", "lisa.hendriks@example.com", true, "Lisa", 8, false, "Hendriks", false, null, null, "", "LISA.HENDRIKS@EXAMPLE.COM", "LISA.HENDRIKS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBaG3r9LjejRCXS25N9zApIDvvmYZ7Y36kQY0o92RYDmxqT/GkmSXr4vkpIFlQ7Zag==", "+31 6 98765432", false, "2345 PQ", "77f4508a-36cf-4aec-973e-4f24842611ac", new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "lisa.hendriks@example.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "b53f6e98-93cc-442e-a562-6f72181ee0da", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAmDpNG98pzvY627s6bj8x5RrJ+tAGupJa4+VD6WKyezWpvs5t0V4f3ItjP+7BcHMg==", "06-12345678", false, "9271 GB", "527e345d-25a8-4530-83a0-e58926dd38c3", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" },
                    { "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", 0, "B008", new DateTime(1983, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ff6bf92d-4760-4087-b181-b29811611625", "mark.willems@example.com", true, "Mark", 25, false, "Willems", false, null, null, "", "MARK.WILLEMS@EXAMPLE.COM", "MARK.WILLEMS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHOvw/YE7k8Ukn4V0R2kDCHTK8rzYi8cFYLu3FlFjJEkP7Fdb9wekE31IOZ13XOQYg==", "+31 6 12341234", false, "5678 MN", "df06bcb3-5a04-4656-af12-a899e689a719", new DateTime(2016, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "mark.willems@example.com" },
                    { "f9e8d7c6-1234-5678-90ab-cdef12345678", 0, "B009", new DateTime(1994, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "269c9bf8-cfe8-4794-8f25-4855dec39780", "eva.smit@example.com", true, "Eva", 10, false, "Smit", false, null, null, "", "EVA.SMIR@EXAMPLE.COM", "EVA.SMIR@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPty7V+jEbGSmZWt8Kh/ivdI5WEfaPNzy3NnfV026k6qLGsmmp9s7RUfu+x2neA1wA==", "+31 6 87654321", false, "5678 KL", "28ffb585-2431-4cb5-91ee-12e1d1965263", new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "eva.smit@example.com" }
========
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "0fb0d2f3-e959-49e9-8904-69f361584d44", "darlon.vandijk@hotmail.com", true, "Darlon", "5", false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEDB1cNbhO4uoYjqfD73OrQaCrCHZFD8sdctaW2yk+pkk8sGe5ZXSB5fQt54aZvpY6A==", "+31 6 34567890", false, "8329 SK", "242875bf-3ea3-42b4-a604-f79265ff1ed3", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "796cfcd8-14b1-4ed7-9d7a-d7863eb02129", "sarah.vanderven@hotmail.com", false, "Sarah", "8", false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOVjdw4LEaJ5C+yeyFGKofr0seEC70EYZojjil84OOkxBzxYgEcsNgBJAZjM6IS0OA==", "+31 6 56789012", false, "2933 KJ", "fff49a22-ee63-4a61-80e9-64fe1d2af2bd", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "889cf922-5066-464b-896f-7875c322f5a3", "david.denboer@gmail.com", false, "David", "30", false, "den Boer", false, null, null, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEOaa/VS+87C9h0xWTOBhAYc6FiFdrNJwiCwXZI2UcnWCE0VvBYbLtuuP2HXPlVzM4w==", "+31 6 67890123", false, "4293 BF", "287ee73f-d4c1-4af7-8868-de3bdcd59341", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "271e86e5-0cd5-4ee1-8adb-8276f288863a", "jane.smith@example.com", true, "Jane", "22", false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAB19igYgZY1AuE/riDYQlDd7W8zMKH43c8mirMUv75wTgTF/WPk5BB1AfJC6ohZ8w==", "06-12345678", false, "9271 GB", "dc2d64a8-3fa0-465c-817a-658e44c79477", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
========
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "127827df-df60-4486-bdd7-9922908a763b", "darlon.vandijk@hotmail.com", true, "Darlon", "5", false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEEsIHRw+T40YbDD8zHXnzrZcKV5EHpvYyaGEWGFZRJzKwpPmMdmEd812iYkvLF6LZg==", "+31 6 34567890", false, "8329 SK", "6339fd3e-1f7a-4ce8-b064-c0f36944eec0", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "c4b6529d-b629-40f9-b709-7873c628eb5f", "sarah.vanderven@hotmail.com", false, "Sarah", "8", false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEMyo3Me/Fq+DmWYoZseww24dWI+7Eipx0bGKdfk6/bJTXI7w2JseAn8RIHmVdlH9aQ==", "+31 6 56789012", false, "2933 KJ", "7fc5f98a-5e66-439b-a200-846b1bd6cad0", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "9ad820af-6c0b-451f-ad6f-98726e8ca417", "david.denboer@gmail.com", false, "David", "30", false, "den Boer", false, null, null, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEDlfLSlXbb7YowJ4LLQOUJBi8q/OEhBKotVCl0kDSxZjbOezO7/LWxapfzwK/OL+/Q==", "+31 6 67890123", false, "4293 BF", "4bc6eeeb-78e6-4c99-ac12-a6eed0d739eb", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0641af5c-70e8-463e-9473-91391bb42a17", "jane.smith@example.com", true, "Jane", "22", false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEG2OpKc0N0pzeyJz910A7ONrz/vsRJxpJe1DO74mXWyd06nhCodsFG1tQ27hQs5MYg==", "06-12345678", false, "9271 GB", "0363c851-38bc-4701-8d0b-1ae3e0d7fd40", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
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
                table: "RequestStatus",
                column: "RequestStatusName",
                values: new object[]
                {
                    "Afgewezen",
                    "Geaccepteerd",
                    "In Afwachting"
                });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 19), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 19), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 20), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 20), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 21), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 21), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 22), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 22), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 23), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 23), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 23), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 24), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 24), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 24), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 26), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 28), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 29), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 30), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 30), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 30), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 1), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 12, 1), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 12, 1), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "ClosingTime", "CountryName", "HouseNumber", "Name", "OpeningTime", "PostalCode", "PrognosisId", "ShelfMeeters", "Street" },
                values: new object[,]
                {
                    { 1, new TimeOnly(18, 0, 0), "Netherlands", "10", "Amsterdam Filiaal", new TimeOnly(9, 0, 0), "1012LG", null, 0, "Damrak" },
                    { 2, new TimeOnly(17, 0, 0), "Belgium", "20", "Brussels Filiaal", new TimeOnly(8, 0, 0), "1000", null, 0, "Grote Markt" },
                    { 3, new TimeOnly(21, 0, 0), "Netherlands", "2", "Alkmaar Filiaal", new TimeOnly(9, 0, 0), "1811KH", null, 0, "Paardenmarkt" },
                    { 4, new TimeOnly(17, 0, 0), "Netherlands", "15", "Rotterdam Filiaal", new TimeOnly(9, 0, 0), "3011HE", null, 0, "Botersloot" },
                    { 5, new TimeOnly(22, 0, 0), "Netherlands", "22", "Den Haag Filiaal", new TimeOnly(9, 0, 0), "2511AG", null, 0, "Binnenhof" },
                    { 6, new TimeOnly(18, 0, 0), "Netherlands", "18", "Leiden Filiaal", new TimeOnly(8, 0, 0), "2311GP", null, 0, "Breestraat" },
                    { 7, new TimeOnly(17, 0, 0), "Belgium", "56", "Gent Filiaal", new TimeOnly(9, 0, 0), "9000", null, 0, "Veldstraat" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeHasDepartment",
                columns: new[] { "DepartmentName", "EmployeeId" },
                values: new object[,]
                {
                    { "Kassa", "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1" },
                    { "Kassa", "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" },
                    { "Kassa", "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" },
                    { "Kassa", "f9e8d7c6-1234-5678-90ab-cdef12345678" },
                    { "Vakkenvullen", "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2" },
                    { "Vakkenvullen", "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4" },
                    { "Vers", "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" },
                    { "Vers", "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9" },
                    { "Vers", "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3" },
                    { "Vers", "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" }
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
                    { new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 19), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 20), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 22), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 27), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 29), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2bad6f3b-3644-45cf-8b24-0df81b5a4a2b", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAECoIcHUEwS+eFGhfJQwaoEX6rG2a3yn6vd9Mnin882kft5SfI3lWInB/dmZk+gEfzg==", "+31 6 12345678", false, "2234 AB", "05e15973-42ec-41c3-a69c-d82a3d10df92", new DateTime(2024, 12, 3, 15, 20, 48, 445, DateTimeKind.Local).AddTicks(4500), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "bf4d3247-a56d-4a70-abce-1dc97c228ee4", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAED6YAohNHJ9Dy+pvESNae5hTDVbmXDaEacxQ4xxaHswCOxdTlDldizwe8+XC//skkg==", "+31 6 87654321", false, "3345 CD", "f73bc38f-8389-45dc-8c54-4f6088e68128", new DateTime(2024, 12, 3, 15, 20, 48, 524, DateTimeKind.Local).AddTicks(8333), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1123a354-6ea4-4b8e-83e8-47c86b20601e", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEIuWHufxTPiGBEmbMkXP3ni92Qu/dO1Bjg9yOqD+R5VIlCQKBjGq2yW1Kr27XbN0dw==", "+31 6 45678901", false, "3894 HT", "43623a5f-9701-4d49-882f-4658e7ed67f9", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "4527dc66-a479-484b-bd46-df9aecbc695a", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEE7y8h2dF+V7tEr/s1w3/Lz2AWMMtfxg4Ly5wn31+hu6FGcrRvNsBYBaRQhAq53IMg==", "06-9876543", false, "12345", "80e74e63-82d7-400b-aee4-f14e4e06ccf9", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
========
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "17c85e29-0600-4ca4-abe4-468549bee76e", "anthony.ross@example.com", true, "Anthony", "7", false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKhO5mAAo+Y7J9z2oMnLB6FI/I5H5gdZlmFJrfEyruWUFR+eFbRFR5QCLP2C30ZBHg==", "+31 6 12345678", false, "2234 AB", "7141001e-4485-4b4c-adc0-8ae990ba7835", new DateTime(2024, 12, 4, 13, 42, 46, 293, DateTimeKind.Local).AddTicks(748), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "f5ba48f0-772b-4c25-ba85-8284a67da6da", "douwe.jansen@example.com", true, "Douwe", "12", false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEI1zEXPx01I51Jb54pGB5N1BBvnzRGltkqx+6Kjx2Uxsq+qzB/Jk5Elh6FybbN7ijg==", "+31 6 87654321", false, "3345 CD", "0319911c-1ae2-4802-ad89-346417925ecf", new DateTime(2024, 12, 4, 13, 42, 46, 387, DateTimeKind.Local).AddTicks(5882), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6b708697-89b8-424d-8a68-d08b6222cbb3", "pasha.bakker@gmail.com", false, "Pasha", "15", false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEEfTEexCIVn5c7thcTJzEl8WfAkyFBAF88+y6rtExhLVWjDVqeaiTxgVM9qb5wMyoQ==", "+31 6 45678901", false, "3894 HT", "1c13eea3-5c0f-4825-8c92-94a13d38519e", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1c5b10ba-d228-4379-9db7-4e7ffaa2cc43", "john.doe@example.com", true, "John", "10", true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOIOZSR5P7KEQpBoQeax1XJxkX7HLG+FWaEvoTdOERK3sXm1olimcmWwY6Cb8Dv2jg==", "06-9876543", false, "12345", "b15f45b5-ca40-4cf1-a578-5cc50452290a", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
========
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "525c30ed-6cf1-4830-b31f-6a483bacba9d", "anthony.ross@example.com", true, "Anthony", "7", false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJpKo2NDhUmIpB5f3DWyd98Ue7XKz4hYhQ2IYvrazVXX45eN8gbVPUdJfJ0ez3qGWA==", "+31 6 12345678", false, "2234 AB", "ead0be7f-e856-499c-8ea9-91a72769eee4", new DateTime(2024, 12, 4, 13, 53, 16, 811, DateTimeKind.Local).AddTicks(9882), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "64c50fb3-636a-4360-93dd-daa4c90e8b78", "douwe.jansen@example.com", true, "Douwe", "12", false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDcHmPJV7xT3YH7ww7Wr4l3CSxzhxHGyAgUJL3L831ZhQojCMpoe/iH+dBTvMnaMbQ==", "+31 6 87654321", false, "3345 CD", "696f10eb-cc8a-4d29-9f12-4f5f88fc9de4", new DateTime(2024, 12, 4, 13, 53, 16, 858, DateTimeKind.Local).AddTicks(7707), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b8d8620a-3a51-4277-bb81-ed90a623867b", "pasha.bakker@gmail.com", false, "Pasha", "15", false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEMJqFr8A2F0J+SEA5+6WcJxkU3iEeu18i7Rf1aFiqABB6aswmUDsT4cNkJeTI7fvZg==", "+31 6 45678901", false, "3894 HT", "c55071f4-1cd6-40cc-bb2d-e2cd69a84584", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "a5d1cbca-6669-4975-896c-494f34b890d8", "john.doe@example.com", true, "John", "10", true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAENTu9v545usBIpjMEEdia8JCLfdI2aaaBn8h29Ip/mH5iXuBoqE8vj7RZx2AZyUt1Q==", "06-9876543", false, "12345", "583ca062-722e-4729-bbf8-2bf0e4fe64e8", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 1, "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Stocker", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Cashier", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", "Stocker", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9", "Manager", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2", "Cashier", new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", "Stocker", new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Cashier", new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", "Stocker", new DateTime(2016, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "f9e8d7c6-1234-5678-90ab-cdef12345678", "Stocker", new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BranchRequestsEmployee",
                columns: new[] { "BranchId", "EmployeeId", "RequestToBranchId", "DateNeeded", "DepartmentName", "EndTime", "Message", "RequestStatusName", "StartTime" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { 1, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 3, new DateTime(2024, 12, 23, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2278), "Kassa", new TimeOnly(16, 0, 0), "Overplaatsing voor trainingssessies.", "Geaccepteerd", new TimeOnly(9, 0, 0) },
                    { 2, "f9e8d7c6-1234-5678-90ab-cdef12345678", 1, new DateTime(2024, 12, 13, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2299), "Kassa", new TimeOnly(18, 0, 0), "Overplaatsing wegens tijdelijke afwezigheid van manager.", "In Afwachting", new TimeOnly(10, 0, 0) },
                    { 3, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 4, new DateTime(2024, 12, 13, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2273), "Vakkenvullen", new TimeOnly(17, 30, 0), "Hulp nodig vanwege ziekte van een collega.", "Afgewezen", new TimeOnly(8, 30, 0) },
                    { 3, "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9", 4, new DateTime(2024, 12, 17, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2330), "Vers", new TimeOnly(16, 30, 0), "Verzoek om overplaatsing vanwege persoonlijke redenen, afgewezen.", "Afgewezen", new TimeOnly(8, 30, 0) },
                    { 4, "c5d6e7f8-90g1-2a34-b5c6-d7e8f9g0h1i2", 5, new DateTime(2024, 12, 8, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2377), "Vakkenvullen", new TimeOnly(17, 0, 0), "Overplaatsing gewenst door nieuwe strategische focus in het bedrijf.", "In Afwachting", new TimeOnly(9, 0, 0) },
                    { 5, "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", 6, new DateTime(2024, 12, 23, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2384), "Vers", new TimeOnly(16, 0, 0), "Verzoek om overplaatsing vanwege training in nieuwe technologie.", "In Afwachting", new TimeOnly(8, 0, 0) }
========
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { 1, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 3, new DateTime(2024, 12, 24, 13, 42, 46, 471, DateTimeKind.Local).AddTicks(8738), "Kassa", new TimeOnly(16, 0, 0), "Overplaatsing voor trainingssessies.", "Geaccepteerd", new TimeOnly(9, 0, 0) },
                    { 3, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 4, new DateTime(2024, 12, 14, 13, 42, 46, 471, DateTimeKind.Local).AddTicks(8732), "Vakkenvullen", new TimeOnly(17, 30, 0), "Hulp nodig vanwege ziekte van een collega.", "Afgewezen", new TimeOnly(8, 30, 0) }
========
                    { 1, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 3, new DateTime(2024, 12, 24, 13, 53, 16, 904, DateTimeKind.Local).AddTicks(9553), "Kassa", new TimeOnly(16, 0, 0), "Overplaatsing voor trainingssessies.", "Geaccepteerd", new TimeOnly(9, 0, 0) },
                    { 3, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 4, new DateTime(2024, 12, 14, 13, 53, 16, 904, DateTimeKind.Local).AddTicks(9549), "Vakkenvullen", new TimeOnly(17, 30, 0), "Hulp nodig vanwege ziekte van een collega.", "Afgewezen", new TimeOnly(8, 30, 0) }
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
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
                    { 5, "Spiegelen", 1, 2, 41, 2024 },
                    { 6, "Coli uitladen", 1, 95, 42, 2024 },
                    { 7, "Vakkenvullen", 1, 35, 42, 2024 },
                    { 8, "Kassa", 1, 3, 42, 2024 },
                    { 9, "Vers", 1, 8, 42, 2024 },
                    { 10, "Spiegelen", 1, 3, 42, 2024 },
                    { 11, "Coli uitladen", 2, 96, 41, 2024 },
                    { 12, "Vakkenvullen", 2, 32, 41, 2024 },
                    { 13, "Kassa", 2, 3, 41, 2024 },
                    { 14, "Vers", 2, 7, 41, 2024 },
                    { 15, "Spiegelen", 2, 2, 41, 2024 },
                    { 16, "Coli uitladen", 2, 91, 42, 2024 },
                    { 17, "Vakkenvullen", 2, 32, 42, 2024 },
                    { 18, "Kassa", 2, 4, 42, 2024 },
                    { 19, "Vers", 2, 7, 42, 2024 },
                    { 20, "Spiegelen", 2, 2, 42, 2024 }
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
                columns: new[] { "BranchId", "Date", "EmployeeId", "DepartmentName", "EndTime", "IsFinal", "IsSick", "StartTime", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 20), "a3b3c4d5-67f8-9a01-b2c3-d4e5f6g7h8i9", "Vakkenvullen", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", "Kassa", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", "Kassa", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", "Kassa", new TimeOnly(13, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "b4c5d6e7-89f0-1a23-b4c5-d6e7f8g9h0i1", "Kassa", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(17, 0, 0), true, false, new TimeOnly(13, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(15, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(13, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(17, 0, 0), true, false, new TimeOnly(13, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { 1, new DateOnly(2024, 11, 21), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(15, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Vers", new TimeOnly(15, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Vers", new TimeOnly(20, 0, 0), true, false, new TimeOnly(15, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "d6e7f8g9-01h2-3a45-b6c7-d8e9f0g1h2i3", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(12, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(20, 0, 0), true, false, new TimeOnly(15, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", "Vakkenvullen", new TimeOnly(18, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", "Kassa", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", "Vakkenvullen", new TimeOnly(20, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "e7f8g9h0-12i3-4a56-b7c8-d9e0f1g2h3i4", "Vakkenvullen", new TimeOnly(18, 0, 0), true, false, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "f9e8d7c6-1234-5678-90ab-cdef12345678", "Kassa", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "f9e8d7c6-1234-5678-90ab-cdef12345678", "Kassa", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "f9e8d7c6-1234-5678-90ab-cdef12345678", "Kassa", new TimeOnly(21, 0, 0), true, false, new TimeOnly(15, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "f9e8d7c6-1234-5678-90ab-cdef12345678", "Kassa", new TimeOnly(17, 0, 0), true, false, new TimeOnly(13, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "f9e8d7c6-1234-5678-90ab-cdef12345678", "Kassa", new TimeOnly(14, 0, 0), true, false, new TimeOnly(10, 0, 0), null }
========
                    { 1, new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(12, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(17, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 29), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 12, 5), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null }
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "BranchBranchId", "Name" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { 1, 1, "Basic Package" },
                    { 2, 1, "Standard Package" },
                    { 3, 2, "Premium Package" },
                    { 4, 2, "Family Package" },
                    { 5, 1, "Weekly Special" },
                    { 6, 3, "Basic Package" },
                    { 7, 3, "Weekly Special" },
                    { 8, 4, "Basic Package" },
                    { 9, 4, "Weekly Special" },
                    { 10, 5, "Basic Package" },
                    { 11, 5, "Weekly Special" },
                    { 12, 6, "Basic Package" },
                    { 13, 6, "Weekly Special" },
                    { 14, 7, "Basic Package" },
                    { 15, 7, "Weekly Special" }
========
                    { 1, 1, "Basispakket" },
                    { 2, 1, "Standaardpakket" },
                    { 3, 2, "Premium pakket" },
                    { 4, 2, "Gezinspakket" },
                    { 5, 1, "Wekelijkse special" }
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
                });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 20), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(14, 30, 0), new TimeOnly(10, 30, 0) },
                    { new DateOnly(2024, 11, 20), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 21), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 21), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 22), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 22), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 23), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 23), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 24), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 24), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 25), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(11, 30, 0) },
                    { new DateOnly(2024, 11, 27), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(14, 30, 0), new TimeOnly(10, 30, 0) },
                    { new DateOnly(2024, 11, 27), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 28), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 11, 28), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(13, 30, 0), new TimeOnly(9, 30, 0) },
                    { new DateOnly(2024, 11, 29), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 29), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 30), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 30), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 30), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(12, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 12, 1), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 1), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 12, 5), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(21, 0, 0), new TimeOnly(8, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 12, 3, 15, 20, 48, 445, DateTimeKind.Local).AddTicks(4500) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 12, 3, 15, 20, 48, 524, DateTimeKind.Local).AddTicks(8333) },
========
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 12, 4, 13, 42, 46, 293, DateTimeKind.Local).AddTicks(748) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 12, 4, 13, 42, 46, 387, DateTimeKind.Local).AddTicks(5882) },
========
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 12, 4, 13, 53, 16, 811, DateTimeKind.Local).AddTicks(9882) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 12, 4, 13, 53, 16, 858, DateTimeKind.Local).AddTicks(7707) },
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BranchRequestsEmployee",
                columns: new[] { "BranchId", "EmployeeId", "RequestToBranchId", "DateNeeded", "DepartmentName", "EndTime", "Message", "RequestStatusName", "StartTime" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
                    { 1, "b2c2d2e2-2222-3333-4444-5555abcdefab", 2, new DateTime(2024, 12, 10, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2168), "Vers", new TimeOnly(17, 0, 0), "Overplaatsing nodig vanwege projectdeadline.", "In Afwachting", new TimeOnly(9, 0, 0) },
                    { 2, "a1b1c1d1-1111-2222-3333-4444abcdabcd", 1, new DateTime(2024, 12, 17, 15, 20, 49, 134, DateTimeKind.Local).AddTicks(2267), "Vakkenvullen", new TimeOnly(16, 0, 0), "Er zijn te weinig medewerkers op deze datum beschikbaar.", "In Afwachting", new TimeOnly(12, 0, 0) }
========
<<<<<<<< HEAD:DataLayer/Migrations/20241204124247_firstCreate.cs
                    { 1, "b2c2d2e2-2222-3333-4444-5555abcdefab", 2, new DateTime(2024, 12, 11, 13, 42, 46, 471, DateTimeKind.Local).AddTicks(8587), "Vers", new TimeOnly(17, 0, 0), "Overplaatsing nodig vanwege projectdeadline.", "In Afwachting", new TimeOnly(9, 0, 0) },
                    { 2, "a1b1c1d1-1111-2222-3333-4444abcdabcd", 1, new DateTime(2024, 12, 18, 13, 42, 46, 471, DateTimeKind.Local).AddTicks(8680), "Vakkenvullen", new TimeOnly(16, 0, 0), "Er zijn te weinig medewerkers op deze datum beschikbaar.", "In Afwachting", new TimeOnly(12, 0, 0) }
========
                    { 1, "b2c2d2e2-2222-3333-4444-5555abcdefab", 2, new DateTime(2024, 12, 11, 13, 53, 16, 904, DateTimeKind.Local).AddTicks(9477), "Vers", new TimeOnly(17, 0, 0), "Overplaatsing nodig vanwege projectdeadline.", "In Afwachting", new TimeOnly(9, 0, 0) },
                    { 2, "a1b1c1d1-1111-2222-3333-4444abcdabcd", 1, new DateTime(2024, 12, 18, 13, 53, 16, 904, DateTimeKind.Local).AddTicks(9545), "Vakkenvullen", new TimeOnly(16, 0, 0), "Er zijn te weinig medewerkers op deze datum beschikbaar.", "In Afwachting", new TimeOnly(12, 0, 0) }
>>>>>>>> 728fb77e0443928a05947bb0056259b320bb603b:DataLayer/Migrations/20241204125317_InitialCreate.cs
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
                });

            migrationBuilder.InsertData(
                table: "EmployeeHasDepartment",
                columns: new[] { "DepartmentName", "EmployeeId" },
                values: new object[,]
                {
                    { "Kassa", "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9" },
                    { "Vakkenvullen", "a1b1c1d1-1111-2222-3333-4444abcdabcd" },
                    { "Vakkenvullen", "b2c2d2e2-2222-3333-4444-5555abcdefab" }
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
                columns: new[] { "BranchId", "Date", "EmployeeId", "DepartmentName", "EndTime", "IsFinal", "IsSick", "StartTime", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Kassa", new TimeOnly(13, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), true, false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), true, false, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), true, false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(21, 30, 0), true, false, new TimeOnly(16, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 20), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(22, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(12, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(12, 0, 0), true, true, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(18, 0, 0), true, false, new TimeOnly(9, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(13, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(12, 0, 0), true, false, new TimeOnly(8, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "SchoolSchedule",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 18), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 19), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 19), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 20), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 21), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 21), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 22), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 22), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 22), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 25), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 25), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 26), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(18, 30, 0), new TimeOnly(15, 30, 0) },
                    { new DateOnly(2024, 11, 26), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(18, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 11, 27), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(16, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 11, 27), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(19, 0, 0), new TimeOnly(16, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 28), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(15, 30, 0), new TimeOnly(12, 30, 0) },
                    { new DateOnly(2024, 11, 29), "a1b1c1d1-1111-2222-3333-4444abcdabcd", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 29), "b2c2d2e2-2222-3333-4444-5555abcdefab", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", new TimeOnly(17, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 11, 29), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(17, 0, 0), new TimeOnly(14, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "TemplateHasDays",
                columns: new[] { "DaysName", "TemplatesId", "ContainerAmount", "CustomerAmount" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241203142050_InitialCreate.cs
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
                    { "Wednesday", 5, 29, 877 },
                    { "Friday", 6, 38, 1045 },
                    { "Monday", 6, 42, 987 },
                    { "Saturday", 6, 42, 957 },
                    { "Sunday", 6, 33, 874 },
                    { "Thursday", 6, 53, 992 },
                    { "Tuesday", 6, 51, 827 },
                    { "Wednesday", 6, 39, 901 },
                    { "Friday", 7, 33, 871 },
                    { "Monday", 7, 53, 835 },
                    { "Saturday", 7, 38, 761 },
                    { "Sunday", 7, 51, 889 },
                    { "Thursday", 7, 40, 983 },
                    { "Tuesday", 7, 50, 934 },
                    { "Wednesday", 7, 27, 879 },
                    { "Friday", 8, 38, 1045 },
                    { "Monday", 8, 42, 987 },
                    { "Saturday", 8, 42, 957 },
                    { "Sunday", 8, 33, 874 },
                    { "Thursday", 8, 53, 992 },
                    { "Tuesday", 8, 51, 827 },
                    { "Wednesday", 8, 39, 901 },
                    { "Friday", 9, 33, 871 },
                    { "Monday", 9, 53, 835 },
                    { "Saturday", 9, 38, 761 },
                    { "Sunday", 9, 51, 889 },
                    { "Thursday", 9, 40, 983 },
                    { "Tuesday", 9, 50, 934 },
                    { "Wednesday", 9, 27, 879 },
                    { "Friday", 10, 38, 1045 },
                    { "Monday", 10, 42, 987 },
                    { "Saturday", 10, 42, 957 },
                    { "Sunday", 10, 33, 874 },
                    { "Thursday", 10, 53, 992 },
                    { "Tuesday", 10, 51, 827 },
                    { "Wednesday", 10, 39, 901 },
                    { "Friday", 11, 33, 871 },
                    { "Monday", 11, 53, 835 },
                    { "Saturday", 11, 38, 761 },
                    { "Sunday", 11, 51, 889 },
                    { "Thursday", 11, 40, 983 },
                    { "Tuesday", 11, 50, 934 },
                    { "Wednesday", 11, 27, 879 },
                    { "Friday", 12, 38, 1045 },
                    { "Monday", 12, 42, 987 },
                    { "Saturday", 12, 42, 957 },
                    { "Sunday", 12, 33, 874 },
                    { "Thursday", 12, 53, 992 },
                    { "Tuesday", 12, 51, 827 },
                    { "Wednesday", 12, 39, 901 },
                    { "Friday", 13, 33, 871 },
                    { "Monday", 13, 53, 835 },
                    { "Saturday", 13, 38, 761 },
                    { "Sunday", 13, 51, 889 },
                    { "Thursday", 13, 40, 983 },
                    { "Tuesday", 13, 50, 934 },
                    { "Wednesday", 13, 27, 879 },
                    { "Friday", 14, 37, 1041 },
                    { "Monday", 14, 44, 989 },
                    { "Saturday", 14, 44, 947 },
                    { "Sunday", 14, 32, 879 },
                    { "Thursday", 14, 55, 988 },
                    { "Tuesday", 14, 49, 817 },
                    { "Wednesday", 14, 38, 924 },
                    { "Friday", 15, 35, 842 },
                    { "Monday", 15, 57, 825 },
                    { "Saturday", 15, 37, 790 },
                    { "Sunday", 15, 52, 878 },
                    { "Thursday", 15, 38, 994 },
                    { "Tuesday", 15, 54, 914 },
                    { "Wednesday", 15, 25, 868 }
========
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
>>>>>>>> development:DataLayer/Migrations/20241204124247_firstCreate.cs
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
