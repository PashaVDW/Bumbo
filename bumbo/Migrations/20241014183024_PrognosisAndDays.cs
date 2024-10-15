using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class PrognosisAndDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "prognoses",
                columns: table => new
                {
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    WeekNr = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prognoses", x => x.PrognosisId);
                    table.ForeignKey(
                        name: "FK_prognoses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prognosis_Has_Days",
                columns: table => new
                {
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrognosisId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    PackagesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prognosis_Has_Days", x => new { x.Days_name, x.PrognosisId });
                    table.ForeignKey(
                        name: "FK_Prognosis_Has_Days_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prognosis_Has_Days_prognoses_PrognosisId",
                        column: x => x.PrognosisId,
                        principalTable: "prognoses",
                        principalColumn: "PrognosisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38d13921-eb09-42ae-bcd5-a05a42a025c4", "AQAAAAIAAYagAAAAEBA7S671qjuodjKDBW4SINza4exQM0ukLkCKU1i/n8ri84iGRL731EMlZlaY+HPWiA==", "2c494950-398b-4cea-9c8f-fbcfaa22d912" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba8c2693-638a-45f2-8cc1-1c27f129ccb3", "AQAAAAIAAYagAAAAEBsos/J1zx46LTXhK0Ydji8winEmXUUShOxEkE/6n9LsNPPD0WJRYUQYgYcwV967Xw==", "1641a9cf-a5c1-486c-bf55-cf86cb4b4b8f" });

            migrationBuilder.CreateIndex(
                name: "IX_prognoses_BranchId",
                table: "prognoses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Prognosis_Has_Days_PrognosisId",
                table: "Prognosis_Has_Days",
                column: "PrognosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prognosis_Has_Days");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "prognoses");

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
