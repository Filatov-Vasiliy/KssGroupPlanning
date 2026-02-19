using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;

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
    public async Task<List<OrderEntity>> GetAll()
    {
        return await _dbcontext.Order.AsNoTracking().OrderBy(c => c.Number).ToListAsync();

    }
    public async Task<List<OrderEntity>> GetAllDetailed()
    {
        return await _dbcontext.Order.AsNoTracking().Include(o => o.Products).ThenInclude(p => p.Stages).Include(o => o.Products).ThenInclude(p1 => p1.ProductSubType).ThenInclude(pst => pst.ProductType).Include(o => o.Products).ThenInclude(p => p.WorkingPeriods).ThenInclude(p => p.WorkingPeriodStages).OrderBy(c => c.Number).ToListAsync();
        //мне тогда надо Order+product + product sub type + productType + stage + workingPeriod + ... + workingPeriodStage
    }

    public async Task<OrderEntity?> GetById(Guid id)
    {

        return await _dbcontext.Order.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<OrderEntity?> GetByNumber(string number)
    {

        return await _dbcontext.Order.AsNoTracking().FirstOrDefaultAsync(c => c.Number == number);
    }
    public async Task Add(OrderEntity order)
    {
        await _dbcontext.AddAsync(order);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(OrderEntity order)
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