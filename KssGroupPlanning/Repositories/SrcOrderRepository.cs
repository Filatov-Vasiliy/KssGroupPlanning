using KssGroupPlanning.Entities;

using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Infrastuction.Db;

public class SrcOrderRepository(ProjectDbContext context) : ISrcOrderRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(List<SrcOrderEntity> srcOrders)
    {
        foreach (SrcOrderEntity srcOrder in srcOrders)
        {

            var srcOrderEntity = new SrcOrderEntity
            {
                OrderName = srcOrder.OrderName,
                Status = srcOrder.Status,
                Contragent = srcOrder.Contragent,
                Dogovor = srcOrder.Dogovor,
                Manager = srcOrder.Manager,
                OrderNumber = srcOrder.OrderNumber,
                OrderDate = srcOrder.OrderDate,
                SchemeDate = srcOrder.SchemeDate, 
                LogisticDate = srcOrder.LogisticDate,
                CreateDate = srcOrder.CreateDate,
                PaymentAmount = srcOrder.PaymentAmount,
                PaymentCurrent = srcOrder.PaymentCurrent,
                Qty = srcOrder.Qty
            };
            await _dbcontext.SrcOrder.AddAsync(srcOrderEntity);
        }
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<SrcOrderEntity>> GetSrcOrders()
    {
        return await _dbcontext.SrcOrder.AsNoTracking().ToListAsync();
    }
}