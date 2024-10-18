using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "114b19b3-0eb9-496e-97ea-1c44c5a41aa6", "AQAAAAIAAYagAAAAECP+vT4AtYCEP4Vi/bSnltOWP/6JnDZ3xygu0XYlxxo5Na4x2DQOCTjD+sdsgtkzmA==", "4abaf2e8-7696-4c1c-a763-a6a12a03e342" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1104e2b-8a65-4f9f-a5f9-3d16535d1f52", "AQAAAAIAAYagAAAAEGm2hcRzXRAFDP/kTkJn99EK67rV7Hwt4e/RioWt//3+JqDyIuDha8A6Acu01GXA4w==", "c8d62379-65f2-4224-a9ea-37d1725d33e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "023b9fd2-25c1-4b07-813a-4620ee419675", "AQAAAAIAAYagAAAAENFBnuZTa3miQ4KqrGzGXZlzVwYHlgGa3D6dxjZ5P7PFYhoj4C5PZEs3np4GRP5I+w==", "0dc8be49-dd2a-4a98-b4f4-a688e66b3e1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7798c3e9-c3c3-47da-9ca2-250cb10d089f", "AQAAAAIAAYagAAAAEC/HJloCLW2UTDg/wRhTfnk34YVagO6qrbaS5w1yqQqK37kwA/htaiYPIL83RucBgA==", "dd23d603-8fc9-4c81-b65b-78ff1b624588" });
        }
    }
}
