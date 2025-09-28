using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ProjectDbContext _dbcontext;

    public OrderRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<Order>> GetAll()
    {
        var orderEntities = await _dbcontext.Order.AsNoTracking().OrderBy(c => c.Number).ToListAsync();
        List<Order> orders = new List<Order>();
        foreach (var orderEntity in orderEntities)
        {
            orders.Add(Order.Create(orderEntity.Id, orderEntity.Number, orderEntity.Manager, orderEntity.Contragent, orderEntity.PaymentAmount, orderEntity.PaymentCurrent, orderEntity.Status, orderEntity.CreateTime, orderEntity.UpdateTime));
        }
        return orders;
    }

    public async Task<Order?> GetById(Guid id)
    {

        var orderEntity = await _dbcontext.Order.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return Order.Create(orderEntity.Id, orderEntity.Number, orderEntity.Manager, orderEntity.Contragent, orderEntity.PaymentAmount,orderEntity.PaymentCurrent,orderEntity.Status,orderEntity.CreateTime,orderEntity.UpdateTime);
    }
    public async Task<Order?> GetByNumber(string number)
    {

        var orderEntity = await _dbcontext.Order.AsNoTracking().FirstOrDefaultAsync(c => c.Number == number);
        return Order.Create(orderEntity.Id, orderEntity.Number, orderEntity.Manager, orderEntity.Contragent, orderEntity.PaymentAmount, orderEntity.PaymentCurrent, orderEntity.Status, orderEntity.CreateTime, orderEntity.UpdateTime);
    }
    public async Task Add(Order order)
    {
        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            Number = order.Number,
            Contragent = order.Contragent,
            PaymentAmount = order.PaymentAmount,
            PaymentCurrent = order.PaymentCurrent,
            Status = order.Status,
            CreateTime = order.CreateTime,
            UpdateTime = order.UpdateTime
        };
        await _dbcontext.AddAsync(orderEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Order order)
    {
        var orderEntity = await _dbcontext.Order.FirstOrDefaultAsync(c => c.Id == order.Id)
            ?? throw new Exception();
        orderEntity.Id = order.Id;
        orderEntity.Number = order.Number;
        orderEntity.Contragent = order.Contragent;
        orderEntity.PaymentAmount = order.PaymentAmount;
        orderEntity.PaymentCurrent = order.PaymentCurrent;
        orderEntity.Status = order.Status;
        orderEntity.CreateTime = order.CreateTime;
        orderEntity.UpdateTime = order.UpdateTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.Order
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}