using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class david : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b1c1d1-1111-2222-3333-4444abcdabcd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "a557f388-6470-41ea-9b16-3bfbb8b52924", "AQAAAAIAAYagAAAAEAHXihBRUDWcVW67X7XVeb09zNYCfTZs5YgHO78dUM/DIg6aJCnyrod8JgKkoykI0A==", "4e85cf32-dadf-4a42-bde8-1f475f875da4", new DateTime(2024, 11, 25, 9, 55, 8, 738, DateTimeKind.Local).AddTicks(4969) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2b2d3e4-56f7-8a90-b1c2-d3e4f5g6h7i8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b3dd5d6-87ed-4e78-bffc-66bb1f29c23b", "AQAAAAIAAYagAAAAEITMQsdhAiSdqwo8ilbxd+MklMTMj0cQnuQi6hSQrHbPUbXqVqRYKYJSjmiKEv3YjA==", "65a7f132-05bb-4108-bd25-f3cbd06b4169" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c2d2e2-2222-3333-4444-5555abcdefab",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "StartDate" },
                values: new object[] { "add32937-03ad-4438-93a1-fa323a00e23f", "AQAAAAIAAYagAAAAEDiZMi1K6kqEBizXF8TWAdO3Cs7vtonONzAvoBYCtLqusCQLw1SFqWDSEvdwXhCdtw==", "3a7c2289-f7a5-4385-a5dd-3af4a60ccc1c", new DateTime(2024, 11, 25, 9, 55, 8, 832, DateTimeKind.Local).AddTicks(9557) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3c3d4e5-67f8-9a01-c2d3-e4f5g6h7i8j9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d84e576c-ba8a-4070-9a8e-a8574dd2ca2b", "AQAAAAIAAYagAAAAEJKJUCKSrNH7EA458bnO6PS4VGTPWFpevjmtYHm7M3TkHv5UtChffdskXo7lhYVORg==", "cc2952a5-3a5b-4dad-b793-931cb2401035" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4d4e5f6-78g9-0a12-d3e4-f5g6h7i8j9k0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f5cba31-07c8-47dd-b5ca-fb7082a91b14", "AQAAAAIAAYagAAAAEAAukKziYBSjaS7/0BKs3oZcqhN9LPGJiVlL4LJyaYUZflimePZn1tONGSgWMVMY8w==", "87ae13da-e824-4a6d-9065-57745203c47f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6afc366e-75c1-4d85-a6e8-7d0a2f0eff56", "AQAAAAIAAYagAAAAEJEOf/YfqSQB5z5Zzjj/GuECrsAXpwF5NRSC+A5LtRzO1qcVLp3MVixuY7eePBcriA==", "4337b7c4-822b-45c3-8829-147dff0aca9c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e6f6g7h8-90i1-2b34-f5g6-h7i8j9k0l1m2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de28ebc1-dbb6-4028-9e3e-1e56d9f8355b", "AQAAAAIAAYagAAAAENwYYMVl6zic+F03/0eJo9yHLyx4Qrp7FfMt8Q/pMOBYZQjkAI2SCxqQKB7p3ZJI4A==", "832f1c6d-bd03-4201-ac9a-0e8713902db4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f7g7h8i9-01j2-3c45-g6h7-i8j9k0l1m2n3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65252415-7e9c-4223-85bd-8a5ea009eafb", "AQAAAAIAAYagAAAAEEz0bbdBAjMlSDpq1OZ+w/0fPY0eRb9DLXzRoilK9Wj/mYVlajN3PQzFKIxTizBZMg==", "13b36ae9-4c4c-4c35-b40d-46dce4d5e8f8" });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Date", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(13, 0, 0), new TimeOnly(9, 0, 0) },
                    { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(16, 30, 0), new TimeOnly(8, 30, 0) },
                    { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(20, 0, 0), new TimeOnly(12, 0, 0) },
                    { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(14, 30, 0), new TimeOnly(6, 30, 0) },
                    { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(18, 0, 0), new TimeOnly(10, 0, 0) },
                    { new DateOnly(2024, 11, 30), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(15, 0, 0), new TimeOnly(7, 0, 0) },
                    { new DateOnly(2024, 12, 1), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(22, 30, 0), new TimeOnly(14, 30, 0) },
                    { new DateOnly(2024, 12, 2), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1", new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) }
                });

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 1, "a1b1c1d1-1111-2222-3333-4444abcdabcd" },
                column: "StartDate",
                value: new DateTime(2024, 11, 25, 9, 55, 8, 738, DateTimeKind.Local).AddTicks(4969));

            migrationBuilder.UpdateData(
                table: "BranchHasEmployees",
                keyColumns: new[] { "BranchId", "EmployeeId" },
                keyValues: new object[] { 2, "b2c2d2e2-2222-3333-4444-5555abcdefab" },
                column: "StartDate",
                value: new DateTime(2024, 11, 25, 9, 55, 8, 832, DateTimeKind.Local).AddTicks(9557));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 25), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 26), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 27), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 28), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 29), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 11, 30), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 1), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumns: new[] { "Date", "EmployeeId" },
                keyValues: new object[] { new DateOnly(2024, 12, 2), "d5e5f6g7-89h0-1a23-e4f5-g6h7i8j9k0l1" });

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
    }
}
