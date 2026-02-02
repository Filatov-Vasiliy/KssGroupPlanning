using KssGroupPlanning.Entities;

using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class WorkingPeriodStageMaterialEndpoints
{
    public static IEndpointRouteBuilder MapWorkingPeriodStageMaterialEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("workingPeriodStageMaterial/", GetAll);
        app.MapGet("workingPeriodStageMaterial/{id::guid}", GetById);
        app.MapGet("workingPeriodStageMaterialByProductId/{id::guid}", GetByProductId);
        app.MapGet("workingPeriodStageMaterialByGroupMaterialId/{id::guid}", GetByGroupMaterialId);
        app.MapPost("workingPeriodStageMaterial/", Create);
        app.MapPut("workingPeriodStageMaterial/{id::guid}", Update);
        app.MapDelete("workingPeriodStageMaterial/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        var workingPeriodStageMaterials = await workingPeriodStageMaterialService.GetAll();
        return Results.Ok(workingPeriodStageMaterials);
    }
    private static async Task<IResult> GetById(Guid id, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        var workingPeriodStageMaterial = await workingPeriodStageMaterialService.GetById(id);
        return Results.Ok(workingPeriodStageMaterial);
    }
    private static async Task<IResult> GetByProductId(Guid id, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        var workingPeriodStageMaterial = await workingPeriodStageMaterialService.GetByProductId(id);
        return Results.Ok(workingPeriodStageMaterial);
    }
    private static async Task<IResult> GetByGroupMaterialId(Guid id, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        var workingPeriodStageMaterial = await workingPeriodStageMaterialService.GetByGroupMaterialId(id);
        return Results.Ok(workingPeriodStageMaterial);
    }
    private static async Task<IResult> Create(WorkingPeriodStageMaterialEntity request, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        await workingPeriodStageMaterialService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, WorkingPeriodStageMaterialEntity request, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        await workingPeriodStageMaterialService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, WorkingPeriodStageMaterialService workingPeriodStageMaterialService)
    {
        await workingPeriodStageMaterialService.Delete(id);
        return Results.Ok();
    }
}
