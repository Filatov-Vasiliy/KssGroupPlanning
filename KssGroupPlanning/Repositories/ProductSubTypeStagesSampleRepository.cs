using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeStagesSampleRepository : IProductSubTypeStagesSampleRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeStagesSampleRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeStagesSample>> GetAll()
    {
        var productSubTypeStagesSampleEntities = await _dbcontext.ProductSubTypeStagesSamples.AsNoTracking().ToListAsync();
        List<ProductSubTypeStagesSample> productSubTypeStagesSamples = new List<ProductSubTypeStagesSample>();
        foreach (var productSubTypeStagesSampleEntity in productSubTypeStagesSampleEntities)
        {
            productSubTypeStagesSamples.Add(ProductSubTypeStagesSample.Create(productSubTypeStagesSampleEntity.Id, productSubTypeStagesSampleEntity.ProductSubTypeId, productSubTypeStagesSampleEntity.RowNumber, productSubTypeStagesSampleEntity.StageName, productSubTypeStagesSampleEntity.StandartTime);
);
        }
        return productSubTypeStagesSamples;
    }

    public async Task<ProductSubTypeStagesSample?> GetById(Guid id)
    {
        var productSubTypeStagesSampleEntity = await _dbcontext.ProductSubTypeStagesSamples.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return ProductSubTypeStagesSample.Create(productSubTypeStagesSampleEntity.Id, productSubTypeStagesSampleEntity.ProductSubTypeId, productSubTypeStagesSampleEntity.RowNumber, productSubTypeStagesSampleEntity.StageName, productSubTypeStagesSampleEntity.StandartTime);
    }
    public async Task<List<ProductSubTypeStagesSample?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        var productSubTypeStagesSampleEntities = await _dbcontext.ProductSubTypeStagesSamples.AsNoTracking().Where(p => p.ProductSubTypeId == productSubTypeId).ToListAsync();
        List<ProductSubTypeStagesSample> productSubTypeStagesSamples = new List<ProductSubTypeStagesSample>();
        foreach (var productSubTypeStagesSampleEntity in productSubTypeStagesSampleEntities)
        {
            productSubTypeStagesSamples.Add(ProductSubTypeStagesSample.Create(productSubTypeStagesSampleEntity.Id, productSubTypeStagesSampleEntity.ProductSubTypeId, productSubTypeStagesSampleEntity.RowNumber, productSubTypeStagesSampleEntity.StageName, productSubTypeStagesSampleEntity.StandartTime);
);
        }
        return productSubTypeStagesSamples;
        }
    public async Task Add(ProductSubTypeStagesSample productSubTypeStagesSample)
    {
        var productSubTypeStagesSampleEntity = new ProductSubTypeStagesSampleEntity
        {
            Id = productSubTypeStagesSample.Id,
            RowNumber = productSubTypeStagesSample.RowNumber,
            ProductSubTypeId = productSubTypeStagesSample.ProductSubTypeId,
            StandartTime = productSubTypeStagesSample.StandartTime,
            StageName = productSubTypeStagesSample.StageName
        };
        await _dbcontext.AddAsync(productSubTypeStagesSampleEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeStagesSample productSubTypeStagesSample)
    {
        var productSubTypeStagesSampleEntity = await _dbcontext.ProductSubTypeStagesSamples.FirstOrDefaultAsync(p => p.Id == productSubTypeStagesSample.Id)
            ?? throw new Exception();

        productSubTypeStagesSampleEntity.RowNumber = productSubTypeStagesSample.RowNumber;
        productSubTypeStagesSampleEntity.StageName = productSubTypeStagesSample.StageName;
        productSubTypeStagesSampleEntity.StandartTime = productSubTypeStagesSample.StandartTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubTypeStagesSamples
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}