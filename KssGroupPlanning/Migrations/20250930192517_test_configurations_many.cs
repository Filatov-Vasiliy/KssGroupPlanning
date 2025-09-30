using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class test_configurations_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropTable(
                name: "BrigadeEntityWorkingPeriodStageEntity");

            migrationBuilder.DropTable(
                name: "GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropColumn(
                name: "ProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropColumn(
                name: "BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropColumn(
                name: "WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropColumn(
                name: "ChildProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropColumn(
                name: "ParentProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodRelation");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductSubTypeWorkingPeriodSamplesId",
                table: "WorkingPeriodRelation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "WorkingPeriodStageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelation",
                column: "ChildProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSam~1",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSamplesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                table: "WorkingPeriodRelation",
                column: "ChildProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Pa~1",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSamplesId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "BrigadeId",
                principalTable: "Brigade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "WorkingPeriodStageId",
                principalTable: "WorkingPeriodStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Pa~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSam~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropColumn(
                name: "ParentProductSubTypeWorkingPeriodSamplesId",
                table: "WorkingPeriodRelation");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodStageTypeRelation",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChildProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodRelation",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductSubTypeWorkingPeriodSampleEntityId",
                table: "WorkingPeriodRelation",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrigadeEntityWorkingPeriodStageEntity",
                columns: table => new
                {
                    BrigadesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStagesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadeEntityWorkingPeriodStageEntity", x => new { x.BrigadesId, x.WorkingPeriodStagesId });
                    table.ForeignKey(
                        name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadesId",
                        column: x => x.BrigadesId,
                        principalTable: "Brigade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStage_Wo~",
                        column: x => x.WorkingPeriodStagesId,
                        principalTable: "WorkingPeriodStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity",
                columns: table => new
                {
                    GroupMaterialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSamplesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity", x => new { x.GroupMaterialsId, x.ProductSubTypeWorkingPeriodSamplesId });
                    table.ForeignKey(
                        name: "FK_GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity_~",
                        column: x => x.GroupMaterialsId,
                        principalTable: "GroupMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity~1",
                        column: x => x.ProductSubTypeWorkingPeriodSamplesId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~",
                columns: table => new
                {
                    ChildProductSubTypeWorkingPeriodSamplesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWorkingPeriodSamplesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkin~", x => new { x.ChildProductSubTypeWorkingPeriodSamplesId, x.ParentProductSubTypeWorkingPeriodSamplesId });
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkin~",
                        column: x => x.ChildProductSubTypeWorkingPeriodSamplesId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorki~1",
                        column: x => x.ParentProductSubTypeWorkingPeriodSamplesId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity",
                columns: table => new
                {
                    ProductSubTypeWorkingPeriodSamplesId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity", x => new { x.ProductSubTypeWorkingPeriodSamplesId, x.StageTypesId });
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity_Prod~",
                        column: x => x.ProductSubTypeWorkingPeriodSamplesId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity_Stag~",
                        column: x => x.StageTypesId,
                        principalTable: "StageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "BrigadeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "WorkingPeriodStageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelation",
                column: "ChildProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSampleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStagesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                column: "WorkingPeriodStagesId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity_~",
                table: "GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity",
                column: "ProductSubTypeWorkingPeriodSamplesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkin~",
                table: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~",
                column: "ParentProductSubTypeWorkingPeriodSamplesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity_Stag~",
                table: "ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity",
                column: "StageTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                table: "WorkingPeriodRelation",
                column: "ChildProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "BrigadeEntityId",
                principalTable: "Brigade",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "WorkingPeriodStageEntityId",
                principalTable: "WorkingPeriodStage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id");
        }
    }
}
