using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class migration_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Pa~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSam~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropColumn(
                name: "ParentProductSubTypeWorkingPeriodSamplesId",
                table: "WorkingPeriodRelation");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkin~",
                table: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~",
                column: "ParentProductSubTypeWorkingPeriodSamplesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductSubTypeWorkingPeriodSamplesId",
                table: "WorkingPeriodRelation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSam~1",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSamplesId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Pa~1",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSamplesId",
                principalTable: "ProductSubTypeWorkingPeriodSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
