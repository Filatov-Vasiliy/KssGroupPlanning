using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeRepository : IProductSubTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubType>> GetAll()
    {
        var productSubTypeEntities = await _dbcontext.ProductSubType.AsNoTracking().OrderBy(f => f.Name).ToListAsync();
        List<ProductSubType> productSubTypes = new List<ProductSubType>();
        foreach (var productSubTypeEntity in productSubTypeEntities)
        {
            productSubTypes.Add(ProductSubType.Create(productSubTypeEntity.Id, productSubTypeEntity.Name, productSubTypeEntity.ProductTypeId));
        }
        return productSubTypes;
    }

    public async Task<ProductSubType?> GetById(Guid id)
    {
        var productSubTypeEntity = await _dbcontext.ProductSubType.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return ProductSubType.Create(productSubTypeEntity.Id, productSubTypeEntity.Name, productSubTypeEntity.ProductTypeId);
    }
    public async Task<ProductSubType?> GetByName(string name)
    {
        var productSubTypeEntity = await _dbcontext.ProductSubType.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        return ProductSubType.Create(productSubTypeEntity.Id, productSubTypeEntity.Name, productSubTypeEntity.ProductTypeId);
    }
    public async Task<ProductSubType?> GetByProductTypeId(Guid productTypeId)
    {
        var productSubTypeEntity = await _dbcontext.ProductSubType.AsNoTracking().FirstOrDefaultAsync(p => p.ProductTypeId == productTypeId);
        return ProductSubType.Create(productSubTypeEntity.Id, productSubTypeEntity.Name, productSubTypeEntity.ProductTypeId);
    }
    public async Task Add(ProductSubType productSubType)
    {
        var productSubTypeEntity = new ProductSubTypeEntity
        {
            Id = productSubType.Id,
            Name = productSubType.Name,
            ProductTypeId = productSubType.ProductTypeId
        };
        await _dbcontext.AddAsync(productSubTypeEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubType productSubType)
    {
        var productSubTypeEntity = await _dbcontext.ProductSubType.FirstOrDefaultAsync(p => p.Id == productSubType.Id)
            ?? throw new Exception();

        productSubTypeEntity.Name = productSubType.Name;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubType
            .Where(pst => pst.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}