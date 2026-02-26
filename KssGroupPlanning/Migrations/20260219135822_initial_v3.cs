using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrderNameChildDate",
                table: "SrcMaterial");

            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "SrcMaterial",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProductOrderDateChild",
                table: "SrcMaterial",
                type: "date",
                nullable: true);

            migrationBuilder.InsertData(
                table: "GroupMaterial",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("02b7c3de-753f-45dd-8f23-c451eceaffae"), "Другое" },
                    { new Guid("28539a79-0747-4d57-9e1e-a2e63adff951"), "Черный металл" },
                    { new Guid("2a62b9a4-d1eb-4bce-b8fb-903736e8ea81"), "ШУ" },
                    { new Guid("46496326-7bc8-4935-8a9c-c319e1ed81e8"), "УПМ" },
                    { new Guid("78b76136-bd49-4a8b-b867-77d16fc98d7a"), "Насосы" },
                    { new Guid("8a01b1ce-4413-4174-b303-c20801699408"), "Арматура" },
                    { new Guid("95d41135-a72b-4386-9734-d579fa0947fe"), "Электрика" },
                    { new Guid("b037956e-2af1-4d26-a2c6-f8b8cd5ce09e"), "Метизы" },
                    { new Guid("d20b1bfc-3752-44b5-b671-448e9308ba54"), "Нержавейка" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("02b7c3de-753f-45dd-8f23-c451eceaffae"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("28539a79-0747-4d57-9e1e-a2e63adff951"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("2a62b9a4-d1eb-4bce-b8fb-903736e8ea81"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("46496326-7bc8-4935-8a9c-c319e1ed81e8"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("78b76136-bd49-4a8b-b867-77d16fc98d7a"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("8a01b1ce-4413-4174-b303-c20801699408"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("95d41135-a72b-4386-9734-d579fa0947fe"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("b037956e-2af1-4d26-a2c6-f8b8cd5ce09e"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("d20b1bfc-3752-44b5-b671-448e9308ba54"));

            migrationBuilder.DropColumn(
                name: "ProductOrderDateChild",
                table: "SrcMaterial");

            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "SrcMaterial",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductOrderNameChildDate",
                table: "SrcMaterial",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
