using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories.DQ;

public class DQSrcOrderRepository : IDQSrcOrderRepository
{
    private readonly ProjectDbContext _dbcontext;
    private readonly ILogger<DQSrcOrderRepository> _logger;

    public DQSrcOrderRepository(ProjectDbContext context, ILogger<DQSrcOrderRepository> logger)
    {
        _dbcontext = context;
        _logger = logger;
    }
    public async Task<List<DQSrcOrderEntity>> GetAll()
    {
        return await _dbcontext.DQSrcOrder.AsNoTracking().ToListAsync();
    }

    public async Task Add(DQSrcOrderEntity dqSrcOrder)
    {
        await _dbcontext.AddAsync(dqSrcOrder);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(DQSrcOrderEntity dqSrcOrder)
    {
        var dqSrcOrderEntity = await _dbcontext.DQSrcOrder.FirstOrDefaultAsync(c => c.Id == dqSrcOrder.Id)
            ?? throw new Exception();
        dqSrcOrderEntity.Id = dqSrcOrderEntity.Id;
        dqSrcOrderEntity.OrderName = dqSrcOrderEntity.OrderName;
        dqSrcOrderEntity.Status = dqSrcOrderEntity.Status;
        dqSrcOrderEntity.Contragent = dqSrcOrderEntity.Contragent;
        dqSrcOrderEntity.Dogovor = dqSrcOrderEntity.Dogovor;
        dqSrcOrderEntity.Manager = dqSrcOrderEntity.Manager;
        dqSrcOrderEntity.OrderNumber = dqSrcOrderEntity.OrderNumber;
        dqSrcOrderEntity.OrderDate = dqSrcOrderEntity.OrderDate;
        dqSrcOrderEntity.SchemeDate = dqSrcOrderEntity.SchemeDate;
        dqSrcOrderEntity.LogisticDate = dqSrcOrderEntity.LogisticDate;
        dqSrcOrderEntity.CreateDate = dqSrcOrderEntity.CreateDate;
        dqSrcOrderEntity.PaymentAmount = dqSrcOrderEntity.PaymentAmount;
        dqSrcOrderEntity.PaymentCurrent = dqSrcOrderEntity.PaymentCurrent;
        dqSrcOrderEntity.Qty = dqSrcOrderEntity.Qty;
        dqSrcOrderEntity.LoadStatus = dqSrcOrderEntity.LoadStatus;
        dqSrcOrderEntity.Reason = dqSrcOrderEntity.Reason;
        dqSrcOrderEntity.OrderId = dqSrcOrderEntity.OrderId;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.DQSrcOrder
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}
