using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addphonenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "023b9fd2-25c1-4b07-813a-4620ee419675", "AQAAAAIAAYagAAAAENFBnuZTa3miQ4KqrGzGXZlzVwYHlgGa3D6dxjZ5P7PFYhoj4C5PZEs3np4GRP5I+w==", "06-9876543", "0dc8be49-dd2a-4a98-b4f4-a688e66b3e1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "7798c3e9-c3c3-47da-9ca2-250cb10d089f", "AQAAAAIAAYagAAAAEC/HJloCLW2UTDg/wRhTfnk34YVagO6qrbaS5w1yqQqK37kwA/htaiYPIL83RucBgA==", "06-12345678", "dd23d603-8fc9-4c81-b65b-78ff1b624588" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "2d567907-c4f9-4888-962a-aab1a3c27db7", "AQAAAAIAAYagAAAAECEQ6mASoItow59+vn1An3Zy4x/biMPMSKcovmxxu53vZUjpWkq7xBcBIAWMPaR/EA==", null, "4ace8e33-7cb8-4e09-a0c9-af7459147b6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "f8c432a5-2a4d-4868-8eaf-c81001346895", "AQAAAAIAAYagAAAAED1PRHaN6+Q3xxaCehJcAYSUZLu8kHSWdsXnoA49CDPNqODQooe0B8KqxOAH4i4CMg==", null, "47c6e445-9e2e-4f11-bf18-414133870383" });
        }
    }
}
