using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeWorkingPeriodsSampleRepository : IProductSubTypeWorkingPeriodsSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeWorkingPeriodsSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeWorkingPeriodsSample>> GetAll()
    {
        var productSubTypeWorkingPeriodsSampleEntities = await _dbcontext.ProductSubTypeWorkingPeriodsSamples.AsNoTracking().ToListAsync();
        List<ProductSubTypeWorkingPeriodsSample> productSubTypeWorkingPeriodsSamples = new List<ProductSubTypeWorkingPeriodsSample>();
        foreach (var productSubTypeWorkingPeriodsSampleEntity in productSubTypeWorkingPeriodsSampleEntities)
        {
            productSubTypeWorkingPeriodsSamples.Add(ProductSubTypeWorkingPeriodsSample.Create(productSubTypeWorkingPeriodsSampleEntity.Id, productSubTypeWorkingPeriodsSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodsSampleEntity.RowNumber, productSubTypeWorkingPeriodsSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodsSampleEntity.StandartTime, productSubTypeWorkingPeriodsSampleEntity.StandartEmployee);
);
        }
        return productSubTypeWorkingPeriodsSamples;
    }

    public async Task<ProductSubTypeWorkingPeriodsSample?> GetById(Guid id)
    {
        var productSubTypeWorkingPeriodsSampleEntity = await _dbcontext.ProductSubTypeWorkingPeriodsSamples.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return ProductSubTypeWorkingPeriodsSample.Create(productSubTypeWorkingPeriodsSampleEntity.Id, productSubTypeWorkingPeriodsSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodsSampleEntity.RowNumber, productSubTypeWorkingPeriodsSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodsSampleEntity.StandartTime, productSubTypeWorkingPeriodsSampleEntity.StandartEmployee);
    }
    public async Task<List<ProductSubTypeWorkingPeriodsSample?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        var productSubTypeWorkingPeriodsSampleEntities = await _dbcontext.ProductSubTypeWorkingPeriodsSamples.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
        List<ProductSubTypeWorkingPeriodsSample> productSubTypeWorkingPeriodsSamples = new List<ProductSubTypeWorkingPeriodsSample>();
        foreach (var productSubTypeWorkingPeriodsSampleEntity in productSubTypeWorkingPeriodsSampleEntities)
        {
            productSubTypeWorkingPeriodsSamples.Add(ProductSubTypeWorkingPeriodsSample.Create(productSubTypeWorkingPeriodsSampleEntity.Id, productSubTypeWorkingPeriodsSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodsSampleEntity.RowNumber, productSubTypeWorkingPeriodsSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodsSampleEntity.StandartTime, productSubTypeWorkingPeriodsSampleEntity.StandartEmployee);
);
        }
        return productSubTypeWorkingPeriodsSamples;
    }
    public async Task Add(ProductSubTypeWorkingPeriodsSample productSubTypeWorkingPeriodsSample)
    {
        var productSubTypeWorkingPeriodsSampleEntity = new ProductSubTypeWorkingPeriodsSampleEntity
        {
            Id = productSubTypeWorkingPeriodsSample.Id,
            RowNumber = productSubTypeWorkingPeriodsSample.RowNumber,
            ProductSubTypeId = productSubTypeWorkingPeriodsSample.ProductSubTypeId,
            StandartTime = productSubTypeWorkingPeriodsSample.StandartTime,
            WorkingPeriodName = productSubTypeWorkingPeriodsSample.WorkingPeriodName
        };
        await _dbcontext.AddAsync(productSubTypeWorkingPeriodsSampleEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeWorkingPeriodsSample productSubTypeWorkingPeriodsSample)
    {
        var productSubTypeWorkingPeriodsSampleEntity = await _dbcontext.ProductSubTypeWorkingPeriodsSamples.FirstOrDefaultAsync(p => p.Id == productSubTypeWorkingPeriodsSample.Id)
            ?? throw new Exception();

        productSubTypeWorkingPeriodsSampleEntity.RowNumber = productSubTypeWorkingPeriodsSample.RowNumber;
        productSubTypeWorkingPeriodsSampleEntity.WorkingPeriodName = productSubTypeWorkingPeriodsSample.WorkingPeriodName;
        productSubTypeWorkingPeriodsSampleEntity.StandartTime = productSubTypeWorkingPeriodsSample.StandartTime;
        productSubTypeWorkingPeriodsSampleEntity.StandartEmployee = productSubTypeWorkingPeriodsSample.StandartEmployee;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubTypeWorkingPeriodsSamples
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}