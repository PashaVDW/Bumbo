using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreate : Migration
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
                    AgeGroup = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MaxHoursPerDay = table.Column<int>(type: "int", nullable: false),
                    MaxEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MaxHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    MaxWorkDaysPerWeek = table.Column<int>(type: "int", nullable: false),
                    MinRestDaysPerWeek = table.Column<int>(type: "int", nullable: false),
                    NumHoursWorkedBeforeBreak = table.Column<int>(type: "int", nullable: false),
                    SickPayPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimePayPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinutesOfBreak = table.Column<int>(type: "int", nullable: false),
                    MaxHoursWithSchool = table.Column<int>(type: "int", nullable: false),
                    MinRestHoursBetweenShifts = table.Column<int>(type: "int", nullable: false),
                    MaxShiftDuration = table.Column<int>(type: "int", nullable: false),
                    MaxOvertimeHoursPerWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourRules", x => x.CountryName);
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
<<<<<<<< HEAD:DataLayer/Migrations/20241122124041_firstCreate.cs
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "830a9ea2-8630-4793-8195-befa89907b99", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFsb1SZN06+rX03aY0nTXQm0xHycZJegB0WmfOxynf/c6kdhDaPUOSng+zjwqLlVEw==", "+31 6 34567890", false, "8329 SK", "a8a75b04-fb4e-48c1-80ed-6daed69533c1", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ad734f11-d848-45e7-9acc-737405ce393f", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFynWilMIPB6aP3KArt+wdkGQy+O1glObmJtMKU8sdfd1L0mcnIgIAxQtRtfpQ0IhQ==", "+31 6 56789012", false, "2933 KJ", "0a6b7e76-8bc7-42bd-b7c1-ce1725e2caf6", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a88ca50f-9aea-4fb2-8962-37ca40da3ebc", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAQFSH/lID0DH1Cwl7voA4hkaznnLjxG2su6KcsbBo7+/QhDQePu9DWav7jTJ28aCA==", "06-12345678", false, "9271 GB", "c973ab50-b76c-4600-8248-30be80ab1c1f", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
========
                    { "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", 0, "B003", new DateTime(1992, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "9e7d6020-0e1f-4cdd-8ddc-ed4f34a6a781", "darlon.vandijk@hotmail.com", true, "Darlon", 5, false, "van Dijk", false, null, null, "", "DARLON.VANDIJK@HOTMAIL.COM", "DARLON.VANDIJK@HOTMAIL.COM", "AQAAAAIAAYagAAAAECYudSWnjAvjKIaFpbRvZOaXRamBNf7NaVuIzrzpmAAGYLDsUTjt/9q/kNO/dW3o3A==", "+31 6 34567890", false, "8329 SK", "04583316-ea54-460a-93b9-d8b3edbba155", new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "darlon.vandijk@hotmail.com" },
                    { "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", 0, "B005", new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "2c31baf5-730e-4e9e-aa3e-d5b3bf66fa91", "sarah.vanderven@hotmail.com", false, "Sarah", 8, false, "van der Ven", false, null, null, "", "SARAH.VANDERVEN@HOTMAIL.COM", "SARAH.VANDERVEN@HOTMAIL.COM", "AQAAAAIAAYagAAAAEL41JJlECexMytcr1mW3+z9+vU+0Bq/IDpomzR+1Fauhlcb3dTinao9D6yPCqo0iqw==", "+31 6 56789012", false, "2933 KJ", "3f9f3205-ac37-47ff-99af-51ef2fc17d07", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "sarah.vanderven@hotmail.com" },
                    { "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", 0, "B002", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "5d895bdd-5e51-414a-90e0-ebbc1a4262fe", "jane.smith@example.com", true, "Jane", 22, false, "Smith", false, null, null, "B.", "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEGuMMjwTbaaUMjd+1LKzhq8l23do5xZ/Ivd26N3v7emiDoxsZUJD5QT2bQa+vu90g==", "06-12345678", false, "9271 GB", "e19a5b00-1ed1-43e0-9584-7e1396b5768c", new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "jane.smith@example.com" }
>>>>>>>> development:DataLayer/Migrations/20241121151424_FirstCreate.cs
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BID", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HouseNumber", "IsSystemManager", "LastName", "LockoutEnabled", "LockoutEnd", "ManagerOfBranchId", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241122124041_firstCreate.cs
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "b3fb5e29-b61e-4c2a-bccc-d17ff99ae373", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPlt6TjQktcM9JMlLssZgaWv//FwkT8Mbaenw0c7DrLBg1BdgVr6e3HpnA1VhCy9Ug==", "+31 6 12345678", false, "2234 AB", "6899a256-eba8-4be2-9e89-fa7742c9ce66", new DateTime(2024, 11, 22, 13, 40, 40, 125, DateTimeKind.Local).AddTicks(559), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "2437c366-f7e2-43e1-93d2-766effcd1b6b", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDvjTyMC55gY3qHU1bMIqUgrR0KtaCuDGe3W8iqNDRRCdEEOwesAAUGTOr+/LLr5lA==", "+31 6 87654321", false, "3345 CD", "df7f2163-a499-4d3a-9d64-aad018cc8738", new DateTime(2024, 11, 22, 13, 40, 40, 185, DateTimeKind.Local).AddTicks(5149), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "997e9565-38d2-42b9-a313-3bc6546ce03e", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAELBldKVqKoee2eHmC65fxA+j92Bx4G2dtj9Hi5TgyC2GxAHh7hx6tVwQH5zHq+ilog==", "+31 6 45678901", false, "3894 HT", "dc34a253-e310-4f1f-a4b3-b3870f3b1541", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "cf80f680-82d6-4f17-96cd-8e797b0c7291", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEEpPH/06xJiHYPU5fcSMNdDix/b53e2Y/bEly4Y6Do5M9PMr4ot3R484SoL4KN9hXg==", "+31 6 67890123", false, "4293 BF", "87c049b1-5a41-4ae3-a894-56fbf98bb5da", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "59815089-514d-406a-856f-4bd4b1972096", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOcBHagvBYRS/dOCTYtW+VYDZAimNCWc9r6N7hyWKqXyY+ueF4gZnBbiFVUz/31LYA==", "06-9876543", false, "12345", "c435a8df-d2ad-4f3c-817a-3f9fb2c5ecca", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
========
                    { "a1b1c1d1-1111-2222-3333-4444abcdabcd", 0, "B012", new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "1f816ff5-cb51-4491-b223-6442c9ff0afa", "anthony.ross@example.com", true, "Anthony", 7, false, "Ross", false, null, 1, "", "ANTHONY.ROSS@EXAMPLE.COM", "ANTHONY.ROSS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFZAaukdgumAkrkwUvQ0vsi41v0ggsGBj7HJTF0ZjHqZvqPqGoUaVyNa6cZC+r7jtg==", "+31 6 12345678", false, "2234 AB", "cac26d8b-3e47-4500-98b8-aea61a4d8aae", new DateTime(2024, 11, 21, 16, 14, 23, 674, DateTimeKind.Local).AddTicks(8712), false, "anthony.ross@example.com" },
                    { "b2c2d2e2-2222-3333-4444-5555abcdefab", 0, "B013", new DateTime(1987, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0a7c2f1b-4306-41b3-ae31-2e44b393f05d", "douwe.jansen@example.com", true, "Douwe", 12, false, "Jansen", false, null, 2, "", "DOUWE.JANSEN@EXAMPLE.COM", "DOUWE.JANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMFLbVm4Bfm221NAjI0DXOJIjrWRfR/Zc7QNvhdtmtztl6HgcKLmn7WHYijzmU1+ag==", "+31 6 87654321", false, "3345 CD", "7eae1dfa-34dc-4c30-a6fc-e86c709c2f78", new DateTime(2024, 11, 21, 16, 14, 23, 740, DateTimeKind.Local).AddTicks(6190), false, "douwe.jansen@example.com" },
                    { "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", 0, "B004", new DateTime(1980, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c98cf4b4-26cf-41a6-b1e6-6d46de424268", "pasha.bakker@gmail.com", false, "Pasha", 15, false, "Bakker", false, null, 3, "", "PASHA.BAKKER@GMAIL.COM", "PASHA.BAKKER@GMAIL.COM", "AQAAAAIAAYagAAAAEJEdqTuOralq6i/ot1h+rMIRr7Afg69ebJgRGuo23p9m2Y65WURk7TPjNh3zx+wzJg==", "+31 6 45678901", false, "3894 HT", "70b1676b-2e4e-4b47-b8e4-b9684cab4c08", new DateTime(2010, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "pasha.bakker@gmail.com" },
                    { "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", 0, "B006", new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "30a0afe1-708a-4e62-a0c7-babdf13002cd", "david.denboer@gmail.com", false, "David", 30, false, "den Boer", false, null, 2, "", "DAVID.DENBOER@GMAIL.COM", "DAVID.DENBOER@GMAIL.COM", "AQAAAAIAAYagAAAAEGsfbceuMvUQGnvhltRmQ6RmDSnajtkyzbg33D0+uRw+qyFxHVrO1uNE4hSTbwENGg==", "+31 6 67890123", false, "4293 BF", "218b0f48-242c-43c5-ab6b-5bad2cf7f505", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "david.denboer@gmail.com" },
                    { "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", 0, "B001", new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "6ac56c0b-2c83-4c29-a029-d902b975742e", "john.doe@example.com", true, "John", 10, true, "Doe", false, null, 1, "A.", "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEH0dO14vMC6BIWsJ/4sWDSLrH5xa5PWdh35wgUXykt3XXAKkDrjEIVnBfxW5VNl8CA==", "06-9876543", false, "12345", "e561854a-d302-4453-84b6-772987c6f677", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "john.doe@example.com" }
>>>>>>>> development:DataLayer/Migrations/20241121151424_FirstCreate.cs
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
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName", "StartDate" },
                values: new object[,]
                {
<<<<<<<< HEAD:DataLayer/Migrations/20241122124041_firstCreate.cs
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 22, 13, 40, 40, 125, DateTimeKind.Local).AddTicks(559) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 22, 13, 40, 40, 185, DateTimeKind.Local).AddTicks(5149) },
========
                    { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd", "Cashier", new DateTime(2024, 11, 21, 16, 14, 23, 674, DateTimeKind.Local).AddTicks(8712) },
                    { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab", "Stocker", new DateTime(2024, 11, 21, 16, 14, 23, 740, DateTimeKind.Local).AddTicks(6190) },
>>>>>>>> development:DataLayer/Migrations/20241121151424_FirstCreate.cs
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
                    { 1, new DateOnly(2024, 11, 18), "b2c2d2e2-2222-3333-4444-5555abcdefab", "Vakkenvullen", new TimeOnly(16, 0, 0), false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Vakkenvullen", new TimeOnly(21, 30, 0), false, new TimeOnly(16, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 24), "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9", "Kassa", new TimeOnly(16, 0, 0), false, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", "Vers", new TimeOnly(18, 0, 0), false, new TimeOnly(14, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 18), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(12, 0, 0), true, new TimeOnly(8, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 19), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(18, 0, 0), false, new TimeOnly(10, 0, 0), null },
                    { 1, new DateOnly(2024, 11, 23), "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3", "Vakkenvullen", new TimeOnly(17, 0, 0), false, new TimeOnly(9, 0, 0), null }
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
