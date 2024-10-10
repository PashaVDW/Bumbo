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
            migrationBuilder.AddColumn<string>(
                name: "PrognosisId",
                table: "Branches",
                type: "nvarchar(45)",
                nullable: true);

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
                values: new object[] { "7f9ae8f9-1535-4c87-b7b9-f6040167bf4e", "AQAAAAIAAYagAAAAEOy0f5XBBMizh9ABEGJHK9tx95rDDifxffhuFhPpAklOj1miQu4BiG7mljEyN7XVUg==", "a6867300-4109-4eb8-bd0c-fdcfec38bbbb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7ec6d38-3fde-4bb5-8695-b02db3100906", "AQAAAAIAAYagAAAAEFxcInkVFtWyr38O4fDtG/Y22+itD62hyhROt88BXjnegiJDhj3QA/E07500JpPcmg==", "5d555813-9337-4969-9116-1473b685871d" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 1,
                column: "PrognosisId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 2,
                column: "PrognosisId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_PrognosisId",
                table: "Branches",
                column: "PrognosisId");

            migrationBuilder.CreateIndex(
                name: "IX_prognoses_BranchId",
                table: "prognoses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Prognosis_Has_Days_PrognosisId",
                table: "Prognosis_Has_Days",
                column: "PrognosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_prognoses_PrognosisId",
                table: "Branches",
                column: "PrognosisId",
                principalTable: "prognoses",
                principalColumn: "PrognosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_prognoses_PrognosisId",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "Prognosis_Has_Days");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "prognoses");

            migrationBuilder.DropIndex(
                name: "IX_Branches_PrognosisId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PrognosisId",
                table: "Branches");

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
