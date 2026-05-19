using System.Diagnostics;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Services;
using KssGroupPlanning.Services.DQServices;
using KssGroupPlanning.Services.IntergrationServices;


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
        app.MapGet("integration/IntegrationArchive", IntegrationArchive);
        app.MapGet("integration/InsertAllv2", InsertAllv2);

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
    private static async Task<IResult> Insert(LoadSrcService loadSrcService, SrcOrderService srcOrderService,SrcMaterialService srcMaterialService,SrcProductService srcProductService)
    {
        await loadSrcService.TruncateSrc();
        await srcOrderService.InsertOrders(DateOnly.Parse("15.12.2025"),false);
        await srcProductService.InsertProducts(DateOnly.Parse("15.12.2025"), false);
        await srcMaterialService.InsertMaterials(DateOnly.Parse("15.12.2025"), false);
        return Results.Ok();
    }
    private static async Task<IResult> LoadSrcToMain(LoadSrcService loadSrcService, IntegrationService integrationService, SrcOrderService srcOrderService, SrcMaterialService srcMaterialService, SrcProductService srcProductService)
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
    private static async Task<IResult> IntegrationArchive(LoadSrcService loadSrcService)
    {
        await loadSrcService.InsertFilesArchive();
        return Results.Ok();
    }
    private static async Task<IResult> InsertAllv2(SrcOrderService srcOrderService,IntegrationV2Service integrationV2Service)
    {
        await srcOrderService.RemoveDuplicates();
        await integrationV2Service.LoadSrcToMain();
        return Results.Ok();
    }
}
