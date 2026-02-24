using System.Text.Json.Serialization;
using KssGroupPlanning.Extentions;
using KssGroupPlanning.Infrastuction.auth;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Interfaces.Infrastruction;
using KssGroupPlanning.Repositories;
using KssGroupPlanning.Services;

using KssGroupPlanning.Services.EntityServices;
using KssGroupPlanning.Services.IntergrationServices;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "KssGroupPlanning");
});

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

services.AddScoped<SrcOrderService>();
services.AddScoped<SrcProductService>();
services.AddScoped<SrcMaterialService>();
services.AddScoped<IntegrationService>();
services.AddTransient<IntegrationJob>();
services.AddScoped<CoreService>(); //?!?!?!?!

services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
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

services.AddDbContext<ProjectDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("ProjectDbContext"));
    });
services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});
// Настройка Quartz
services.AddQuartz(q =>
{    
    var jobKey = new JobKey("IntegrationJob");
    q.AddJob<IntegrationJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("IntegrationJobTrigger")
        .WithCronSchedule("0 0 18 * * ?", x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Etc/GMT-3")))
        .StartNow());
});

services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("===== Приложение запущено в окружении {Environment} =====", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddMappedEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Приложение упало с критической ошибкой");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
