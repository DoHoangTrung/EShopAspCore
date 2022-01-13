using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class add_more_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "77df1f14-07a1-45a3-80ff-e11a1a47ed33");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("15479c4a-d0f2-418e-8662-cb79a3dd49f4"), "846a4df3-8664-45f4-b295-d17d7916a43a", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2d7c25c8-9dbf-44c9-8c44-b8eff60a7950", "AQAAAAEAACcQAAAAEAWnMthFrCxe02VHm7EngRRO4XFpP9xwg4nBAWCtL6Qz7TOwlTs0lZWZ2ej+3uW6Kw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 13, 16, 15, 28, 962, DateTimeKind.Local).AddTicks(5109));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("15479c4a-d0f2-418e-8662-cb79a3dd49f4"));

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1298688-bb9e-4e73-81d7-274b3df9ec22", "AQAAAAEAACcQAAAAEPrOGZRNXKek1NOwfZaEjbikKYZF376C5hqIsbJ9sW8ZrliceEykK7rZBncr+BObPw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 11, 20, 29, 35, 243, DateTimeKind.Local).AddTicks(2346));
        }
    }
}
