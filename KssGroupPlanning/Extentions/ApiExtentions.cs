using KssGroupPlanning.Endpoints;

namespace KssGroupPlanning.Extentions;

public static class ApiExtentions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoints();
        app.MapProductTypesEndpoints();
    }
}
