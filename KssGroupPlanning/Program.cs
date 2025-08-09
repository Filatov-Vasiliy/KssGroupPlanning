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


services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

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
