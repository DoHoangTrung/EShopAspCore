using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class newAppUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "e87f7b7b-9daf-4907-84a6-59340be7aa16");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "f1298688-bb9e-4e73-81d7-274b3df9ec22", "AQAAAAEAACcQAAAAEPrOGZRNXKek1NOwfZaEjbikKYZF376C5hqIsbJ9sW8ZrliceEykK7rZBncr+BObPw==", "trung123" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 11, 20, 29, 35, 243, DateTimeKind.Local).AddTicks(2346));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "121b7849-c026-4e7c-b487-e45c1cf7af4c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "fba200ea-351c-4ce6-a85d-bf5712ffe1a6", "AQAAAAEAACcQAAAAEOr1P5BLrbsAzJFVfypWnjjtNhfCM2uCANlKqr2ZjckMzXEbDZUia2YYIh98h25rwA==", "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 3, 19, 44, 7, 529, DateTimeKind.Local).AddTicks(6747));
        }
    }
}
