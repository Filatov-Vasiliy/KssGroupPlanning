using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

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
    private static async Task<IResult> GetAll(NewGroupMaterialService groupMaterialService)
    {
        var groupMaterials = await groupMaterialService.GetAll();
        return Results.Ok(groupMaterials);
    }
    private static async Task<IResult> GetById(Guid id, NewGroupMaterialService groupMaterialService)
    {
        var groupMaterial = await groupMaterialService.GetById(id);
        return Results.Ok(groupMaterial);
    }
    private static async Task<IResult> GetByName(string name, NewGroupMaterialService groupMaterialService)
    {
        var groupMaterial = await groupMaterialService.GetByName(name);
        return Results.Ok(groupMaterial);
    }
    private static async Task<IResult> Create(GroupMaterialEntity request, NewGroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, GroupMaterialEntity request, NewGroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewGroupMaterialService groupMaterialService)
    {
        await groupMaterialService.Delete(id);
        return Results.Ok();
    }
}
