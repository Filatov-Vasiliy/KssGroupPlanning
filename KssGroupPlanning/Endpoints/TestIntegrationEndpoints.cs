using KssGroupPlanning.Entities;
using KssGroupPlanning.Services;


namespace KssGroupPlanning.Endpoints;

public static class TestIntegrationEndpoints
{
    public static IEndpointRouteBuilder MapTestIntegrationEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("integration/getAllOrders", GetAllOrders);
        app.MapGet("integration/getAllProducts", GetAllProducts);
        app.MapGet("integration/getAllMaterials", GetAllMaterials);
        app.MapGet("integration/InsertAll", Insert);
        //app.MapGet("brigadeByName/{name::string}", GetByName);
        //app.MapPost("brigade/", Create);
        return app;
    }
    private static async Task<IResult> GetAllOrders(SrcOrderService srcOrderService)
    {
        var data = await srcOrderService.GetAll();
        return Results.Ok(data);
    }
    private static async Task<IResult> GetAllProducts(SrcProductService srcProductService)
    {
        var data = await srcProductService.GetAll();
        return Results.Ok(data);
    }
    private static async Task<IResult> GetAllMaterials(SrcMaterialService srcMaterialService)
    {
        var data = await srcMaterialService.GetAll();
        return Results.Ok(data);
    }
    private static async Task<IResult> Insert(SrcOrderService srcOrderService,SrcMaterialService srcMaterialService,SrcProductService srcProductService)
    {
        await srcOrderService.InsertOrders();
        await srcProductService.InsertProducts();
        await srcMaterialService.InsertMaterials();
        return Results.Ok();
    }
}
