using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaterialStageId",
                table: "ProductSubTypeStageSample",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MaterialStage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageName = table.Column<string>(type: "text", nullable: false),
                    GroupMaterialId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialStage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialStage_GroupMaterial_GroupMaterialId",
                        column: x => x.GroupMaterialId,
                        principalTable: "GroupMaterial",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSample_MaterialStageId",
                table: "ProductSubTypeStageSample",
                column: "MaterialStageId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStage_GroupMaterialId",
                table: "MaterialStage",
                column: "GroupMaterialId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubTypeStageSample_MaterialStage_MaterialStageId",
                table: "ProductSubTypeStageSample",
                column: "MaterialStageId",
                principalTable: "MaterialStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubTypeStageSample_MaterialStage_MaterialStageId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropTable(
                name: "MaterialStage");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubTypeStageSample_MaterialStageId",
                table: "ProductSubTypeStageSample");

            migrationBuilder.DropColumn(
                name: "MaterialStageId",
                table: "ProductSubTypeStageSample");
        }
    }
}
