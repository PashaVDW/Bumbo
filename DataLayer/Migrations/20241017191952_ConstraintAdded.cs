using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Norms_branchId_year_week",
                table: "Norms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b5eb9eb-10ac-4235-851c-7cf273b6e7c7", "AQAAAAIAAYagAAAAEMuBYvcVu5TcCJRjZE+lG6Oj75xrG6z00LfG/YrlwrfDsRXlsYdEiogyr9QlzbhkDg==", "c803a282-1beb-489a-8c20-f2ffe2c96261" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72b1804b-6cbd-4614-816f-733c9c77acb6", "AQAAAAIAAYagAAAAEKDOlI6zFowqOTqOXi8jKKJID38JlHmOlPpWaNia5nRyqeLA8paxsc50sxw5vzqtbg==", "039015ab-9e82-4a80-8f0a-ac58ebf8081e" });

            migrationBuilder.CreateIndex(
                name: "IX_Norms_branchId_year_week_activity",
                table: "Norms",
                columns: new[] { "branchId", "year", "week", "activity" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Norms_branchId_year_week_activity",
                table: "Norms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "140608ab-0da9-4532-be89-bd94cccf61a5", "AQAAAAIAAYagAAAAEGjHteE+BkGqmlXjjpdCKMImIDAF9JeNkjFdSPRViQ1/ev7+8G499IzfydW2aOX2Cw==", "8c759da9-8e25-4c13-b7a9-8d600f23b3e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec8e3964-a70d-4cac-9330-be7e87c05f80", "AQAAAAIAAYagAAAAEPDOFD6CvnTtqvAHjyKp6cidFeyA5G3R7Mbr6hzd+V4SAf1On+Fbk1VSrabF1CNa0g==", "986c8b8c-fde5-4cf3-a35a-64649fef0ad9" });

            migrationBuilder.CreateIndex(
                name: "IX_Norms_branchId_year_week",
                table: "Norms",
                columns: new[] { "branchId", "year", "week" },
                unique: true);
        }
    }
}
