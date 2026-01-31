using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class GroupMaterialEndpoints
{
    public static IEndpointRouteBuilder MapGroupMaterialEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapPost("register", Register);
        //app.MapPost("login", Login);
        app.MapGet("groupMaterial/", GetAll);
        app.MapGet("groupMaterial/{id::guid}", GetById);
        app.MapGet("groupMaterialByName/{name}", GetByName);
        app.MapPost("groupMaterial/", Create);
        app.MapPut("groupMaterial/{id::guid}", Update);
        app.MapDelete("groupMaterial/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(GroupMaterialService groupMaterialService)
    {
        var groupMaterials = await groupMaterialService.GetAll();
        return Results.Ok(groupMaterials);
    }
    private static async Task<IResult> GetById(Guid id, GroupMaterialService groupMaterialService)
    {
        var groupMaterial = await groupMaterialService.GetById(id);
        return Results.Ok(groupMaterial);
    }
    private static async Task<IResult> GetByName(string name, GroupMaterialService groupMaterialService)
    {
        var groupMaterial = await groupMaterialService.GetByName(name);
        return Results.Ok(groupMaterial);
    }
    private static async Task<IResult> Create(GroupMaterialEntity request, GroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, GroupMaterialEntity request, GroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, GroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Delete(id);
        return Results.Ok();
    }
}
