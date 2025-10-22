using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class WorkingPeriodStageEndpoints
{
    public static IEndpointRouteBuilder MapWorkingPeriodStageEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("workingPeriodStage/", GetAll);
        app.MapGet("workingPeriodStage/{id::guid}", GetById);
        app.MapGet("workingPeriodStageByWorkingPeriodId/{id::guid}", GetByWorkingPeriodId);
        app.MapGet("workingPeriodStageByProductSubTypeWorkingPeriodSampleId/{id::guid}", GetByProductSubTypeWorkingPeriodSampleId);
        app.MapPost("workingPeriodStage/", Create);
        app.MapPut("workingPeriodStage/{id::guid}", Update);
        app.MapDelete("workingPeriodStage/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewWorkingPeriodStageService workingPeriodStageService)
    {
        var workingPeriodStages = await workingPeriodStageService.GetAll();
        return Results.Ok(workingPeriodStages);
    }
    private static async Task<IResult> GetById(Guid id, NewWorkingPeriodStageService workingPeriodStageService)
    {
        var workingPeriodStage = await workingPeriodStageService.GetById(id);
        return Results.Ok(workingPeriodStage);
    }
    private static async Task<IResult> GetByWorkingPeriodId(Guid id, NewWorkingPeriodStageService workingPeriodStageService)
    {
        var workingPeriodStage = await workingPeriodStageService.GetByWorkingPeriodId(id);
        return Results.Ok(workingPeriodStage);
    }
    private static async Task<IResult> GetByProductSubTypeWorkingPeriodSampleId(Guid id, NewWorkingPeriodStageService workingPeriodStageService)
    {
        var workingPeriodStage = await workingPeriodStageService.GetByProductSubTypeWorkingPeriodSampleId(id);
        return Results.Ok(workingPeriodStage);
    }
    private static async Task<IResult> Create(WorkingPeriodStageEntity request, NewWorkingPeriodStageService workingPeriodStageService)
    {
        await workingPeriodStageService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, WorkingPeriodStageEntity request, NewWorkingPeriodStageService workingPeriodStageService)
    {
        await workingPeriodStageService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewWorkingPeriodStageService workingPeriodStageService)
    {
        await workingPeriodStageService.Delete(id);
        return Results.Ok();
    }
}
