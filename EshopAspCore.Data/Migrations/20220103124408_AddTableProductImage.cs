using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class AddTableProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fba200ea-351c-4ce6-a85d-bf5712ffe1a6", "AQAAAAEAACcQAAAAEOr1P5BLrbsAzJFVfypWnjjtNhfCM2uCANlKqr2ZjckMzXEbDZUia2YYIh98h25rwA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 3, 19, 44, 7, 529, DateTimeKind.Local).AddTicks(6747));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "c6355368-d23f-4b39-a847-a6533da96cce");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a692b42-8b07-479a-8336-4b07ff017fc0", "AQAAAAEAACcQAAAAEJRGdK7eSr/JM7XR89qpkTKo8Nboe5BpFwiEjxy0PgiLc42oYY7HXh6widVXh3XgWw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 2, 17, 18, 13, 862, DateTimeKind.Local).AddTicks(3329));
        }
    }
}
