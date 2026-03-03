using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories.DQ;

public class DQSrcMaterialRepository : IDQSrcMaterialRepository
{
    private readonly ProjectDbContext _dbcontext;
    private readonly ILogger<DQSrcMaterialRepository> _logger;

    public DQSrcMaterialRepository(ProjectDbContext context, ILogger<DQSrcMaterialRepository> logger)
    {
        _dbcontext = context;
        _logger = logger;
    }
    public async Task<List<DQSrcMaterialEntity>> GetAll()
    {
        return await _dbcontext.DQSrcMaterial.AsNoTracking().ToListAsync();
    }

    public async Task Add(DQSrcMaterialEntity dqSrcMaterial)
    {
        await _dbcontext.AddAsync(dqSrcMaterial);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(DQSrcMaterialEntity dqSrcMaterial)
    {
        var dqSrcMaterialEntity = await _dbcontext.DQSrcMaterial.FirstOrDefaultAsync(c => c.Id == dqSrcMaterial.Id)
            ?? throw new Exception();
        dqSrcMaterialEntity.Id = dqSrcMaterialEntity.Id;
        dqSrcMaterialEntity.ProductOrderName = dqSrcMaterialEntity.ProductOrderName;
        dqSrcMaterialEntity.ProductOrderDate = dqSrcMaterialEntity.ProductOrderDate;
        dqSrcMaterialEntity.MaterialName = dqSrcMaterialEntity.MaterialName;
        dqSrcMaterialEntity.MaterialGroup = dqSrcMaterialEntity.MaterialGroup;
        dqSrcMaterialEntity.Qty = dqSrcMaterialEntity.Qty;
        dqSrcMaterialEntity.CurrentQty = dqSrcMaterialEntity.CurrentQty;
        dqSrcMaterialEntity.PostedDate = dqSrcMaterialEntity.PostedDate;
        dqSrcMaterialEntity.ForAdmissionDate = dqSrcMaterialEntity.ForAdmissionDate;
        dqSrcMaterialEntity.CreateDate = dqSrcMaterialEntity.CreateDate;
        dqSrcMaterialEntity.ProductOrderNameChild = dqSrcMaterialEntity.ProductOrderNameChild;
        dqSrcMaterialEntity.ProductOrderDateChild = dqSrcMaterialEntity.ProductOrderDateChild;
        dqSrcMaterialEntity.LoadStatus = dqSrcMaterialEntity.LoadStatus;
        dqSrcMaterialEntity.Reason = dqSrcMaterialEntity.Reason;
        dqSrcMaterialEntity.GroupMaterialId = dqSrcMaterialEntity.GroupMaterialId;
        dqSrcMaterialEntity.ProductId = dqSrcMaterialEntity.ProductId;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.DQSrcMaterial
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}
