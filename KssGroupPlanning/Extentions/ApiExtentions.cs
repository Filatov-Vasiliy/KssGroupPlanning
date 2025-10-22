using KssGroupPlanning.Endpoints;

namespace KssGroupPlanning.Extentions;

public static class ApiExtentions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoints();
        app.MapProductTypeEndpoints();
        app.MapBrigadeEndpoints();
        app.MapFactoryEndpoints();
        app.MapGroupMaterialEndpoints();
        app.MapMaterialStageEndpoints();
        app.MapOrderEndpoints();
        app.MapProductEndpoints();
        app.MapProductSubTypeEndpoints();
        app.MapProductSubTypeStageSampleEndpoints();
        app.MapProductSubTypeWorkingPeriodSampleEndpoints();
        app.MapStageEndpoints();
        app.MapStageTypeEndpoints();
        app.MapWorkingPeriodEndpoints();
        app.MapWorkingPeriodStageEndpoints();
        app.MapWorkingPeriodStageMaterialEndpoints();

    }
}
