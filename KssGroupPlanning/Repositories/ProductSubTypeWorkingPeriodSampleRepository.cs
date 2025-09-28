using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeWorkingPeriodSampleRepository : IProductSubTypeWorkingPeriodSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeWorkingPeriodSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeWorkingPeriodSample>> GetAll()
    {
        var productSubTypeWorkingPeriodSampleEntities = await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().ToListAsync();
        List<ProductSubTypeWorkingPeriodSample> productSubTypeWorkingPeriodSamples = new List<ProductSubTypeWorkingPeriodSample>();
        foreach (var productSubTypeWorkingPeriodSampleEntity in productSubTypeWorkingPeriodSampleEntities)
        {
            productSubTypeWorkingPeriodSamples.Add(ProductSubTypeWorkingPeriodSample.Create(productSubTypeWorkingPeriodSampleEntity.Id, productSubTypeWorkingPeriodSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodSampleEntity.RowNumber, productSubTypeWorkingPeriodSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodSampleEntity.StandartTime, productSubTypeWorkingPeriodSampleEntity.StandartEmployee));
        }
        return productSubTypeWorkingPeriodSamples;
    }

    public async Task<ProductSubTypeWorkingPeriodSample?> GetById(Guid id)
    {
        var productSubTypeWorkingPeriodSampleEntity = await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return ProductSubTypeWorkingPeriodSample.Create(productSubTypeWorkingPeriodSampleEntity.Id, productSubTypeWorkingPeriodSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodSampleEntity.RowNumber, productSubTypeWorkingPeriodSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodSampleEntity.StandartTime, productSubTypeWorkingPeriodSampleEntity.StandartEmployee);
    }
    public async Task<List<ProductSubTypeWorkingPeriodSample?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        var productSubTypeWorkingPeriodsSampleEntities = await _dbcontext.ProductSubTypeWorkingPeriodSample.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
        List<ProductSubTypeWorkingPeriodSample> productSubTypeWorkingPeriodSamples = new List<ProductSubTypeWorkingPeriodSample>();
        foreach (var productSubTypeWorkingPeriodSampleEntity in productSubTypeWorkingPeriodsSampleEntities)
        {
            productSubTypeWorkingPeriodSamples.Add(ProductSubTypeWorkingPeriodSample.Create(productSubTypeWorkingPeriodSampleEntity.Id, productSubTypeWorkingPeriodSampleEntity.ProductSubTypeId, productSubTypeWorkingPeriodSampleEntity.RowNumber, productSubTypeWorkingPeriodSampleEntity.WorkingPeriodName, productSubTypeWorkingPeriodSampleEntity.StandartTime, productSubTypeWorkingPeriodSampleEntity.StandartEmployee));
        }
        return productSubTypeWorkingPeriodSamples;
    }
    public async Task Add(ProductSubTypeWorkingPeriodSample productSubTypeWorkingPeriodSample)
    {
        var productSubTypeWorkingPeriodSampleEntity = new ProductSubTypeWorkingPeriodSampleEntity
        {
            Id = productSubTypeWorkingPeriodSample.Id,
            RowNumber = productSubTypeWorkingPeriodSample.RowNumber,
            ProductSubTypeId = productSubTypeWorkingPeriodSample.ProductSubTypeId,
            StandartTime = productSubTypeWorkingPeriodSample.StandartTime,
            WorkingPeriodName = productSubTypeWorkingPeriodSample.WorkingPeriodName
        };
        await _dbcontext.AddAsync(productSubTypeWorkingPeriodSampleEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeWorkingPeriodSample productSubTypeWorkingPeriodSample)
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