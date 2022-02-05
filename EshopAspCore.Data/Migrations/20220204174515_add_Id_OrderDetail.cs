using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class add_Id_OrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2dab6c16-ca2b-4b24-9435-2b54d5047568"));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a981257-02b3-49cd-a85c-bf584a2d8205"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                column: "ConcurrencyStamp",
                value: "b115f7d3-4ebe-4c0e-b4bc-70ff3f3678aa");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("2dab6c16-ca2b-4b24-9435-2b54d5047568"), "b95035ad-9d7e-4673-8bd8-810522a9ae75", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a335db36-e2ec-4c82-93c7-d2f79fefc86d", "AQAAAAEAACcQAAAAEJzmsau9sFNh4G0cxCaLouzBQqj5zuf/ssdlAhyAuvgHONfriLzNMNWtfrseYd7TyQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 2, 4, 20, 34, 36, 599, DateTimeKind.Local).AddTicks(2896));
        }
    }
}
