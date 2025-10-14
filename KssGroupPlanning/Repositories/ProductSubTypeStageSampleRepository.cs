using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeStageSampleRepository : IProductSubTypeStageSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeStageSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeStageSample>> GetAll()
    {
        var productSubTypeStageSampleEntities = await _dbcontext.ProductSubTypeStageSample.AsNoTracking().ToListAsync();
        List<ProductSubTypeStageSample> productSubTypeStageSamples = new List<ProductSubTypeStageSample>();
        foreach (var productSubTypeStageSampleEntity in productSubTypeStageSampleEntities)
        {
            productSubTypeStageSamples.Add(ProductSubTypeStageSample.Create(productSubTypeStageSampleEntity.Id, productSubTypeStageSampleEntity.ProductSubTypeId, productSubTypeStageSampleEntity.RowNumber, productSubTypeStageSampleEntity.MaterialStageId, productSubTypeStageSampleEntity.StandartTime));
        }
        return productSubTypeStageSamples;
    }

    public async Task<ProductSubTypeStageSample?> GetById(Guid id)
    {
        var productSubTypeStageSampleEntity = await _dbcontext.ProductSubTypeStageSample.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return ProductSubTypeStageSample.Create(productSubTypeStageSampleEntity.Id, productSubTypeStageSampleEntity.ProductSubTypeId, productSubTypeStageSampleEntity.RowNumber, productSubTypeStageSampleEntity.MaterialStageId, productSubTypeStageSampleEntity.StandartTime);
    }
    public async Task<List<ProductSubTypeStageSample?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        var productSubTypeStageSampleEntities = await _dbcontext.ProductSubTypeStageSample.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
        List<ProductSubTypeStageSample> productSubTypeStageSamples = new List<ProductSubTypeStageSample>();
        foreach (var productSubTypeStageSampleEntity in productSubTypeStageSampleEntities)
        {
            productSubTypeStageSamples.Add(ProductSubTypeStageSample.Create(productSubTypeStageSampleEntity.Id, productSubTypeStageSampleEntity.ProductSubTypeId, productSubTypeStageSampleEntity.RowNumber, productSubTypeStageSampleEntity.MaterialStageId, productSubTypeStageSampleEntity.StandartTime));
        }
        return productSubTypeStageSamples;
        }
    public async Task<List<ProductSubTypeStageSample?>> GetByMaterialStageId(Guid materialStageId)
    {
        var productSubTypeStageSampleEntities = await _dbcontext.ProductSubTypeStageSample.AsNoTracking().Where(p => p.MaterialStageId == materialStageId).ToListAsync();
        List<ProductSubTypeStageSample> productSubTypeStageSamples = new List<ProductSubTypeStageSample>();
        foreach (var productSubTypeStageSampleEntity in productSubTypeStageSampleEntities)
        {
            productSubTypeStageSamples.Add(ProductSubTypeStageSample.Create(productSubTypeStageSampleEntity.Id, productSubTypeStageSampleEntity.ProductSubTypeId, productSubTypeStageSampleEntity.RowNumber, productSubTypeStageSampleEntity.MaterialStageId, productSubTypeStageSampleEntity.StandartTime));
        }
        return productSubTypeStageSamples;
    }
    public async Task Add(ProductSubTypeStageSample productSubTypeStagesSample)
    {
        var productSubTypeStagesSampleEntity = new ProductSubTypeStageSampleEntity
        {
            Id = productSubTypeStagesSample.Id,
            RowNumber = productSubTypeStagesSample.RowNumber,
            ProductSubTypeId = productSubTypeStagesSample.ProductSubTypeId,
            StandartTime = productSubTypeStagesSample.StandartTime,
            MaterialStageId = productSubTypeStagesSample.MaterialStageId,
        };
        await _dbcontext.AddAsync(productSubTypeStagesSampleEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeStageSample productSubTypeStageSample)
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