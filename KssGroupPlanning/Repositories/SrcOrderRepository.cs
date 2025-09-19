using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning;
using Microsoft.EntityFrameworkCore;

public class SrcOrderRepository(ProjectDbContext context) : ISrcOrderRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(List<SrcOrder> srcOrders)
    {
        foreach (SrcOrder srcOrder in srcOrders)
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

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbcontext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
        return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);
    }

    public async Task<List<SrcOrder>> GetSrcOrders()
    {
        var srcOrderEntities = await _dbcontext.SrcOrder.AsNoTracking().ToListAsync();
        List<SrcOrder> srcOrders = new List<SrcOrder>();
        foreach (var srcOrderEntity in srcOrderEntities) 
        {
            srcOrders.Add(SrcOrder.Create(srcOrderEntity.OrderName, srcOrderEntity.Status, srcOrderEntity.Contragent, srcOrderEntity.Dogovor, srcOrderEntity.Manager, srcOrderEntity.OrderNumber, srcOrderEntity.OrderDate, srcOrderEntity.SchemeDate, srcOrderEntity.LogisticDate, srcOrderEntity.CreateDate, srcOrderEntity.PaymentAmount, srcOrderEntity.PaymentCurrent, srcOrderEntity.Qty));
        }
        return srcOrders;
    }
}