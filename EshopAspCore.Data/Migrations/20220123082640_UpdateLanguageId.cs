using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class UpdateLanguageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("15479c4a-d0f2-418e-8662-cb79a3dd49f4"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "vi-VN");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "a4adb7a8-edca-46b8-b46f-f6b16f3945b4");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("4c96553e-b170-4735-9344-2ccb7cd508e2"), "dc46002a-9bdb-4ad1-808b-263c89ed2b50", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f6a90528-59e3-4d8d-802c-2fd2cb9ff342", "AQAAAAEAACcQAAAAEFqZctgCSYWQhS/iXhH2tNfLf1/2lP5z3TZ5FQnUzjzRymXmX51wjWAYlw7Sjy0d2A==" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "vi", true, "Tiếng Việt" },
                    { "en", false, "English" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 23, 15, 26, 39, 712, DateTimeKind.Local).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: "vi");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: "en");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 3,
                column: "LanguageId",
                value: "vi");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 4,
                column: "LanguageId",
                value: "en");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: "vi");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: "en");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4c96553e-b170-4735-9344-2ccb7cd508e2"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "vi");

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

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "vi-VN", true, "Tiếng Việt" },
                    { "en-US", false, "English" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 13, 16, 15, 28, 962, DateTimeKind.Local).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: "vi-VN");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: "en-US");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 3,
                column: "LanguageId",
                value: "vi-VN");

            migrationBuilder.UpdateData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 4,
                column: "LanguageId",
                value: "en-US");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: "vi-VN");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: "en-US");
        }
    }
}
