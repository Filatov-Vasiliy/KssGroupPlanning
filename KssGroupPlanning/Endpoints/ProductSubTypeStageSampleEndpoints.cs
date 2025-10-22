using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class ProductSubTypeStageSampleEndpoints
{
    public static IEndpointRouteBuilder MapProductSubTypeStageSampleEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("productSubTypeStageSample/", GetAll);
        app.MapGet("productSubTypeStageSample/{id::guid}", GetById);
        app.MapGet("productSubTypeStageSampleByProductSubTypeId/{id::guid}", GetByProductSubTypeId);
        app.MapGet("productSubTypeStageSampleByMaterialStageId/{id::guid}", GetByMaterialStageId);
        app.MapPost("productSubTypeStageSample/", Create);
        app.MapPut("productSubTypeStageSample/{id::guid}", Update);
        app.MapDelete("productSubTypeStageSample/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        var productSubTypeStageSamples = await productSubTypeStageSampleService.GetAll();
        return Results.Ok(productSubTypeStageSamples);
    }
    private static async Task<IResult> GetById(Guid id, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        var productSubTypeStageSample = await productSubTypeStageSampleService.GetById(id);
        return Results.Ok(productSubTypeStageSample);
    }
    private static async Task<IResult> GetByMaterialStageId(Guid id, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        var productSubTypeStageSample = await productSubTypeStageSampleService.GetByMaterialStageId(id);
        return Results.Ok(productSubTypeStageSample);
    }
    private static async Task<IResult> GetByProductSubTypeId(Guid id, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        var productSubTypeStageSample = await productSubTypeStageSampleService.GetByProductSubTypeId(id);
        return Results.Ok(productSubTypeStageSample);
    }
    private static async Task<IResult> Create(ProductSubTypeStageSampleEntity request, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        await productSubTypeStageSampleService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, ProductSubTypeStageSampleEntity request, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        await productSubTypeStageSampleService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewProductSubTypeStageSampleService productSubTypeStageSampleService)
    {
        await productSubTypeStageSampleService.Delete(id);
        return Results.Ok();
    }
}
