using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class ProductSubTypeWorkingPeriodSampleEndpoints
{
    public static IEndpointRouteBuilder MapProductSubTypeWorkingPeriodSampleEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("productSubTypeWorkingPeriodSample/", GetAll);
        app.MapGet("productSubTypeWorkingPeriodSample/{id::guid}", GetById);
        app.MapGet("productSubTypeWorkingPeriodSampleByProductSubTypeId/{id::guid}", GetByProductSubTypeId);
        app.MapPost("productSubTypeWorkingPeriodSample/", Create);
        app.MapPut("productSubTypeWorkingPeriodSample/{id::guid}", Update);
        app.MapDelete("productSubTypeWorkingPeriodSample/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        var productSubTypeWorkingPeriodSamples = await productSubTypeWorkingPeriodSampleService.GetAll();
        return Results.Ok(productSubTypeWorkingPeriodSamples);
    }
    private static async Task<IResult> GetById(Guid id, ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        var productSubTypeWorkingPeriodSample = await productSubTypeWorkingPeriodSampleService.GetById(id);
        return Results.Ok(productSubTypeWorkingPeriodSample);
    }
    private static async Task<IResult> GetByProductSubTypeId(Guid id, ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        var productSubTypeWorkingPeriodSample = await productSubTypeWorkingPeriodSampleService.GetByProductSubTypeId(id);
        return Results.Ok(productSubTypeWorkingPeriodSample);
    }
    private static async Task<IResult> Create(ProductSubTypeWorkingPeriodSampleEntity request, ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        await productSubTypeWorkingPeriodSampleService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, ProductSubTypeWorkingPeriodSampleEntity request, ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        await productSubTypeWorkingPeriodSampleService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, ProductSubTypeWorkingPeriodSampleService productSubTypeWorkingPeriodSampleService)
    {
        await productSubTypeWorkingPeriodSampleService.Delete(id);
        return Results.Ok();
    }
}
