using KssGroupPlanning.Entities;

using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class StageTypeEndpoints
{
    public static IEndpointRouteBuilder MapStageTypeEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("stageType/", GetAll);
        app.MapGet("stageType/{id::guid}", GetById);
        app.MapGet("stageTypeByName/{name}", GetByName);
        app.MapPost("stageType/", Create);
        app.MapPut("stageType/{id::guid}", Update);
        app.MapDelete("stageType/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(StageTypeService stageTypeService)
    {
        var stageTypes = await stageTypeService.GetAll();
        return Results.Ok(stageTypes);
    }
    private static async Task<IResult> GetById(Guid id, StageTypeService stageTypeService)
    {
        var stageType = await stageTypeService.GetById(id);
        return Results.Ok(stageType);
    }
    private static async Task<IResult> GetByName(string name, StageTypeService stageTypeService)
    {
        var stageType = await stageTypeService.GetByName(name);
        return Results.Ok(stageType);
    }
    private static async Task<IResult> Create(StageTypeEntity request, StageTypeService stageTypeService)
    {
        await stageTypeService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, StageTypeEntity request, StageTypeService stageTypeService)
    {
        await stageTypeService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, StageTypeService stageTypeService)
    {
        await stageTypeService.Delete(id);
        return Results.Ok();
    }
}
