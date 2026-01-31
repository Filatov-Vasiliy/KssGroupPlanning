using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class StageEndpoints
{
    public static IEndpointRouteBuilder MapStageEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("stage/", GetAll);
        app.MapGet("stage/{id::guid}", GetById);
        app.MapGet("stageByProductId/{id::guid}", GetByProductId);
        app.MapPost("stage/", Create);
        app.MapPut("stage/{id::guid}", Update);
        app.MapDelete("stage/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(StageService stageService)
    {
        var stages = await stageService.GetAll();
        return Results.Ok(stages);
    }
    private static async Task<IResult> GetById(Guid id, StageService stageService)
    {
        var stage = await stageService.GetById(id);
        return Results.Ok(stage);
    }
    private static async Task<IResult> GetByProductId(Guid id, StageService stageService)
    {
        var stage = await stageService.GetByProductId(id);
        return Results.Ok(stage);
    }
    private static async Task<IResult> Create(StageEntity request, StageService stageService)
    {
        await stageService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, StageEntity request, StageService stageService)
    {
        await stageService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, StageService stageService)
    {
        await stageService.Delete(id);
        return Results.Ok();
    }
}
