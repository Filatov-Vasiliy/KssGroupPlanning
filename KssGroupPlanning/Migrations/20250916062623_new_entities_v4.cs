using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class new_entities_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterials_MaterialSamples_MaterialSampleE~",
                table: "WorkingPeriodStageMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialSampleId",
                table: "WorkingPeriodStageMaterials",
                newName: "GroupMaterialId");

            migrationBuilder.RenameColumn(
                name: "MaterialSampleEntityId",
                table: "WorkingPeriodStageMaterials",
                newName: "GroupMaterialEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterials_MaterialSampleEntityId",
                table: "WorkingPeriodStageMaterials",
                newName: "IX_WorkingPeriodStageMaterials_GroupMaterialEntitiesId");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductEntityId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeGroupMaterialRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeGroupMaterialRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeGroupMaterialRelations_GroupMaterials_GroupMa~",
                        column: x => x.GroupMaterialId,
                        principalTable: "GroupMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                        column: x => x.ProductSubTypeWorkingPeriodsSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodsSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ParentProductEntityId",
                table: "Products",
                column: "ParentProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_GroupMaterialId",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "GroupMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelations_ProductSubTypeWorkingP~",
                table: "ProductSubTypeGroupMaterialRelations",
                column: "ProductSubTypeWorkingPeriodsSampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ParentProductEntityId",
                table: "Products",
                column: "ParentProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterials_GroupMaterials_GroupMaterialEnt~",
                table: "WorkingPeriodStageMaterials",
                column: "GroupMaterialEntitiesId",
                principalTable: "GroupMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ParentProductEntityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodStageMaterials_GroupMaterials_GroupMaterialEnt~",
                table: "WorkingPeriodStageMaterials");

            migrationBuilder.DropTable(
                name: "ProductSubTypeGroupMaterialRelations");

            migrationBuilder.DropTable(
                name: "GroupMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Products_ParentProductEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentProductEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "GroupMaterialId",
                table: "WorkingPeriodStageMaterials",
                newName: "MaterialSampleId");

            migrationBuilder.RenameColumn(
                name: "GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterials",
                newName: "MaterialSampleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingPeriodStageMaterials_GroupMaterialEntitiesId",
                table: "WorkingPeriodStageMaterials",
                newName: "IX_WorkingPeriodStageMaterials_MaterialSampleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodStageMaterials_MaterialSamples_MaterialSampleE~",
                table: "WorkingPeriodStageMaterials",
                column: "MaterialSampleEntityId",
                principalTable: "MaterialSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
