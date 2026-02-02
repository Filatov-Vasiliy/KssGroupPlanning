using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Manager = table.Column<string>(type: "text", nullable: false),
                    Contragent = table.Column<string>(type: "text", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCurrent = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrcMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductOrderName = table.Column<string>(type: "text", nullable: false),
                    ProductOrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MaterialName = table.Column<string>(type: "text", nullable: false),
                    MaterialGroup = table.Column<string>(type: "text", nullable: false),
                    Qty = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentQty = table.Column<decimal>(type: "numeric", nullable: false),
                    PostedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ForAdmissionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductOrderNameChild = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrcMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrcOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_SrcOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrcProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductOrderName = table.Column<string>(type: "text", nullable: false),
                    ProductOrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Factory = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrcProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ProductSubType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubType_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brigade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountEmployee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brigade_Factory_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brigade_StageType_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Factory_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductSubType_ProductSubTypeId",
                        column: x => x.ProductSubTypeId,
                        principalTable: "ProductSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Product_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeStageSample",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    StageName = table.Column<string>(type: "text", nullable: false),
                    MaterialStageId = table.Column<Guid>(type: "uuid", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeStageSample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeStageSample_MaterialStage_MaterialStageId",
                        column: x => x.MaterialStageId,
                        principalTable: "MaterialStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeStageSample_ProductSubType_ProductSubTypeId",
                        column: x => x.ProductSubTypeId,
                        principalTable: "ProductSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeWorkingPeriodSample",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    WorkingPeriodName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false),
                    StandartEmployee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeWorkingPeriodSample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeWorkingPeriodSample_ProductSubType_ProductSub~",
                        column: x => x.ProductSubTypeId,
                        principalTable: "ProductSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriod_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateDelivery = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageMaterial_GroupMaterial_GroupMaterialId",
                        column: x => x.GroupMaterialId,
                        principalTable: "GroupMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageMaterial_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeStageSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stage_ProductSubTypeStageSample_ProductSubTypeStageSampleId",
                        column: x => x.ProductSubTypeStageSampleId,
                        principalTable: "ProductSubTypeStageSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stage_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypeGroupMaterialRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypeGroupMaterialRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeGroupMaterialRelation_GroupMaterial_GroupMate~",
                        column: x => x.GroupMaterialId,
                        principalTable: "GroupMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~",
                        column: x => x.ProductSubTypeWorkingPeriodSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Chi~",
                        column: x => x.ChildProductSubTypeWorkingPeriodSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Par~",
                        column: x => x.ParentProductSubTypeWorkingPeriodSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageTypeRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageTypeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                        column: x => x.ProductSubTypeWorkingPeriodSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageTypeRelation_StageType_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Recycling = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ProductSubTypeWorkingPeriodSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStage_ProductSubTypeWorkingPeriodSample_Produc~",
                        column: x => x.ProductSubTypeWorkingPeriodSampleId,
                        principalTable: "ProductSubTypeWorkingPeriodSample",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStage_WorkingPeriod_WorkingPeriodId",
                        column: x => x.WorkingPeriodId,
                        principalTable: "WorkingPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStageBrigadeRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodStageId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriodStageBrigadeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageBrigadeRelation_Brigade_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingPeriodStageBrigadeRelation_WorkingPeriodStage_Workin~",
                        column: x => x.WorkingPeriodStageId,
                        principalTable: "WorkingPeriodStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0cb6cef3-948e-426e-b655-a481772a84ca"), "Ш/У" },
                    { new Guid("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca"), "ЖУВ" },
                    { new Guid("3d747667-d70c-436c-8856-4f56a38a19ea"), "КГН" },
                    { new Guid("42a52c7e-1fcd-46bf-bf78-34a89f642cd3"), "Водомерный узел" },
                    { new Guid("56938081-7b16-4bb4-a002-38bea806f87c"), "ПНС в корпусе" },
                    { new Guid("898d6acb-7ad6-4ca6-982c-d297af815a64"), "ПНС" },
                    { new Guid("8b12a5b4-5905-4eb9-8927-1b31eb9e960d"), "КР" },
                    { new Guid("96197298-d396-424a-8ed8-0f97bd038ba0"), "ВНС в блок-контейнере" },
                    { new Guid("993548ea-fde7-4614-ac09-6c12bbfbfd9d"), "ВНС в корпусе" },
                    { new Guid("aef1f8a9-9fa9-4696-abcf-228129da8ad1"), "Блок-контейнер" },
                    { new Guid("b19d093d-2deb-4b33-a69b-f1583aa27ab6"), "ПНС в блок-контейнере" },
                    { new Guid("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d"), "КГН" },
                    { new Guid("c871d38b-21c7-4429-bdce-3c70c379ae4b"), "КНС" },
                    { new Guid("d64ac138-b11f-44ef-b75a-48c338bc8157"), "ЛОС" },
                    { new Guid("def39874-75ce-4a53-8c1e-6a6bc88f92b9"), "Автомойка" },
                    { new Guid("e02387d3-03c9-4acf-acca-fad34ac47038"), "КНС в корпусе" },
                    { new Guid("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561"), "ЕН" },
                    { new Guid("edf5834b-a1a0-42a7-8b55-84de08280fd7"), "КП" },
                    { new Guid("f102fb2a-b3af-462f-9989-9d1f3169d44b"), "ВНС" }
                });

            migrationBuilder.InsertData(
                table: "ProductSubType",
                columns: new[] { "Id", "Name", "ProductTypeId" },
                values: new object[,]
                {
                    { new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), "Подтип А", new Guid("993548ea-fde7-4614-ac09-6c12bbfbfd9d") },
                    { new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), "Подтип В", new Guid("0cb6cef3-948e-426e-b655-a481772a84ca") },
                    { new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), "Подтип Б", new Guid("d64ac138-b11f-44ef-b75a-48c338bc8157") },
                    { new Guid("212d7ed0-547a-4611-a514-b47f5eb5e7ac"), "Подтип Б", new Guid("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca") },
                    { new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), "Подтип Б", new Guid("aef1f8a9-9fa9-4696-abcf-228129da8ad1") },
                    { new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), "Подтип А", new Guid("e02387d3-03c9-4acf-acca-fad34ac47038") },
                    { new Guid("32fed513-1b3c-49cb-9e06-ddfaaa82a488"), "Подтип Б", new Guid("edf5834b-a1a0-42a7-8b55-84de08280fd7") },
                    { new Guid("3762494e-93a2-458b-8030-10397640cf4b"), "Подтип А", new Guid("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca") },
                    { new Guid("37f914f9-3463-4a46-919d-850ab0438593"), "Подтип А", new Guid("42a52c7e-1fcd-46bf-bf78-34a89f642cd3") },
                    { new Guid("3fbc3814-6bce-4046-a0b2-1bd279b1d295"), "Подтип А", new Guid("96197298-d396-424a-8ed8-0f97bd038ba0") },
                    { new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), "Подтип А", new Guid("0cb6cef3-948e-426e-b655-a481772a84ca") },
                    { new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), "Подтип А", new Guid("898d6acb-7ad6-4ca6-982c-d297af815a64") },
                    { new Guid("4e4c686b-1698-4718-af84-2ccd154b7475"), "Подтип Б", new Guid("96197298-d396-424a-8ed8-0f97bd038ba0") },
                    { new Guid("57f44c4b-8bb9-41d6-9199-1bd8b52bef34"), "Подтип А", new Guid("def39874-75ce-4a53-8c1e-6a6bc88f92b9") },
                    { new Guid("5daed2ae-ca02-4ea7-a7fa-f2ad68ba5f16"), "Подтип Б", new Guid("c871d38b-21c7-4429-bdce-3c70c379ae4b") },
                    { new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), "Подтип Б", new Guid("56938081-7b16-4bb4-a002-38bea806f87c") },
                    { new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), "Подтип А", new Guid("aef1f8a9-9fa9-4696-abcf-228129da8ad1") },
                    { new Guid("86a6d038-d6f0-41b5-b372-37cd66e0ff0a"), "Подтип А", new Guid("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d") },
                    { new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), "Подтип А", new Guid("d64ac138-b11f-44ef-b75a-48c338bc8157") },
                    { new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), "Подтип Б", new Guid("e02387d3-03c9-4acf-acca-fad34ac47038") },
                    { new Guid("af05f00d-070e-494c-a734-84cf1ae27c84"), "Подтип А", new Guid("3d747667-d70c-436c-8856-4f56a38a19ea") },
                    { new Guid("b574504f-1e86-42da-b016-48a92df3a22f"), "Подтип Б", new Guid("42a52c7e-1fcd-46bf-bf78-34a89f642cd3") },
                    { new Guid("b63d4462-6380-4f35-a746-56702ae9e1e8"), "Подтип Б", new Guid("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d") },
                    { new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), "Подтип А", new Guid("8b12a5b4-5905-4eb9-8927-1b31eb9e960d") },
                    { new Guid("b853406e-78d8-4d2c-bfb6-1827da7e301b"), "Подтип А", new Guid("b19d093d-2deb-4b33-a69b-f1583aa27ab6") },
                    { new Guid("c04349f0-de53-4c71-a700-a74acb81fae2"), "Подтип Б", new Guid("3d747667-d70c-436c-8856-4f56a38a19ea") },
                    { new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), "Подтип А", new Guid("56938081-7b16-4bb4-a002-38bea806f87c") },
                    { new Guid("d8fe2c6f-d3ad-428e-8536-79d8f3298da8"), "Подтип А", new Guid("c871d38b-21c7-4429-bdce-3c70c379ae4b") },
                    { new Guid("dcb0b6e8-1a86-435a-b2a9-28dfd2deb699"), "Подтип Б", new Guid("b19d093d-2deb-4b33-a69b-f1583aa27ab6") },
                    { new Guid("dd235869-87fa-492a-9896-810b2d69261b"), "Подтип Б", new Guid("898d6acb-7ad6-4ca6-982c-d297af815a64") },
                    { new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), "Подтип Б", new Guid("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561") },
                    { new Guid("e0a9c3e6-3102-4892-a44b-8bec8bfcdf1c"), "Подтип А", new Guid("edf5834b-a1a0-42a7-8b55-84de08280fd7") },
                    { new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), "Подтип А", new Guid("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561") },
                    { new Guid("e86fec9c-55b8-4740-b396-1aea0e964e67"), "Подтип Б", new Guid("def39874-75ce-4a53-8c1e-6a6bc88f92b9") },
                    { new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), "Подтип Б", new Guid("0cb6cef3-948e-426e-b655-a481772a84ca") },
                    { new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), "Подтип Б", new Guid("f102fb2a-b3af-462f-9989-9d1f3169d44b") },
                    { new Guid("f8402906-63fa-4781-9240-2cf192133da0"), "Подтип Б", new Guid("8b12a5b4-5905-4eb9-8927-1b31eb9e960d") },
                    { new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), "Подтип Б", new Guid("993548ea-fde7-4614-ac09-6c12bbfbfd9d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brigade_FactoryId",
                table: "Brigade",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigade_StageTypeId",
                table: "Brigade",
                column: "StageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStage_GroupMaterialId",
                table: "MaterialStage",
                column: "GroupMaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_FactoryId",
                table: "Product",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ParentProductId",
                table: "Product",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubTypeId",
                table: "Product",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubType_ProductTypeId",
                table: "ProductSubType",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelation_GroupMaterialId",
                table: "ProductSubTypeGroupMaterialRelation",
                column: "GroupMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeGroupMaterialRelation_ProductSubTypeWorkingPe~",
                table: "ProductSubTypeGroupMaterialRelation",
                column: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSample_MaterialStageId",
                table: "ProductSubTypeStageSample",
                column: "MaterialStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeStageSample_ProductSubTypeId",
                table: "ProductSubTypeStageSample",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubTypeWorkingPeriodSample_ProductSubTypeId",
                table: "ProductSubTypeWorkingPeriodSample",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_ProductId",
                table: "Stage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_ProductSubTypeStageSampleId",
                table: "Stage",
                column: "ProductSubTypeStageSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriod_ProductId",
                table: "WorkingPeriod",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ChildProductSubTypeWorkingPeriodSampl~",
                table: "WorkingPeriodRelation",
                column: "ChildProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSamp~",
                table: "WorkingPeriodRelation",
                column: "ParentProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStage_ProductSubTypeWorkingPeriodSampleId",
                table: "WorkingPeriodStage",
                column: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStage_WorkingPeriodId",
                table: "WorkingPeriodStage",
                column: "WorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_BrigadeId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageBrigadeRelation_WorkingPeriodStageId",
                table: "WorkingPeriodStageBrigadeRelation",
                column: "WorkingPeriodStageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterial_GroupMaterialId",
                table: "WorkingPeriodStageMaterial",
                column: "GroupMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageMaterial_ProductId",
                table: "WorkingPeriodStageMaterial",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelation_ProductSubTypeWorkingPeriodS~",
                table: "WorkingPeriodStageTypeRelation",
                column: "ProductSubTypeWorkingPeriodSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriodStageTypeRelation_StageTypeId",
                table: "WorkingPeriodStageTypeRelation",
                column: "StageTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubTypeGroupMaterialRelation");

            migrationBuilder.DropTable(
                name: "SrcMaterial");

            migrationBuilder.DropTable(
                name: "SrcOrder");

            migrationBuilder.DropTable(
                name: "SrcProduct");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WorkingPeriodRelation");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageBrigadeRelation");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageMaterial");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStageTypeRelation");

            migrationBuilder.DropTable(
                name: "ProductSubTypeStageSample");

            migrationBuilder.DropTable(
                name: "Brigade");

            migrationBuilder.DropTable(
                name: "WorkingPeriodStage");

            migrationBuilder.DropTable(
                name: "MaterialStage");

            migrationBuilder.DropTable(
                name: "StageType");

            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSample");

            migrationBuilder.DropTable(
                name: "WorkingPeriod");

            migrationBuilder.DropTable(
                name: "GroupMaterial");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Factory");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductSubType");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
