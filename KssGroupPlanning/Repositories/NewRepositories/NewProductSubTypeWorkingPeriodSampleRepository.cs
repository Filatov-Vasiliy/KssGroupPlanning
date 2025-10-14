using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class NewProductSubTypeWorkingPeriodSampleRepository : INewProductSubTypeWorkingPeriodSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewProductSubTypeWorkingPeriodSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeWorkingPeriodSampleEntity>> GetAll()
    {
        return await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().ToListAsync();
    }

    public async Task<ProductSubTypeWorkingPeriodSampleEntity?> GetById(Guid id)
    {
        return await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<List<ProductSubTypeWorkingPeriodSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        return await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
    }
    public async Task Add(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample)
    {
        await _dbcontext.AddAsync(productSubTypeWorkingPeriodSample);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample)
    {
        var productSubTypeWorkingPeriodSampleEntity = await _dbcontext.ProductSubTypeWorkingPeriodSample.FirstOrDefaultAsync(p => p.Id == productSubTypeWorkingPeriodSample.Id)
            ?? throw new Exception();

        productSubTypeWorkingPeriodSampleEntity.RowNumber = productSubTypeWorkingPeriodSample.RowNumber;
        productSubTypeWorkingPeriodSampleEntity.WorkingPeriodName = productSubTypeWorkingPeriodSample.WorkingPeriodName;
        productSubTypeWorkingPeriodSampleEntity.StandartTime = productSubTypeWorkingPeriodSample.StandartTime;
        productSubTypeWorkingPeriodSampleEntity.StandartEmployee = productSubTypeWorkingPeriodSample.StandartEmployee;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubTypeWorkingPeriodSample
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}