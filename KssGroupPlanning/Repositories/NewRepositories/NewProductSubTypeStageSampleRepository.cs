using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class NewProductSubTypeStageSampleRepository : INewProductSubTypeStageSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewProductSubTypeStageSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeStageSampleEntity>> GetAll()
    {
        return await _dbcontext.ProductSubTypeStageSample.AsNoTracking().ToListAsync();
    }

    public async Task<ProductSubTypeStageSampleEntity?> GetById(Guid id)
    {
        return await _dbcontext.ProductSubTypeStageSample.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<List<ProductSubTypeStageSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        return await _dbcontext.ProductSubTypeStageSample.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
    }
    public async Task<List<ProductSubTypeStageSampleEntity?>> GetByMaterialStageId(Guid materialStageId)
    {
        return await _dbcontext.ProductSubTypeStageSample.AsNoTracking().Where(p => p.MaterialStageId == materialStageId).ToListAsync();
    }
    public async Task Add(ProductSubTypeStageSampleEntity productSubTypeStagesSample)
    {

        await _dbcontext.AddAsync(productSubTypeStagesSample);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeStageSampleEntity productSubTypeStageSample)
    {
        var productSubTypeStageSampleEntity = await _dbcontext.ProductSubTypeStageSample.FirstOrDefaultAsync(p => p.Id == productSubTypeStageSample.Id)
            ?? throw new Exception();

        productSubTypeStageSampleEntity.RowNumber = productSubTypeStageSample.RowNumber;
        productSubTypeStageSampleEntity.MaterialStageId = productSubTypeStageSample.MaterialStageId;
        productSubTypeStageSampleEntity.StandartTime = productSubTypeStageSample.StandartTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubTypeStageSample
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}