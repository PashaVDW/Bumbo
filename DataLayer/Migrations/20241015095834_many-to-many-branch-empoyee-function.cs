using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class manytomanybranchempoyeefunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "BranchHasEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FunctionName = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHasEmployees", x => x.Id);
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
                        principalColumn: "FunctionName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c963d62-6ade-4b54-9c60-26f6b8633f96", "AQAAAAIAAYagAAAAEFA8Hre+DIeFere5QquGW4Jl5vY5KH+p77ZW8e+gOp8lEI4ZP5lHG9w61plH3QPCHg==", "e59fb4d0-9e3c-4a07-b344-8d46ab009af3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5cfd6925-41e3-42d4-bcb6-a4d8692e8374", "AQAAAAIAAYagAAAAELGKzAoBN2p1UUi83L4bpNjSR2NclvGWnqzLW3q/81BEdyhwDpBmQuHuFNY7YKfutw==", "42c361de-c05c-4dd8-a1f6-cb53e9d5b51b" });

            migrationBuilder.InsertData(
                table: "Functions",
                column: "FunctionName",
                values: new object[]
                {
                    "Cashier",
                    "Manager",
                    "Stocker"
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchHasEmployees_BranchId",
                table: "BranchHasEmployees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHasEmployees_EmployeeId",
                table: "BranchHasEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHasEmployees_FunctionName",
                table: "BranchHasEmployees",
                column: "FunctionName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchHasEmployees");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be6a2682-1629-4f8d-987a-0203f23feef7", "AQAAAAIAAYagAAAAEH6HHdaFZ8sVzr8GuuBrxjE4JRob17hWTQqu0mI3e+l2lgylFZh21s13gqbnc3xcbA==", "43168cf4-d8bd-49d1-a721-b050ba06e528" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b0728b2-6d24-43de-afb3-cb4f87b034e7", "AQAAAAIAAYagAAAAEN8ZXBXrog3LfEWkE/z+yW69XOPnd323IDUMV9ET8mTHj7GpaXjzPCiq9Gc9dmgV9A==", "f706b431-d6d3-4476-ae19-4a0120d62d34" });
        }
    }
}
