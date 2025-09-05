using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class new_entities_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrigadeId",
                table: "WorkingPeriodStages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Brigades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountEmployee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brigades_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brigades_StageTypes_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialSampleEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateDelivery = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageMaterials_MaterialSamples_MaterialSampleE~",
                        column: x => x.MaterialSampleEntityId,
                        principalTable: "MaterialSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageMaterials_WorkingPeriodStages_WorkingPeri~",
                        column: x => x.WorkingPeriodStageEntityId,
                        principalTable: "WorkingPeriodStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStages_BrigadeId",
                table: "WorkingPeriodStages",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigades_FactoryId",
                table: "Brigades",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigades_StageTypeId",
                table: "Brigades",
                column: "StageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterials_MaterialSampleEntityId",
                table: "WorkingPeriodStageMaterials",
                column: "MaterialSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterials_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterials",
                column: "WorkingPeriodStageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_Brigades_BrigadeId",
                table: "WorkingPeriodStages",
                column: "BrigadeId",
                principalTable: "Brigades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_Brigades_BrigadeId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropTable(
                name: "Brigades");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageMaterials");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStages_BrigadeId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropColumn(
                name: "BrigadeId",
                table: "WorkingPeriodStages");
        }
    }
}
