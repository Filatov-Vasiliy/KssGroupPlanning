using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class new_entities_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingPeriodsRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChildProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodsRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodsRelations_ProductSubTypeWorkingPeriodsSamples~",
                        column: x => x.ChildProductSubTypeWokingPeriodsSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodsSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingPeriodsRelations_ProductSubTypeWorkingPeriodsSample~1",
                        column: x => x.ParentProductSubTypeWokingPeriodsSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodsSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypes_ProductSubTypeWorkingPeriodsSamples~",
                        column: x => x.ProductSubTypeWokingPeriodsSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodsSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypes_StageTypes_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodsRelations_ChildProductSubTypeWokingPeriodsSam~",
                table: "WorkingPeriodsRelations",
                column: "ChildProductSubTypeWokingPeriodsSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodsRelations_ParentProductSubTypeWokingPeriodsSa~",
                table: "WorkingPeriodsRelations",
                column: "ParentProductSubTypeWokingPeriodsSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypes_ProductSubTypeWokingPeriodsSampleEn~",
                table: "WorkingPeriodStageTypes",
                column: "ProductSubTypeWokingPeriodsSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypes_StageTypeId",
                table: "WorkingPeriodStageTypes",
                column: "StageTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingPeriodsRelations");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageTypes");
        }
    }
}
