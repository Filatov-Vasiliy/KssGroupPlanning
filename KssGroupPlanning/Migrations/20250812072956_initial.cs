using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Manager = table.Column<string>(type: "text", nullable: false),
                    Contragent = table.Column<string>(type: "text", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCurrent = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrcOrders",
                columns: table => new
                {
                    OrderName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Contragent = table.Column<string>(type: "text", nullable: false),
                    Dogovor = table.Column<string>(type: "text", nullable: false),
                    Manager = table.Column<string>(type: "text", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SchemeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LogisticDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCurrent = table.Column<decimal>(type: "numeric", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrcOrders", x => x.OrderName);
                });

            migrationBuilder.CreateTable(
                name: "StageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypes_ProductTypes_ProductTypeEntityId",
                        column: x => x.ProductTypeEntityId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactoryEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    End_date = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Factories_FactoryEntityId",
                        column: x => x.FactoryEntityId,
                        principalTable: "Factories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductSubTypes_ProductSubTypeEntityId",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeStagesSamples",
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
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    WorkingPeriodName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false),
                    StandartEmployee = table.Column<int>(type: "integer", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodsSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodsSamples_ProductSubTypes_Product~",
                        column: x => x.ProductSubTypeEntityId,
                        principalTable: "ProductSubTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodsSamples_StageTypes_StageTypeEnt~",
                        column: x => x.StageTypeEntityId,
                        principalTable: "StageTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriods_Products_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Recycling = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ProductSubTypeWorkingPeriodsSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSamples_Pro~",
                        column: x => x.ProductSubTypeWorkingPeriodsSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodsSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStages_WorkingPeriods_WorkingPeriodId",
                        column: x => x.WorkingPeriodId,
                        principalTable: "WorkingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FactoryEntityId",
                table: "Products",
                column: "FactoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderEntityId",
                table: "Products",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSubTypeEntityId",
                table: "Products",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypes_ProductTypeEntityId",
                table: "ProductSubTypes",
                column: "ProductTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStagesSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeStagesSamples",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodsSamples_ProductSubTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                column: "ProductSubTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodsSamples_StageTypeEntityId",
                table: "ProductSubTypeWorkingPeriodsSamples",
                column: "StageTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ProductId",
                table: "Stages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriods_ProductEntityId",
                table: "WorkingPeriods",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStages_ProductSubTypeWorkingPeriodsSampleId",
                table: "WorkingPeriodStages",
                column: "ProductSubTypeWorkingPeriodsSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStages_WorkingPeriodId",
                table: "WorkingPeriodStages",
                column: "WorkingPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialSamples");

            migrationBuilder.DropTable(
                name: "ProductSubTypeStagesSamples");

            migrationBuilder.DropTable(
                name: "SrcOrders");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStages");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodsSamples");

            migrationBuilder.DropTable(
                name: "WorkingPeriods");

            migrationBuilder.DropTable(
                name: "StageTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Factories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductSubTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
