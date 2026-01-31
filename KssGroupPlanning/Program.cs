using KssGroupPlanning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using KssGroupPlanning.Repositories;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using KssGroupPlanning.Extentions;
using KssGroupPlanning.Services;
using KssGroupPlanning.Entities;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// Add services to the container.
var configuration = builder.Configuration;
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
//services.AddTransient<ExceptionHandlerMiddleware>();

services.AddScoped<UserService>();
services.AddScoped<ProductTypeService>();
services.AddScoped<NewBrigadeService>();
services.AddScoped<NewFactoryService>();
services.AddScoped<NewGroupMaterialService>();
services.AddScoped<NewMaterialStageService>();
services.AddScoped<NewOrderService>();
services.AddScoped<NewProductService>();
services.AddScoped<NewProductSubTypeGroupMaterialRelationService>();
services.AddScoped<NewProductSubTypeService>();
services.AddScoped<NewProductSubTypeStageSampleService>();
services.AddScoped<NewProductSubTypeWorkingPeriodSampleService>();
services.AddScoped<NewProductTypeService>();
services.AddScoped<NewStageService>();
services.AddScoped<NewStageTypeService>();
services.AddScoped<NewWorkingPeriodRelationService>();
services.AddScoped<NewWorkingPeriodService>();
services.AddScoped<NewWorkingPeriodStageBrigadeRelationService>();
services.AddScoped<NewWorkingPeriodStageMaterialService>();
services.AddScoped<NewWorkingPeriodStageService>();
services.AddScoped<NewWorkingPeriodStageTypeRelationService>();

services.AddScoped<CoreService>(); //?!?!?!?!

services.AddScoped<IProductTypeRepository,ProductTypeRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<INewBrigadeRepository, NewBrigadeRepository>();
services.AddScoped<INewFactoryRepository, NewFactoryRepository>();
services.AddScoped<INewGroupMaterialRepository, NewGroupMaterialRepository>();
services.AddScoped<INewMaterialStageRepository, NewMaterialStageRepository>();
services.AddScoped<INewOrderRepository, NewOrderRepository>();
services.AddScoped<INewProductRepository, NewProductRepository>();
services.AddScoped<INewProductSubTypeGroupMaterialRelationRepository, NewProductSubTypeGroupMaterialRelationRepository>();
services.AddScoped<INewProductSubTypeRepository, NewProductSubTypeRepository>();
services.AddScoped<INewProductSubTypeStageSampleRepository, NewProductSubTypeStageSampleRepository>();
services.AddScoped<INewProductSubTypeWorkingPeriodSampleRepository, NewProductSubTypeWorkingPeriodSampleRepository>();
services.AddScoped<INewProductTypeRepository, NewProductTypeRepository>();
services.AddScoped<INewStageRepository, NewStageRepository>();
services.AddScoped<INewStageTypeRepository, NewStageTypeRepository>();
services.AddScoped<INewWorkingPeriodRelationRepository, NewWorkingPeriodRelationRepository>();
services.AddScoped<INewWorkingPeriodRepository, NewWorkingPeriodRepository>();
services.AddScoped<INewWorkingPeriodStageBrigadeRelationRepository, NewWorkingPeriodStageBrigadeRelationRepository>();
services.AddScoped<INewWorkingPeriodStageMaterialRepository, NewWorkingPeriodStageMaterialRepository>();
services.AddScoped<INewWorkingPeriodStageRepository, NewWorkingPeriodStageRepository>();
services.AddScoped<INewWorkingPeriodStageTypeRelationRepository, NewWorkingPeriodStageTypeRelationRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

services.AddDbContext<ProjectDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("ProjectDbContext"));
    });
services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
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
