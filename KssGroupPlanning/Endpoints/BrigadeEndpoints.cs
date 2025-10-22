using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class BrigadeEndpoints
{
    public static IEndpointRouteBuilder MapBrigadeEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("brigade/", GetAll);
        app.MapGet("brigade/{id::guid}", GetById);
        app.MapGet("brigadeByStageTypeId/{id::guid}", GetByStageTypeId);
        app.MapGet("brigadeByFactoryId/{id::guid}", GetByFactoryId);
        app.MapPost("brigade/", Create);
        app.MapPut("brigade/{id::guid}", Update);
        app.MapDelete("brigade/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewBrigadeService brigadeService)
    {
        var Brigades = await brigadeService.GetAll();
        return Results.Ok(Brigades);
    }
    private static async Task<IResult> GetById(Guid id, NewBrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetById(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> GetByStageTypeId(Guid id, NewBrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetByStageTypeId(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> GetByFactoryId(Guid id, NewBrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetByFactoryId(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> Create(BrigadeEntity request, NewBrigadeService brigadeService)
    {
        await brigadeService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, BrigadeEntity request, NewBrigadeService brigadeService)
    {
        await brigadeService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewBrigadeService brigadeService)
    {
        await brigadeService.Delete(id);
        return Results.Ok();
    }
}
