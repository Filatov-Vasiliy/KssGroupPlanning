using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

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
    private static async Task<IResult> GetAll(ProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetAll();
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetById(Guid id, ProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetById(id);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetByName(string name, ProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetByName(name);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> GetByProductTypeId(Guid id, ProductSubTypeService productSubTypeService)
    {
        var productSubType = await productSubTypeService.GetByProductTypeId(id);
        return Results.Ok(productSubType);
    }
    private static async Task<IResult> Create(ProductSubTypeEntity request, ProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, ProductSubTypeEntity request, ProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, ProductSubTypeService productSubTypeService)
    {
        await productSubTypeService.Delete(id);
        return Results.Ok();
    }
}
