using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class new_entities_v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadeEntiti~",
                table: "BrigadeEntityWorkingPeriodStageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Factory_FactoryEntityId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSubType_ProductSubTypeEntityId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ParentProductEntityId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubType_ProductType_ProductTypeEntityId",
                table: "ProductSubType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeEnti~",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriod_Product_ProductEntityId",
                table: "WorkingPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialEntit~",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_WorkingPeriodStage_WorkingPeriod~",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriod_ProductEntityId",
                table: "WorkingPeriod");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeEntityId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubType_ProductTypeEntityId",
                table: "ProductSubType");

            migrationBuilder.DropIndex(
                name: "IX_Product_FactoryEntityId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ParentProductEntityId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductSubTypeEntityId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropColumn(
                name: "WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "WorkingPeriod");

            migrationBuilder.DropColumn(
                name: "ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropColumn(
                name: "ProductSubTypeEntityId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropColumn(
                name: "ProductTypeEntityId",
                table: "ProductSubType");

            migrationBuilder.DropColumn(
                name: "FactoryEntityId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ParentProductEntityId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductSubTypeEntityId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "WorkingPeriodStageEntitiesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "WorkingPeriodStagesId");

            migrationBuilder.RenameColumn(
                name: "BrigadeEntitiesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "BrigadesId");

            migrationBuilder.RenameIndex(
                name: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStageEnt~",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStagesId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentProductId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialId",
                table: "WorkingPeriodStageMaterial",
                column: "GroupMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageId",
                table: "WorkingPeriodStageMaterial",
                column: "WorkingPeriodStageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriod_ProductId",
                table: "WorkingPeriod",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeId",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeId",
                table: "ProductSubTypeStageSample",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubType_ProductTypeId",
                table: "ProductSubType",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FactoryId",
                table: "Product",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ParentProductId",
                table: "Product",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubTypeId",
                table: "Product",
                column: "ProductSubTypeId");

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
                name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                column: "BrigadesId",
                principalTable: "Brigade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Factory_FactoryId",
                table: "Product",
                column: "FactoryId",
                principalTable: "Factory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubType_ProductSubTypeId",
                table: "Product",
                column: "ProductSubTypeId",
                principalTable: "ProductSubType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ParentProductId",
                table: "Product",
                column: "ParentProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubType_ProductType_ProductTypeId",
                table: "ProductSubType",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeId",
                table: "ProductSubTypeStageSample",
                column: "ProductSubTypeId",
                principalTable: "ProductSubType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "ProductSubTypeId",
                principalTable: "ProductSubType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriod_Product_ProductId",
                table: "WorkingPeriod",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialId",
                table: "WorkingPeriodStageMaterial",
                column: "GroupMaterialId",
                principalTable: "GroupMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterial_WorkingPeriodStage_WorkingPeriod~",
                table: "WorkingPeriodStageMaterial",
                column: "WorkingPeriodStageId",
                principalTable: "WorkingPeriodStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadesId",
                table: "BrigadeEntityWorkingPeriodStageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Factory_FactoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSubType_ProductSubTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ParentProductId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubType_ProductType_ProductTypeId",
                table: "ProductSubType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriod_Product_ProductId",
                table: "WorkingPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_WorkingPeriodStage_WorkingPeriod~",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropTable(
                name: "GroupMaterialEntityProductSubTypeWorkingPeriodSampleEntity");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityStageTypeEntity");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageId",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriod_ProductId",
                table: "WorkingPeriod");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeId",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubType_ProductTypeId",
                table: "ProductSubType");

            migrationBuilder.DropIndex(
                name: "IX_Product_FactoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ParentProductId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductSubTypeId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "WorkingPeriodStagesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "WorkingPeriodStageEntitiesId");

            migrationBuilder.RenameColumn(
                name: "BrigadesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "BrigadeEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStagesId",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                newName: "IX_BrigadeEntityWorkingPeriodStageEntity_WorkingPeriodStageEnt~");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterial",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterial",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "WorkingPeriod",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSample",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSubTypeEntityId",
                table: "ProductSubTypeStageSample",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeEntityId",
                table: "ProductSubType",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentProductId",
                table: "Product",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "FactoryEntityId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductEntityId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSubTypeEntityId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterial",
                column: "GroupMaterialEntitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterial",
                column: "WorkingPeriodStageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriod_ProductEntityId",
                table: "WorkingPeriod",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeEntityId",
                table: "ProductSubTypeStageSample",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubType_ProductTypeEntityId",
                table: "ProductSubType",
                column: "ProductTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FactoryEntityId",
                table: "Product",
                column: "FactoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ParentProductEntityId",
                table: "Product",
                column: "ParentProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubTypeEntityId",
                table: "Product",
                column: "ProductSubTypeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrigadeEntityWorkingPeriodStageEntity_Brigade_BrigadeEntiti~",
                table: "BrigadeEntityWorkingPeriodStageEntity",
                column: "BrigadeEntitiesId",
                principalTable: "Brigade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Factory_FactoryEntityId",
                table: "Product",
                column: "FactoryEntityId",
                principalTable: "Factory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubType_ProductSubTypeEntityId",
                table: "Product",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ParentProductEntityId",
                table: "Product",
                column: "ParentProductEntityId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubType_ProductType_ProductTypeEntityId",
                table: "ProductSubType",
                column: "ProductTypeEntityId",
                principalTable: "ProductType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeEnti~",
                table: "ProductSubTypeStageSample",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriod_Product_ProductEntityId",
                table: "WorkingPeriod",
                column: "ProductEntityId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialEntit~",
                table: "WorkingPeriodStageMaterial",
                column: "GroupMaterialEntitiesId",
                principalTable: "GroupMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterial_WorkingPeriodStage_WorkingPeriod~",
                table: "WorkingPeriodStageMaterial",
                column: "WorkingPeriodStageEntityId",
                principalTable: "WorkingPeriodStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
