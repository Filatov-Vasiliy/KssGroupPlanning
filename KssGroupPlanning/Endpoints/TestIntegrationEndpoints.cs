using KssGroupPlanning.Entities;
using KssGroupPlanning.Services;
using KssGroupPlanning.Services.IntergrationServices;
using System.Diagnostics;


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
        app.MapGet("integration/LoadSrcToTest", LoadSrcToMain);

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
    private static async Task<IResult> LoadSrcToMain(IntegrationService integrationService, SrcOrderService srcOrderService, SrcMaterialService srcMaterialService, SrcProductService srcProductService)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        await srcOrderService.RemoveDuplicates();
        await srcProductService.RemoveDuplicates();
        await srcMaterialService.RemoveDuplicates();
        await integrationService.LoadSrcToMain();
        stopwatch.Stop();
        return Results.Ok(stopwatch.ElapsedMilliseconds);
    }
}
