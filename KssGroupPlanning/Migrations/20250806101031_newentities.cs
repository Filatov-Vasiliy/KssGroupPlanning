using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KssGroupPlanning.Migrations
{
    /// <inheritdoc />
    public partial class newentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "CourseEntityStudentEntity");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    qty = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrcOrders", x => x.OrderName);
                });

            migrationBuilder.CreateTable(
                name: "StageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProductTypeEntityId = table.Column<int>(type: "integer", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: true),
                    ProductSubTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProductSubTypeEntityId = table.Column<int>(type: "integer", nullable: true),
                    FactoryId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_Products_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductSubTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProductSubTypeEntityId = table.Column<int>(type: "integer", nullable: true),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductSubTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProductSubTypeEntityId = table.Column<int>(type: "integer", nullable: true),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    WorkingPeriodName = table.Column<string>(type: "text", nullable: false),
                    StandartTime = table.Column<string>(type: "text", nullable: false),
                    StandartEmployee = table.Column<int>(type: "integer", nullable: false),
                    StageTypeId = table.Column<int>(type: "integer", nullable: false),
                    StageTypeEntityId = table.Column<int>(type: "integer", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriods_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPeriodStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkingPeriodId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Recycling = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ProductSubTypeWorkingPeriodsSampleId = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_Products_FactoryId",
                table: "Products",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

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
                name: "IX_WorkingPeriods_ProductId",
                table: "WorkingPeriods",
                column: "ProductId");

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

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LessonText = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEntityStudentEntity",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    StudentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEntityStudentEntity", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseEntityStudentEntity_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEntityStudentEntity_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CourseId",
                table: "Authors",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntityStudentEntity_StudentsId",
                table: "CourseEntityStudentEntity",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");
        }
    }
}
