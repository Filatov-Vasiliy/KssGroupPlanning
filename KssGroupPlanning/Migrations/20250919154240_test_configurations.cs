using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class test_configurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderEntityId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderEntityId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "BrigadeEntityWorkingPeriodStageEntity",
                columns: table => new
                {
                    BrigadeEntitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageEntitiesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadeEntityWorkingPeriodStageEntity", x => new { x.BrigadeEntitiesId, x.WorkingPeriodStageEntitiesId });
                    table.ForeignKey(
                        name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadeEntiti~",
                        column: x => x.BrigadeEntitiesId,
                        principalTable: "Brigade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStage_Wo~",
                        column: x => x.WorkingPeriodStageEntitiesId,
                        principalTable: "WorkingPeriodStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStageEnt~",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                column: "WorkingPeriodStageEntitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "BrigadeEntityWorkingPeriodStageEntity");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderId",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderEntityId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderEntityId",
                table: "Product",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderEntityId",
                table: "Product",
                column: "OrderEntityId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
