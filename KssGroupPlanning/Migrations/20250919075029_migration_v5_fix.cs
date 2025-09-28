using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class migration_v5_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brigades_Factories_FactoryId",
                table: "Brigades");

            migrationBuilder.DropForeignKey(
                name: "FK_Brigades_StageTypes_StageTypeId",
                table: "Brigades");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Factories_FactoryEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSubTypes_ProductSubTypeEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ParentProductEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_GroupMaterials_GroupMa~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypes_ProductTypes_ProductTypeEntityId",
                table: "ProductSubTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeStageSamples_ProductSubTypes_ProductSubTypeEn~",
                table: "ProductSubTypeStageSamples");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSamples_ProductSubTypes_ProductS~",
                table: "ProductSubTypeWorkingPeriodSamples");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Products_ProductId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_C~",
                table: "WorkingPeriodRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_P~",
                table: "WorkingPeriodRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriods_Products_ProductEntityId",
                table: "WorkingPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelations_Brigades_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelations_WorkingPeriodStages_Work~",
                table: "WorkingPeriodStageBrigadeRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterials_GroupMaterials_GroupMaterialEnt~",
                table: "WorkingPeriodStageMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterials_WorkingPeriodStages_WorkingPeri~",
                table: "WorkingPeriodStageMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodSamples_Prod~",
                table: "WorkingPeriodStages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStages_WorkingPeriods_WorkingPeriodId",
                table: "WorkingPeriodStages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~",
                table: "WorkingPeriodStageTypeRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelations_StageTypes_StageTypeId",
                table: "WorkingPeriodStageTypeRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageTypeRelations",
                table: "WorkingPeriodStageTypeRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStages",
                table: "WorkingPeriodStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageMaterials",
                table: "WorkingPeriodStageMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageBrigadeRelations",
                table: "WorkingPeriodStageBrigadeRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriods",
                table: "WorkingPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodRelations",
                table: "WorkingPeriodRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StageTypes",
                table: "StageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stages",
                table: "Stages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SrcOrders",
                table: "SrcOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeWorkingPeriodSamples",
                table: "ProductSubTypeWorkingPeriodSamples");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeStageSamples",
                table: "ProductSubTypeStageSamples");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypes",
                table: "ProductSubTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeGroupMaterialRelations",
                table: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMaterials",
                table: "GroupMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factories",
                table: "Factories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brigades",
                table: "Brigades");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageTypeRelations",
                newName: "WorkingPeriodStageTypeRelation");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStages",
                newName: "WorkingPeriodStage");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageMaterials",
                newName: "WorkingPeriodStageMaterial");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageBrigadeRelations",
                newName: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.RenameTable(
                name: "WorkingPeriods",
                newName: "WorkingPeriod");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodRelations",
                newName: "WorkingPeriodRelation");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "StageTypes",
                newName: "StageType");

            migrationBuilder.RenameTable(
                name: "Stages",
                newName: "Stage");

            migrationBuilder.RenameTable(
                name: "SrcOrders",
                newName: "SrcOrder");

            migrationBuilder.RenameTable(
                name: "ProductTypes",
                newName: "ProductType");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeWorkingPeriodSamples",
                newName: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeStageSamples",
                newName: "ProductSubTypeStageSample");

            migrationBuilder.RenameTable(
                name: "ProductSubTypes",
                newName: "ProductSubType");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeGroupMaterialRelations",
                newName: "ProductSubTypeGroupMaterialRelation");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "GroupMaterials",
                newName: "GroupMaterial");

            migrationBuilder.RenameTable(
                name: "Factories",
                newName: "Factory");

            migrationBuilder.RenameTable(
                name: "Brigades",
                newName: "Brigade");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageTypeRelations_StageTypeId",
                table: "WorkingPeriodStageTypeRelation",
                newName: "IX_WorkingPeriodStageTypeRelation_StageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~",
                table: "WorkingPeriodStageTypeRelation",
                newName: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStages_WorkingPeriodId",
                table: "WorkingPeriodStage",
                newName: "IX_WorkingPeriodStage_WorkingPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodStage",
                newName: "IX_WorkingPeriodStage_ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterials_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterial",
                newName: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterials_GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterial",
                newName: "IX_WorkingPeriodStageMaterial_GroupMaterialEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageBrigadeRelations_WorkingPeriodStageEntity~",
                table: "WorkingPeriodStageBrigadeRelation",
                newName: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageBrigadeRelations_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation",
                newName: "IX_WorkingPeriodStageBrigadeRelation_BrigadeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriods_ProductEntityId",
                table: "WorkingPeriod",
                newName: "IX_WorkingPeriod_ProductEntityId");

            migrationBuilder.RenameColumn(
                name: "ParentProductSubTypeWokingPeriodsSampleId",
                table: "WorkingPeriodRelation",
                newName: "ParentProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameColumn(
                name: "ChildProductSubTypeWokingPeriodsSampleId",
                table: "WorkingPeriodRelation",
                newName: "ChildProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodRelations_ParentProductSubTypeWorkingPeriodSam~",
                table: "WorkingPeriodRelation",
                newName: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodRelations_ChildProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation",
                newName: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~");

            migrationBuilder.RenameIndex(
                name: "IX_Stages_ProductId",
                table: "Stage",
                newName: "IX_Stage_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeWorkingPeriodSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSample",
                newName: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeStageSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeStageSample",
                newName: "IX_ProductSubTypeStageSample_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypes_ProductTypeEntityId",
                table: "ProductSubType",
                newName: "IX_ProductSubType_ProductTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelation",
                newName: "IX_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_GroupMaterialId",
                table: "ProductSubTypeGroupMaterialRelation",
                newName: "IX_ProductSubTypeGroupMaterialRelation_GroupMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductSubTypeEntityId",
                table: "Product",
                newName: "IX_Product_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ParentProductEntityId",
                table: "Product",
                newName: "IX_Product_ParentProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrderEntityId",
                table: "Product",
                newName: "IX_Product_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FactoryEntityId",
                table: "Product",
                newName: "IX_Product_FactoryEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Brigades_StageTypeId",
                table: "Brigade",
                newName: "IX_Brigade_StageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Brigades_FactoryId",
                table: "Brigade",
                newName: "IX_Brigade_FactoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageTypeRelation",
                table: "WorkingPeriodStageTypeRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStage",
                table: "WorkingPeriodStage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageMaterial",
                table: "WorkingPeriodStageMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageBrigadeRelation",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriod",
                table: "WorkingPeriod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodRelation",
                table: "WorkingPeriodRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StageType",
                table: "StageType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stage",
                table: "Stage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SrcOrder",
                table: "SrcOrder",
                column: "OrderName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeWorkingPeriodSample",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeStageSample",
                table: "ProductSubTypeStageSample",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubType",
                table: "ProductSubType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeGroupMaterialRelation",
                table: "ProductSubTypeGroupMaterialRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMaterial",
                table: "GroupMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factory",
                table: "Factory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brigade",
                table: "Brigade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brigade_Factory_FactoryId",
                table: "Brigade",
                column: "FactoryId",
                principalTable: "Factory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Brigade_StageType_StageTypeId",
                table: "Brigade",
                column: "StageTypeId",
                principalTable: "StageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Factory_FactoryEntityId",
                table: "Product",
                column: "FactoryEntityId",
                principalTable: "Factory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderEntityId",
                table: "Product",
                column: "OrderEntityId",
                principalTable: "Order",
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
                name: "FK_ProductSubTypeGroupMaterialRelation_GroupMaterial_GroupMate~",
                table: "ProductSubTypeGroupMaterialRelation",
                column: "GroupMaterialId",
                principalTable: "GroupMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~",
                table: "ProductSubTypeGroupMaterialRelation",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Stage_Product_ProductId",
                table: "Stage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriod_Product_ProductEntityId",
                table: "WorkingPeriod",
                column: "ProductEntityId",
                principalTable: "Product",
                principalColumn: "Id");

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
                name: "FK_WorkingPeriodStage_ProductSubTypeWorkingPeriodSample_Produc~",
                table: "WorkingPeriodStage",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStage_WorkingPeriod_WorkingPeriodId",
                table: "WorkingPeriodStage",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_StageType_StageTypeId",
                table: "WorkingPeriodStageTypeRelation",
                column: "StageTypeId",
                principalTable: "StageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brigade_Factory_FactoryId",
                table: "Brigade");

            migrationBuilder.DropForeignKey(
                name: "FK_Brigade_StageType_StageTypeId",
                table: "Brigade");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Factory_FactoryEntityId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderEntityId",
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
                name: "FK_ProductSubTypeGroupMaterialRelation_GroupMaterial_GroupMate~",
                table: "ProductSubTypeGroupMaterialRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~",
                table: "ProductSubTypeGroupMaterialRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeEnti~",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropForeignKey(
                name: "FK_Stage_Product_ProductId",
                table: "Stage");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriod_Product_ProductEntityId",
                table: "WorkingPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStage_ProductSubTypeWorkingPeriodSample_Produc~",
                table: "WorkingPeriodStage");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStage_WorkingPeriod_WorkingPeriodId",
                table: "WorkingPeriodStage");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialEntit~",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterial_WorkingPeriodStage_WorkingPeriod~",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageTypeRelation_StageType_StageTypeId",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageTypeRelation",
                table: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageMaterial",
                table: "WorkingPeriodStageMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStageBrigadeRelation",
                table: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodStage",
                table: "WorkingPeriodStage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodRelation",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriod",
                table: "WorkingPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StageType",
                table: "StageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stage",
                table: "Stage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SrcOrder",
                table: "SrcOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeWorkingPeriodSample",
                table: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeStageSample",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubTypeGroupMaterialRelation",
                table: "ProductSubTypeGroupMaterialRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubType",
                table: "ProductSubType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMaterial",
                table: "GroupMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factory",
                table: "Factory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brigade",
                table: "Brigade");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageTypeRelation",
                newName: "WorkingPeriodStageTypeRelations");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageMaterial",
                newName: "WorkingPeriodStageMaterials");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStageBrigadeRelation",
                newName: "WorkingPeriodStageBrigadeRelations");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodStage",
                newName: "WorkingPeriodStages");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodRelation",
                newName: "WorkingPeriodRelations");

            migrationBuilder.RenameTable(
                name: "WorkingPeriod",
                newName: "WorkingPeriods");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "StageType",
                newName: "StageTypes");

            migrationBuilder.RenameTable(
                name: "Stage",
                newName: "Stages");

            migrationBuilder.RenameTable(
                name: "SrcOrder",
                newName: "SrcOrders");

            migrationBuilder.RenameTable(
                name: "ProductType",
                newName: "ProductTypes");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeWorkingPeriodSample",
                newName: "ProductSubTypeWorkingPeriodSamples");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeStageSample",
                newName: "ProductSubTypeStageSamples");

            migrationBuilder.RenameTable(
                name: "ProductSubTypeGroupMaterialRelation",
                newName: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.RenameTable(
                name: "ProductSubType",
                newName: "ProductSubTypes");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "GroupMaterial",
                newName: "GroupMaterials");

            migrationBuilder.RenameTable(
                name: "Factory",
                newName: "Factories");

            migrationBuilder.RenameTable(
                name: "Brigade",
                newName: "Brigades");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageTypeRelation_StageTypeId",
                table: "WorkingPeriodStageTypeRelations",
                newName: "IX_WorkingPeriodStageTypeRelations_StageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelations",
                newName: "IX_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterial_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageMaterials",
                newName: "IX_WorkingPeriodStageMaterials_WorkingPeriodStageEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterials",
                newName: "IX_WorkingPeriodStageMaterials_GroupMaterialEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageEntityId",
                table: "WorkingPeriodStageBrigadeRelations",
                newName: "IX_WorkingPeriodStageBrigadeRelations_WorkingPeriodStageEntity~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelations",
                newName: "IX_WorkingPeriodStageBrigadeRelations_BrigadeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStage_WorkingPeriodId",
                table: "WorkingPeriodStages",
                newName: "IX_WorkingPeriodStages_WorkingPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStage_ProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodStages",
                newName: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.RenameColumn(
                name: "ParentProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodRelations",
                newName: "ParentProductSubTypeWokingPeriodsSampleId");

            migrationBuilder.RenameColumn(
                name: "ChildProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodRelations",
                newName: "ChildProductSubTypeWokingPeriodsSampleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelations",
                newName: "IX_WorkingPeriodRelations_ParentProductSubTypeWorkingPeriodSam~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelations",
                newName: "IX_WorkingPeriodRelations_ChildProductSubTypeWorkingPeriodSamp~");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriod_ProductEntityId",
                table: "WorkingPeriods",
                newName: "IX_WorkingPeriods_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Stage_ProductId",
                table: "Stages",
                newName: "IX_Stages_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodSamples",
                newName: "IX_ProductSubTypeWorkingPeriodSamples_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeEntityId",
                table: "ProductSubTypeStageSamples",
                newName: "IX_ProductSubTypeStageSamples_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~",
                table: "ProductSubTypeGroupMaterialRelations",
                newName: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubTypeGroupMaterialRelation_GroupMaterialId",
                table: "ProductSubTypeGroupMaterialRelations",
                newName: "IX_ProductSubTypeGroupMaterialRelations_GroupMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubType_ProductTypeEntityId",
                table: "ProductSubTypes",
                newName: "IX_ProductSubTypes_ProductTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductSubTypeEntityId",
                table: "Products",
                newName: "IX_Products_ProductSubTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ParentProductEntityId",
                table: "Products",
                newName: "IX_Products_ParentProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_OrderEntityId",
                table: "Products",
                newName: "IX_Products_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_FactoryEntityId",
                table: "Products",
                newName: "IX_Products_FactoryEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Brigade_StageTypeId",
                table: "Brigades",
                newName: "IX_Brigades_StageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Brigade_FactoryId",
                table: "Brigades",
                newName: "IX_Brigades_FactoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageTypeRelations",
                table: "WorkingPeriodStageTypeRelations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageMaterials",
                table: "WorkingPeriodStageMaterials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStageBrigadeRelations",
                table: "WorkingPeriodStageBrigadeRelations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodStages",
                table: "WorkingPeriodStages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodRelations",
                table: "WorkingPeriodRelations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriods",
                table: "WorkingPeriods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StageTypes",
                table: "StageTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stages",
                table: "Stages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SrcOrders",
                table: "SrcOrders",
                column: "OrderName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeWorkingPeriodSamples",
                table: "ProductSubTypeWorkingPeriodSamples",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeStageSamples",
                table: "ProductSubTypeStageSamples",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypeGroupMaterialRelations",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubTypes",
                table: "ProductSubTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMaterials",
                table: "GroupMaterials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factories",
                table: "Factories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brigades",
                table: "Brigades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brigades_Factories_FactoryId",
                table: "Brigades",
                column: "FactoryId",
                principalTable: "Factories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Brigades_StageTypes_StageTypeId",
                table: "Brigades",
                column: "StageTypeId",
                principalTable: "StageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Factories_FactoryEntityId",
                table: "Products",
                column: "FactoryEntityId",
                principalTable: "Factories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderEntityId",
                table: "Products",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSubTypes_ProductSubTypeEntityId",
                table: "Products",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ParentProductEntityId",
                table: "Products",
                column: "ParentProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_GroupMaterials_GroupMa~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "GroupMaterialId",
                principalTable: "GroupMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypes_ProductTypes_ProductTypeEntityId",
                table: "ProductSubTypes",
                column: "ProductTypeEntityId",
                principalTable: "ProductTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeStageSamples_ProductSubTypes_ProductSubTypeEn~",
                table: "ProductSubTypeStageSamples",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeWorkingPeriodSamples_ProductSubTypes_ProductS~",
                table: "ProductSubTypeWorkingPeriodSamples",
                column: "ProductSubTypeEntityId",
                principalTable: "ProductSubTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Products_ProductId",
                table: "Stages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_C~",
                table: "WorkingPeriodRelations",
                column: "ChildProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelations_ProductSubTypeWorkingPeriodSamples_P~",
                table: "WorkingPeriodRelations",
                column: "ParentProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriods_Products_ProductEntityId",
                table: "WorkingPeriods",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelations_Brigades_BrigadeEntityId",
                table: "WorkingPeriodStageBrigadeRelations",
                column: "BrigadeEntityId",
                principalTable: "Brigades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageBrigadeRelations_WorkingPeriodStages_Work~",
                table: "WorkingPeriodStageBrigadeRelations",
                column: "WorkingPeriodStageEntityId",
                principalTable: "WorkingPeriodStages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterials_GroupMaterials_GroupMaterialEnt~",
                table: "WorkingPeriodStageMaterials",
                column: "GroupMaterialEntitiesId",
                principalTable: "GroupMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterials_WorkingPeriodStages_WorkingPeri~",
                table: "WorkingPeriodStageMaterials",
                column: "WorkingPeriodStageEntityId",
                principalTable: "WorkingPeriodStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodSamples_Prod~",
                table: "WorkingPeriodStages",
                column: "ProductSubTypeWorkingPeriodSampleId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStages_WorkingPeriods_WorkingPeriodId",
                table: "WorkingPeriodStages",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelations_ProductSubTypeWorkingPeriod~",
                table: "WorkingPeriodStageTypeRelations",
                column: "ProductSubTypeWorkingPeriodSampleEntityId",
                principalTable: "ProductSubTypeWorkingPeriodSamples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageTypeRelations_StageTypes_StageTypeId",
                table: "WorkingPeriodStageTypeRelations",
                column: "StageTypeId",
                principalTable: "StageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
