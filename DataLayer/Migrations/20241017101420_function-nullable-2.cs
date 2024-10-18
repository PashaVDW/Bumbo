using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class functionnullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchHasEmployees_Functions_FunctionName",
                table: "BranchHasEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchHasEmployees",
                table: "BranchHasEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "FunctionName",
                table: "BranchHasEmployees",
                type: "nvarchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchHasEmployees",
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d567907-c4f9-4888-962a-aab1a3c27db7", "AQAAAAIAAYagAAAAECEQ6mASoItow59+vn1An3Zy4x/biMPMSKcovmxxu53vZUjpWkq7xBcBIAWMPaR/EA==", "4ace8e33-7cb8-4e09-a0c9-af7459147b6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c432a5-2a4d-4868-8eaf-c81001346895", "AQAAAAIAAYagAAAAED1PRHaN6+Q3xxaCehJcAYSUZLu8kHSWdsXnoA49CDPNqODQooe0B8KqxOAH4i4CMg==", "47c6e445-9e2e-4f11-bf18-414133870383" });

            migrationBuilder.AddForeignKey(
                name: "FK_BranchHasEmployees_Functions_FunctionName",
                table: "BranchHasEmployees",
                column: "FunctionName",
                principalTable: "Functions",
                principalColumn: "FunctionName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchHasEmployees_Functions_FunctionName",
                table: "BranchHasEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchHasEmployees",
                table: "BranchHasEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "FunctionName",
                table: "BranchHasEmployees",
                type: "nvarchar(45)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchHasEmployees",
                table: "BranchHasEmployees",
                columns: new[] { "BranchId", "EmployeeId", "FunctionName" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c9eda95-c1e7-4206-9d69-aa324e4dcf70", "AQAAAAIAAYagAAAAEMWQQvfsizUQMr00JbSn+0WLTiUxdGY8fE4Or9NX77zw/GDbo5+CJ9/JEbYzx6QJEw==", "c9478f89-bf84-41b1-aa73-d90c5d414856" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e908211-7e75-47d5-b3ab-e40af579c807", "AQAAAAIAAYagAAAAEBGrgnH/se2iMhtycxGWZ6QhwugO1omvG1X6XkxoSDete3tr9yTPoxJaPndgUeV91g==", "5fc99fc9-5702-40b0-8de0-fdf6e1a761db" });

            migrationBuilder.AddForeignKey(
                name: "FK_BranchHasEmployees_Functions_FunctionName",
                table: "BranchHasEmployees",
                column: "FunctionName",
                principalTable: "Functions",
                principalColumn: "FunctionName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
