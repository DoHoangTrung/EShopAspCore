using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class SeedingIdentityDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 30, 19, 34, 18, 323, DateTimeKind.Local).AddTicks(4184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 30, 16, 30, 50, 613, DateTimeKind.Local).AddTicks(704));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"), "f26ce4de-373c-4161-b8ed-ddc4aaac2b7f", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"), new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"), 0, "ec338ac7-1d83-4563-9fef-a82935ce0929", new DateTime(1998, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "some-admin-email@nonce.fake", true, "Trung", "Do", false, null, "some-admin-email@nonce.fake", "admin", "AQAAAAEAACcQAAAAEHaI9ng4smurLOxRDEpn0s3UxZEcTbfkI8ibRidTv3wjwCPUm6XFvDiUDLslsELLAQ==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 30, 19, 34, 18, 339, DateTimeKind.Local).AddTicks(4493));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"), new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 30, 16, 30, 50, 613, DateTimeKind.Local).AddTicks(704),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 30, 19, 34, 18, 323, DateTimeKind.Local).AddTicks(4184));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 30, 16, 30, 50, 627, DateTimeKind.Local).AddTicks(6018));
        }
    }
}
