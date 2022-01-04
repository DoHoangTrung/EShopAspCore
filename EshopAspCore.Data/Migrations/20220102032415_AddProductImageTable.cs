using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 30, 19, 34, 18, 323, DateTimeKind.Local).AddTicks(4184));

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "2c29f3b1-aafc-412c-8b02-e866f98db45c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d0d7d7d-ff3a-4c12-a07b-1236ef55b2ea", "AQAAAAEAACcQAAAAEEwfhoUHuqdcY0b4MdASTAQZDtl1ibXngL8hlfGJ8nzMlKHGEnQAwFKc8dyBzge2mA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 2, 10, 24, 14, 737, DateTimeKind.Local).AddTicks(8847));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 30, 19, 34, 18, 323, DateTimeKind.Local).AddTicks(4184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                column: "ConcurrencyStamp",
                value: "f26ce4de-373c-4161-b8ed-ddc4aaac2b7f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ec338ac7-1d83-4563-9fef-a82935ce0929", "AQAAAAEAACcQAAAAEHaI9ng4smurLOxRDEpn0s3UxZEcTbfkI8ibRidTv3wjwCPUm6XFvDiUDLslsELLAQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 30, 19, 34, 18, 339, DateTimeKind.Local).AddTicks(4493));
        }
    }
}
