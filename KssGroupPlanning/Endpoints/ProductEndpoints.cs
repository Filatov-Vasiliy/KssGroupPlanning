using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("product/", GetAll);
        app.MapGet("product/{id::guid}", GetById);
        app.MapGet("productByNumber/{number}", GetByNumber);
        app.MapGet("productByOrderId/{id::guid}", GetByOrderId);
        app.MapGet("productByFactoryId/{id::guid}", GetByFactoryId);
        app.MapGet("productByProductSubTypeId/{id::guid}", GetByProductSubTypeId);
        app.MapPost("product/", Create);
        app.MapPut("product/{id::guid}", Update);
        app.MapDelete("product/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewProductService productService)
    {
        var products = await productService.GetAll();
        return Results.Ok(products);
    }
    private static async Task<IResult> GetById(Guid id, NewProductService productService)
    {
        var product = await productService.GetById(id);
        return Results.Ok(product);
    }
    private static async Task<IResult> GetByNumber(string number, NewProductService productService)
    {
        var product = await productService.GetByNumber(number);
        return Results.Ok(product);
    }
    private static async Task<IResult> GetByProductSubTypeId(Guid id, NewProductService productService)
    {
        var product = await productService.GetByProductSubTypeId(id);
        return Results.Ok(product);
    }
    private static async Task<IResult> GetByFactoryId(Guid id, NewProductService productService)
    {
        var product = await productService.GetByFactoryId(id);
        return Results.Ok(product);
    }
    private static async Task<IResult> GetByOrderId(Guid id, NewProductService productService)
    {
        var product = await productService.GetByOrderId(id);
        return Results.Ok(product);
    }
    private static async Task<IResult> Create(ProductEntity request, NewProductService productService)
    {
        await productService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, ProductEntity request, NewProductService productService)
    {
        await productService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewProductService productService)
    {
        await productService.Delete(id);
        return Results.Ok();
    }
}
