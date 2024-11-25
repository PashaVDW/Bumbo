using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class availability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "f6d9d1fb-210b-45b4-8729-9c5ca64536b8", "AQAAAAIAAYagAAAAEHzwn2L6y2A0KZlUmt85B9SzJ9KtdQxCtsaI/HFGwQp7O8nlVXeH1mdi+DfqFS4LnQ==", "79658281-6852-46f8-a553-efbfaf03b447", new DateTime(2024, 11, 25, 8, 55, 6, 905, DateTimeKind.Local).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db6c5d5c-95d0-40b8-ace1-96b306007cbc", "AQAAAAIAAYagAAAAEB6g1WWj8zQH/ecesYRISRE3CAHZthYgVcaSPRODstpkrEzGEJsAN2qB5fa4clBCjQ==", "eb30346f-923d-423b-9876-a5fedc4b8227" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c2d2e2-2222-3333-4444-5555abcdefab",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "0e3f331d-c1ab-4cab-89ee-9a1a002d6dc6", "AQAAAAIAAYagAAAAEKx0Cx9jtxTG6gv6fcMKLFCI7w+qR+i25AG4nH28Wtt2A0RuDuDedsLCnKdCCgttjQ==", "236f2830-fdf2-4cbd-81d0-248ab202a201", new DateTime(2024, 11, 25, 8, 55, 6, 984, DateTimeKind.Local).AddTicks(4704) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39a2d4e0-87d1-4281-bc4b-a499f2f6e3b4", "AQAAAAIAAYagAAAAEJZn0FxyjxRXk5BjGcRvroEAZDonNE1HND3iDIumzXjwhZLMDNCp0sA1qRLrOPW58Q==", "52c99992-66e2-4035-9a30-603210d38a38" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea40da27-b58b-4840-9c72-2d4db85ad949", "AQAAAAIAAYagAAAAEHfyE3gYKA/PSx9tJlnfzXDWP9qRuoGCi2woGc0/ZnaLtn9h7vqTqZHFboubPFQFYw==", "69d28f0d-92cb-4612-8574-ed87f25d9283" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62c61499-c836-4666-b75f-0d3d3c805185", "AQAAAAIAAYagAAAAEDW1RdpOk7yyYDRj+BlJ04TJbYCXjWZEPb1QqIV9xRy1eDLlNNO4MZJInkWCvwwtKg==", "ccfc66c0-1ea2-4730-b13d-6727b2da4b46" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b565e5e5-6ed8-4f12-99e7-ee610e411bb9", "AQAAAAIAAYagAAAAEDYb79RhC/u5wrHjChMabWu3Xit8EcDmXfUZCcDz6lD3X2Hb7yLNgtUg0BY2aiH93w==", "6cd910d1-4930-45b5-a12a-cf95101dae0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10be76da-97c4-4f07-b258-f3ac2e5e9e58", "AQAAAAIAAYagAAAAEEuSrBcvTycNQvomJ+StdTFV4aWoawVd3ucVQ22dh94kdcGLZl5N/mY8qhRplZbscg==", "7ad77566-1455-4e21-b007-7909bf209d83" });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(22, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(15, 0, 0), new TimeOnly(7, 0, 0) },
                    { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 30), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 1), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 1), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 12, 2), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 12, 2), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 12, 3), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 12, 3), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(15, 0, 0), new TimeOnly(7, 0, 0) },
                    { new DateOnly(2024, 12, 4), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(22, 0, 0), new TimeOnly(14, 0, 0) },
                    { new DateOnly(2024, 12, 5), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 12, 5), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 12, 6), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(21, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 12, 6), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 7), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(19, 0, 0), new TimeOnly(11, 0, 0) },
                    { new DateOnly(2024, 12, 8), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 12, 9), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(15, 0, 0), new TimeOnly(7, 0, 0) },
                    { new DateOnly(2024, 12, 9), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { new DateOnly(2024, 12, 10), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(23, 0, 0), new TimeOnly(15, 0, 0) },
                    { new DateOnly(2024, 12, 11), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 12, 11), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 12, 12), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8", new TimeOnly(21, 0, 0), new TimeOnly(13, 0, 0) },
                    { new DateOnly(2024, 12, 12), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0", new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) }
                });

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd" },
                column: "StartDate",
                value: new DateTime(2024, 11, 25, 8, 55, 6, 905, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab" },
                column: "StartDate",
                value: new DateTime(2024, 11, 25, 8, 55, 6, 984, DateTimeKind.Local).AddTicks(4704));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 25), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 25), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 25), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 26), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 26), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 27), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 28), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 28), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 29), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 30), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 1), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 1), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 2), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 2), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 3), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 3), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 4), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 5), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 5), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 6), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 6), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 7), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 8), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 9), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 9), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 10), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 11), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 11), "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 12), "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 12), "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "d8807484-d1a7-422d-abcf-348ffa98d5ea", "AQAAAAIAAYagAAAAEE4+9LPUrYIupm04B0gZxFLJjpU6ThIHjA37NTYEwI8fQneI9EjFj/Eg/1wpXw+Ztw==", "87ec5556-e5bf-4eb1-b57f-7d4e8d14530e", new DateTime(2024, 11, 24, 21, 39, 17, 491, DateTimeKind.Local).AddTicks(5226) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cd800a7-7dd3-4465-9048-85e824e68e0a", "AQAAAAIAAYagAAAAELhoIq1BVy4NJiMt/UGsetmA2mACZhn10CkYCsEfSDZbRqvmMVG4sSZGFS0KIlZizg==", "f14dbedf-117e-46e8-a749-ae54bfc0e783" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c2d2e2-2222-3333-4444-5555abcdefab",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "32fddee7-4a64-4ae3-a1e4-8ebb0078aa40", "AQAAAAIAAYagAAAAEAoT5Wf4amGxzGO94LqCeJTuvItCeDvLPofS1URsyQ7LVqd3qFLIczMlr2qyFFs2nA==", "6fc8394e-a7cb-4c63-8a2c-88593d3c789b", new DateTime(2024, 11, 24, 21, 39, 17, 566, DateTimeKind.Local).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff31ed4e-d914-4209-ad8c-8b5159f83651", "AQAAAAIAAYagAAAAEK4hjXRG6TtDxUybmnw6rNsUoMQnc2CjP6VsUoj7/xzYijZsrdEAFlmAaHlOFLy/4Q==", "4edc2472-0c86-434b-b541-7b03318d4562" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6cfb44f3-0b73-4a31-9694-e59a6f7ca915", "AQAAAAIAAYagAAAAEIntqw378SiJkSHJrZGl1ku33F9xbMA2CnxITgY+vINOB67lzTwBicrv8wnoMeQVXg==", "8630507f-fcb4-4987-b88c-f509cc3577c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "297c55af-5390-4713-a102-895303bbc471", "AQAAAAIAAYagAAAAENXkcRwawZ4T2TwVxQoOokjoU4SNLYsTNFUwMGnPcKgqVxgmtXydW7BcOW/dG0i2Lw==", "25392dae-fcf4-49ca-9a1f-7fe1c718e79a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bca48494-6f85-49e7-a8f0-6785906543cf", "AQAAAAIAAYagAAAAEK4kTUav2Gv8vvN2ok2QRQoBCtxIw78ngef88/H0DOeMUB9tq9jEtO0zL4Vwf2Mj4g==", "57fff06f-b976-4e2c-a231-0bc63220761a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e572c0c-59c8-4323-afe2-01a9fd18179c", "AQAAAAIAAYagAAAAEGeZ3TLrmw6b7neS9SdHtmqXuGETreITYQa8SuXtAuqVlY42jDxR0E5JeuxnQMwZjg==", "41b2dcdb-03de-4da6-b68e-7ad5649f3459" });

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd" },
                column: "StartDate",
                value: new DateTime(2024, 11, 24, 21, 39, 17, 491, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab" },
                column: "StartDate",
                value: new DateTime(2024, 11, 24, 21, 39, 17, 566, DateTimeKind.Local).AddTicks(5730));
        }
    }
}
