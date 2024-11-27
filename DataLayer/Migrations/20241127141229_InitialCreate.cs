using System;
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
                    NumHoursWorkedBeforeBreak = table.Column<int>(type: "int", nullable: false),
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
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "48b4d517-3df8-4be5-a744-11498e99bc50", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEHnlopgZvLPqEoOVEbP0KlmZ7KYq3yguTnkFmMIkOp7wd9HY5QC/Rxi61lbHdaIuBA==", "+31 6 34567890", false, "8329 SK", "19bb3867-2a23-438a-9263-ee369e21a79d", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0124cb09-c9b6-48a9-8542-4a13831fe79d", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEMH679gO/CKaBe+V/638hAv2OM4t1VQc32/mSWCJhKy5XP9e0Jmz2oM/8jHdyAOmFg==", "+31 6 56789012", false, "2933 KJ", "37add83c-7cf1-4fcb-8909-c222a4c2b397", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ae785bd5-7eb8-40db-81e8-285c40c6aea3", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, null, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEANmBxeq9h6SGDNqoDH1DzxWf3Z1uic5+UgXfrWuRRsAAL4V0PD7QWOCIBFCmPeBsA==", "+31 6 67890123", false, "4293 BF", "11a8c558-127e-4ec0-99f0-cb37d8de0675", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "26540e33-05f7-45d8-bd22-fab36019be4c", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKl931ipWI88bavWf3SMQQGYSKC79r2/Qnz3kYVfBPMQ8PVq6HvZwhaNjMCQMNTK/A==", "06-12345678", false, "9271 GB", "ee83f955-115a-48fc-836e-388cb1541dc2", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
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
                    { 1, new TimeOnly(18, 0, 0), "Netherlands", "10", "Amsterdam Filiaal", new TimeOnly(9, 0, 0), "1012 LG", null, 0, "Damrak" },
                    { 2, new TimeOnly(17, 0, 0), "Belgium", "20", "Brussels Filiaal", new TimeOnly(8, 0, 0), "1000", null, 0, "Grote Markt" },
                    { 3, new TimeOnly(21, 0, 0), "Netherlands", "2", "Alkmaar Filiaal", new TimeOnly(9, 0, 0), "1811 KH", null, 0, "Paardenmarkt" },
                    { 4, new TimeOnly(17, 0, 0), "Netherlands", "15", "Rotterdam Filiaal", new TimeOnly(9, 0, 0), "3011 HE", null, 0, "Botersloot" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeHasDepartment",
                columns: new[] { "DepartmentName", "EmployeeId" },
                values: new object[,]
                {
                    { "Kassa", "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" },
                    { "Kassa", "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" },
                    { "Vers", "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" },
                    { "Vers", "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" }
                });

            migrationBuilder.InsertData(
                table: "LabourRules",
                columns: new[] { "AgeGroup", "CountryName", "MaxEndTime", "MaxHoursPerDay", "MaxHoursPerWeek", "MaxHoursWithSchool", "MaxOvertimeHoursPerWeek", "MaxShiftDuration", "MaxWorkDaysPerWeek", "MinRestDaysPerWeek", "MinRestHoursBetweenShifts", "MinutesOfBreak", "NumHoursWorkedBeforeBreak", "OvertimePayPercentage", "SickPayPercentage" },
                values: new object[,]
                {
                    { "<16", "Netherlands", new TimeSpan(0, 19, 0, 0, 0), 8, 40, 12, 0, 8, 5, 2, 12, 30, 4, 0m, 70m },
                    { ">17", "Netherlands", new TimeSpan(1, 0, 0, 0, 0), 12, 60, 0, 20, 12, 6, 1, 11, 30, 4, 150m, 70m },
                    { "16-17", "Netherlands", new TimeSpan(0, 22, 0, 0, 0), 9, 40, 40, 0, 9, 5, 2, 12, 30, 4, 0m, 70m }
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
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "cffe73a4-4487-4336-8384-c402c2d33794", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEN3+Aw1tqox1KZ24Mut0Xzlx3gkYq+UvVTSMw/CFvknCExCcG2qLLwz0WFK8UAUEjg==", "+31 6 12345678", false, "2234 AB", "9804acac-1a48-4673-a0d1-961476c5a097", new DateTime(2024, 11, 27, 15, 12, 27, 480, DateTimeKind.Local).AddTicks(4689), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "6a6e532a-e14e-4e11-ba7d-33661236e5a3", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAELQ4MufbRAMOwhZR2zilGnh+I0pvFA3bfmLnUJ7cAEYzzD8B9jTjPmqbsvvtZoZ3Ow==", "+31 6 87654321", false, "3345 CD", "09b0f51e-08fd-4eed-98d4-eecacfede549", new DateTime(2024, 11, 27, 15, 12, 27, 592, DateTimeKind.Local).AddTicks(8729), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ce03942e-db04-4afa-9bc6-f755b53de4ed", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEKgak8w5diTTQZ7WZppwb70wVgIIDRY2syauaNfRxLXZ7JCxKpX+b+BakB6qi0g8EQ==", "+31 6 45678901", false, "3894 HT", "94c5d6b9-d0de-4b3d-99c7-489988584352", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "931bc0df-6ef4-4cc2-93b3-f4f45d70641d", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJWydmt95MZo8zd9iK5VUlryIpyPBGPv+WJPy052XrPTSEVi6CAv4Tx9f6zuIPza/A==", "06-9876543", false, "12345", "20f6e5e0-6ef2-4e75-b814-5090c6478958", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 2, "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Manager", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Cashier", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", "Stocker", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BranchRequestsEmployee",
                columns: new[] { "BranchId", "EmployeeId", "RequestToBranchId", "DateNeeded", "DepartmentName", "EndTime", "Message", "RequestStatusName", "StartTime" },
                values: new object[,]
                {
                    { 1, "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 3, new DateTime(2024, 12, 17, 15, 12, 27, 684, DateTimeKind.Local).AddTicks(5487), "Kassa", new TimeOnly(16, 0, 0), "Overplaatsing voor trainingssessies.", "Geaccepteerd", new TimeOnly(9, 0, 0) },
                    { 3, "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 4, new DateTime(2024, 12, 7, 15, 12, 27, 684, DateTimeKind.Local).AddTicks(5483), "Vakkenvullen", new TimeOnly(17, 30, 0), "Hulp nodig vanwege ziekte van een collega.", "Afgewezen", new TimeOnly(8, 30, 0) }
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
                columns: new[] { "BranchId", "Date", "EmployeeId", "DepartmentName", "EndTime", "IsFinal", "IsSick", "StartTime", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 18), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(17, 0, 0), true, false, new TimeOnly(13, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 22), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", "Kassa", new TimeOnly(20, 0, 0), true, true, new TimeOnly(12, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(18, 0, 0), true, false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(14, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(12, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 21), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", "Vers", new TimeOnly(17, 0, 0), true, false, new TimeOnly(9, 0, 0), null }
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
                    { new DateOnly(2024, 12, 1), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", new TimeOnly(14, 0, 0), new TimeOnly(10, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 27, 15, 12, 27, 480, DateTimeKind.Local).AddTicks(4689) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 27, 15, 12, 27, 592, DateTimeKind.Local).AddTicks(8729) },
                    { 3, "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Manager", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BranchRequestsEmployee",
                columns: new[] { "BranchId", "EmployeeId", "RequestToBranchId", "DateNeeded", "DepartmentName", "EndTime", "Message", "RequestStatusName", "StartTime" },
                values: new object[,]
                {
                    { 1, "b2c2d2e2-2222-3333-4444-5555abcdefab", 2, new DateTime(2024, 12, 4, 15, 12, 27, 684, DateTimeKind.Local).AddTicks(5398), "Vers", new TimeOnly(17, 0, 0), "Overplaatsing nodig vanwege projectdeadline.", "In Afwachting", new TimeOnly(9, 0, 0) },
                    { 2, "a1b1c1d1-1111-2222-3333-4444abcdabcd", 1, new DateTime(2024, 12, 11, 15, 12, 27, 684, DateTimeKind.Local).AddTicks(5472), "Vakkenvullen", new TimeOnly(16, 0, 0), "Er zijn te weinig medewerkers op deze datum beschikbaar.", "In Afwachting", new TimeOnly(12, 0, 0) }
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
                    { 1, new DateOnly(2024, 11, 20), "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Kassa", new TimeOnly(15, 0, 0), false, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), true, false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(21, 30, 0), true, false, new TimeOnly(16, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Kassa", new TimeOnly(16, 0, 0), true, false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(12, 0, 0), true, true, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(18, 0, 0), true, false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(17, 0, 0), true, false, new TimeOnly(9, 0, 0), null }
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
