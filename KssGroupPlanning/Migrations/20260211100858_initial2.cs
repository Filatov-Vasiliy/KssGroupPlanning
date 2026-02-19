using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductOrderNameChildDate",
                table: "SrcMaterial",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SubProductStageId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubProductStageId",
                table: "Product",
                column: "SubProductStageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Stage_SubProductStageId",
                table: "Product",
                column: "SubProductStageId",
                principalTable: "Stage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Stage_SubProductStageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_SubProductStageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductOrderNameChildDate",
                table: "SrcMaterial");

            migrationBuilder.DropColumn(
                name: "SubProductStageId",
                table: "Product");
        }
    }
}
