using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class fix_orderDetails_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Products_OrderId",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a981257-02b3-49cd-a85c-bf584a2d8205"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                column: "ConcurrencyStamp",
                value: "87ecd522-86a3-422a-a857-9101f18763fb");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("e30efbc8-f12b-44cb-a434-c7f1a9859f74"), "ed4e6a0e-3eb7-4e8a-b53e-f6e59e7081d1", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ccd9b03d-b1a5-48f2-9590-d9289fb9ded0", "AQAAAAEAACcQAAAAELBVmX8N2XROsY2f4qt+TjROR+VXTbzjfK0EnDv+4E8kg8jyE22ce5qaT81d2l/0Xg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 5, 0, 50, 50, 189, DateTimeKind.Local).AddTicks(3661));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e30efbc8-f12b-44cb-a434-c7f1a9859f74"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                column: "ConcurrencyStamp",
                value: "3503ab8a-5c71-45d8-9afb-08d4ae8f692a");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("1a981257-02b3-49cd-a85c-bf584a2d8205"), "991c4173-cfd1-4ec7-97bd-f58532948d67", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c29fad55-6c33-4945-a51b-e19a049990df", "AQAAAAEAACcQAAAAELo8PlZSe3K2h9+WAh8bhGrqgO++pLx0BV7Pz5zgeEKIPaFX1XPGkk+bMTwG/qmMSw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 5, 0, 45, 14, 201, DateTimeKind.Local).AddTicks(9355));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Products_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
