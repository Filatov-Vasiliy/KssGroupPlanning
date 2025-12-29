using KssGroupPlanning.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KssGroupPlanning.Endpoints
{
    public static class CoreTestEndpoints
    {
        public static IEndpointRouteBuilder MapCoreEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("coreTest/", Test);
            return app;
        }
        private static async Task<IResult> Test(CoreService coreService)
        {
            await coreService.PlanProductionAsync();
            return Results.Ok();
        }
    }
}
