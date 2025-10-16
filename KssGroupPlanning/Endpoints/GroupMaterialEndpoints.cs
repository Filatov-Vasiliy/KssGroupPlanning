using KssGroupPlanning.Contracts.ProductTypes;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class GroupMaterialEndpoints
{
    public static IEndpointRouteBuilder MapGroupMaterialEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("productType/", GetAll);
        app.MapGet("productType/{id::guid}", GetById);
        app.MapGet("productTypeByName/{name}", GetByName);
        app.MapPost("productType/", Create);
        app.MapPut("productType/{id::guid}", Update);
        app.MapDelete("productType/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(ProductTypeService productTypeService)
    {
        var productTypes = await productTypeService.GetAll();
        return Results.Ok(productTypes);
    }
    private static async Task<IResult> GetById(Guid id, ProductTypeService productTypeService)
    {
        var productType = await productTypeService.GetById(id);
        return Results.Ok(productType);
    }
    private static async Task<IResult> GetByName(string name, ProductTypeService productTypeService)
    {
        var productType = await productTypeService.GetByName(name);
        return Results.Ok(productType);
    }
    private static async Task<IResult> Create(CreateProductType request, ProductTypeService productTypeService)
    {
        await productTypeService.Add(request.Name);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, UpdateProductType request, ProductTypeService productTypeService)
    {
        await productTypeService.Update(ProductType.Create(id, request.Name));
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, ProductTypeService productTypeService)
    {
        await productTypeService.Delete(id);
        return Results.Ok();
    }
}
