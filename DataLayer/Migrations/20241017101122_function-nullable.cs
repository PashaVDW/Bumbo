using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class functionnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Functions_FunctionName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FunctionName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "AspNetUsers");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "AspNetUsers",
                type: "nvarchar(45)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "FunctionName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9954dc8e-0b32-410b-abb4-ea54aee2cce8", null, "AQAAAAIAAYagAAAAEN+OdGxMHC/guT2YESHQJUZbjv5FVJcbY5E5O8XzZwnyEXFB+43VubdTjGqdgxIFBg==", "4ab98cf6-8499-400e-a26e-17003e6f382f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "FunctionName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ba3cfd4-e3f5-489c-a0ec-8662f8b5a934", null, "AQAAAAIAAYagAAAAEJrowK0whg+oE+95zlgERkOrrI+wiWjAVFJYL/z6aK475GnHVvbx0EvHRFOwaGx5TQ==", "dac3f719-6c6c-43c1-aa9e-9b999c066089" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FunctionName",
                table: "AspNetUsers",
                column: "FunctionName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Functions_FunctionName",
                table: "AspNetUsers",
                column: "FunctionName",
                principalTable: "Functions",
                principalColumn: "FunctionName");
        }
    }
}
