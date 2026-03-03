using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Entities.Src;
using KssGroupPlanning.Interfaces.EntityInterfaces.Src;

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
    public async Task RemoveDuplicates()
    {
        var orders = await _dbcontext.SrcOrder.ToListAsync();
        var duplicates =  orders.GroupBy(p => p.OrderNumber).Where(g => g.Count() > 1).SelectMany(g => g.Skip(1)).ToList();
        if (duplicates.Any())
        {
            _dbcontext.SrcOrder.RemoveRange(duplicates);
            _dbcontext.SaveChanges();
        }
    }
    public async Task TruncateTable()
    {
        await _dbcontext.SrcOrder.ExecuteDeleteAsync();
    }
}