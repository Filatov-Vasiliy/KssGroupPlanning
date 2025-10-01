using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class test_fix_workingperiodrelation_v5 : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
