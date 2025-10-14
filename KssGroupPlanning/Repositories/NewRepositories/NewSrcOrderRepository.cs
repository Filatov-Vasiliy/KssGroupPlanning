using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning;
using Microsoft.EntityFrameworkCore;

public class NewSrcOrderRepository(ProjectDbContext context) : INewSrcOrderRepository
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

    public async Task<UserEntity> GetByEmail(string email)
    {
        return await _dbcontext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
    }

    public async Task<List<SrcOrderEntity>> GetSrcOrders()
    {
        return await _dbcontext.SrcOrder.AsNoTracking().ToListAsync();
    }
}