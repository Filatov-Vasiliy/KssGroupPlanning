using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class initial_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("28539a79-0747-4d57-9e1e-a2e63adff951"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("2a62b9a4-d1eb-4bce-b8fb-903736e8ea81"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("46496326-7bc8-4935-8a9c-c319e1ed81e8"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("78b76136-bd49-4a8b-b867-77d16fc98d7a"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("8a01b1ce-4413-4174-b303-c20801699408"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("95d41135-a72b-4386-9734-d579fa0947fe"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("b037956e-2af1-4d26-a2c6-f8b8cd5ce09e"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("d20b1bfc-3752-44b5-b671-448e9308ba54"));

            migrationBuilder.InsertData(
                table: "GroupMaterial",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), "Оборудование" },
                    { new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), "ШУ" },
                    { new Guid("364de2b0-c161-4093-9e5c-43e43f28ae2d"), "Расходные материалы" },
                    { new Guid("37db04d9-37f0-4fbd-9359-8ba5f7354d03"), "УПМ" },
                    { new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), "Нержа" },
                    { new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), "Электрика" },
                    { new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), "Насосы" },
                    { new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), "Станция" },
                    { new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), "Арматура" },
                    { new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), "Металл" },
                    { new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), "Метизы" }
                });

            migrationBuilder.InsertData(
                table: "MaterialStage",
                columns: new[] { "Id", "GroupMaterialId", "StageName" },
                values: new object[,]
                {
                    { new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), null, "С" },
                    { new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), null, "П" }
                });

            migrationBuilder.InsertData(
                table: "ProductSubType",
                columns: new[] { "Id", "Name", "ProductTypeId" },
                values: new object[] { new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), "Подтип А", new Guid("f102fb2a-b3af-462f-9989-9d1f3169d44b") });

            migrationBuilder.InsertData(
                table: "ProductSubTypeWorkingPeriodSample",
                columns: new[] { "Id", "ProductSubTypeId", "RowNumber", "StandartEmployee", "StandartTime", "WorkingPeriodName" },
                values: new object[,]
                {
                    { new Guid("0068c383-ddcd-46f9-af97-a5490d66b88a"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 4, 1, "80", "Сварка рамы" },
                    { new Guid("04498db6-fe2c-4228-9065-4b0341dbd892"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 5, 3, "420", "Монтаж площадки" },
                    { new Guid("081e2d98-a604-44f2-96e6-1cd486e5db2f"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 2, 2, "120", "Изготовление лестницы" },
                    { new Guid("0b1ca170-699b-47aa-b6a0-c73e7166641f"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 7, 1, "60", "Сварка стойки для Ш/У" },
                    { new Guid("0b252a71-179e-4e31-8698-19ac4357050e"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 4, 3, "180", "Подготовка муфт" },
                    { new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, 1, "80", "Формирование рамы" },
                    { new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, 1, "80", "Формирование рамы" },
                    { new Guid("135f6269-66f7-4ba7-9c48-18c2b03ecf3b"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 6, 1, "180", "Покраска крыши" },
                    { new Guid("13c0b41c-048e-4d0c-b4ad-2e13a99e179d"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 4, 1, "120", "Подготовка муфт" },
                    { new Guid("142b775f-44e3-490b-a593-70b8698c93b5"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 6, 1, "240", "Обкатка болтов" },
                    { new Guid("173177f4-e827-4098-ac98-cf621cfc9804"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 1, 1, "120", "Формирование труб коллектора" },
                    { new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 1, 2, "480", "Зачистка, нарезка труб, швеллера" },
                    { new Guid("1c2a76d1-285b-4a89-954c-3738305288e8"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 7, 1, "120", "Покраска" },
                    { new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 7, 1, "240", "Покраска" },
                    { new Guid("267a492a-2d55-4a33-9bb8-d3076bea8e1d"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 6, 1, "120", "Обкатка болтов" },
                    { new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 7, 1, "120", "Покраска" },
                    { new Guid("27e28db9-c523-48db-883d-d0ccfa8121c7"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 4, 2, "150", "Подготовка оснастки для намотки корпуса" },
                    { new Guid("2cf2af17-da41-4eb0-ac8d-ccb34fa708e6"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 8, 1, "30", "Крепление Ш/У к стойке" },
                    { new Guid("2e736ab4-400c-4c59-a496-f6f748259f54"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 5, 1, "480", "Настройка и отладка" },
                    { new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 5, 2, "180", "Монтаж рамы под УПМ" },
                    { new Guid("3193c06a-a1f8-42c5-abcd-c30fee6612b5"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 3, 1, "240", "Изготовление такелажных петель" },
                    { new Guid("3bf0a6d2-d337-4061-9222-84dc95fc215a"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 5, 2, "180", "Монтаж оснастки" },
                    { new Guid("3c932b0e-6668-4512-9633-fb2472bce36e"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 2, 2, "120", "Намотка корпуса" },
                    { new Guid("3d0aa127-4111-471b-be31-2968e736a4af"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("3e1fc4a8-5a0b-4be5-95f9-4893a64171d3"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 5, 3, "300", "Монтаж площадки" },
                    { new Guid("42a2c51f-b3ef-44c1-ad30-961279727186"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 6, 1, "180", "Обкатка болтов" },
                    { new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 2, 1, "240", "Сварка нижней рамы" },
                    { new Guid("46fd1905-67cd-4ab4-b242-a924322db5cc"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 1, 1, "180", "Устанока клемм" },
                    { new Guid("474890d4-4172-434b-a950-de96eb1808ce"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 6, 1, "120", "Обкатка болтов" },
                    { new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 3, 3, "80", "Извлечение изделия, разборка оснастки" },
                    { new Guid("4b9095f8-5d68-4440-8558-1d52c7391a42"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 2, 1, "180", "Сварка труб коллектора" },
                    { new Guid("4d325a32-7d5b-496a-a312-209df69bdb2c"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 7, 1, "120", "Покраска" },
                    { new Guid("4f9640f4-2a15-41e9-a917-8300356f4492"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("50a3a8be-8720-4a3e-a780-23f16a642350"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 8, 3, "120", "Устанвка и монтаж распорок" },
                    { new Guid("526a5359-0914-4418-a29c-94151052effd"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 5, 1, "240", "Сварка крыши" },
                    { new Guid("533662ef-436c-4743-a872-f4c5d17418bf"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 1, 2, "90", "Нанесение карбоната, стрейч-пленки" },
                    { new Guid("5576f277-d9d1-4d9b-93e3-9288a4aff46a"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 5, 1, "480", "Настройка и отладка" },
                    { new Guid("5c2e714b-d3f3-49db-bf3d-c6e676319348"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 5, 3, "300", "Монтаж площадки" },
                    { new Guid("5c7f8886-f1d0-44bd-b6a0-83436dc7d796"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 5, 1, "180", "Сборка насосов с рамой" },
                    { new Guid("5d06464c-3f4b-43c2-9b82-9d61d9d76f44"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 3, 2, "120", "Стыковка стоек" },
                    { new Guid("5d618e84-632e-4ebf-a985-88342bae2a00"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 6, 2, "120", "Покраска крыши" },
                    { new Guid("5efd0f05-e595-4a2f-94b3-2347f94eecc7"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 1, 2, "60", "Намотка горловины" },
                    { new Guid("621e09d9-e112-4845-b565-0db2f2426992"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 7, 1, "180", "Покраска" },
                    { new Guid("62f2a48e-af3f-4fc3-8dbe-e1ff5a434646"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 2, 1, "120", "Подключение клемм" },
                    { new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 1, 1, "120", "Формирование труб коллектора" },
                    { new Guid("65f7f254-32e3-47ca-931a-81f705a449e6"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 2, 2, "120", "Намотка корпуса" },
                    { new Guid("66b8f43f-a427-4e21-ab1e-db2941306f52"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 2, 1, "120", "Подключение клемм" },
                    { new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 1, 1, "180", "Формирование труб коллектора" },
                    { new Guid("67eb07a6-7deb-48b0-a9d0-f1968c45cb94"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 6, 1, "70", "Подготовка оснастки для формования сферы" },
                    { new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 5, 2, "120", "Монтаж рамы под УПМ" },
                    { new Guid("6bd776c7-8727-4acd-873a-8356a86b3071"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("7484d536-2104-4e11-a322-ce4c6e0b35a0"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 5, 1, "300", "Формовка муфт" },
                    { new Guid("762c4c6f-9015-4fdf-bcd8-4e27b105fbec"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("76303263-3782-4d46-b880-c19822006c04"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 4, 2, "240", "Стыковка добавочного кольца" },
                    { new Guid("77ccfad7-8078-4fd3-aa6c-b4323ab5778f"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 4, 1, "60", "Подготовка Чеппера" },
                    { new Guid("783c2ca2-5a56-4130-8d02-ec3cc86c0c31"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 2, 2, "180", "Намотка корпуса" },
                    { new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 2, 1, "180", "Сварка нижней рамы" },
                    { new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 7, 1, "90", "Сварка стойки Ш/У" },
                    { new Guid("7c9f77a1-5ebf-4ed6-b3ee-9dc1aef00dc9"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 6, 1, "80", "Сварка стойки для Ш/У" },
                    { new Guid("7ce6529f-2e9f-4d90-8cc1-d4103587a64e"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 3, 1, "60", "Установка ПЧР" },
                    { new Guid("7fc5f63e-2118-49d7-baea-9b9d3617b736"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, 2, "150", "Зачистка корпуса бочки" },
                    { new Guid("7fdcb535-058e-40b3-a94d-8bde6e7cbbf8"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 1, 2, "150", "Подготовка оснастки для намотки" },
                    { new Guid("81e13934-f2c3-453b-ad90-25e296b0fef1"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 5, 1, "420", "Формовка муфт" },
                    { new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 5, 1, "180", "Крепление насосов к раме" },
                    { new Guid("8234c78c-a01e-4a78-8713-981203e05a1d"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 4, 1, "60", "Сварка рамы" },
                    { new Guid("8397b6bc-3b49-4713-8b9d-3f2c1b0fbb82"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 2, 2, "60", "Установка дна" },
                    { new Guid("874cdded-4702-41e5-8966-040ab54d391b"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 4, 1, "180", "Подключение УПП" },
                    { new Guid("875bbfba-88c8-454f-903a-6746583208f1"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, 2, "220", "Зачистка корпусса бочки" },
                    { new Guid("879de3f9-f723-4be3-a586-2b701a539b29"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 7, 6, "240", "Формовка сферы" },
                    { new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 3, 3, "60", "Извлечение изделия, разборка оснастки" },
                    { new Guid("89347670-f793-486c-b128-8f7a2f515ef0"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 3, 1, "480", "Настройка и отладка" },
                    { new Guid("8bb0c743-e355-45b1-88e6-0fada09890f6"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 2, 1, "120", "Подключение клемм" },
                    { new Guid("8d603740-7fc0-4e94-b39e-3668dff81a9e"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 8, 3, "120", "Установка и монтаж распорок" },
                    { new Guid("8e4d0088-6115-4b6f-b7ef-d413506538eb"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 2, 2, "120", "Намотка корпуса" },
                    { new Guid("8e75f834-b4c1-4053-b900-bd66989e0d4b"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 7, 1, "60", " Крепление Ш/У к стойке" },
                    { new Guid("8ea09125-f185-4e0b-adda-53297635bbec"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 4, 1, "180", "Покраска дна" },
                    { new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 4, 2, "240", "Стыковка добавочного кольца" },
                    { new Guid("9412d24f-2f65-492c-8849-f476ceec0b87"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 4, 1, "60", "Сварка рамы" },
                    { new Guid("94535c6e-9aac-453a-b685-ca2b99adf6f4"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, 2, "150", "Зачистка корпуса бочки" },
                    { new Guid("9569d494-3594-4f37-bc8a-feeb7cdb234f"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, 2, "150", "Зачиска корпуса бочки" },
                    { new Guid("9821c143-c77b-4e52-be86-a3bc96b0b084"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 6, 1, "60", "Сварка стойки для Ш/У" },
                    { new Guid("98a18796-c791-41c0-b73f-14e7ff602154"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 2, 1, "180", "Сварка труб коллектора" },
                    { new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 4, 2, "360", "Стыковка добавочного кольца" },
                    { new Guid("9bd2a371-8bad-423f-8dc9-d4c427ff1914"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 1, 1, "180", "Устанвка клемм" },
                    { new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 4, 2, "300", "Стыковка добавочного кольца" },
                    { new Guid("9d274b06-dea3-4ccb-b6c4-d6827cf61194"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 5, 3, "420", "Монтаж площадки" },
                    { new Guid("a007ef54-77f0-4308-99a9-35d3f65ea262"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 6, 1, "120", "Обкатка болтов" },
                    { new Guid("a0aea18f-cb86-4e14-8b4c-ec7f247c30cc"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 6, 1, "30", "Крепление жокей-насоса к раме" },
                    { new Guid("a13286a4-eb36-4480-89af-0f190977231b"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 5, 2, "220", "Намотка корпуса" },
                    { new Guid("a30f3e81-db5b-419c-80b2-ffc87cffec56"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 7, 1, "30", "Крепление Ш/У к стойке" },
                    { new Guid("a74d2e07-14ce-4fe4-b789-03c42bd2fcc8"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 3, 2, "300", "Формовка дна" },
                    { new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 4, 2, "240", "Стыковка добавочного кольца" },
                    { new Guid("a7b4ab35-169d-4d41-a052-908482eef14a"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 3, 1, "240", "Изготовление такелажных петель" },
                    { new Guid("a9385f40-0c5e-4eaa-9b2b-233bc7ebe1ba"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, 2, "240", "Зачистка корпуса бочки" },
                    { new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, 1, "120", "Формирование рамы" },
                    { new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 7, 1, "60", "Сварка стойки Ш/У" },
                    { new Guid("b5bc2d06-302c-4ada-9d2c-7c64493d51e6"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 4, 2, "150", "Подготовка оснастки для намотки корпуса" },
                    { new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 4, 2, "300", "Стыковка добавочного кольца" },
                    { new Guid("be22fa22-51fd-4bef-8666-662fed9e980d"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 7, 1, "240", "Покраска" },
                    { new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 1, 2, "90", "Зачистка" },
                    { new Guid("bfa2f600-66b7-4bdd-ab88-2245351fe7aa"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 4, 1, "60", "Подготовка Чоппера" },
                    { new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 1, 2, "120", "Зачистка" },
                    { new Guid("c89a781b-a303-4164-a737-9a560a84c5f3"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 4, 1, "240", "Покраска дна" },
                    { new Guid("c8ff7225-d396-44eb-956f-6b9d2acbbb46"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 6, 1, "60", "Потготовка оснвстки для формования сферы" },
                    { new Guid("ce3b68c8-2180-4f16-b9aa-eada2ba6f69f"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 1, 2, "60", "Нанесение карбоната, стрейч-пленки" },
                    { new Guid("ce69ebc6-8f65-4f84-82d8-92fc1b3e3030"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 4, 1, "120", "Подключение ПЧР" },
                    { new Guid("d1ab2e54-dbfc-4399-a21a-2c4aae9b906d"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 2, 2, "60", "Установка дна" },
                    { new Guid("d3cf379a-9eb2-4892-b43a-089e0bb03431"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 9, 1, "60", "Монтаж и закатка лестницы" },
                    { new Guid("d52103d7-f93c-4a76-9914-3b480276ad9f"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 1, 1, "180", "Устанвка клемм" },
                    { new Guid("d5d06f09-da5d-4ba3-b502-0c517a811b13"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 3, 1, "80", "Установка УПП" },
                    { new Guid("d83d1e6b-6d9f-42e6-9029-845408b7849a"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 3, 2, "120", "Стыковка стоек" },
                    { new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 5, 1, "240", "Крепление насосов к раме" },
                    { new Guid("dc9553cb-eaf7-4f84-aeed-cd3c6938a158"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 2, 2, "180", "Намотка корпуса" },
                    { new Guid("e5b1c92b-a56a-4f77-ac00-1b10b0d9bab2"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 2, 1, "240", "Сварка труб коллектора" },
                    { new Guid("e5fc984b-a598-4d67-836e-5dcfe975ca3f"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 7, 6, "180", "Формование сферы" },
                    { new Guid("e7224acb-1c27-4833-b22f-298787c65305"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 6, 1, "120", "Обкадка болтов" },
                    { new Guid("e7df452e-f592-4d99-ba1e-385068e6a192"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 5, 3, "180", "Сварка крыши" },
                    { new Guid("e8973d21-c07a-44b5-9fb7-e8250797bc8d"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 1, 2, "80", "Намотка горловины" },
                    { new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, 2, "150", "Зачиска корпуса бочки" },
                    { new Guid("f291556f-4885-4ee5-b3e6-2f0c5f504db7"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 5, 2, "240", "Монтаж оснастки" },
                    { new Guid("f6da68f1-7e76-43cf-986c-802945e91d7e"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 3, 2, "180", "Формовка дна" },
                    { new Guid("f8a7550f-8a1a-4d34-8fa3-c69d15772c16"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 5, 2, "180", "Намотка корпуса" },
                    { new Guid("f9a88a4a-0d39-4d4f-9968-87e28aad1805"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 2, 2, "200", "Изготовление лестницы" },
                    { new Guid("faca59eb-4f95-4a46-aa76-94a0bb571523"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 2, 2, "180", "Намотка корпуса" },
                    { new Guid("fc2a1cba-ac3d-4c2b-a48c-b4e35d4e223d"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 2, 2, "120", "Намотка корпуса" },
                    { new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 1, 2, "480", "Зачистка, нарезка труб, швеллера" },
                    { new Guid("fe7f104a-2a93-486e-9ed7-0e8cdb9aeb49"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 2, 2, "120", "Намотка корпуса" }
                });

            migrationBuilder.InsertData(
                table: "StageType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb"), "Намотка" },
                    { new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa"), "Сварка" },
                    { new Guid("8216d505-b9c4-42a0-b855-4a649146222e"), "Сборка" },
                    { new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c"), "Формовка" },
                    { new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb"), "Слесарка" }
                });

            migrationBuilder.InsertData(
                table: "Brigade",
                columns: new[] { "Id", "CountEmployee", "FactoryId", "Name", "StageTypeId" },
                values: new object[,]
                {
                    { new Guid("035b8ebf-ac1a-48cd-9c67-a8ad90818f8a"), 2, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Слесарка", new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("0a6e0ea8-782c-4aa2-a24a-d3fddd2176de"), 1, new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Сборочная бригада 5", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("10eed623-c0dc-4e06-ad3d-05a7aa3c161a"), 1, new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Сборочная бригада 2", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("29c7903d-2297-4e6d-9218-72cab8d2cd21"), 1, new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Сборочная бригада 1", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("3843eac5-c541-4798-a158-dae8a221c0ce"), 2, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Бригада намотки 2", new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("5fd8f19f-799b-43bb-b65a-df494ee7ab22"), 1, new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Сборочная бригада 3", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("66817757-6f11-4560-b31d-7b7f4998f43a"), 3, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Сборочная бригада 2", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("672fae64-78a5-437f-a03c-d9b105ac138e"), 1, new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Сборочная бригада 4", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("70604fd7-a3d8-4622-95a6-ed67bf5b432a"), 2, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Сварка", new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("8362c64e-54e8-45cc-a4fb-bc06bb4cc8a3"), 7, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Формовка", new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("84576bf6-46de-4d4d-a4af-a4e054754bd9"), 1, new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Сборочная бригада 3", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("aa9f497d-e5a3-4b20-8af1-f8ac8271256d"), 6, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Сборочная бригада 1", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("aaf6647e-5b51-4366-a200-6019cfa0d5de"), 1, new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Сборочная бригада 2", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("b3acd3fa-071a-4449-bfe8-56c9e7c4e11e"), 2, new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Сварка", new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("c9fbcb8d-638b-4be6-8450-da7e7bf34ba9"), 2, new Guid("ed3f7875-2760-4853-8c6b-554657503449"), "Слесарка", new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("f48cbb13-cab5-463b-8f92-3c88ec140235"), 2, new Guid("4baad45d-a632-40d5-82ef-3bee797ddef8"), "Бригада намотки 1", new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("f751cf17-4b20-44f5-9520-1afa465c5b0a"), 1, new Guid("7e64daa0-2c53-4840-8b73-7fcd16718f78"), "Сборочная бригада 1", new Guid("8216d505-b9c4-42a0-b855-4a649146222e") }
                });

            migrationBuilder.InsertData(
                table: "MaterialStage",
                columns: new[] { "Id", "GroupMaterialId", "StageName" },
                values: new object[,]
                {
                    { new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), "М2" },
                    { new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), "ШУ" },
                    { new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"), new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), "Н" },
                    { new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), "А" },
                    { new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"), new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), "О" },
                    { new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), "М1" },
                    { new Guid("7e9d18a6-d0ce-4674-9401-52b3dd904c0d"), new Guid("37db04d9-37f0-4fbd-9359-8ba5f7354d03"), "У" },
                    { new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), "Э" },
                    { new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"), new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), "СТ" }
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypeGroupMaterialRelation",
                columns: new[] { "Id", "GroupMaterialId", "ProductSubTypeWorkingPeriodSampleId" },
                values: new object[,]
                {
                    { new Guid("02de8641-cd18-43f5-80de-82c60779f5e6"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("0677ff97-712f-46ad-8f5e-39c0a85e433e"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("07539f7b-2b8a-4e83-b281-f6ef82b9b5ce"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797") },
                    { new Guid("093a5db4-346a-4053-82bd-cae8d457afc6"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("0a24a5ff-6bd0-47c9-806c-a9a33d2587f5"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610") },
                    { new Guid("0c785add-14bd-49c5-bc22-e62fd5eba4f1"), new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("17442260-2a27-4f53-8b9b-c4e60450215b"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("1a28e447-5b81-4abb-9807-bcaeeeb67e3d"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("1cc8d3c7-27df-403b-b103-b4b75413d2a4"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("f9a88a4a-0d39-4d4f-9968-87e28aad1805") },
                    { new Guid("1d6bb553-7765-4066-98bc-38c0e03fef88"), new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0") },
                    { new Guid("1f0564fe-9241-4654-a68d-faa6d760b7e9"), new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("23641c84-2303-49e0-a89e-c5c2d0670c77"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("2cf2af17-da41-4eb0-ac8d-ccb34fa708e6") },
                    { new Guid("250f4fe7-5d0d-495d-83e2-9027f5c605ee"), new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c") },
                    { new Guid("28454aac-6245-4bbd-bc84-125b29369db0"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39") },
                    { new Guid("28ed49d4-4d9f-4104-93fe-f0e975723b73"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("29f365fc-a09b-4acb-ae77-de549ab887f6"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("2d97caf5-4519-4477-9113-498586ed954d"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c") },
                    { new Guid("2e9ba222-3879-4cf1-bfbd-fdaaaf5d372e"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("2f0c8c8b-65ca-49fd-b4e4-8d4b689bbf14"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d") },
                    { new Guid("32b47f46-9973-4702-98d1-34f8b57bdc6f"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e") },
                    { new Guid("365c00f9-33ed-4acd-a798-d6ee03a27edd"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1") },
                    { new Guid("39fdd183-e62a-42a0-90c9-d446d49cecfc"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("3a7e5547-718e-4450-9ac5-fd485af9a9c5"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("3da7ae97-97ec-49dc-a1d3-32f89146455e"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("46fd1905-67cd-4ab4-b242-a924322db5cc") },
                    { new Guid("3e4e68cc-1178-458f-85b7-35883c382a28"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("43c04d75-9b61-4cd1-8d8e-a5d4d1441f95"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("48b0acff-0ccf-4e2e-bcee-3282d06a004d"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("4a8916cb-454a-43f9-a62d-da8650fa85bd"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("51f102f5-b331-4c7b-b6b8-bd5c76c2142d"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("53212737-8afc-4af1-a953-75374eddfcf7"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("54cc4f7c-b5ab-486c-a105-322c8bc97195"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("552816d1-f728-43d8-9399-7da2d0a78853"), new Guid("37db04d9-37f0-4fbd-9359-8ba5f7354d03"), new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2") },
                    { new Guid("5a30817d-5016-41a2-899f-cdfbe65b7c37"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("5cb9fd05-cacf-4ccf-b538-93ea56f88412"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("62017377-7697-4f96-9230-81c8510ac387"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("62a789f7-4bfb-4f13-bb90-f8012c7a86c7"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("62b92acf-0eb6-4d44-b1f3-701717e51e1b"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e") },
                    { new Guid("6844152f-e5b1-4dec-b7a4-9d2eb7966caa"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("6844bb0d-bb73-4062-bc20-0e39ac176f5e"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88") },
                    { new Guid("69c4099a-2e39-44ec-a77b-5a90276d7fe1"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d") },
                    { new Guid("6cbd0575-8733-4e95-a655-99e8683d9b9a"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457") },
                    { new Guid("6e43aa3e-8a97-46f5-95f0-cac0924dc918"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958") },
                    { new Guid("76118a9d-e20d-4eb3-8464-24721e9c6c0b"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("7f6a43ed-3ade-46fd-b2a3-8ed8a659bb14"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("d52103d7-f93c-4a76-9914-3b480276ad9f") },
                    { new Guid("7ff55aae-5d3a-46bc-84aa-ab3c7c06da6f"), new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb") },
                    { new Guid("881a16ca-6370-4608-8b37-d08ec610471d"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("9554ac6a-9d2e-44cc-be40-db0ee1a540e6"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("173177f4-e827-4098-ac98-cf621cfc9804") },
                    { new Guid("9656bf60-29d8-42c6-8445-899b61d28733"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("9adeaa17-5d46-4764-87bb-1f84659a5466"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe") },
                    { new Guid("9e860497-f2ca-4b13-b409-7628113e64ad"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88") },
                    { new Guid("9fe251e1-d7b2-41d8-8a80-347727841bed"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("173177f4-e827-4098-ac98-cf621cfc9804") },
                    { new Guid("a3883624-5fd3-40b7-8356-703b9b1e2a68"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("a6e1d6d6-7351-4605-ba53-74b470fa648f"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08") },
                    { new Guid("b0481716-a8e3-46a6-82d1-4841d31cc21f"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39") },
                    { new Guid("b083878f-ff41-4275-8e3e-fbce6202850f"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08") },
                    { new Guid("b2d77f3a-f890-4d17-a3f4-6b20e59b4c3c"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e") },
                    { new Guid("b591292e-f513-4a9c-b134-1aa219d6c779"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610") },
                    { new Guid("bb9a8afb-97c2-4628-9830-a800b30d009a"), new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"), new Guid("9bd2a371-8bad-423f-8dc9-d4c427ff1914") },
                    { new Guid("bcb2c2f8-e21d-4e69-bf50-858bbf7416b8"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39") },
                    { new Guid("c06df845-203f-4840-8c71-142333bd88dd"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("081e2d98-a604-44f2-96e6-1cd486e5db2f") },
                    { new Guid("c7c2636d-b4b5-4b69-868d-0d23a0bbfe1f"), new Guid("37db04d9-37f0-4fbd-9359-8ba5f7354d03"), new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af") },
                    { new Guid("ca675cad-11e7-4f87-81ce-314f28f4a4df"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("cb2b20ff-c681-4ebb-bbb9-d8edc2b4d8b0"), new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb") },
                    { new Guid("ce6f0a7f-37af-466d-980f-32dd6e84dea7"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40") },
                    { new Guid("d1f97190-4f7e-4e5f-868d-12f0b2cc349d"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("081e2d98-a604-44f2-96e6-1cd486e5db2f") },
                    { new Guid("d2669dc8-cdc6-4119-96d5-efe37ddecf5c"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("dbe3bf90-76bb-423a-9b56-980333a73bf4"), new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("dc890769-7afb-45e1-967d-692eb5e5d09f"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("173177f4-e827-4098-ac98-cf621cfc9804") },
                    { new Guid("e2a1255c-c1f9-4ca4-ab4f-c31075359822"), new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("e3625e30-0b03-4091-81f0-19bcdfd888cc"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("e684ad23-7197-4506-9035-bc650ea8b2b1"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("a30f3e81-db5b-419c-80b2-ffc87cffec56") },
                    { new Guid("e6b17543-d832-4c6e-97cc-5ce97714939c"), new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c") },
                    { new Guid("eb39a962-a8e2-4da1-831c-c6a8640c560b"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe") },
                    { new Guid("f2e2fffe-eacf-4b15-a619-a59d235dc86a"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("f5128cfc-a15f-428d-9390-41916c2fd550"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1") },
                    { new Guid("f9379b68-a12b-4730-91d7-f8b3ab8435c8"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("8e75f834-b4c1-4053-b900-bd66989e0d4b") },
                    { new Guid("fd5ae800-84b5-4cdb-a705-5d4b9da60d98"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("f9a88a4a-0d39-4d4f-9968-87e28aad1805") },
                    { new Guid("fffce197-3705-47a7-9783-6994bbcc4912"), new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0") }
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypeStageSample",
                columns: new[] { "Id", "MaterialStageId", "ProductSubTypeId", "RowNumber", "StageName", "StandartTime" },
                values: new object[,]
                {
                    { new Guid("05acab2d-d004-4a00-b60c-bcb7171248ff"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 2, "---", "24 " },
                    { new Guid("089fda0a-d8d4-409a-95bf-7081a51c05a9"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 2, "---", "24 " },
                    { new Guid("09be01ff-a019-4ab8-9a9d-cc579ce16a6e"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 1, "---", "48 " },
                    { new Guid("17d4705f-84e1-4ba9-a972-445aa73c6106"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 1, "---", "48 " },
                    { new Guid("27b5da07-5256-44fd-b209-7c21e67fc7e5"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 2, "---", "48 " },
                    { new Guid("2bbbf53f-ce59-4b59-b491-6352e6c297a9"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 1, "---", "48 " },
                    { new Guid("2bc9e8e4-ac3e-499e-b5f5-d82ad12d7cfa"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 1, "---", "48 " },
                    { new Guid("3640de89-764a-4ed9-8f9a-c7f883bfb017"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 1, "---", "48 " },
                    { new Guid("39203c27-88d2-4a9a-b62f-79319df29f0d"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 2, "---", "72 " },
                    { new Guid("3d815557-d474-47dc-a583-db17d6f0c5c7"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 2, "---", "72 " },
                    { new Guid("4f507183-3e09-4a18-92c8-31a8a82bc787"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 2, "---", "24 " },
                    { new Guid("5808c83f-27cf-46e3-80b9-2f61f56912c9"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 2, "---", "48 " },
                    { new Guid("65b4b0c7-be67-46d6-bd02-e1c36202fd6a"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 2, "---", "48 " },
                    { new Guid("721f8111-63e5-4209-951d-41d80aae5206"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 1, "---", "48 " },
                    { new Guid("734fe414-0828-404d-a33f-99760e69b8fc"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 1, "---", "48 " },
                    { new Guid("7a6a851c-fc3d-4a1e-ae76-6602a799dfd6"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 1, "---", "48 " },
                    { new Guid("7d1e1aeb-be4d-433c-85f8-d3221aba3e5d"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 2, "---", "24 " },
                    { new Guid("81a1a6f7-7ff0-48f7-bac4-512a16300489"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 2, "---", "96 " },
                    { new Guid("8b129e37-4c8a-490e-83ba-d4a868988774"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 1, "---", "48 " },
                    { new Guid("8e87cb81-5257-4b49-994b-ad4c158ceec4"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 2, "---", "48 " },
                    { new Guid("959c47a9-25fd-464f-bda7-0ae1cd2cb167"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 2, "---", "24 " },
                    { new Guid("98a01cec-1e3b-43f0-9554-3073be88da63"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 2, "---", "24 " },
                    { new Guid("a1867f2b-ec21-4f1c-a7ff-8eca144aa708"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 1, "---", "48 " },
                    { new Guid("a66f8dcb-add5-4dec-a152-016fabc4faad"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 2, "---", "24 " },
                    { new Guid("acb9b76a-cf26-4452-affd-a7b42a9c42c4"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 2, "---", "24 " },
                    { new Guid("b34248b4-1087-4a7a-8ba5-45b7ab769fa8"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 2, "---", "24 " },
                    { new Guid("b3ce376a-4718-4016-831d-da1064b0b9cd"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 1, "---", "48 " },
                    { new Guid("b58d1947-5813-4b45-a44d-5b3a1f686d1e"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 2, "---", "48 " },
                    { new Guid("b85de8df-6e60-493e-8ba4-19a86fb8a78e"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 1, "---", "48 " },
                    { new Guid("b900064c-5648-40b1-bc37-99776445621b"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 2, "---", "72 " },
                    { new Guid("bac73bbb-6b72-46f5-a50c-12f1cabbcba0"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 1, "---", "48 " },
                    { new Guid("bdd5baaf-ade0-409b-b424-84b488e43da7"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 2, "---", "24 " },
                    { new Guid("bf24fb86-c84a-4261-958e-55a7049ce74a"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 2, "---", "48 " },
                    { new Guid("bf8f2d4e-3a84-451a-a8bd-597a6a930243"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 1, "---", "48 " },
                    { new Guid("ce3834b1-fbf5-40f4-9ce6-b68f84b5ad62"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 1, "---", "48 " },
                    { new Guid("d51d9e30-3660-4dd1-bbca-48073552bd38"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 1, "---", "48 " },
                    { new Guid("d5874272-5cb8-493a-bce9-6b12b212dc90"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 1, "---", "48 " },
                    { new Guid("d7def012-2160-4602-a725-9f3ce14b5270"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 1, "---", "48 " },
                    { new Guid("e355ab3e-00a3-417c-a15f-4aa0dce1c514"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 1, "---", "48 " },
                    { new Guid("e6f73c99-40d3-47a6-af10-c18e947c3068"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 1, "---", "48 " },
                    { new Guid("fde9611a-cb91-49ad-b336-07a69ff9cf0d"), new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 2, "---", "24 " },
                    { new Guid("fe0bbca2-ab8b-45d4-9209-b989d213da19"), new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 1, "---", "48 " }
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypeWorkingPeriodSample",
                columns: new[] { "Id", "ProductSubTypeId", "RowNumber", "StandartEmployee", "StandartTime", "WorkingPeriodName" },
                values: new object[,]
                {
                    { new Guid("13ecdd39-21e9-474f-b85f-2ad934ffa167"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 7, 1, "30", "Крепление Ш/У к стойке" },
                    { new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 1, 1, "120", "Формирование труб коллектора" },
                    { new Guid("3b838262-a986-41e9-875e-cd26de2a9a61"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 6, 1, "60", "Сварка стойки для Ш/У" },
                    { new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 5, 1, "180", "Крепление насосов к раме" },
                    { new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, 1, "80", "Формирование рамы" },
                    { new Guid("d21033f7-2d82-4593-8296-aa5fd66fe160"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 4, 1, "60", "Сварка рамы" },
                    { new Guid("db44829e-2df6-4857-bd94-157dfd70dcce"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 2, 1, "180", "Сварка труб коллектора" }
                });

            migrationBuilder.InsertData(
                table: "WorkingPeriodRelation",
                columns: new[] { "Id", "ChildProductSubTypeWorkingPeriodSampleId", "ParentProductSubTypeWorkingPeriodSampleId" },
                values: new object[,]
                {
                    { new Guid("0152e0d8-f6bf-4373-be7f-c16d1d61ad27"), new Guid("13c0b41c-048e-4d0c-b4ad-2e13a99e179d"), new Guid("f6da68f1-7e76-43cf-986c-802945e91d7e") },
                    { new Guid("05ad0daf-35b6-40da-a42b-911c04d968c0"), new Guid("e5b1c92b-a56a-4f77-ac00-1b10b0d9bab2"), new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e") },
                    { new Guid("08016396-82d3-40d0-8a54-d8502dcbf855"), new Guid("f6da68f1-7e76-43cf-986c-802945e91d7e"), new Guid("8397b6bc-3b49-4713-8b9d-3f2c1b0fbb82") },
                    { new Guid("137b5459-b818-407e-9c8c-2989211cf84d"), new Guid("5d618e84-632e-4ebf-a985-88342bae2a00"), new Guid("e7df452e-f592-4d99-ba1e-385068e6a192") },
                    { new Guid("1398e90e-b7a4-4222-b87e-4532405a8388"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b"), new Guid("7fc5f63e-2118-49d7-baea-9b9d3617b736") },
                    { new Guid("13bc1e0a-4b82-42f3-92fd-68a4dfc1939e"), new Guid("fe7f104a-2a93-486e-9ed7-0e8cdb9aeb49"), new Guid("5c7f8886-f1d0-44bd-b6a0-83436dc7d796") },
                    { new Guid("16c41e6b-a86f-4a74-8fe9-a861cb011f27"), new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c"), new Guid("98a18796-c791-41c0-b73f-14e7ff602154") },
                    { new Guid("1a202092-9568-4a1b-85d4-dece3ccc975a"), new Guid("76303263-3782-4d46-b880-c19822006c04"), new Guid("94535c6e-9aac-453a-b685-ca2b99adf6f4") },
                    { new Guid("1a35ecd2-d241-4a1b-ac6c-a4b257238788"), new Guid("4d325a32-7d5b-496a-a312-209df69bdb2c"), new Guid("3e1fc4a8-5a0b-4be5-95f9-4893a64171d3") },
                    { new Guid("1a9d65c3-d74b-4691-82d6-97d3bdace4b8"), new Guid("879de3f9-f723-4be3-a586-2b701a539b29"), new Guid("67eb07a6-7deb-48b0-a9d0-f1968c45cb94") },
                    { new Guid("1b369839-f692-4650-91d0-6ff67e95448d"), new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1"), new Guid("e7df452e-f592-4d99-ba1e-385068e6a192") },
                    { new Guid("1b6f780c-47d8-4032-b928-62293e76d818"), new Guid("874cdded-4702-41e5-8966-040ab54d391b"), new Guid("d5d06f09-da5d-4ba3-b502-0c517a811b13") },
                    { new Guid("1b8bf584-d8bd-4f90-846d-cc5c228a886e"), new Guid("d1ab2e54-dbfc-4399-a21a-2c4aae9b906d"), new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08") },
                    { new Guid("1c5e75f6-ae22-4dc0-9b8a-bf5da517e785"), new Guid("5c2e714b-d3f3-49db-bf3d-c6e676319348"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("26ad3a32-f616-43a2-8f3c-864a8d320468"), new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"), new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2") },
                    { new Guid("271f74ef-e118-44c0-93f8-c21a40caee1d"), new Guid("be22fa22-51fd-4bef-8666-662fed9e980d"), new Guid("42a2c51f-b3ef-44c1-ad30-961279727186") },
                    { new Guid("2815fd89-8027-4de5-bc63-23faa3c27768"), new Guid("8ea09125-f185-4e0b-adda-53297635bbec"), new Guid("5d06464c-3f4b-43c2-9b82-9d61d9d76f44") },
                    { new Guid("2b2b010b-00ba-4a10-bf0f-b680d8b95120"), new Guid("a74d2e07-14ce-4fe4-b789-03c42bd2fcc8"), new Guid("d1ab2e54-dbfc-4399-a21a-2c4aae9b906d") },
                    { new Guid("2b6883df-4351-4b1b-bbe2-0d500c0e32d7"), new Guid("621e09d9-e112-4845-b565-0db2f2426992"), new Guid("9d274b06-dea3-4ccb-b6c4-d6827cf61194") },
                    { new Guid("2bd04434-00b2-44b9-8527-27c4978f3568"), new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88"), new Guid("526a5359-0914-4418-a29c-94151052effd") },
                    { new Guid("2ce2347a-d262-4110-8291-8741af9413d9"), new Guid("621e09d9-e112-4845-b565-0db2f2426992"), new Guid("142b775f-44e3-490b-a593-70b8698c93b5") },
                    { new Guid("2d6170ef-269c-4afc-a9fd-8697d5c21231"), new Guid("dc9553cb-eaf7-4f84-aeed-cd3c6938a158"), new Guid("4f9640f4-2a15-41e9-a917-8300356f4492") },
                    { new Guid("2dbdf34e-ae10-4bf9-891f-64cb724f4083"), new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0"), new Guid("8234c78c-a01e-4a78-8713-981203e05a1d") },
                    { new Guid("2f64f019-1e7f-4ce3-8527-75092602157c"), new Guid("526a5359-0914-4418-a29c-94151052effd"), new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261") },
                    { new Guid("31848483-af56-4483-9ac7-882cdba5c694"), new Guid("f291556f-4885-4ee5-b3e6-2f0c5f504db7"), new Guid("bfa2f600-66b7-4bdd-ab88-2245351fe7aa") },
                    { new Guid("34d00829-f226-4fcd-853d-4866e7d190c3"), new Guid("9412d24f-2f65-492c-8849-f476ceec0b87"), new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c") },
                    { new Guid("3779f516-d147-47b7-abff-4784bd54f7aa"), new Guid("3bf0a6d2-d337-4061-9222-84dc95fc215a"), new Guid("77ccfad7-8078-4fd3-aa6c-b4323ab5778f") },
                    { new Guid("37fbf580-a4a8-421c-8bb6-bbdaca97c9c3"), new Guid("94535c6e-9aac-453a-b685-ca2b99adf6f4"), new Guid("fe7f104a-2a93-486e-9ed7-0e8cdb9aeb49") },
                    { new Guid("380d2f34-830e-4ab1-8bdf-9d9503df6d08"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd"), new Guid("a9385f40-0c5e-4eaa-9b2b-233bc7ebe1ba") },
                    { new Guid("422144d7-25ce-437e-8ff8-6e7c1055b189"), new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"), new Guid("e7224acb-1c27-4833-b22f-298787c65305") },
                    { new Guid("4321a64b-2633-4254-910c-0cbd4bfcfd8f"), new Guid("67eb07a6-7deb-48b0-a9d0-f1968c45cb94"), new Guid("27e28db9-c523-48db-883d-d0ccfa8121c7") },
                    { new Guid("46cf6749-0a31-48d2-9463-2b4827a58c5c"), new Guid("bfa2f600-66b7-4bdd-ab88-2245351fe7aa"), new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d") },
                    { new Guid("4b4c8add-10af-457b-9f78-9325980dc2fc"), new Guid("fc2a1cba-ac3d-4c2b-a48c-b4e35d4e223d"), new Guid("7fdcb535-058e-40b3-a94d-8bde6e7cbbf8") },
                    { new Guid("4c229dd4-06c0-4a77-941f-3736c160629b"), new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb"), new Guid("0068c383-ddcd-46f9-af97-a5490d66b88a") },
                    { new Guid("4c47b4eb-3d13-4cca-a64f-23987101c638"), new Guid("0b1ca170-699b-47aa-b6a0-c73e7166641f"), new Guid("a0aea18f-cb86-4e14-8b4c-ec7f247c30cc") },
                    { new Guid("4e6182c0-247f-464f-a873-cb99a6e6a61d"), new Guid("c89a781b-a303-4164-a737-9a560a84c5f3"), new Guid("d83d1e6b-6d9f-42e6-9029-845408b7849a") },
                    { new Guid("52cabdc4-e82e-4362-b7af-51278cd0fbce"), new Guid("4b9095f8-5d68-4440-8558-1d52c7391a42"), new Guid("173177f4-e827-4098-ac98-cf621cfc9804") },
                    { new Guid("53798e78-af1c-4b37-be05-b6c4905cf254"), new Guid("04498db6-fe2c-4228-9065-4b0341dbd892"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("56deeed9-3742-42d2-926a-ff2e337f17d8"), new Guid("a30f3e81-db5b-419c-80b2-ffc87cffec56"), new Guid("9821c143-c77b-4e52-be86-a3bc96b0b084") },
                    { new Guid("573bd3b1-8a99-4144-8b06-bf8722b3521c"), new Guid("8d603740-7fc0-4e94-b39e-3668dff81a9e"), new Guid("879de3f9-f723-4be3-a586-2b701a539b29") },
                    { new Guid("5b1cc73a-5f7d-40c7-8914-b0271c4438f4"), new Guid("be22fa22-51fd-4bef-8666-662fed9e980d"), new Guid("04498db6-fe2c-4228-9065-4b0341dbd892") },
                    { new Guid("5b729243-aa20-409c-b1a5-d880cf27493e"), new Guid("4d325a32-7d5b-496a-a312-209df69bdb2c"), new Guid("267a492a-2d55-4a33-9bb8-d3076bea8e1d") },
                    { new Guid("5ba5f7e7-23c7-4be6-937e-0d5c3bb69b8d"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89"), new Guid("9569d494-3594-4f37-bc8a-feeb7cdb234f") },
                    { new Guid("5c6502a3-d3d9-4f98-b57c-9bec6f6a666b"), new Guid("9d274b06-dea3-4ccb-b6c4-d6827cf61194"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("5d44abed-827e-4313-8a6f-4c812b552719"), new Guid("81e13934-f2c3-453b-ad90-25e296b0fef1"), new Guid("0b252a71-179e-4e31-8698-19ac4357050e") },
                    { new Guid("5e4d635e-068a-426e-883c-e1609deb990e"), new Guid("7484d536-2104-4e11-a322-ce4c6e0b35a0"), new Guid("13c0b41c-048e-4d0c-b4ad-2e13a99e179d") },
                    { new Guid("614c4932-6476-4ff0-a69c-4fbff81c2a05"), new Guid("d83d1e6b-6d9f-42e6-9029-845408b7849a"), new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797") },
                    { new Guid("63660f64-5ccb-434d-af34-f93b4a13b8a6"), new Guid("1c2a76d1-285b-4a89-954c-3738305288e8"), new Guid("5c2e714b-d3f3-49db-bf3d-c6e676319348") },
                    { new Guid("64c7974e-6468-4a91-82b3-88372a4d9973"), new Guid("ce69ebc6-8f65-4f84-82d8-92fc1b3e3030"), new Guid("7ce6529f-2e9f-4d90-8cc1-d4103587a64e") },
                    { new Guid("696ce1e3-d314-41a6-8cab-b849398aa17a"), new Guid("8e75f834-b4c1-4053-b900-bd66989e0d4b"), new Guid("7c9f77a1-5ebf-4ed6-b3ee-9dc1aef00dc9") },
                    { new Guid("6c9ff42d-bff5-4fc9-86cc-e3736d0bea94"), new Guid("a9385f40-0c5e-4eaa-9b2b-233bc7ebe1ba"), new Guid("dc9553cb-eaf7-4f84-aeed-cd3c6938a158") },
                    { new Guid("6f581c7f-519f-4cd8-bdc1-6f4c8c7eeb1a"), new Guid("65f7f254-32e3-47ca-931a-81f705a449e6"), new Guid("3d0aa127-4111-471b-be31-2968e736a4af") },
                    { new Guid("7608c35b-4c68-4c99-a2e7-1698ce392c66"), new Guid("8e4d0088-6115-4b6f-b7ef-d413506538eb"), new Guid("ce3b68c8-2180-4f16-b9aa-eada2ba6f69f") },
                    { new Guid("76cd2f43-2b12-4ea2-8163-5c15c2d5f80b"), new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c"), new Guid("9412d24f-2f65-492c-8849-f476ceec0b87") },
                    { new Guid("76d36a23-1e29-4aff-a0d6-561e02f2b28c"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520"), new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c") },
                    { new Guid("78d4c13a-c222-4e7d-acfd-d2df6cd03f88"), new Guid("e7df452e-f592-4d99-ba1e-385068e6a192"), new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d") },
                    { new Guid("7a7daa3f-9f3a-4a45-a2cd-ea79599dd996"), new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610"), new Guid("8e4d0088-6115-4b6f-b7ef-d413506538eb") },
                    { new Guid("7b957a19-99c3-4829-8017-67002848495e"), new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261"), new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797") },
                    { new Guid("7e171e4f-0c32-4e7b-8780-3d809b9430a1"), new Guid("8397b6bc-3b49-4713-8b9d-3f2c1b0fbb82"), new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe") },
                    { new Guid("7e68fd61-f88b-4d5c-87c7-dd92fe17cec9"), new Guid("0068c383-ddcd-46f9-af97-a5490d66b88a"), new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958") },
                    { new Guid("80dac4c5-ab17-42fe-8a48-7a08154801c4"), new Guid("9569d494-3594-4f37-bc8a-feeb7cdb234f"), new Guid("3c932b0e-6668-4512-9633-fb2472bce36e") },
                    { new Guid("83ca605e-9374-44c3-9af0-42895e362698"), new Guid("c8ff7225-d396-44eb-956f-6b9d2acbbb46"), new Guid("b5bc2d06-302c-4ada-9d2c-7c64493d51e6") },
                    { new Guid("869d04af-eca2-4398-af82-d3db53b92a42"), new Guid("8ea09125-f185-4e0b-adda-53297635bbec"), new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d") },
                    { new Guid("87d52e84-2420-4c34-86e7-730d10cae0fd"), new Guid("62f2a48e-af3f-4fc3-8dbe-e1ff5a434646"), new Guid("46fd1905-67cd-4ab4-b242-a924322db5cc") },
                    { new Guid("8adfa5a3-2e99-40ca-b42d-bfeb72a20d7a"), new Guid("5576f277-d9d1-4d9b-93e3-9288a4aff46a"), new Guid("ce69ebc6-8f65-4f84-82d8-92fc1b3e3030") },
                    { new Guid("8bb1d7fb-dc08-47c9-a88b-2e1007b5141b"), new Guid("50a3a8be-8720-4a3e-a780-23f16a642350"), new Guid("e5fc984b-a598-4d67-836e-5dcfe975ca3f") },
                    { new Guid("8c1f4c47-7c21-429f-a70a-0d8d99fb612f"), new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d"), new Guid("faca59eb-4f95-4a46-aa76-94a0bb571523") },
                    { new Guid("8c565dd0-2801-4650-aa77-a1876f460944"), new Guid("5d06464c-3f4b-43c2-9b82-9d61d9d76f44"), new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40") },
                    { new Guid("8cf606a2-7b98-4523-8d93-c6fd87f3fff6"), new Guid("7c9f77a1-5ebf-4ed6-b3ee-9dc1aef00dc9"), new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb") },
                    { new Guid("8d60b97c-c931-48b5-8763-26db0501b206"), new Guid("42a2c51f-b3ef-44c1-ad30-961279727186"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd") },
                    { new Guid("8fb03127-e4ce-4425-8c2a-77c1c8577d61"), new Guid("135f6269-66f7-4ba7-9c48-18c2b03ecf3b"), new Guid("526a5359-0914-4418-a29c-94151052effd") },
                    { new Guid("9179850d-6e75-4e01-b81b-235e20929ac3"), new Guid("c89a781b-a303-4164-a737-9a560a84c5f3"), new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261") },
                    { new Guid("9195ef09-d3cf-443f-8640-c829492e3c1e"), new Guid("267a492a-2d55-4a33-9bb8-d3076bea8e1d"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("926e196e-cbe7-40c2-a8e7-bfd8800988c7"), new Guid("89347670-f793-486c-b128-8f7a2f515ef0"), new Guid("62f2a48e-af3f-4fc3-8dbe-e1ff5a434646") },
                    { new Guid("949e045b-32c8-4d2b-9675-50f36055b840"), new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d"), new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40") },
                    { new Guid("98ade418-b4f6-4c8f-bf67-3c5be2fcde21"), new Guid("875bbfba-88c8-454f-903a-6746583208f1"), new Guid("783c2ca2-5a56-4130-8d02-ec3cc86c0c31") },
                    { new Guid("99deca26-b4d6-4067-be64-984a1629046c"), new Guid("98a18796-c791-41c0-b73f-14e7ff602154"), new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39") },
                    { new Guid("9cc921dc-d16c-4395-8d11-56958a432806"), new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958"), new Guid("e5b1c92b-a56a-4f77-ac00-1b10b0d9bab2") },
                    { new Guid("9cef5f6a-b3af-48d2-b184-3f70f522f5e3"), new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2"), new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c") },
                    { new Guid("9e9ff025-01c0-4446-a1ab-7749ff25814d"), new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520") },
                    { new Guid("9f9183b4-c607-4749-baa2-ce942f444474"), new Guid("d5d06f09-da5d-4ba3-b502-0c517a811b13"), new Guid("8bb0c743-e355-45b1-88e6-0fada09890f6") },
                    { new Guid("a282bf3f-3b2a-470d-9a54-11dd405dcf48"), new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457"), new Guid("4b9095f8-5d68-4440-8558-1d52c7391a42") },
                    { new Guid("a4fb9e2a-805f-427b-b94b-741487c4dc1c"), new Guid("e5fc984b-a598-4d67-836e-5dcfe975ca3f"), new Guid("c8ff7225-d396-44eb-956f-6b9d2acbbb46") },
                    { new Guid("a9171d24-3bce-48ab-a23f-5211a6614a4c"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897"), new Guid("875bbfba-88c8-454f-903a-6746583208f1") },
                    { new Guid("ab387c50-1a34-47eb-9263-bbd005c63885"), new Guid("9821c143-c77b-4e52-be86-a3bc96b0b084"), new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c") },
                    { new Guid("aca93872-ae15-409c-9863-7d91568b0238"), new Guid("474890d4-4172-434b-a950-de96eb1808ce"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b") },
                    { new Guid("b224fd3e-f1fd-4652-a522-b5e1661ec9b3"), new Guid("faca59eb-4f95-4a46-aa76-94a0bb571523"), new Guid("533662ef-436c-4743-a872-f4c5d17418bf") },
                    { new Guid("b62857ad-d2d3-4ac4-bb51-8a1f0a4b10be"), new Guid("77ccfad7-8078-4fd3-aa6c-b4323ab5778f"), new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610") },
                    { new Guid("bd095a91-842b-4783-ae87-433284b33eaa"), new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c"), new Guid("65f7f254-32e3-47ca-931a-81f705a449e6") },
                    { new Guid("bf0eee35-d9c8-4b07-bc6e-c4dc87156f0e"), new Guid("783c2ca2-5a56-4130-8d02-ec3cc86c0c31"), new Guid("6bd776c7-8727-4acd-873a-8356a86b3071") },
                    { new Guid("c8b45d01-9308-4c01-9be2-2538bafc7590"), new Guid("a007ef54-77f0-4308-99a9-35d3f65ea262"), new Guid("875bbfba-88c8-454f-903a-6746583208f1") },
                    { new Guid("cc729f5d-b371-4d90-a4e4-b38e9d88881e"), new Guid("3e1fc4a8-5a0b-4be5-95f9-4893a64171d3"), new Guid("76303263-3782-4d46-b880-c19822006c04") },
                    { new Guid("cf428431-64d5-4245-a2d7-9d0d275ba5a3"), new Guid("a0aea18f-cb86-4e14-8b4c-ec7f247c30cc"), new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0") },
                    { new Guid("d0a40867-fc22-4cfb-8441-3c367ea46999"), new Guid("1c2a76d1-285b-4a89-954c-3738305288e8"), new Guid("474890d4-4172-434b-a950-de96eb1808ce") },
                    { new Guid("d10e984d-7454-45df-9db5-3e69fa1ae936"), new Guid("7ce6529f-2e9f-4d90-8cc1-d4103587a64e"), new Guid("66b8f43f-a427-4e21-ab1e-db2941306f52") },
                    { new Guid("d3c6cdc0-fd62-47af-9826-eaafe4c55acb"), new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af"), new Guid("875bbfba-88c8-454f-903a-6746583208f1") },
                    { new Guid("d3cab1d9-c866-4c13-b928-7f2a3b259261"), new Guid("2e736ab4-400c-4c59-a496-f6f748259f54"), new Guid("874cdded-4702-41e5-8966-040ab54d391b") },
                    { new Guid("d7a7a830-994b-4a01-b815-9ff894ae45da"), new Guid("0b252a71-179e-4e31-8698-19ac4357050e"), new Guid("a74d2e07-14ce-4fe4-b789-03c42bd2fcc8") },
                    { new Guid("d809eab0-9b7f-49b7-b02b-01c76c588d59"), new Guid("66b8f43f-a427-4e21-ab1e-db2941306f52"), new Guid("9bd2a371-8bad-423f-8dc9-d4c427ff1914") },
                    { new Guid("dda1ce29-6d10-45ed-9587-710a9e5583cf"), new Guid("d3cf379a-9eb2-4892-b43a-089e0bb03431"), new Guid("8d603740-7fc0-4e94-b39e-3668dff81a9e") },
                    { new Guid("ddecc927-bdeb-4af6-b243-212fd1487037"), new Guid("7fc5f63e-2118-49d7-baea-9b9d3617b736"), new Guid("fc2a1cba-ac3d-4c2b-a48c-b4e35d4e223d") },
                    { new Guid("de7cefc6-38c8-4f2c-a163-be06eae4d73d"), new Guid("3c932b0e-6668-4512-9633-fb2472bce36e"), new Guid("762c4c6f-9015-4fdf-bcd8-4e27b105fbec") },
                    { new Guid("debbfb13-1314-4ff9-ae58-c1647ae9493e"), new Guid("e7224acb-1c27-4833-b22f-298787c65305"), new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c") },
                    { new Guid("df95414c-681a-40df-bc92-fcd19f47afd9"), new Guid("f8a7550f-8a1a-4d34-8fa3-c69d15772c16"), new Guid("b5bc2d06-302c-4ada-9d2c-7c64493d51e6") },
                    { new Guid("e375886f-0529-438e-84ff-9688d9db4ce4"), new Guid("142b775f-44e3-490b-a593-70b8698c93b5"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89") },
                    { new Guid("e7fd78c4-7221-47d7-8e34-b5f67af57aa8"), new Guid("2cf2af17-da41-4eb0-ac8d-ccb34fa708e6"), new Guid("0b1ca170-699b-47aa-b6a0-c73e7166641f") },
                    { new Guid("e83b901b-92cc-4f29-87e8-f686652707b4"), new Guid("a13286a4-eb36-4480-89af-0f190977231b"), new Guid("27e28db9-c523-48db-883d-d0ccfa8121c7") },
                    { new Guid("e89f6904-eaaf-4fe2-b6f9-ba863bdb11eb"), new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"), new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af") },
                    { new Guid("ecd4f9ae-e71d-480f-987d-207513952f74"), new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"), new Guid("a007ef54-77f0-4308-99a9-35d3f65ea262") },
                    { new Guid("f1497535-846b-45ac-8b87-1b2845ba8dc9"), new Guid("8bb0c743-e355-45b1-88e6-0fada09890f6"), new Guid("d52103d7-f93c-4a76-9914-3b480276ad9f") },
                    { new Guid("f2d585ba-f4f4-49d1-a531-59b2bb949861"), new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897") },
                    { new Guid("fdf6a6b1-66c0-4cb2-84de-36bc4d016404"), new Guid("8234c78c-a01e-4a78-8713-981203e05a1d"), new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457") }
                });

            migrationBuilder.InsertData(
                table: "WorkingPeriodStageTypeRelation",
                columns: new[] { "Id", "ProductSubTypeWorkingPeriodSampleId", "StageTypeId" },
                values: new object[,]
                {
                    { new Guid("03a2dddd-85fb-4e1c-94d8-3741031925bc"), new Guid("faca59eb-4f95-4a46-aa76-94a0bb571523"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("03c26172-c992-463a-99ce-f2c21473a193"), new Guid("0b252a71-179e-4e31-8698-19ac4357050e"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("03dab76a-3478-48e2-a765-edb838d19233"), new Guid("6bd776c7-8727-4acd-873a-8356a86b3071"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("0851193f-5c58-4750-8ebe-c42d4f09ca95"), new Guid("50a3a8be-8720-4a3e-a780-23f16a642350"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("0d6e05f1-f98e-48f1-91ff-e6d30e6428a3"), new Guid("0068c383-ddcd-46f9-af97-a5490d66b88a"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("0f8a68e2-6f33-4730-84b2-e5004a1f4242"), new Guid("875bbfba-88c8-454f-903a-6746583208f1"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("1001337b-9211-478f-b9bf-5beb8b049602"), new Guid("f8a7550f-8a1a-4d34-8fa3-c69d15772c16"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("1001399d-778d-4b41-9f6c-022b27ce2252"), new Guid("a9385f40-0c5e-4eaa-9b2b-233bc7ebe1ba"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("10d3bc58-ef0c-4714-abf8-a1778ad27d77"), new Guid("173177f4-e827-4098-ac98-cf621cfc9804"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("1286f4ff-2f4f-4d90-9595-f1f73fa1eb9f"), new Guid("be22fa22-51fd-4bef-8666-662fed9e980d"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("14723694-ef35-48ca-8585-c24ba9854200"), new Guid("3193c06a-a1f8-42c5-abcd-c30fee6612b5"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("16173999-b3f6-42bf-b14a-e15cad238f38"), new Guid("3e1fc4a8-5a0b-4be5-95f9-4893a64171d3"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("19befd5f-a981-41ad-90c9-6d4b730f3d8a"), new Guid("d1ab2e54-dbfc-4399-a21a-2c4aae9b906d"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("1b2fded8-38ac-4855-99a9-178063b6ef3a"), new Guid("f291556f-4885-4ee5-b3e6-2f0c5f504db7"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("1f608adf-4af5-41c8-abfb-acbaa0c9409b"), new Guid("4b9095f8-5d68-4440-8558-1d52c7391a42"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("1fe2d979-b331-4630-ae49-04b42d90748f"), new Guid("d52103d7-f93c-4a76-9914-3b480276ad9f"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("2081739b-8b76-4bb5-935f-47c191d1db2b"), new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("22ec1f17-2e0e-4f85-bd7b-aa3591649cd1"), new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("24f6994c-f50e-460f-8036-43be3fdb3d4b"), new Guid("d5d06f09-da5d-4ba3-b502-0c517a811b13"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("25b15cdf-dfa9-47d6-8b85-93d9d902d899"), new Guid("e8973d21-c07a-44b5-9fb7-e8250797bc8d"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("27ce7b6f-5dd2-47ce-8372-d928defa62d0"), new Guid("5c2e714b-d3f3-49db-bf3d-c6e676319348"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("2ac6f0b9-5972-4969-9e88-a059b615e7c2"), new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("2b4d392b-ad18-470f-bb52-34ab00946d5c"), new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("2d08cedb-2dd3-4542-b852-f4a363440aeb"), new Guid("5c7f8886-f1d0-44bd-b6a0-83436dc7d796"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("2d48abd9-5c4a-4076-8535-170365fb947b"), new Guid("fc2a1cba-ac3d-4c2b-a48c-b4e35d4e223d"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("326dbe72-728e-4385-82d1-1d09b41af17a"), new Guid("9412d24f-2f65-492c-8849-f476ceec0b87"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("38686d72-cec9-4590-9e87-63791437c982"), new Guid("d3cf379a-9eb2-4892-b43a-089e0bb03431"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("38837865-cb3c-4088-a551-f0ae6144aa78"), new Guid("ce69ebc6-8f65-4f84-82d8-92fc1b3e3030"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("3df7f6d2-18fe-47b6-945d-140e7b5541b3"), new Guid("a13286a4-eb36-4480-89af-0f190977231b"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("3e525d3f-3912-440c-81fc-908bc28a3570"), new Guid("8e4d0088-6115-4b6f-b7ef-d413506538eb"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("407d01df-2671-4a70-aec6-b0a5bb883509"), new Guid("67eb07a6-7deb-48b0-a9d0-f1968c45cb94"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("419cdf4d-ddec-48b7-9536-e0bee0748406"), new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("41bf76c9-4351-4e9a-a1ab-77c1ef4de217"), new Guid("9d274b06-dea3-4ccb-b6c4-d6827cf61194"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("42f249f4-d5eb-45a3-8b87-47b008ea0afd"), new Guid("8234c78c-a01e-4a78-8713-981203e05a1d"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("44359239-5f8d-4790-9f31-afa86ade4374"), new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("48a25548-14ca-4a3b-aebb-29aef90d3683"), new Guid("474890d4-4172-434b-a950-de96eb1808ce"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("499d10bf-4229-47a0-8013-ddc817d8fd4e"), new Guid("7fc5f63e-2118-49d7-baea-9b9d3617b736"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("4a45e387-dbf1-4264-9961-8875d0a021ea"), new Guid("4f9640f4-2a15-41e9-a917-8300356f4492"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("4a474502-c336-41b2-aa59-16f84d82860f"), new Guid("89347670-f793-486c-b128-8f7a2f515ef0"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("4b35a069-bc87-46b9-bb3d-0e3dfc0672eb"), new Guid("a74d2e07-14ce-4fe4-b789-03c42bd2fcc8"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("4c24bb51-9bec-4e53-b9d0-b94551985a2f"), new Guid("77ccfad7-8078-4fd3-aa6c-b4323ab5778f"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("4f74f1ba-522a-4e98-8721-8f3e01de5ce3"), new Guid("a007ef54-77f0-4308-99a9-35d3f65ea262"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("502b94f1-c258-4497-9015-4ee165b240ff"), new Guid("4d325a32-7d5b-496a-a312-209df69bdb2c"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("52734a65-8c94-479c-b53b-f4e954bcd449"), new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("53138e86-3e72-40e4-958b-43bf1ab07bab"), new Guid("a30f3e81-db5b-419c-80b2-ffc87cffec56"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("5331c10f-45fc-4042-8207-37a11a7ced8a"), new Guid("e5fc984b-a598-4d67-836e-5dcfe975ca3f"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("53b61fdd-e3ae-40d8-b25f-05aaba19dda7"), new Guid("3d0aa127-4111-471b-be31-2968e736a4af"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("54111ded-87c1-4e5b-995f-557ad3255eaa"), new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("56d6390d-4f14-4bb7-9632-f4b9ffcc9fd8"), new Guid("76303263-3782-4d46-b880-c19822006c04"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("58d4cb0f-f877-4b34-93cd-c0f6477590f5"), new Guid("2e736ab4-400c-4c59-a496-f6f748259f54"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("5b1d938f-5575-45a1-aad1-155a252ee7c4"), new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("5b7e0656-1fee-4d9f-b874-d9104d55d6c1"), new Guid("fe7f104a-2a93-486e-9ed7-0e8cdb9aeb49"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("5bc76299-f2dd-4372-94e2-538a76d6a268"), new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("5c0c8437-2169-4666-9787-2bfa56562dc3"), new Guid("8d603740-7fc0-4e94-b39e-3668dff81a9e"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("5c8e3936-6b8d-4b36-8538-8199396a0ef5"), new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("5d4fedc7-edac-4206-b453-9ea2280f39f7"), new Guid("f6da68f1-7e76-43cf-986c-802945e91d7e"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("5d992bec-be3d-49da-9d22-92794a128ad1"), new Guid("bfa2f600-66b7-4bdd-ab88-2245351fe7aa"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("614b4461-c4fb-41bd-91f9-45b3b98f0dd3"), new Guid("d83d1e6b-6d9f-42e6-9029-845408b7849a"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("64765ee4-d372-41ac-af9f-8eac0dff5a71"), new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("64cb9299-537c-497b-b46a-b2c6b8080bff"), new Guid("c89a781b-a303-4164-a737-9a560a84c5f3"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("653bf6e3-27ca-4a4f-baba-acc91a894d23"), new Guid("ce3b68c8-2180-4f16-b9aa-eada2ba6f69f"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("65579eca-02b7-4649-ae84-f44b9230c6a7"), new Guid("9821c143-c77b-4e52-be86-a3bc96b0b084"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("673cff01-eabf-4eba-bac7-6896cb3cb550"), new Guid("42a2c51f-b3ef-44c1-ad30-961279727186"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("677ff94b-0895-428d-9ed4-05eb857332db"), new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("6eab99a0-0f83-41ab-b903-51b717178a45"), new Guid("c8ff7225-d396-44eb-956f-6b9d2acbbb46"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("70db30a4-b6c6-42f1-81f0-de1e4b57e392"), new Guid("135f6269-66f7-4ba7-9c48-18c2b03ecf3b"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("71031a6c-481d-43df-a1dd-a57a8cd85d01"), new Guid("a0aea18f-cb86-4e14-8b4c-ec7f247c30cc"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("71d2a0ad-ea4c-4928-af77-e0afd08af9bc"), new Guid("081e2d98-a604-44f2-96e6-1cd486e5db2f"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("73c85496-c294-46a1-8d83-0effa0f4227d"), new Guid("8397b6bc-3b49-4713-8b9d-3f2c1b0fbb82"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("759c5f65-ec28-439a-8c7e-e912c38f3593"), new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("78101b64-14ae-4dd6-8bd8-8108b9b135de"), new Guid("533662ef-436c-4743-a872-f4c5d17418bf"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("7a3c46c8-2f1f-45cb-8364-86f99a92a1dc"), new Guid("8ea09125-f185-4e0b-adda-53297635bbec"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("7f5ebf16-4b08-403c-8e17-c83ec4c6de0b"), new Guid("267a492a-2d55-4a33-9bb8-d3076bea8e1d"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("7f82fc84-d3e6-4674-bc93-bc0b1cb4f726"), new Guid("874cdded-4702-41e5-8966-040ab54d391b"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("814869e9-7c0b-422d-8a1d-0ee5753db2eb"), new Guid("5d06464c-3f4b-43c2-9b82-9d61d9d76f44"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("829b4c5e-e93c-4594-a7a9-830180ce663c"), new Guid("f9a88a4a-0d39-4d4f-9968-87e28aad1805"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("8355040d-0d8f-4afd-91f1-779eb102bcd8"), new Guid("5d618e84-632e-4ebf-a985-88342bae2a00"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("857ab8e2-2950-4481-9138-742c19436713"), new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("893d9749-3762-4830-8d56-4f3426aafa50"), new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("8b5a7977-c423-4257-8719-3b98018ba4d1"), new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("8dbf6a0e-c3d3-4699-93a3-5326f827269f"), new Guid("98a18796-c791-41c0-b73f-14e7ff602154"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("8f7d67f6-46f9-4e0f-8fd1-e9fc844fbdb2"), new Guid("7484d536-2104-4e11-a322-ce4c6e0b35a0"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("8fded863-916f-42d2-969b-3190f76c1068"), new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("93475e9b-030f-401b-83d0-8b86bbd8717a"), new Guid("b5bc2d06-302c-4ada-9d2c-7c64493d51e6"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("951c6b38-f2b2-401b-885e-f594e2ab9263"), new Guid("8bb0c743-e355-45b1-88e6-0fada09890f6"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("986fde90-ee0e-4bff-91f6-0e5cc2f4c50b"), new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("997f97fe-a645-4e2b-ac24-a493fe276fa8"), new Guid("621e09d9-e112-4845-b565-0db2f2426992"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("9b7fac00-3778-4d05-9ed2-952f5a8c9da8"), new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("9f48a4aa-a76f-490f-bb92-3523050e4250"), new Guid("e5b1c92b-a56a-4f77-ac00-1b10b0d9bab2"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("9fd568f1-1f13-424d-8243-a8ff2fe769f4"), new Guid("5efd0f05-e595-4a2f-94b3-2347f94eecc7"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("a12d2864-1293-4030-a1d9-86e15ca03df7"), new Guid("94535c6e-9aac-453a-b685-ca2b99adf6f4"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("a1714eb2-ae6a-4b34-a467-a09049efd953"), new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("a8acf1c7-1cba-4738-b04d-1c730e616e9d"), new Guid("9bd2a371-8bad-423f-8dc9-d4c427ff1914"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("ac741d81-c7f9-4623-8e60-8dff5ffd392c"), new Guid("46fd1905-67cd-4ab4-b242-a924322db5cc"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("ae06ac91-1d39-4102-af01-e04327171207"), new Guid("81e13934-f2c3-453b-ad90-25e296b0fef1"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("ae3ab933-3b7c-4513-bfad-c411272e7371"), new Guid("dc9553cb-eaf7-4f84-aeed-cd3c6938a158"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("af70ccc0-c309-4d93-83f8-12f174cdd4ba"), new Guid("3bf0a6d2-d337-4061-9222-84dc95fc215a"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("afe5b77a-fbac-44eb-b71c-808e5ccc2df8"), new Guid("04498db6-fe2c-4228-9065-4b0341dbd892"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("b26a1b8e-c391-4e0f-b69e-3be4a49f4fbd"), new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("bcbf1b23-bfcf-41a2-a7c2-d7f832de9911"), new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("c050c652-bca6-4dc3-850d-5c86c2f56859"), new Guid("8e75f834-b4c1-4053-b900-bd66989e0d4b"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("c06eb19f-39a8-4f34-8ed8-78a3a40eb00b"), new Guid("e7df452e-f592-4d99-ba1e-385068e6a192"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("c5ae394c-db0d-4f56-a650-b48559637e8c"), new Guid("13c0b41c-048e-4d0c-b4ad-2e13a99e179d"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("c94c9543-c48a-4b2e-9ba9-7b171a1280e3"), new Guid("142b775f-44e3-490b-a593-70b8698c93b5"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("c98dbc4c-ed44-49c6-8b95-ae114197bbbf"), new Guid("783c2ca2-5a56-4130-8d02-ec3cc86c0c31"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("d37f49a5-ca5f-4db9-af60-1efee3029715"), new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("d5498780-f64b-4407-8f32-352addecf420"), new Guid("a7b4ab35-169d-4d41-a052-908482eef14a"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("d64ee27e-f2b4-4768-bf06-f7a8aebfc4df"), new Guid("66b8f43f-a427-4e21-ab1e-db2941306f52"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("d6a02f6b-804f-4fd4-b64f-674932b52044"), new Guid("7fdcb535-058e-40b3-a94d-8bde6e7cbbf8"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("d9025ddc-40c6-4195-b105-dc114d57c0b3"), new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("db2514e8-625c-45d3-9831-108fb517f8a0"), new Guid("1c2a76d1-285b-4a89-954c-3738305288e8"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("dfb08bae-c5dd-42c8-ba89-4e6d5e31a1aa"), new Guid("762c4c6f-9015-4fdf-bcd8-4e27b105fbec"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("dfee6c05-c220-4cda-8bf1-a3d509591f7e"), new Guid("9569d494-3594-4f37-bc8a-feeb7cdb234f"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") },
                    { new Guid("e111f3d8-1750-4a17-b8e4-9261d9a329a8"), new Guid("7c9f77a1-5ebf-4ed6-b3ee-9dc1aef00dc9"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("e193be8a-cefc-44f3-bf91-7bd9ba42043a"), new Guid("e7224acb-1c27-4833-b22f-298787c65305"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("e1f1e2cc-a2fe-44ad-ad0f-db73ef5a80f6"), new Guid("2cf2af17-da41-4eb0-ac8d-ccb34fa708e6"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("e3655df2-f4a1-47b7-beab-d4be49fc116d"), new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("e47b5c05-4309-4b37-ab26-a59d623b4d38"), new Guid("5576f277-d9d1-4d9b-93e3-9288a4aff46a"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("e589c35a-220c-455a-8e3a-04d7e9ba6115"), new Guid("62f2a48e-af3f-4fc3-8dbe-e1ff5a434646"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("e6dd58a9-e934-4b83-883c-347b368350a6"), new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("ec5fa1c2-d3d0-4e39-b15c-d88bfdd5ed69"), new Guid("7ce6529f-2e9f-4d90-8cc1-d4103587a64e"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("ef8f98b3-1aaf-416e-ae1f-c8c404ebf4c2"), new Guid("3c932b0e-6668-4512-9633-fb2472bce36e"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("f1486fba-242f-4091-a29a-8fc21270787b"), new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("f1d53854-d7d5-477b-86b3-3599404b252a"), new Guid("526a5359-0914-4418-a29c-94151052effd"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("f73c89db-f25e-4891-be57-6a96d30973af"), new Guid("65f7f254-32e3-47ca-931a-81f705a449e6"), new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb") },
                    { new Guid("f7c30cbc-3d8a-4210-9a2e-f8a15c9ee042"), new Guid("27e28db9-c523-48db-883d-d0ccfa8121c7"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("fa47e298-a648-42fa-ade0-9a2f896f9917"), new Guid("0b1ca170-699b-47aa-b6a0-c73e7166641f"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("fc70e5c0-7aeb-4746-ba08-1a2b7c522d27"), new Guid("879de3f9-f723-4be3-a586-2b701a539b29"), new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c") }
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypeGroupMaterialRelation",
                columns: new[] { "Id", "GroupMaterialId", "ProductSubTypeWorkingPeriodSampleId" },
                values: new object[,]
                {
                    { new Guid("0bd59fa0-ea0f-442b-8ead-24be6fbe3d61"), new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"), new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c") },
                    { new Guid("0c5c5b13-2caf-4f56-9dd5-d6312d3d0b5c"), new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"), new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a") },
                    { new Guid("0d8ecb71-7fb0-4805-b55b-7e03e4d3265e"), new Guid("99efee09-84ad-446c-b154-2c18de67adf4"), new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a") },
                    { new Guid("1a26a770-bf1b-4975-a32a-6bb4b5396a26"), new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"), new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24") },
                    { new Guid("351fd367-f11a-4c09-bdcc-eb0355472d86"), new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"), new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a") },
                    { new Guid("47df4222-9391-4962-b94f-c23047d5f48c"), new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"), new Guid("13ecdd39-21e9-474f-b85f-2ad934ffa167") },
                    { new Guid("67267f6e-daa9-4d65-95de-0cd8f392a413"), new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"), new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c") }
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypeStageSample",
                columns: new[] { "Id", "MaterialStageId", "ProductSubTypeId", "RowNumber", "StageName", "StandartTime" },
                values: new object[,]
                {
                    { new Guid("00f08023-773b-4b22-a247-814a4e7b1eab"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("dee59352-bf5e-4901-8912-88a7afa0aea8"), 3, "---", "48 " },
                    { new Guid("0664aa30-a3ae-4359-ba0a-0c8c89110018"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "72 " },
                    { new Guid("1b28a283-9d1a-4bdf-aeb6-c2b078eaadac"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, "---", "120 " },
                    { new Guid("1dcb1c09-ef58-43f0-9bcf-edb6c043e1a1"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "48 " },
                    { new Guid("200cb4a3-a254-4479-90d7-a4ef1ac96cdd"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("b7ef5558-ba1c-4a3d-bd53-ebb961d189c4"), 3, "---", "48 " },
                    { new Guid("21678816-bb48-41c6-93b9-1432c6d0c859"), new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, "---", "336 " },
                    { new Guid("230aa190-5b4b-498f-976e-501d14c9b674"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, "---", "24 " },
                    { new Guid("2400e059-d044-4565-b293-c511759e8cb7"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, "---", "24 " },
                    { new Guid("26ea2952-b604-4aee-8d73-884540641793"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("41bfadaa-08ee-4a09-ab66-4c53d72c9959"), 3, "---", "96 " },
                    { new Guid("29ea4a1a-835b-4d7e-a9b9-189e07cf91be"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, "---", "48 " },
                    { new Guid("2c3ce88d-9140-4178-bacc-a45a64df6fd6"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "48 " },
                    { new Guid("2e923623-2b28-42a5-b53b-a259c3156351"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "24 " },
                    { new Guid("301e9c98-c816-4829-874e-c79a810e97f5"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 3, "---", "120 " },
                    { new Guid("306cc8c5-e66c-47ec-9889-bb5b2734e5ad"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, "---", "24 " },
                    { new Guid("35ce056b-6c5f-4ed6-9c42-a59b80ae6d70"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, "---", "72 " },
                    { new Guid("39713655-26b1-4823-b130-766ce4c626c3"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "120 " },
                    { new Guid("3dbdb78b-a68c-4680-a7f5-82f3960ca2e4"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, "---", "72 " },
                    { new Guid("3fc7b91a-2e97-4fc9-b46c-bac641c96a64"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("1d0fca15-a4b0-4878-85b8-40ce384a7777"), 3, "---", "48 " },
                    { new Guid("43f58187-e6a1-4bcf-ae7b-3e3938cdc9e7"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 3, "---", "48 " },
                    { new Guid("45ef92d6-043f-4a1a-99c6-bcd37237d513"), new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "48 " },
                    { new Guid("4c83a1df-489b-4395-b89e-f05ac875780d"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "24 " },
                    { new Guid("4cbeb4b4-e809-40d8-be70-65830baa7486"), new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, "---", "288 " },
                    { new Guid("574b365b-34ea-4138-9faf-c6cee68952e6"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "24 " },
                    { new Guid("5a61a6aa-b1fc-4dba-afd2-d8376f5342f1"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "48 " },
                    { new Guid("606e8069-b471-4ee0-ad50-987456f2e2e8"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 3, "---", "72 " },
                    { new Guid("64240ff6-1a05-43ae-b232-7c1a1bf25249"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, "---", "48 " },
                    { new Guid("64f7ee90-8d66-456e-9b5f-726ee5c39d18"), new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "168 " },
                    { new Guid("659f0652-3ff8-4889-903d-135e34202cd6"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("0a283d26-f089-410e-8fdd-08135ba9b999"), 3, "---", "72 " },
                    { new Guid("671731b7-88c7-4a9e-a892-ebf51aa623a1"), new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "24 " },
                    { new Guid("67bc558b-4395-4f2c-8c90-7cc2c0901a3a"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, "---", "72 " },
                    { new Guid("67fa07a6-f69e-414c-bc8e-f1004eda567a"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"), 3, "---", "48 " },
                    { new Guid("6ef45df0-44af-403d-88ce-14f31aa7a854"), new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "96 " },
                    { new Guid("897ea0f3-05df-4ed7-9538-fa5c87f7d9a4"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "120 " },
                    { new Guid("8b699c6c-b831-4617-8df4-d532e42535a2"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, "---", "24 " },
                    { new Guid("8bfc289b-9e8c-44a1-b67f-2813bd32424b"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, "---", "72 " },
                    { new Guid("9d4cb79b-6965-4f25-812c-ed9e893effed"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, "---", "48 " },
                    { new Guid("a5e66365-c5eb-48e9-8fa3-dfb51ce9842f"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 3, "---", "72 " },
                    { new Guid("a94bdf7d-c8ba-4251-984b-c57e77d14d9b"), new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, "---", "240 " },
                    { new Guid("ab0d7e47-8700-4935-b146-d4cdc34f9eca"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("f8402906-63fa-4781-9240-2cf192133da0"), 3, "---", "72 " },
                    { new Guid("ae780bc8-4eaa-480e-967b-12687471e536"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "120 " },
                    { new Guid("b30163f9-abd0-4fdd-8f06-ffba28cbf947"), new Guid("7e9d18a6-d0ce-4674-9401-52b3dd904c0d"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, "---", "96 " },
                    { new Guid("b4529458-8754-4a3b-ae2c-59732a8e6823"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, "---", "48 " },
                    { new Guid("b67bbb06-5350-4473-a621-658a27c1ad56"), new Guid("7e9d18a6-d0ce-4674-9401-52b3dd904c0d"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, "---", "120 " },
                    { new Guid("be7aae72-8dbe-4b62-a425-88c0fe677e95"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("e7a65ae3-8290-499c-9226-fde4efc80914"), 3, "---", "24 " },
                    { new Guid("c4befc25-7748-45f9-b115-461c6628cdd1"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, "---", "48 " },
                    { new Guid("c9180fb9-2d73-4211-9dfb-15169d701528"), new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "72 " },
                    { new Guid("cb9dbbdc-595a-4b5d-915a-491ee514b055"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "48 " },
                    { new Guid("cc805cb7-209b-468d-ba38-9b77d7cf611a"), new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"), new Guid("4791ac18-2d8e-4d63-bf4e-eefbee5eb204"), 3, "---", "96 " },
                    { new Guid("d1bd01ed-c6ce-4804-b5aa-eaf25f29955d"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "24 " },
                    { new Guid("d56acef9-68a2-442c-9878-79b3ed25ae46"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, "---", "48 " },
                    { new Guid("d6a55506-80bf-4bf7-8cd5-aa59a53420e0"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, "---", "48 " },
                    { new Guid("e1271b93-4936-4ed1-81c0-825d9af6d8cd"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, "---", "120 " },
                    { new Guid("e1fa28b2-b9f5-40bd-bb95-c925c76e3e77"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "48 " },
                    { new Guid("e3c186b4-5525-4138-9478-c215116f32a8"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "120 " },
                    { new Guid("e3c3d904-587f-4391-9bb4-580fdd55e0b0"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, "---", "72 " },
                    { new Guid("e46de723-1745-41fe-902b-7bb0c9ca2daa"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, "---", "24 " },
                    { new Guid("e5edf27e-ac8a-4858-a61b-f6041df4b2c1"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("eaeda842-4989-4a5d-9755-0090f45db54f"), 3, "---", "96 " },
                    { new Guid("e7747ef2-7c44-4635-a204-04bb4755c73a"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("2ae54ed5-c1bf-4c3a-a21f-ffc9cc7b2c50"), 3, "---", "48 " },
                    { new Guid("ee2c6b1a-e7d4-4038-9b43-76ad72cc888f"), new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"), new Guid("65310518-d75f-44f7-bc26-20ec63245b19"), 3, "---", "48 " },
                    { new Guid("efb863fe-c62b-4e55-80c3-9ea34f49a470"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("2c891976-9006-4f0a-b493-52f1b8366946"), 3, "---", "24 " },
                    { new Guid("f075c47d-f7d1-473b-94e9-e0a296368a5c"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("91b34819-6e7a-45dd-9b6f-3e35665fded3"), 3, "---", "24 " },
                    { new Guid("f0de5931-0252-41ec-805b-afd05d0a8b7a"), new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"), new Guid("7865463d-4417-4adc-910a-9f14ba709c7b"), 3, "---", "120 " },
                    { new Guid("f1067501-4a43-47a5-8da4-df57eb125de4"), new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"), new Guid("f2a48405-4728-4e50-b99e-54ee9f9235ac"), 3, "---", "24 " },
                    { new Guid("f1f90bb1-98f6-4d17-bb49-d4a7115940ee"), new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"), new Guid("05b9823b-c2d4-49c9-bf70-20cd1d1ddbbc"), 3, "---", "240 " },
                    { new Guid("f5147391-12c6-49e8-88ee-fd8791ac43e5"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "48 " },
                    { new Guid("f8532b71-720f-4802-bc02-f1a1e5da846e"), new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"), new Guid("fba229fd-dd29-4e80-bb79-09991987c4b4"), 3, "---", "48 " },
                    { new Guid("f85d8131-c68a-4882-bb47-f50a2f9277b6"), new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"), new Guid("dd235869-87fa-492a-9896-810b2d69261b"), 3, "---", "96 " },
                    { new Guid("faa60df4-5f60-45e4-b632-11c014dd4376"), new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"), new Guid("cca39ec7-4ad5-4f4d-aaf0-7fcf30f6ed8e"), 3, "---", "72 " },
                    { new Guid("fc240ad4-a383-4ac6-aca9-527cc7fe53f1"), new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"), new Guid("9b812aef-3c14-4b2a-9b3f-e92f84b12f91"), 3, "---", "48 " }
                });

            migrationBuilder.InsertData(
                table: "WorkingPeriodRelation",
                columns: new[] { "Id", "ChildProductSubTypeWorkingPeriodSampleId", "ParentProductSubTypeWorkingPeriodSampleId" },
                values: new object[,]
                {
                    { new Guid("64458677-cb47-40c0-8daf-8e23f9f31a5f"), new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c"), new Guid("d21033f7-2d82-4593-8296-aa5fd66fe160") },
                    { new Guid("755dca6a-5246-4ad5-83cf-3f669bba0ccc"), new Guid("db44829e-2df6-4857-bd94-157dfd70dcce"), new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a") },
                    { new Guid("9e591e51-0ae5-4e34-b6b6-d81cef6e87c2"), new Guid("3b838262-a986-41e9-875e-cd26de2a9a61"), new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c") },
                    { new Guid("a7588be9-91c3-4154-a3a5-85e1a1be2746"), new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24"), new Guid("db44829e-2df6-4857-bd94-157dfd70dcce") },
                    { new Guid("a8d050e0-154a-4a69-adc7-f521d4c007a8"), new Guid("13ecdd39-21e9-474f-b85f-2ad934ffa167"), new Guid("3b838262-a986-41e9-875e-cd26de2a9a61") },
                    { new Guid("bff9ffb3-e98a-4ac9-b11f-eb9d34e32d2d"), new Guid("d21033f7-2d82-4593-8296-aa5fd66fe160"), new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24") }
                });

            migrationBuilder.InsertData(
                table: "WorkingPeriodStageTypeRelation",
                columns: new[] { "Id", "ProductSubTypeWorkingPeriodSampleId", "StageTypeId" },
                values: new object[,]
                {
                    { new Guid("1689f516-81c1-4ba5-bc62-b74313f6b801"), new Guid("13ecdd39-21e9-474f-b85f-2ad934ffa167"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("69f9782b-2fcb-4fd4-a191-a01307531fd6"), new Guid("3b838262-a986-41e9-875e-cd26de2a9a61"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("8588f563-5c09-42f1-adec-161e7bf59c38"), new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("8ead6673-9693-4499-b521-e4e33cfe8823"), new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24"), new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb") },
                    { new Guid("cd7fc456-0e1b-4e9a-ae19-0d1822cd5479"), new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c"), new Guid("8216d505-b9c4-42a0-b855-4a649146222e") },
                    { new Guid("d280adbb-15ad-4d64-982d-7c8d5d177dd6"), new Guid("d21033f7-2d82-4593-8296-aa5fd66fe160"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") },
                    { new Guid("eb5183f3-270d-4151-8682-153fec76c076"), new Guid("db44829e-2df6-4857-bd94-157dfd70dcce"), new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("035b8ebf-ac1a-48cd-9c67-a8ad90818f8a"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("0a6e0ea8-782c-4aa2-a24a-d3fddd2176de"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("10eed623-c0dc-4e06-ad3d-05a7aa3c161a"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("29c7903d-2297-4e6d-9218-72cab8d2cd21"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("3843eac5-c541-4798-a158-dae8a221c0ce"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("5fd8f19f-799b-43bb-b65a-df494ee7ab22"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("66817757-6f11-4560-b31d-7b7f4998f43a"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("672fae64-78a5-437f-a03c-d9b105ac138e"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("70604fd7-a3d8-4622-95a6-ed67bf5b432a"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("8362c64e-54e8-45cc-a4fb-bc06bb4cc8a3"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("84576bf6-46de-4d4d-a4af-a4e054754bd9"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("aa9f497d-e5a3-4b20-8af1-f8ac8271256d"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("aaf6647e-5b51-4366-a200-6019cfa0d5de"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("b3acd3fa-071a-4449-bfe8-56c9e7c4e11e"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("c9fbcb8d-638b-4be6-8450-da7e7bf34ba9"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("f48cbb13-cab5-463b-8f92-3c88ec140235"));

            migrationBuilder.DeleteData(
                table: "Brigade",
                keyColumn: "Id",
                keyValue: new Guid("f751cf17-4b20-44f5-9520-1afa465c5b0a"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("364de2b0-c161-4093-9e5c-43e43f28ae2d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("02de8641-cd18-43f5-80de-82c60779f5e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0677ff97-712f-46ad-8f5e-39c0a85e433e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("07539f7b-2b8a-4e83-b281-f6ef82b9b5ce"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("093a5db4-346a-4053-82bd-cae8d457afc6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0a24a5ff-6bd0-47c9-806c-a9a33d2587f5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0bd59fa0-ea0f-442b-8ead-24be6fbe3d61"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0c5c5b13-2caf-4f56-9dd5-d6312d3d0b5c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0c785add-14bd-49c5-bc22-e62fd5eba4f1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("0d8ecb71-7fb0-4805-b55b-7e03e4d3265e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("17442260-2a27-4f53-8b9b-c4e60450215b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("1a26a770-bf1b-4975-a32a-6bb4b5396a26"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("1a28e447-5b81-4abb-9807-bcaeeeb67e3d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("1cc8d3c7-27df-403b-b103-b4b75413d2a4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("1d6bb553-7765-4066-98bc-38c0e03fef88"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("1f0564fe-9241-4654-a68d-faa6d760b7e9"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("23641c84-2303-49e0-a89e-c5c2d0670c77"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("250f4fe7-5d0d-495d-83e2-9027f5c605ee"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("28454aac-6245-4bbd-bc84-125b29369db0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("28ed49d4-4d9f-4104-93fe-f0e975723b73"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("29f365fc-a09b-4acb-ae77-de549ab887f6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("2d97caf5-4519-4477-9113-498586ed954d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("2e9ba222-3879-4cf1-bfbd-fdaaaf5d372e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("2f0c8c8b-65ca-49fd-b4e4-8d4b689bbf14"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("32b47f46-9973-4702-98d1-34f8b57bdc6f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("351fd367-f11a-4c09-bdcc-eb0355472d86"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("365c00f9-33ed-4acd-a798-d6ee03a27edd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("39fdd183-e62a-42a0-90c9-d446d49cecfc"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("3a7e5547-718e-4450-9ac5-fd485af9a9c5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("3da7ae97-97ec-49dc-a1d3-32f89146455e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("3e4e68cc-1178-458f-85b7-35883c382a28"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("43c04d75-9b61-4cd1-8d8e-a5d4d1441f95"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("47df4222-9391-4962-b94f-c23047d5f48c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("48b0acff-0ccf-4e2e-bcee-3282d06a004d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("4a8916cb-454a-43f9-a62d-da8650fa85bd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("51f102f5-b331-4c7b-b6b8-bd5c76c2142d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("53212737-8afc-4af1-a953-75374eddfcf7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("54cc4f7c-b5ab-486c-a105-322c8bc97195"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("552816d1-f728-43d8-9399-7da2d0a78853"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("5a30817d-5016-41a2-899f-cdfbe65b7c37"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("5cb9fd05-cacf-4ccf-b538-93ea56f88412"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("62017377-7697-4f96-9230-81c8510ac387"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("62a789f7-4bfb-4f13-bb90-f8012c7a86c7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("62b92acf-0eb6-4d44-b1f3-701717e51e1b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("67267f6e-daa9-4d65-95de-0cd8f392a413"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("6844152f-e5b1-4dec-b7a4-9d2eb7966caa"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("6844bb0d-bb73-4062-bc20-0e39ac176f5e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("69c4099a-2e39-44ec-a77b-5a90276d7fe1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("6cbd0575-8733-4e95-a655-99e8683d9b9a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("6e43aa3e-8a97-46f5-95f0-cac0924dc918"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("76118a9d-e20d-4eb3-8464-24721e9c6c0b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("7f6a43ed-3ade-46fd-b2a3-8ed8a659bb14"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("7ff55aae-5d3a-46bc-84aa-ab3c7c06da6f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("881a16ca-6370-4608-8b37-d08ec610471d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("9554ac6a-9d2e-44cc-be40-db0ee1a540e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("9656bf60-29d8-42c6-8445-899b61d28733"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("9adeaa17-5d46-4764-87bb-1f84659a5466"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("9e860497-f2ca-4b13-b409-7628113e64ad"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("9fe251e1-d7b2-41d8-8a80-347727841bed"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("a3883624-5fd3-40b7-8356-703b9b1e2a68"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("a6e1d6d6-7351-4605-ba53-74b470fa648f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("b0481716-a8e3-46a6-82d1-4841d31cc21f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("b083878f-ff41-4275-8e3e-fbce6202850f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("b2d77f3a-f890-4d17-a3f4-6b20e59b4c3c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("b591292e-f513-4a9c-b134-1aa219d6c779"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("bb9a8afb-97c2-4628-9830-a800b30d009a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("bcb2c2f8-e21d-4e69-bf50-858bbf7416b8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("c06df845-203f-4840-8c71-142333bd88dd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("c7c2636d-b4b5-4b69-868d-0d23a0bbfe1f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("ca675cad-11e7-4f87-81ce-314f28f4a4df"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("cb2b20ff-c681-4ebb-bbb9-d8edc2b4d8b0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("ce6f0a7f-37af-466d-980f-32dd6e84dea7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("d1f97190-4f7e-4e5f-868d-12f0b2cc349d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("d2669dc8-cdc6-4119-96d5-efe37ddecf5c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("dbe3bf90-76bb-423a-9b56-980333a73bf4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("dc890769-7afb-45e1-967d-692eb5e5d09f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("e2a1255c-c1f9-4ca4-ab4f-c31075359822"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("e3625e30-0b03-4091-81f0-19bcdfd888cc"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("e684ad23-7197-4506-9035-bc650ea8b2b1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("e6b17543-d832-4c6e-97cc-5ce97714939c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("eb39a962-a8e2-4da1-831c-c6a8640c560b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("f2e2fffe-eacf-4b15-a619-a59d235dc86a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("f5128cfc-a15f-428d-9390-41916c2fd550"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("f9379b68-a12b-4730-91d7-f8b3ab8435c8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("fd5ae800-84b5-4cdb-a705-5d4b9da60d98"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeGroupMaterialRelation",
                keyColumn: "Id",
                keyValue: new Guid("fffce197-3705-47a7-9783-6994bbcc4912"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("00f08023-773b-4b22-a247-814a4e7b1eab"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("05acab2d-d004-4a00-b60c-bcb7171248ff"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("0664aa30-a3ae-4359-ba0a-0c8c89110018"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("089fda0a-d8d4-409a-95bf-7081a51c05a9"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("09be01ff-a019-4ab8-9a9d-cc579ce16a6e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("17d4705f-84e1-4ba9-a972-445aa73c6106"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("1b28a283-9d1a-4bdf-aeb6-c2b078eaadac"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("1dcb1c09-ef58-43f0-9bcf-edb6c043e1a1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("200cb4a3-a254-4479-90d7-a4ef1ac96cdd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("21678816-bb48-41c6-93b9-1432c6d0c859"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("230aa190-5b4b-498f-976e-501d14c9b674"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("2400e059-d044-4565-b293-c511759e8cb7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("26ea2952-b604-4aee-8d73-884540641793"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("27b5da07-5256-44fd-b209-7c21e67fc7e5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("29ea4a1a-835b-4d7e-a9b9-189e07cf91be"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("2bbbf53f-ce59-4b59-b491-6352e6c297a9"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("2bc9e8e4-ac3e-499e-b5f5-d82ad12d7cfa"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("2c3ce88d-9140-4178-bacc-a45a64df6fd6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("2e923623-2b28-42a5-b53b-a259c3156351"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("301e9c98-c816-4829-874e-c79a810e97f5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("306cc8c5-e66c-47ec-9889-bb5b2734e5ad"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("35ce056b-6c5f-4ed6-9c42-a59b80ae6d70"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("3640de89-764a-4ed9-8f9a-c7f883bfb017"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("39203c27-88d2-4a9a-b62f-79319df29f0d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("39713655-26b1-4823-b130-766ce4c626c3"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("3d815557-d474-47dc-a583-db17d6f0c5c7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("3dbdb78b-a68c-4680-a7f5-82f3960ca2e4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("3fc7b91a-2e97-4fc9-b46c-bac641c96a64"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("43f58187-e6a1-4bcf-ae7b-3e3938cdc9e7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("45ef92d6-043f-4a1a-99c6-bcd37237d513"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("4c83a1df-489b-4395-b89e-f05ac875780d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("4cbeb4b4-e809-40d8-be70-65830baa7486"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("4f507183-3e09-4a18-92c8-31a8a82bc787"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("574b365b-34ea-4138-9faf-c6cee68952e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("5808c83f-27cf-46e3-80b9-2f61f56912c9"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("5a61a6aa-b1fc-4dba-afd2-d8376f5342f1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("606e8069-b471-4ee0-ad50-987456f2e2e8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("64240ff6-1a05-43ae-b232-7c1a1bf25249"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("64f7ee90-8d66-456e-9b5f-726ee5c39d18"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("659f0652-3ff8-4889-903d-135e34202cd6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("65b4b0c7-be67-46d6-bd02-e1c36202fd6a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("671731b7-88c7-4a9e-a892-ebf51aa623a1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("67bc558b-4395-4f2c-8c90-7cc2c0901a3a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("67fa07a6-f69e-414c-bc8e-f1004eda567a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("6ef45df0-44af-403d-88ce-14f31aa7a854"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("721f8111-63e5-4209-951d-41d80aae5206"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("734fe414-0828-404d-a33f-99760e69b8fc"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("7a6a851c-fc3d-4a1e-ae76-6602a799dfd6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("7d1e1aeb-be4d-433c-85f8-d3221aba3e5d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("81a1a6f7-7ff0-48f7-bac4-512a16300489"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("897ea0f3-05df-4ed7-9538-fa5c87f7d9a4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("8b129e37-4c8a-490e-83ba-d4a868988774"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("8b699c6c-b831-4617-8df4-d532e42535a2"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("8bfc289b-9e8c-44a1-b67f-2813bd32424b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("8e87cb81-5257-4b49-994b-ad4c158ceec4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("959c47a9-25fd-464f-bda7-0ae1cd2cb167"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("98a01cec-1e3b-43f0-9554-3073be88da63"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("9d4cb79b-6965-4f25-812c-ed9e893effed"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("a1867f2b-ec21-4f1c-a7ff-8eca144aa708"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("a5e66365-c5eb-48e9-8fa3-dfb51ce9842f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("a66f8dcb-add5-4dec-a152-016fabc4faad"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("a94bdf7d-c8ba-4251-984b-c57e77d14d9b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("ab0d7e47-8700-4935-b146-d4cdc34f9eca"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("acb9b76a-cf26-4452-affd-a7b42a9c42c4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("ae780bc8-4eaa-480e-967b-12687471e536"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b30163f9-abd0-4fdd-8f06-ffba28cbf947"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b34248b4-1087-4a7a-8ba5-45b7ab769fa8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b3ce376a-4718-4016-831d-da1064b0b9cd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b4529458-8754-4a3b-ae2c-59732a8e6823"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b58d1947-5813-4b45-a44d-5b3a1f686d1e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b67bbb06-5350-4473-a621-658a27c1ad56"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b85de8df-6e60-493e-8ba4-19a86fb8a78e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("b900064c-5648-40b1-bc37-99776445621b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("bac73bbb-6b72-46f5-a50c-12f1cabbcba0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("bdd5baaf-ade0-409b-b424-84b488e43da7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("be7aae72-8dbe-4b62-a425-88c0fe677e95"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("bf24fb86-c84a-4261-958e-55a7049ce74a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("bf8f2d4e-3a84-451a-a8bd-597a6a930243"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("c4befc25-7748-45f9-b115-461c6628cdd1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("c9180fb9-2d73-4211-9dfb-15169d701528"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("cb9dbbdc-595a-4b5d-915a-491ee514b055"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("cc805cb7-209b-468d-ba38-9b77d7cf611a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("ce3834b1-fbf5-40f4-9ce6-b68f84b5ad62"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d1bd01ed-c6ce-4804-b5aa-eaf25f29955d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d51d9e30-3660-4dd1-bbca-48073552bd38"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d56acef9-68a2-442c-9878-79b3ed25ae46"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d5874272-5cb8-493a-bce9-6b12b212dc90"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d6a55506-80bf-4bf7-8cd5-aa59a53420e0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("d7def012-2160-4602-a725-9f3ce14b5270"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e1271b93-4936-4ed1-81c0-825d9af6d8cd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e1fa28b2-b9f5-40bd-bb95-c925c76e3e77"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e355ab3e-00a3-417c-a15f-4aa0dce1c514"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e3c186b4-5525-4138-9478-c215116f32a8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e3c3d904-587f-4391-9bb4-580fdd55e0b0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e46de723-1745-41fe-902b-7bb0c9ca2daa"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e5edf27e-ac8a-4858-a61b-f6041df4b2c1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e6f73c99-40d3-47a6-af10-c18e947c3068"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("e7747ef2-7c44-4635-a204-04bb4755c73a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("ee2c6b1a-e7d4-4038-9b43-76ad72cc888f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("efb863fe-c62b-4e55-80c3-9ea34f49a470"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f075c47d-f7d1-473b-94e9-e0a296368a5c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f0de5931-0252-41ec-805b-afd05d0a8b7a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f1067501-4a43-47a5-8da4-df57eb125de4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f1f90bb1-98f6-4d17-bb49-d4a7115940ee"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f5147391-12c6-49e8-88ee-fd8791ac43e5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f8532b71-720f-4802-bc02-f1a1e5da846e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("f85d8131-c68a-4882-bb47-f50a2f9277b6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("faa60df4-5f60-45e4-b632-11c014dd4376"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("fc240ad4-a383-4ac6-aca9-527cc7fe53f1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("fde9611a-cb91-49ad-b336-07a69ff9cf0d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeStageSample",
                keyColumn: "Id",
                keyValue: new Guid("fe0bbca2-ab8b-45d4-9209-b989d213da19"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("0152e0d8-f6bf-4373-be7f-c16d1d61ad27"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("05ad0daf-35b6-40da-a42b-911c04d968c0"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("08016396-82d3-40d0-8a54-d8502dcbf855"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("137b5459-b818-407e-9c8c-2989211cf84d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1398e90e-b7a4-4222-b87e-4532405a8388"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("13bc1e0a-4b82-42f3-92fd-68a4dfc1939e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("16c41e6b-a86f-4a74-8fe9-a861cb011f27"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1a202092-9568-4a1b-85d4-dece3ccc975a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1a35ecd2-d241-4a1b-ac6c-a4b257238788"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1a9d65c3-d74b-4691-82d6-97d3bdace4b8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1b369839-f692-4650-91d0-6ff67e95448d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1b6f780c-47d8-4032-b928-62293e76d818"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1b8bf584-d8bd-4f90-846d-cc5c228a886e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("1c5e75f6-ae22-4dc0-9b8a-bf5da517e785"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("26ad3a32-f616-43a2-8f3c-864a8d320468"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("271f74ef-e118-44c0-93f8-c21a40caee1d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2815fd89-8027-4de5-bc63-23faa3c27768"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2b2b010b-00ba-4a10-bf0f-b680d8b95120"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2b6883df-4351-4b1b-bbe2-0d500c0e32d7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2bd04434-00b2-44b9-8527-27c4978f3568"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2ce2347a-d262-4110-8291-8741af9413d9"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2d6170ef-269c-4afc-a9fd-8697d5c21231"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2dbdf34e-ae10-4bf9-891f-64cb724f4083"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("2f64f019-1e7f-4ce3-8527-75092602157c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("31848483-af56-4483-9ac7-882cdba5c694"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("34d00829-f226-4fcd-853d-4866e7d190c3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("3779f516-d147-47b7-abff-4784bd54f7aa"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("37fbf580-a4a8-421c-8bb6-bbdaca97c9c3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("380d2f34-830e-4ab1-8bdf-9d9503df6d08"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("422144d7-25ce-437e-8ff8-6e7c1055b189"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("4321a64b-2633-4254-910c-0cbd4bfcfd8f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("46cf6749-0a31-48d2-9463-2b4827a58c5c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("4b4c8add-10af-457b-9f78-9325980dc2fc"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("4c229dd4-06c0-4a77-941f-3736c160629b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("4c47b4eb-3d13-4cca-a64f-23987101c638"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("4e6182c0-247f-464f-a873-cb99a6e6a61d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("52cabdc4-e82e-4362-b7af-51278cd0fbce"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("53798e78-af1c-4b37-be05-b6c4905cf254"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("56deeed9-3742-42d2-926a-ff2e337f17d8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("573bd3b1-8a99-4144-8b06-bf8722b3521c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5b1cc73a-5f7d-40c7-8914-b0271c4438f4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5b729243-aa20-409c-b1a5-d880cf27493e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5ba5f7e7-23c7-4be6-937e-0d5c3bb69b8d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5c6502a3-d3d9-4f98-b57c-9bec6f6a666b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5d44abed-827e-4313-8a6f-4c812b552719"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("5e4d635e-068a-426e-883c-e1609deb990e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("614c4932-6476-4ff0-a69c-4fbff81c2a05"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("63660f64-5ccb-434d-af34-f93b4a13b8a6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("64458677-cb47-40c0-8daf-8e23f9f31a5f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("64c7974e-6468-4a91-82b3-88372a4d9973"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("696ce1e3-d314-41a6-8cab-b849398aa17a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("6c9ff42d-bff5-4fc9-86cc-e3736d0bea94"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("6f581c7f-519f-4cd8-bdc1-6f4c8c7eeb1a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("755dca6a-5246-4ad5-83cf-3f669bba0ccc"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("7608c35b-4c68-4c99-a2e7-1698ce392c66"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("76cd2f43-2b12-4ea2-8163-5c15c2d5f80b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("76d36a23-1e29-4aff-a0d6-561e02f2b28c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("78d4c13a-c222-4e7d-acfd-d2df6cd03f88"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("7a7daa3f-9f3a-4a45-a2cd-ea79599dd996"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("7b957a19-99c3-4829-8017-67002848495e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("7e171e4f-0c32-4e7b-8780-3d809b9430a1"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("7e68fd61-f88b-4d5c-87c7-dd92fe17cec9"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("80dac4c5-ab17-42fe-8a48-7a08154801c4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("83ca605e-9374-44c3-9af0-42895e362698"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("869d04af-eca2-4398-af82-d3db53b92a42"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("87d52e84-2420-4c34-86e7-730d10cae0fd"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8adfa5a3-2e99-40ca-b42d-bfeb72a20d7a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8bb1d7fb-dc08-47c9-a88b-2e1007b5141b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8c1f4c47-7c21-429f-a70a-0d8d99fb612f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8c565dd0-2801-4650-aa77-a1876f460944"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8cf606a2-7b98-4523-8d93-c6fd87f3fff6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8d60b97c-c931-48b5-8763-26db0501b206"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("8fb03127-e4ce-4425-8c2a-77c1c8577d61"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9179850d-6e75-4e01-b81b-235e20929ac3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9195ef09-d3cf-443f-8640-c829492e3c1e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("926e196e-cbe7-40c2-a8e7-bfd8800988c7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("949e045b-32c8-4d2b-9675-50f36055b840"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("98ade418-b4f6-4c8f-bf67-3c5be2fcde21"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("99deca26-b4d6-4067-be64-984a1629046c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9cc921dc-d16c-4395-8d11-56958a432806"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9cef5f6a-b3af-48d2-b184-3f70f522f5e3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9e591e51-0ae5-4e34-b6b6-d81cef6e87c2"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9e9ff025-01c0-4446-a1ab-7749ff25814d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("9f9183b4-c607-4749-baa2-ce942f444474"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("a282bf3f-3b2a-470d-9a54-11dd405dcf48"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("a4fb9e2a-805f-427b-b94b-741487c4dc1c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("a7588be9-91c3-4154-a3a5-85e1a1be2746"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("a8d050e0-154a-4a69-adc7-f521d4c007a8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("a9171d24-3bce-48ab-a23f-5211a6614a4c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("ab387c50-1a34-47eb-9263-bbd005c63885"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("aca93872-ae15-409c-9863-7d91568b0238"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("b224fd3e-f1fd-4652-a522-b5e1661ec9b3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("b62857ad-d2d3-4ac4-bb51-8a1f0a4b10be"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("bd095a91-842b-4783-ae87-433284b33eaa"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("bf0eee35-d9c8-4b07-bc6e-c4dc87156f0e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("bff9ffb3-e98a-4ac9-b11f-eb9d34e32d2d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("c8b45d01-9308-4c01-9be2-2538bafc7590"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("cc729f5d-b371-4d90-a4e4-b38e9d88881e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("cf428431-64d5-4245-a2d7-9d0d275ba5a3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d0a40867-fc22-4cfb-8441-3c367ea46999"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d10e984d-7454-45df-9db5-3e69fa1ae936"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d3c6cdc0-fd62-47af-9826-eaafe4c55acb"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d3cab1d9-c866-4c13-b928-7f2a3b259261"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d7a7a830-994b-4a01-b815-9ff894ae45da"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("d809eab0-9b7f-49b7-b02b-01c76c588d59"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("dda1ce29-6d10-45ed-9587-710a9e5583cf"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("ddecc927-bdeb-4af6-b243-212fd1487037"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("de7cefc6-38c8-4f2c-a163-be06eae4d73d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("debbfb13-1314-4ff9-ae58-c1647ae9493e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("df95414c-681a-40df-bc92-fcd19f47afd9"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("e375886f-0529-438e-84ff-9688d9db4ce4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("e7fd78c4-7221-47d7-8e34-b5f67af57aa8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("e83b901b-92cc-4f29-87e8-f686652707b4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("e89f6904-eaaf-4fe2-b6f9-ba863bdb11eb"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("ecd4f9ae-e71d-480f-987d-207513952f74"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("f1497535-846b-45ac-8b87-1b2845ba8dc9"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("f2d585ba-f4f4-49d1-a531-59b2bb949861"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodRelation",
                keyColumn: "Id",
                keyValue: new Guid("fdf6a6b1-66c0-4cb2-84de-36bc4d016404"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("03a2dddd-85fb-4e1c-94d8-3741031925bc"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("03c26172-c992-463a-99ce-f2c21473a193"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("03dab76a-3478-48e2-a765-edb838d19233"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("0851193f-5c58-4750-8ebe-c42d4f09ca95"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("0d6e05f1-f98e-48f1-91ff-e6d30e6428a3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("0f8a68e2-6f33-4730-84b2-e5004a1f4242"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1001337b-9211-478f-b9bf-5beb8b049602"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1001399d-778d-4b41-9f6c-022b27ce2252"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("10d3bc58-ef0c-4714-abf8-a1778ad27d77"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1286f4ff-2f4f-4d90-9595-f1f73fa1eb9f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("14723694-ef35-48ca-8585-c24ba9854200"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("16173999-b3f6-42bf-b14a-e15cad238f38"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1689f516-81c1-4ba5-bc62-b74313f6b801"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("19befd5f-a981-41ad-90c9-6d4b730f3d8a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1b2fded8-38ac-4855-99a9-178063b6ef3a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1f608adf-4af5-41c8-abfb-acbaa0c9409b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("1fe2d979-b331-4630-ae49-04b42d90748f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("2081739b-8b76-4bb5-935f-47c191d1db2b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("22ec1f17-2e0e-4f85-bd7b-aa3591649cd1"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("24f6994c-f50e-460f-8036-43be3fdb3d4b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("25b15cdf-dfa9-47d6-8b85-93d9d902d899"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("27ce7b6f-5dd2-47ce-8372-d928defa62d0"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("2ac6f0b9-5972-4969-9e88-a059b615e7c2"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("2b4d392b-ad18-470f-bb52-34ab00946d5c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("2d08cedb-2dd3-4542-b852-f4a363440aeb"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("2d48abd9-5c4a-4076-8535-170365fb947b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("326dbe72-728e-4385-82d1-1d09b41af17a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("38686d72-cec9-4590-9e87-63791437c982"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("38837865-cb3c-4088-a551-f0ae6144aa78"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("3df7f6d2-18fe-47b6-945d-140e7b5541b3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("3e525d3f-3912-440c-81fc-908bc28a3570"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("407d01df-2671-4a70-aec6-b0a5bb883509"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("419cdf4d-ddec-48b7-9536-e0bee0748406"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("41bf76c9-4351-4e9a-a1ab-77c1ef4de217"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("42f249f4-d5eb-45a3-8b87-47b008ea0afd"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("44359239-5f8d-4790-9f31-afa86ade4374"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("48a25548-14ca-4a3b-aebb-29aef90d3683"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("499d10bf-4229-47a0-8013-ddc817d8fd4e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("4a45e387-dbf1-4264-9961-8875d0a021ea"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("4a474502-c336-41b2-aa59-16f84d82860f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("4b35a069-bc87-46b9-bb3d-0e3dfc0672eb"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("4c24bb51-9bec-4e53-b9d0-b94551985a2f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("4f74f1ba-522a-4e98-8721-8f3e01de5ce3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("502b94f1-c258-4497-9015-4ee165b240ff"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("52734a65-8c94-479c-b53b-f4e954bcd449"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("53138e86-3e72-40e4-958b-43bf1ab07bab"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5331c10f-45fc-4042-8207-37a11a7ced8a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("53b61fdd-e3ae-40d8-b25f-05aaba19dda7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("54111ded-87c1-4e5b-995f-557ad3255eaa"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("56d6390d-4f14-4bb7-9632-f4b9ffcc9fd8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("58d4cb0f-f877-4b34-93cd-c0f6477590f5"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5b1d938f-5575-45a1-aad1-155a252ee7c4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5b7e0656-1fee-4d9f-b874-d9104d55d6c1"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5bc76299-f2dd-4372-94e2-538a76d6a268"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5c0c8437-2169-4666-9787-2bfa56562dc3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5c8e3936-6b8d-4b36-8538-8199396a0ef5"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5d4fedc7-edac-4206-b453-9ea2280f39f7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("5d992bec-be3d-49da-9d22-92794a128ad1"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("614b4461-c4fb-41bd-91f9-45b3b98f0dd3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("64765ee4-d372-41ac-af9f-8eac0dff5a71"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("64cb9299-537c-497b-b46a-b2c6b8080bff"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("653bf6e3-27ca-4a4f-baba-acc91a894d23"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("65579eca-02b7-4649-ae84-f44b9230c6a7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("673cff01-eabf-4eba-bac7-6896cb3cb550"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("677ff94b-0895-428d-9ed4-05eb857332db"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("69f9782b-2fcb-4fd4-a191-a01307531fd6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("6eab99a0-0f83-41ab-b903-51b717178a45"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("70db30a4-b6c6-42f1-81f0-de1e4b57e392"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("71031a6c-481d-43df-a1dd-a57a8cd85d01"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("71d2a0ad-ea4c-4928-af77-e0afd08af9bc"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("73c85496-c294-46a1-8d83-0effa0f4227d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("759c5f65-ec28-439a-8c7e-e912c38f3593"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("78101b64-14ae-4dd6-8bd8-8108b9b135de"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("7a3c46c8-2f1f-45cb-8364-86f99a92a1dc"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("7f5ebf16-4b08-403c-8e17-c83ec4c6de0b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("7f82fc84-d3e6-4674-bc93-bc0b1cb4f726"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("814869e9-7c0b-422d-8a1d-0ee5753db2eb"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("829b4c5e-e93c-4594-a7a9-830180ce663c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8355040d-0d8f-4afd-91f1-779eb102bcd8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("857ab8e2-2950-4481-9138-742c19436713"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8588f563-5c09-42f1-adec-161e7bf59c38"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("893d9749-3762-4830-8d56-4f3426aafa50"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8b5a7977-c423-4257-8719-3b98018ba4d1"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8dbf6a0e-c3d3-4699-93a3-5326f827269f"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8ead6673-9693-4499-b521-e4e33cfe8823"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8f7d67f6-46f9-4e0f-8fd1-e9fc844fbdb2"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("8fded863-916f-42d2-969b-3190f76c1068"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("93475e9b-030f-401b-83d0-8b86bbd8717a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("951c6b38-f2b2-401b-885e-f594e2ab9263"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("986fde90-ee0e-4bff-91f6-0e5cc2f4c50b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("997f97fe-a645-4e2b-ac24-a493fe276fa8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("9b7fac00-3778-4d05-9ed2-952f5a8c9da8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("9f48a4aa-a76f-490f-bb92-3523050e4250"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("9fd568f1-1f13-424d-8243-a8ff2fe769f4"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("a12d2864-1293-4030-a1d9-86e15ca03df7"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("a1714eb2-ae6a-4b34-a467-a09049efd953"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("a8acf1c7-1cba-4738-b04d-1c730e616e9d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("ac741d81-c7f9-4623-8e60-8dff5ffd392c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("ae06ac91-1d39-4102-af01-e04327171207"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("ae3ab933-3b7c-4513-bfad-c411272e7371"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("af70ccc0-c309-4d93-83f8-12f174cdd4ba"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("afe5b77a-fbac-44eb-b71c-808e5ccc2df8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("b26a1b8e-c391-4e0f-b69e-3be4a49f4fbd"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("bcbf1b23-bfcf-41a2-a7c2-d7f832de9911"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("c050c652-bca6-4dc3-850d-5c86c2f56859"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("c06eb19f-39a8-4f34-8ed8-78a3a40eb00b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("c5ae394c-db0d-4f56-a650-b48559637e8c"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("c94c9543-c48a-4b2e-9ba9-7b171a1280e3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("c98dbc4c-ed44-49c6-8b95-ae114197bbbf"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("cd7fc456-0e1b-4e9a-ae19-0d1822cd5479"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d280adbb-15ad-4d64-982d-7c8d5d177dd6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d37f49a5-ca5f-4db9-af60-1efee3029715"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d5498780-f64b-4407-8f32-352addecf420"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d64ee27e-f2b4-4768-bf06-f7a8aebfc4df"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d6a02f6b-804f-4fd4-b64f-674932b52044"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("d9025ddc-40c6-4195-b105-dc114d57c0b3"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("db2514e8-625c-45d3-9831-108fb517f8a0"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("dfb08bae-c5dd-42c8-ba89-4e6d5e31a1aa"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("dfee6c05-c220-4cda-8bf1-a3d509591f7e"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e111f3d8-1750-4a17-b8e4-9261d9a329a8"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e193be8a-cefc-44f3-bf91-7bd9ba42043a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e1f1e2cc-a2fe-44ad-ad0f-db73ef5a80f6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e3655df2-f4a1-47b7-beab-d4be49fc116d"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e47b5c05-4309-4b37-ab26-a59d623b4d38"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e589c35a-220c-455a-8e3a-04d7e9ba6115"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("e6dd58a9-e934-4b83-883c-347b368350a6"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("eb5183f3-270d-4151-8682-153fec76c076"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("ec5fa1c2-d3d0-4e39-b15c-d88bfdd5ed69"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("ef8f98b3-1aaf-416e-ae1f-c8c404ebf4c2"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("f1486fba-242f-4091-a29a-8fc21270787b"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("f1d53854-d7d5-477b-86b3-3599404b252a"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("f73c89db-f25e-4891-be57-6a96d30973af"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("f7c30cbc-3d8a-4210-9a2e-f8a15c9ee042"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("fa47e298-a648-42fa-ade0-9a2f896f9917"));

            migrationBuilder.DeleteData(
                table: "WorkingPeriodStageTypeRelation",
                keyColumn: "Id",
                keyValue: new Guid("fc70e5c0-7aeb-4746-ba08-1a2b7c522d27"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("99efee09-84ad-446c-b154-2c18de67adf4"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("078e0ed2-e7b3-41af-b8c7-cd7f77f745c7"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("141b0ebe-acd9-459f-ae85-7b6d7b6d6974"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("239001eb-652d-4c77-aa4b-aeb31a7b689e"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("3d4097bc-cb30-42c3-bc93-f766f60b742b"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("4420a76e-8d00-4a77-aaf6-e2ac58efc0d0"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("61878627-59d5-4683-bfce-05bdc90a3e75"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("71454376-7c69-4f27-bf0b-bc2f77924002"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("7e9d18a6-d0ce-4674-9401-52b3dd904c0d"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("cc694c77-914a-4ec5-be1b-626dcbcfb2ad"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("eda8327c-3f43-4d8f-9224-1ea1e640e011"));

            migrationBuilder.DeleteData(
                table: "MaterialStage",
                keyColumn: "Id",
                keyValue: new Guid("fca98875-3078-4a79-8c24-9a82c7ff5641"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("0068c383-ddcd-46f9-af97-a5490d66b88a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("04498db6-fe2c-4228-9065-4b0341dbd892"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("081e2d98-a604-44f2-96e6-1cd486e5db2f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("0b1ca170-699b-47aa-b6a0-c73e7166641f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("0b252a71-179e-4e31-8698-19ac4357050e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("0c1a9111-1dea-4725-aa11-8862da0c122c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("0c323ae3-db44-4b8d-91dd-c29e562c5457"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("135f6269-66f7-4ba7-9c48-18c2b03ecf3b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("13c0b41c-048e-4d0c-b4ad-2e13a99e179d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("13ecdd39-21e9-474f-b85f-2ad934ffa167"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("142b775f-44e3-490b-a593-70b8698c93b5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("162fb06a-8c2c-4a5c-8cc3-9501efabba0a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("173177f4-e827-4098-ac98-cf621cfc9804"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("1a19566e-5e8d-4e8b-b991-b37ddb420f40"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("1c2a76d1-285b-4a89-954c-3738305288e8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("265e87ff-c1c4-4b34-a331-63e297741cdb"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("267a492a-2d55-4a33-9bb8-d3076bea8e1d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("26add4ba-2196-4322-bb59-0bbe86cf3180"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("27e28db9-c523-48db-883d-d0ccfa8121c7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("2cf2af17-da41-4eb0-ac8d-ccb34fa708e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("2e736ab4-400c-4c59-a496-f6f748259f54"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("2f46e366-dfb5-405e-b815-fd00e3ece8af"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3193c06a-a1f8-42c5-abcd-c30fee6612b5"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3b838262-a986-41e9-875e-cd26de2a9a61"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3bf0a6d2-d337-4061-9222-84dc95fc215a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3c932b0e-6668-4512-9633-fb2472bce36e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3d0aa127-4111-471b-be31-2968e736a4af"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("3e1fc4a8-5a0b-4be5-95f9-4893a64171d3"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("42a2c51f-b3ef-44c1-ad30-961279727186"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("437b65ad-48f6-4fdb-bd8e-e6f8b9496261"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("46fd1905-67cd-4ab4-b242-a924322db5cc"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("474890d4-4172-434b-a950-de96eb1808ce"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("4ad07bcf-f2bc-43e3-b084-56759f8d3d3d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("4b9095f8-5d68-4440-8558-1d52c7391a42"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("4d325a32-7d5b-496a-a312-209df69bdb2c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("4f9640f4-2a15-41e9-a917-8300356f4492"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("50a3a8be-8720-4a3e-a780-23f16a642350"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("526a5359-0914-4418-a29c-94151052effd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("533662ef-436c-4743-a872-f4c5d17418bf"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5576f277-d9d1-4d9b-93e3-9288a4aff46a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5c2e714b-d3f3-49db-bf3d-c6e676319348"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5c7f8886-f1d0-44bd-b6a0-83436dc7d796"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5cdfe5ac-9e77-4eea-aca6-8a5144f9987c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5d06464c-3f4b-43c2-9b82-9d61d9d76f44"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5d618e84-632e-4ebf-a985-88342bae2a00"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5df88071-dae0-4ffe-a0ce-ea5938999d7c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("5efd0f05-e595-4a2f-94b3-2347f94eecc7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("621e09d9-e112-4845-b565-0db2f2426992"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("62f2a48e-af3f-4fc3-8dbe-e1ff5a434646"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("64e3019a-c9c8-4358-86af-4290c1de2d39"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("65f7f254-32e3-47ca-931a-81f705a449e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("66b8f43f-a427-4e21-ab1e-db2941306f52"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("66ed8de3-1b4a-476c-ad59-f6ba8746300e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("67eb07a6-7deb-48b0-a9d0-f1968c45cb94"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("681a9d44-aa1b-42f7-8ba8-3a3aad0dd8a2"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("6bd776c7-8727-4acd-873a-8356a86b3071"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7484d536-2104-4e11-a322-ce4c6e0b35a0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("762c4c6f-9015-4fdf-bcd8-4e27b105fbec"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("76303263-3782-4d46-b880-c19822006c04"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("77ccfad7-8078-4fd3-aa6c-b4323ab5778f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("783c2ca2-5a56-4130-8d02-ec3cc86c0c31"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7ab41cbf-f967-4096-8c0c-852002c1037d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7c6a9a11-11fd-4d2c-b54a-f97475affe88"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7c9f77a1-5ebf-4ed6-b3ee-9dc1aef00dc9"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7ce6529f-2e9f-4d90-8cc1-d4103587a64e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7fc5f63e-2118-49d7-baea-9b9d3617b736"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("7fdcb535-058e-40b3-a94d-8bde6e7cbbf8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("81e13934-f2c3-453b-ad90-25e296b0fef1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("820bcdeb-7443-43d4-89a8-e2306411d5d0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8234c78c-a01e-4a78-8713-981203e05a1d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8397b6bc-3b49-4713-8b9d-3f2c1b0fbb82"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("874cdded-4702-41e5-8966-040ab54d391b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("875bbfba-88c8-454f-903a-6746583208f1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("879de3f9-f723-4be3-a586-2b701a539b29"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("87ff2d83-65ce-40a6-82cb-923c3ccf0610"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("89347670-f793-486c-b128-8f7a2f515ef0"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8bb0c743-e355-45b1-88e6-0fada09890f6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8d603740-7fc0-4e94-b39e-3668dff81a9e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8e4d0088-6115-4b6f-b7ef-d413506538eb"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8e75f834-b4c1-4053-b900-bd66989e0d4b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("8ea09125-f185-4e0b-adda-53297635bbec"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("926f36f4-1699-4101-82c6-4b29aae48f6b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9412d24f-2f65-492c-8849-f476ceec0b87"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("94535c6e-9aac-453a-b685-ca2b99adf6f4"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9569d494-3594-4f37-bc8a-feeb7cdb234f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9821c143-c77b-4e52-be86-a3bc96b0b084"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("98a18796-c791-41c0-b73f-14e7ff602154"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9b8116fe-1a12-482a-b9f2-b08c846c9e89"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9bd2a371-8bad-423f-8dc9-d4c427ff1914"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9c160aff-d5ac-412a-bc6f-2c73eeadb897"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("9d274b06-dea3-4ccb-b6c4-d6827cf61194"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a007ef54-77f0-4308-99a9-35d3f65ea262"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a0aea18f-cb86-4e14-8b4c-ec7f247c30cc"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a13286a4-eb36-4480-89af-0f190977231b"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a30f3e81-db5b-419c-80b2-ffc87cffec56"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a74d2e07-14ce-4fe4-b789-03c42bd2fcc8"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a7709fcc-234e-4e72-8f5d-aa53f4aee520"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a7b4ab35-169d-4d41-a052-908482eef14a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("a9385f40-0c5e-4eaa-9b2b-233bc7ebe1ba"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("aadc86a5-1eba-46af-bdcc-d6cdbbeaa958"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("ae2bdd76-af11-4527-a6ca-2fb63c49a7d1"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("b5bc2d06-302c-4ada-9d2c-7c64493d51e6"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("b8c90299-2bd3-4a09-b132-bdf7b88e72bd"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("be22fa22-51fd-4bef-8666-662fed9e980d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("bf0ef41c-d0e7-465b-bf56-0ace3151b9fe"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("bfa2f600-66b7-4bdd-ab88-2245351fe7aa"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("c1a04e12-c117-434f-98ba-c91ae90f1f08"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("c57f13e4-8066-4eab-ae61-34668e7ddf24"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("c89a781b-a303-4164-a737-9a560a84c5f3"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("c8ff7225-d396-44eb-956f-6b9d2acbbb46"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("ce3b68c8-2180-4f16-b9aa-eada2ba6f69f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("ce69ebc6-8f65-4f84-82d8-92fc1b3e3030"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d1ab2e54-dbfc-4399-a21a-2c4aae9b906d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d21033f7-2d82-4593-8296-aa5fd66fe160"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d3cf379a-9eb2-4892-b43a-089e0bb03431"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d52103d7-f93c-4a76-9914-3b480276ad9f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d5d06f09-da5d-4ba3-b502-0c517a811b13"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("d83d1e6b-6d9f-42e6-9029-845408b7849a"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("db44829e-2df6-4857-bd94-157dfd70dcce"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("dbcbdd74-0304-4004-b240-a9679f3407cb"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("dc9553cb-eaf7-4f84-aeed-cd3c6938a158"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("e5b1c92b-a56a-4f77-ac00-1b10b0d9bab2"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("e5fc984b-a598-4d67-836e-5dcfe975ca3f"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("e7224acb-1c27-4833-b22f-298787c65305"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("e7df452e-f592-4d99-ba1e-385068e6a192"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("e8973d21-c07a-44b5-9fb7-e8250797bc8d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("eda343bc-52b4-48ce-b3f2-5cbd1c7d1f2c"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("f291556f-4885-4ee5-b3e6-2f0c5f504db7"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("f6da68f1-7e76-43cf-986c-802945e91d7e"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("f8a7550f-8a1a-4d34-8fa3-c69d15772c16"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("f9a88a4a-0d39-4d4f-9968-87e28aad1805"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("faca59eb-4f95-4a46-aa76-94a0bb571523"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("fc2a1cba-ac3d-4c2b-a48c-b4e35d4e223d"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("fc6934a6-cb68-4398-ae4c-37efcdf60797"));

            migrationBuilder.DeleteData(
                table: "ProductSubTypeWorkingPeriodSample",
                keyColumn: "Id",
                keyValue: new Guid("fe7f104a-2a93-486e-9ed7-0e8cdb9aeb49"));

            migrationBuilder.DeleteData(
                table: "StageType",
                keyColumn: "Id",
                keyValue: new Guid("0cfbbbf2-bc32-420c-82ab-2a330887f0eb"));

            migrationBuilder.DeleteData(
                table: "StageType",
                keyColumn: "Id",
                keyValue: new Guid("6c294275-9ed7-46bb-b56a-0cdf5e729dfa"));

            migrationBuilder.DeleteData(
                table: "StageType",
                keyColumn: "Id",
                keyValue: new Guid("8216d505-b9c4-42a0-b855-4a649146222e"));

            migrationBuilder.DeleteData(
                table: "StageType",
                keyColumn: "Id",
                keyValue: new Guid("b2a4e3eb-6f8e-4096-a170-58add6332c0c"));

            migrationBuilder.DeleteData(
                table: "StageType",
                keyColumn: "Id",
                keyValue: new Guid("d4f01970-7d11-4a29-9d89-7351f16f3dcb"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("2f76a3c8-8e69-4c7c-a922-ef92b87e36e5"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("2fc07976-bc8c-43b0-aba0-48e3bfb5cbc3"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("37db04d9-37f0-4fbd-9359-8ba5f7354d03"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("50a6096e-585a-4d84-a71f-3d1b7d934774"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("708187ca-51c1-4366-bcb1-a212b6b003d9"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("7d1ab87a-3926-404a-85c3-aaf38f6251f0"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("8a0f97a4-a490-474d-aaba-ef5f5f5e31bb"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("8a4a43c7-6fa1-46c9-9863-ecc296f65b7f"));

            migrationBuilder.DeleteData(
                table: "GroupMaterial",
                keyColumn: "Id",
                keyValue: new Guid("8f2c0980-ac80-4c04-94d7-b8dc3982a77d"));

            migrationBuilder.DeleteData(
                table: "ProductSubType",
                keyColumn: "Id",
                keyValue: new Guid("4373dba9-30ac-4f46-8d51-cd79edd9d649"));

            migrationBuilder.InsertData(
                table: "GroupMaterial",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("28539a79-0747-4d57-9e1e-a2e63adff951"), "Черный металл" },
                    { new Guid("2a62b9a4-d1eb-4bce-b8fb-903736e8ea81"), "ШУ" },
                    { new Guid("46496326-7bc8-4935-8a9c-c319e1ed81e8"), "УПМ" },
                    { new Guid("78b76136-bd49-4a8b-b867-77d16fc98d7a"), "Насосы" },
                    { new Guid("8a01b1ce-4413-4174-b303-c20801699408"), "Арматура" },
                    { new Guid("95d41135-a72b-4386-9734-d579fa0947fe"), "Электрика" },
                    { new Guid("b037956e-2af1-4d26-a2c6-f8b8cd5ce09e"), "Метизы" },
                    { new Guid("d20b1bfc-3752-44b5-b671-448e9308ba54"), "Нержавейка" }
                });
        }
    }
}
