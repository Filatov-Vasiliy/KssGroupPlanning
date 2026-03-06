using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial_v6_dq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DQSrcMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductOrderName = table.Column<string>(type: "text", nullable: true),
                    ProductOrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaterialName = table.Column<string>(type: "text", nullable: true),
                    MaterialGroup = table.Column<string>(type: "text", nullable: true),
                    Qty = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentQty = table.Column<decimal>(type: "numeric", nullable: true),
                    PostedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ForAdmissionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProductOrderNameChild = table.Column<string>(type: "text", nullable: true),
                    ProductOrderDateChild = table.Column<DateOnly>(type: "date", nullable: true),
                    LoadStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    GroupMaterialId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DQSrcMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DQSrcOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderName = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Contragent = table.Column<string>(type: "text", nullable: true),
                    Dogovor = table.Column<string>(type: "text", nullable: true),
                    Manager = table.Column<string>(type: "text", nullable: true),
                    OrderNumber = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SchemeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LogisticDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentCurrent = table.Column<decimal>(type: "numeric", nullable: true),
                    Qty = table.Column<int>(type: "integer", nullable: true),
                    LoadStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DQSrcOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DQSrcProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductOrderName = table.Column<string>(type: "text", nullable: true),
                    ProductOrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Factory = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OrderNumber = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    Qty = table.Column<int>(type: "integer", nullable: true),
                    LoadStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DQSrcProduct", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DQSrcMaterial");

            migrationBuilder.DropTable(
                name: "DQSrcOrder");

            migrationBuilder.DropTable(
                name: "DQSrcProduct");
        }
    }
}
