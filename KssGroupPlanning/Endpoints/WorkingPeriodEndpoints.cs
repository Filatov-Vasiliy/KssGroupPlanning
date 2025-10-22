using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class WorkingPeriodEndpoints
{
    public static IEndpointRouteBuilder MapWorkingPeriodEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("workingPeriod/", GetAll);
        app.MapGet("workingPeriod/{id::guid}", GetById);
        app.MapGet("workingPeriodByName/{name}", GetByName);
        app.MapGet("workingPeriodByProductId/{id::guid}", GetByProductId);
        app.MapPost("workingPeriod/", Create);
        app.MapPut("workingPeriod/{id::guid}", Update);
        app.MapDelete("workingPeriod/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewWorkingPeriodService workingPeriodService)
    {
        var workingPeriods = await workingPeriodService.GetAll();
        return Results.Ok(workingPeriods);
    }
    private static async Task<IResult> GetById(Guid id, NewWorkingPeriodService workingPeriodService)
    {
        var workingPeriod = await workingPeriodService.GetById(id);
        return Results.Ok(workingPeriod);
    }
    private static async Task<IResult> GetByName(string name, NewWorkingPeriodService workingPeriodService)
    {
        var workingPeriod = await workingPeriodService.GetByName(name);
        return Results.Ok(workingPeriod);
    }
    private static async Task<IResult> GetByProductId(Guid id, NewWorkingPeriodService workingPeriodService)
    {
        var workingPeriod = await workingPeriodService.GetByProductId(id);
        return Results.Ok(workingPeriod);
    }
    private static async Task<IResult> Create(WorkingPeriodEntity request, NewWorkingPeriodService workingPeriodService)
    {
        await workingPeriodService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, WorkingPeriodEntity request, NewWorkingPeriodService workingPeriodService)
    {
        await workingPeriodService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewWorkingPeriodService workingPeriodService)
    {
        await workingPeriodService.Delete(id);
        return Results.Ok();
    }
}
