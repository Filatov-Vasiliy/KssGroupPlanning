using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Factory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("019c765b-e572-7a27-84ca-277461509d9f"), "Другое" },
                    { new Guid("0ea947d9-3af7-40dd-83ae-35cc07c5a068"), "Бершанская" },
                    { new Guid("1a3fa20a-992a-4487-a114-fd65444704d2"), "Кореновск" },
                    { new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Васюринская" },
                    { new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Тихорецкая" },
                    { new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Новороссийская" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("019c765b-e572-7a27-84ca-277461509d9f"), "Неопределено" });

            migrationBuilder.InsertData(
                table: "ProductSubType",
                columns: new[] { "Id", "Name", "ProductTypeId" },
                values: new object[] { new Guid("0ea947d9-3af7-40dd-83ae-35cc07c5a068"), "Неопределено", new Guid("019c765b-e572-7a27-84ca-277461509d9f") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("019c765b-e572-7a27-84ca-277461509d9f"));

            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("0ea947d9-3af7-40dd-83ae-35cc07c5a068"));

            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("1a3fa20a-992a-4487-a114-fd65444704d2"));

            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"));

            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"));

            migrationBuilder.DeleteData(
                table: "Factory",
                keyColumn: "Id",
                keyValue: new Guid("ed3f7875-2760-4853-8c6b-554657503449"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("0ea947d9-3af7-40dd-83ae-35cc07c5a068"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("019c765b-e572-7a27-84ca-277461509d9f"));
        }
    }
}
