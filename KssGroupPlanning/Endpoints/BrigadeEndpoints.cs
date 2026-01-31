using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

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
    private static async Task<IResult> GetAll(BrigadeService brigadeService)
    {
        var Brigades = await brigadeService.GetAll();
        return Results.Ok(Brigades);
    }
    private static async Task<IResult> GetById(Guid id, BrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetById(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> GetByStageTypeId(Guid id, BrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetByStageTypeId(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> GetByFactoryId(Guid id, BrigadeService brigadeService)
    {
        var Brigade = await brigadeService.GetByFactoryId(id);
        return Results.Ok(Brigade);
    }
    private static async Task<IResult> Create(BrigadeEntity request, BrigadeService brigadeService)
    {
        await brigadeService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, BrigadeEntity request, BrigadeService brigadeService)
    {
        await brigadeService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, BrigadeService brigadeService)
    {
        await brigadeService.Delete(id);
        return Results.Ok();
    }
}
