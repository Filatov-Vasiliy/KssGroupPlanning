using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class ProductSubTypeEndpoints
{
    public static IEndpointRouteBuilder MapProductSubTypeEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("productSubType/", GetAll);
        app.MapGet("productSubType/{id::guid}", GetById);
        app.MapGet("productSubTypeByName/{name}", GetByName);
        app.MapGet("productSubTypeByProductTypeId/{id::guid}", GetByProductTypeId);
        app.MapPost("productSubType/", Create);
        app.MapPut("productSubType/{id::guid}", Update);
        app.MapDelete("productSubType/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetAll();
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetById(Guid id, NewProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetById(id);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetByName(string name, NewProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetByName(name);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetByProductTypeId(Guid id, NewProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetByProductTypeId(id);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> Create(ProductSubTypeEntity request, NewProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, ProductSubTypeEntity request, NewProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Delete(id);
        return Results.Ok();
    }
}
