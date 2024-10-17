using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class Templates : Migration
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
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_branchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Branches_Branch_branchId",
                        column: x => x.Branch_branchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Template_Has_Days",
                columns: table => new
                {
                    Templates_id = table.Column<int>(type: "int", nullable: false),
                    Days_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustomerAmount = table.Column<int>(type: "int", nullable: false),
                    ContainerAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template_Has_Days", x => new { x.Templates_id, x.Days_name });
                    table.ForeignKey(
                        name: "FK_Template_Has_Days_Days_Days_name",
                        column: x => x.Days_name,
                        principalTable: "Days",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_Has_Days_Templates_Templates_id",
                        column: x => x.Templates_id,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbcff31b-ae52-4f0a-8f74-7bef08f86871", "AQAAAAIAAYagAAAAEIYeRpcEk0C1LSFoQgdiL2Ev/W/0kzuEPom6/Zpx2uTPBbBNri978CfSo7RopHlang==", "b695df79-e2e0-4eb1-9c7b-5304039ef7d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88e9e2a6-f4f2-4482-a31b-46f50db87746", "AQAAAAIAAYagAAAAEMsZ5smgzTq5UrtVHdpJQsKILEzjOqdHeABJpZ1Ms0iQ9LV+DdYlQC3bk4oOdsohVg==", "44d276fe-1da9-46e8-9c58-5d64f6b20f57" });

            migrationBuilder.InsertData(
                table: "Days",
                column: "Name",
                values: new object[]
                {
                    "Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday",
                    "Sunday"
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Branch_branchId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Basic Package" },
                    { 2, 1, "Standard Package" },
                    { 3, 2, "Premium Package" },
                    { 4, 2, "Family Package" },
                    { 5, 1, "Weekly Special" }
                });

            migrationBuilder.InsertData(
                table: "Template_Has_Days",
                columns: new[] { "Templates_id", "Days_name", "ContainerAmount", "CustomerAmount" },
                values: new object[,]
                {
                    { 1, "Monday", 41, 989 },
                    { 1, "Tuesday", 52, 825 },
                    { 1, "Wednesday", 38, 902 },
                    { 1, "Thursday", 52, 990 },
                    { 1, "Friday", 39, 1040 },
                    { 1, "Saturday", 43, 953 },
                    { 1, "Sunday", 32, 872 },

                    { 2, "Monday", 42, 916 },
                    { 2, "Tuesday", 38, 912 },
                    { 2, "Wednesday", 32, 902 },
                    { 2, "Thursday", 45, 940 },
                    { 2, "Friday", 47, 816 },
                    { 2, "Saturday", 38, 842 },
                    { 2, "Sunday", 45, 885 },

                    { 3, "Monday", 53, 872 },
                    { 3, "Tuesday", 41, 989 },
                    { 3, "Wednesday", 42, 916 },
                    { 3, "Thursday", 36, 875 },
                    { 3, "Friday", 29, 877 },
                    { 3, "Saturday", 53, 945 },
                    { 3, "Sunday", 52, 880 },

                    { 4, "Monday", 49, 900 },
                    { 4, "Tuesday", 38, 903 },
                    { 4, "Wednesday", 45, 930 },
                    { 4, "Thursday", 42, 985 },
                    { 4, "Friday", 36, 865 },
                    { 4, "Saturday", 43, 950 },
                    { 4, "Sunday", 38, 950 },

                    { 5, "Monday", 52, 832 },
                    { 5, "Tuesday", 49, 935 },
                    { 5, "Wednesday", 29, 877 },
                    { 5, "Thursday", 41, 989 },
                    { 5, "Friday", 32, 872 },
                    { 5, "Saturday", 36, 771 },
                    { 5, "Sunday", 52, 885 }
                });


            migrationBuilder.CreateIndex(
                name: "IX_Template_Has_Days_Days_name",
                table: "Template_Has_Days",
                column: "Days_name");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Branch_branchId",
                table: "Templates",
                column: "Branch_branchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template_Has_Days");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Templates");

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
