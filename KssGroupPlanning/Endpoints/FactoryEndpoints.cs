using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class FactoryEndpoints
{
    public static IEndpointRouteBuilder MapFactoryEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("factory/", GetAll);
        app.MapGet("factory/{id::guid}", GetById);
        app.MapGet("factoryByName/{name}", GetByName);
        app.MapPost("factory/", Create);
        app.MapPut("factory/{id::guid}", Update);
        app.MapDelete("factory/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewFactoryService factoryService)
    {
        var factorys = await factoryService.GetAll();
        return Results.Ok(factorys);
    }
    private static async Task<IResult> GetById(Guid id, NewFactoryService factoryService)
    {
        var factory = await factoryService.GetById(id);
        return Results.Ok(factory);
    }
    private static async Task<IResult> GetByName(string name, NewFactoryService factoryService)
    {
        var factory = await factoryService.GetByName(name);
        return Results.Ok(factory);
    }
    private static async Task<IResult> Create(FactoryEntity request, NewFactoryService factoryService)
    {
        await factoryService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, FactoryEntity request, NewFactoryService factoryService)
    {
        await factoryService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewFactoryService factoryService)
    {
        await factoryService.Delete(id);
        return Results.Ok();
    }
}
