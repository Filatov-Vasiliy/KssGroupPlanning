using KssGroupPlanning.Entities;
using KssGroupPlanning.Services.EntityServices;

namespace KssGroupPlanning.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("order/", GetAll);
        app.MapGet("orderDetailed/", GetAllDetailed);
        app.MapGet("order/{id::guid}", GetById);
        app.MapGet("orderByNumber/{number}", GetByNumber);
        app.MapPost("order/", Create);
        app.MapPut("order/{id::guid}", Update);
        app.MapDelete("order/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(OrderService orderService)
    {
        var orders = await orderService.GetAll();
        return Results.Ok(orders);
    }
    private static async Task<IResult> GetAllDetailed(OrderService orderService)
    {
        var orders = await orderService.GetAllDetailed();
        return Results.Ok(orders);
    }
    private static async Task<IResult> GetById(Guid id, OrderService orderService)
    {
        var order = await orderService.GetById(id);
        return Results.Ok(order);
    }
    private static async Task<IResult> GetByNumber(string number, OrderService orderService)
    {
        var order = await orderService.GetByNumber(number);
        return Results.Ok(order);
    }
    private static async Task<IResult> Create(OrderEntity request, OrderService orderService)
    {
        await orderService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, OrderEntity request, OrderService orderService)
    {
        await orderService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, OrderService orderService)
    {
        await orderService.Delete(id);
        return Results.Ok();
    }
}
