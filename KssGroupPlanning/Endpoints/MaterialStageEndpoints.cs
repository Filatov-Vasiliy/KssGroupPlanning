using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class MaterialStageEndpoints
{
    public static IEndpointRouteBuilder MapMaterialStageEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("materialStage/", GetAll);
        app.MapGet("materialStage/{id::guid}", GetById);
        app.MapGet("materialStageByGroupMaterialId/{id::guid}", GetByGroupMaterialId);
        app.MapPost("materialStage/", Create);
        app.MapPut("materialStage/{id::guid}", Update);
        app.MapDelete("materialStage/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(MaterialStageService MaterialStageService)
    {
        var MaterialStages = await MaterialStageService.GetAll();
        return Results.Ok(MaterialStages);
    }
    private static async Task<IResult> GetById(Guid id, MaterialStageService materialStageService)
    {
        var materialStage = await materialStageService.GetById(id);
        return Results.Ok(materialStage);
    }
    private static async Task<IResult> GetByGroupMaterialId(Guid id, MaterialStageService materialStageService)
    {
        var materialStage = await materialStageService.GetByGroupMaterialId(id);
        return Results.Ok(materialStage);
    }
    private static async Task<IResult> Create(MaterialStageEntity request, MaterialStageService materialStageService)
    {
        await materialStageService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, MaterialStageEntity request, MaterialStageService materialStageService)
    {
        await materialStageService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, MaterialStageService materialStageService)
    {
        await materialStageService.Delete(id);
        return Results.Ok();
    }
}
