using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Services;

namespace KssGroupPlanning.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("order/", GetAll);
        app.MapGet("order/{id::guid}", GetById);
        app.MapGet("orderByNumber/{number}", GetByNumber);
        app.MapPost("order/", Create);
        app.MapPut("order/{id::guid}", Update);
        app.MapDelete("order/{id:guid}", Delete);
        return app;
    }
    private static async Task<IResult> GetAll(NewOrderService orderService)
    {
        var orders = await orderService.GetAll();
        return Results.Ok(orders);
    }
    private static async Task<IResult> GetById(Guid id, NewOrderService orderService)
    {
        var order = await orderService.GetById(id);
        return Results.Ok(order);
    }
    private static async Task<IResult> GetByNumber(string number, NewOrderService orderService)
    {
        var order = await orderService.GetByNumber(number);
        return Results.Ok(order);
    }
    private static async Task<IResult> Create(OrderEntity request, NewOrderService orderService)
    {
        await orderService.Add(request);
        return Results.Ok();
    }
    private static async Task<IResult> Update(Guid id, OrderEntity request, NewOrderService orderService)
    {
        await orderService.Update(request);
        return Results.Ok();
    }
    private static async Task<IResult> Delete(Guid id, NewOrderService orderService)
    {
        await orderService.Delete(id);
        return Results.Ok();
    }
}
