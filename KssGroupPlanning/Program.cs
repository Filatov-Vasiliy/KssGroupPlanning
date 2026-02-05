using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using KssGroupPlanning.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using KssGroupPlanning.Extentions;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Infrastruction;
using KssGroupPlanning.Services.EntityServices;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Infrastuction.auth;
using KssGroupPlanning.Infrastuction.Db;
using System.Text.Json.Serialization;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// Add services to the container.
var configuration = builder.Configuration;
/*
static void orderScvReader()
{
    List<string> badRecord = new List<string>();
    var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" , BadDataFound = context => badRecord.Add(context.RawRecord) };
    using (var streamReader = new StreamReader(@"./csv/Order.xls"))
    {
        using (var csvReader = new CsvReader(streamReader, config))
        {
            var records = csvReader.GetRecords<SrcOrderEntity>().ToList();
        }
    }
}
orderScvReader();
*/
//services.AddTransient<ExceptionHandlerMiddleware>();

services.AddScoped<UserService>();
services.AddScoped<ProductTypeService>();
services.AddScoped<BrigadeService>();
services.AddScoped<FactoryService>();
services.AddScoped<GroupMaterialService>();
services.AddScoped<MaterialStageService>();
services.AddScoped<OrderService>();
services.AddScoped<ProductService>();
services.AddScoped<ProductSubTypeGroupMaterialRelationService>();
services.AddScoped<ProductSubTypeService>();
services.AddScoped<ProductSubTypeStageSampleService>();
services.AddScoped<ProductSubTypeWorkingPeriodSampleService>();
services.AddScoped<ProductTypeService>();
services.AddScoped<StageService>();
services.AddScoped<StageTypeService>();
services.AddScoped<WorkingPeriodRelationService>();
services.AddScoped<WorkingPeriodService>();
services.AddScoped<WorkingPeriodStageBrigadeRelationService>();
services.AddScoped<WorkingPeriodStageMaterialService>();
services.AddScoped<WorkingPeriodStageService>();
services.AddScoped<WorkingPeriodStageTypeRelationService>();

services.AddScoped<CoreService>(); //?!?!?!?!

services.AddScoped<IProductTypeRepository,ProductTypeRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IBrigadeRepository, BrigadeRepository>();
services.AddScoped<IFactoryRepository, FactoryRepository>();
services.AddScoped<IGroupMaterialRepository, GroupMaterialRepository>();
services.AddScoped<IMaterialStageRepository, MaterialStageRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductSubTypeGroupMaterialRelationRepository, ProductSubTypeGroupMaterialRelationRepository>();
services.AddScoped<IProductSubTypeRepository, ProductSubTypeRepository>();
services.AddScoped<IProductSubTypeStageSampleRepository, ProductSubTypeStageSampleRepository>();
services.AddScoped<IProductSubTypeWorkingPeriodSampleRepository, ProductSubTypeWorkingPeriodSampleRepository>();
services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
services.AddScoped<IStageRepository, StageRepository>();
services.AddScoped<IStageTypeRepository, StageTypeRepository>();
services.AddScoped<IWorkingPeriodRelationRepository, WorkingPeriodRelationRepository>();
services.AddScoped<IWorkingPeriodRepository, WorkingPeriodRepository>();
services.AddScoped<IWorkingPeriodStageBrigadeRelationRepository, WorkingPeriodStageBrigadeRelationRepository>();
services.AddScoped<IWorkingPeriodStageMaterialRepository, WorkingPeriodStageMaterialRepository>();
services.AddScoped<IWorkingPeriodStageRepository, WorkingPeriodStageRepository>();
services.AddScoped<IWorkingPeriodStageTypeRelationRepository, NewWorkingPeriodStageTypeRelationRepository>();
services.AddScoped<ISrcOrderRepository, SrcOrderRepository>();
services.AddScoped<ISrcProductRepository, SrcProductRepository>();
services.AddScoped<ISrcMaterialRepository, SrcMaterialRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

services.AddDbContext<ProjectDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("ProjectDbContext"));
    });
services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    // Optional: Add other options as needed, e.g.,
    options.SerializerOptions.WriteIndented = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.AddMappedEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
