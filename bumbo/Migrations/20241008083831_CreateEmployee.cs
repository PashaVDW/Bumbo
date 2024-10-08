using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    houseNumber = table.Column<int>(type: "int", nullable: false),
                    phoneNumber = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    functionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    managerOfBranchId = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isSystemManager = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
