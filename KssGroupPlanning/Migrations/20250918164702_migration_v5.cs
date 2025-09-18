using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class migration_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_Brigades_BrigadeId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSamples_Pro~",
                table: "WorkingPeriodStages");

            migrationBuilder.DropTable(
                name: "MaterialSamples");

            migrationBuilder.DropTable(
                name: "ProductSubTypeStagesSamples");

            migrationBuilder.DropTable(
                name: "WorkingPeriodsRelations");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageTypes");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodsSamples");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStages_BrigadeId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropColumn(
                name: "BrigadeId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropColumn(
                name: "ProductSubTypeWorkingPeriodsSampleId",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "WorkingPeriodStages",
                newName: "DateTo");

            migrationBuilder.RenameColumn(
                name: "ProductSubTypeWorkingPeriodsSampleId",
                table: "WorkingPeriodStages",
                newName: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "WorkingPeriodStages",
                newName: "DateFrom");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSampleId",
                table: "WorkingPeriodStages",
                newName: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Products",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Products",
                newName: "EndDate");

            migrationBuilder.CreateTable(
                name: "ProductSubTypeStageSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    StageName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeStageSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeStageSamples_ProductSubTypes_ProductSubTypeEn~",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeWorkingPeriodSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    WorkingPeriodName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false),
                    StandartEmployee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSamples_ProductSubTypes_ProductS~",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageBrigadeRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrigadeEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageBrigadeRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageBrigadeRelations_Brigades_BrigadeEntityId",
                        column: x => x.BrigadeEntityId,
                        principalTable: "Brigades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageBrigadeRelations_WorkingPeriodStages_Work~",
                        column: x => x.WorkingPeriodStageEntityId,
                        principalTable: "WorkingPeriodStages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWorkingPeriodSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChildProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildProductSubTypeWorkingPeriodSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_C~",
                        column: x => x.ChildProductSubTypeWorkingPeriodSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_P~",
                        column: x => x.ParentProductSubTypeWorkingPeriodSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageTypeRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageTypeRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~",
                        column: x => x.ProductSubTypeWorkingPeriodSampleEntityId,
                        principalTable: "ProductSubTypeWorkingPeriodSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypeRelations_StageTypes_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeStageSamples",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSamples",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelations_ChildProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelations",
                column: "ChildProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelations_ParentProductSubTypeWorkingPeriodSam~",
                table: "WorkingPeriodRelations",
                column: "ParentProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelations_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelations",
                column: "BrigadeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelations_WorkingPeriodStageEntity~",
                table: "WorkingPeriodStageBrigadeRelations",
                column: "WorkingPeriodStageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~",
                table: "WorkingPeriodStageTypeRelations",
                column: "ProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelations_StageTypeId",
                table: "WorkingPeriodStageTypeRelations",
                column: "StageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodSamples_Prod~",
                table: "WorkingPeriodStages",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodSamples_Prod~",
                table: "WorkingPeriodStages");

            migrationBuilder.DropTable(
                name: "ProductSubTypeStageSamples");

            migrationBuilder.DropTable(
                name: "WorkingPeriodRelations");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageBrigadeRelations");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageTypeRelations");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSamples");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.RenameColumn(
                name: "ProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodStages",
                newName: "ProductSubTypeWorkingPeriodsSampleId");

            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "WorkingPeriodStages",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateFrom",
                table: "WorkingPeriodStages",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodStages",
                newName: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSampleId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Products",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Products",
                newName: "End_date");

            migrationBuilder.AddColumn<Guid>(
                name: "BrigadeId",
                table: "WorkingPeriodStages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSubTypeWorkingPeriodsSampleId",
                table: "ProductSubTypeGroupMaterialRelations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaterialSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeStagesSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    StageName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeStagesSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeStagesSamples_ProductSubTypes_ProductSubTypeE~",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeWorkingPeriodsSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    StandartEmployee = table.Column<int>(type: "integer", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false),
                    WorkingPeriodName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodsSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodsSamples_ProductSubTypes_Product~",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodsRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChildProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    ProductSubTypeWokingPeriodsSampleEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWokingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_WorkingPeriodStages_BrigadeId",
                table: "WorkingPeriodStages",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodsSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStagesSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeStagesSamples",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodsSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                column: "ProductSubTypeEntityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodsSampleId",
                principalTable: "ProductSubTypeWorkingPeriodsSamples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_Brigades_BrigadeId",
                table: "WorkingPeriodStages",
                column: "BrigadeId",
                principalTable: "Brigades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSamples_Pro~",
                table: "WorkingPeriodStages",
                column: "ProductSubTypeWorkingPeriodsSampleId",
                principalTable: "ProductSubTypeWorkingPeriodsSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
