using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopAspCore.Data.Migrations
{
    public partial class Order_FK_UserID_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("273d0f75-8d5b-456e-a062-b57778675c0e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                columns: new[] { "DateCreated", "IsFeatured" },
                values: new object[] { new DateTime(2022, 2, 4, 20, 34, 36, 599, DateTimeKind.Local).AddTicks(2896), true });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2dab6c16-ca2b-4b24-9435-2b54d5047568"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a905b66-98fb-4e82-9d98-5cf68ebb16ea"),
                column: "ConcurrencyStamp",
                value: "03f7ffcd-ad2a-40ae-b33c-5a7f4aafb6f8");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("273d0f75-8d5b-456e-a062-b57778675c0e"), "d60944b0-142c-460a-9bd7-0d40bd761518", "user role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("db9ed923-492b-467a-97e4-ee81c9de0a64"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0edefbe-cece-4410-b26a-19521604bc89", "AQAAAAEAACcQAAAAEPK7mUyrB09+0zxjzFIgJOft7D9ZJQKwsYNFPJK64yd6qQvLR1cQN6siXUysnG894Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "IsFeatured" },
                values: new object[] { new DateTime(2022, 1, 24, 22, 40, 16, 253, DateTimeKind.Local).AddTicks(6528), null });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
