using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class migration_v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubTypeWorkingPeriodSampleEntityProductSubTypeWorkingPe~");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductSubTypeWorkingPeriodSamplesId",
                table: "WorkingPeriodRelation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodRelation_ProductSubTypeWorkingPeriodSample_Pa~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriodRelation_ParentProductSubTypeWorkingPeriodSam~1",
                table: "WorkingPeriodRelation");

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("212d7ed0-547a-4611-a514-b47f5eb5e7ac"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("2c891976-9006-4f0a-b493-52f1b8366946"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("32fed513-1b3c-49cb-9e06-ddfaaa82a488"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("3762494e-93a2-458b-8030-10397640cf4b"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("37f914f9-3463-4a46-919d-850ab0438593"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("3fbc3814-6bce-4046-a0b2-1bd279b1d295"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("4e4c686b-1698-4718-af84-2ccd154b7475"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("57f44c4b-8bb9-41d6-9199-1bd8b52bef34"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("5daed2ae-ca02-4ea7-a7fa-f2ad68ba5f16"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("65310518-d75f-44f7-bc26-20ec63245b19"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("86a6d038-d6f0-41b5-b372-37cd66e0ff0a"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("af05f00d-070e-494c-a734-84cf1ae27c84"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("b574504f-1e86-42da-b016-48a92df3a22f"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("b63d4462-6380-4f35-a746-56702ae9e1e8"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("b853406e-78d8-4d2c-bfb6-1827da7e301b"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("c04349f0-de53-4c71-a700-a74acb81fae2"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("d8fe2c6f-d3ad-428e-8536-79d8f3298da8"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("dcb0b6e8-1a86-435a-b2a9-28dfd2deb699"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("dd235869-87fa-492a-9896-810b2d69261b"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("e0a9c3e6-3102-4892-a44b-8bec8bfcdf1c"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("e86fec9c-55b8-4740-b396-1aea0e964e67"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("f8402906-63fa-4781-9240-2cf192133da0"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("0cb6cef3-948e-426e-b655-a481772a84ca"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("11f6dadc-53d7-4ba3-90d4-8e9bfc0e4bca"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("3d747667-d70c-436c-8856-4f56a38a19ea"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("42a52c7e-1fcd-46bf-bf78-34a89f642cd3"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("56938081-7b16-4bb4-a002-38bea806f87c"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("898d6acb-7ad6-4ca6-982c-d297af815a64"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("8b12a5b4-5905-4eb9-8927-1b31eb9e960d"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("96197298-d396-424a-8ed8-0f97bd038ba0"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("993548ea-fde7-4614-ac09-6c12bbfbfd9d"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("aef1f8a9-9fa9-4696-abcf-228129da8ad1"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("b19d093d-2deb-4b33-a69b-f1583aa27ab6"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("bf5381ed-4534-4ec5-87f7-8b28eaa22f9d"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("c871d38b-21c7-4429-bdce-3c70c379ae4b"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("d64ac138-b11f-44ef-b75a-48c338bc8157"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("def39874-75ce-4a53-8c1e-6a6bc88f92b9"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("e02387d3-03c9-4acf-acca-fad34ac47038"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("e7d8c946-0a0e-4556-9cbb-2e1e3e6d1561"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("edf5834b-a1a0-42a7-8b55-84de08280fd7"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: new Guid("f102fb2a-b3af-462f-9989-9d1f3169d44b"));

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
    }
}
