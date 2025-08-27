using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class new_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodsSamples_StageTypes_StageTypeEnt~",
                table: "ProductSubTypeWorkingPeriodsSamples");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeWorkingPeriodsSamples_StageTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples");

            migrationBuilder.DropColumn(
                name: "StageTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples");

            migrationBuilder.DropColumn(
                name: "StageTypeId",
                table: "ProductSubTypeWorkingPeriodsSamples");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StageTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StageTypeId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodsSamples_StageTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                column: "StageTypeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodsSamples_StageTypes_StageTypeEnt~",
                table: "ProductSubTypeWorkingPeriodsSamples",
                column: "StageTypeEntityId",
                principalTable: "StageTypes",
                principalColumn: "Id");
        }
    }
}
