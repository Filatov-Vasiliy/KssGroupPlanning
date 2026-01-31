using KssGroupPlanning.Services.EntityServices;
using static System.Net.Mime.MediaTypeNames;

namespace KssGroupPlanning.Endpoints
{
    public static class WorkingPeriodStageBrigadeRelationEndpoints
    {
        public static IEndpointRouteBuilder MapWorkingPeriodStageBrigadeRelationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("workingPeriodStageBrigadeRelation/", GetAll);
            return app;
        }

        private static async Task<IResult> GetAll(WorkingPeriodStageBrigadeRelationService relationService)
        {
            var relation = await relationService.GetAll();
            return Results.Ok(relation);
        }
    }
}
