using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories.DQ;

public class DQSrcProductRepository : IDQSrcProductRepository
{
    private readonly ProjectDbContext _dbcontext;
    private readonly ILogger<DQSrcProductRepository> _logger;

    public DQSrcProductRepository(ProjectDbContext context, ILogger<DQSrcProductRepository> logger)
    {
        _dbcontext = context;
        _logger = logger;
    }
    public async Task<List<DQSrcProductEntity>> GetAll()
    {
        return await _dbcontext.DQSrcProduct.AsNoTracking().ToListAsync();
    }

    public async Task Add(DQSrcProductEntity dqSrcProduct)
    {
        await _dbcontext.AddAsync(dqSrcProduct);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(DQSrcProductEntity dqSrcProduct)
    {
        var dqSrcProductEntity = await _dbcontext.DQSrcProduct.FirstOrDefaultAsync(c => c.Id == dqSrcProduct.Id)
            ?? throw new Exception();
        dqSrcProductEntity.Id = dqSrcProductEntity.Id;
        dqSrcProductEntity.ProductOrderName = dqSrcProductEntity.ProductOrderName;
        dqSrcProductEntity.ProductOrderDate = dqSrcProductEntity.ProductOrderDate;
        dqSrcProductEntity.Comment = dqSrcProductEntity.Comment;
        dqSrcProductEntity.Factory = dqSrcProductEntity.Factory;
        dqSrcProductEntity.Status = dqSrcProductEntity.Status;
        dqSrcProductEntity.CreateDate = dqSrcProductEntity.CreateDate;
        dqSrcProductEntity.OrderNumber = dqSrcProductEntity.OrderNumber;
        dqSrcProductEntity.OrderDate = dqSrcProductEntity.OrderDate;
        dqSrcProductEntity.ProductName = dqSrcProductEntity.ProductName;
        dqSrcProductEntity.Qty = dqSrcProductEntity.Qty;
        dqSrcProductEntity.LoadStatus = dqSrcProductEntity.LoadStatus;
        dqSrcProductEntity.Reason = dqSrcProductEntity.Reason;
        dqSrcProductEntity.ProductId = dqSrcProductEntity.ProductId;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.DQSrcProduct
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}
