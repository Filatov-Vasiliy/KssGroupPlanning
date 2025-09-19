using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductType>> GetAll()
    {
        var productTypeEntities = await _dbcontext.ProductType.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        List<ProductType> productTypes = new List<ProductType>();
        foreach (var productTypeEntity in productTypeEntities) 
        {
            productTypes.Add(ProductType.Create(productTypeEntity.Id, productTypeEntity.Name));
        }
        return productTypes;
    }

    public async Task<ProductType?> GetById(Guid id)
    {
        
        var productTypeEntity = await _dbcontext.ProductType.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return ProductType.Create(productTypeEntity.Id, productTypeEntity.Name);
    }
    public async Task<ProductType?> GetByName(string name)
    {

        var productTypeEntity = await _dbcontext.ProductType.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        return ProductType.Create(productTypeEntity.Id, productTypeEntity.Name);
    }
    public async Task<List<ProductType>> GetByPage(int page, int pageSize)
    {
        var productTypeEntities = await _dbcontext.ProductType
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        List<ProductType> productTypes = new List<ProductType>();
        foreach (var productTypeEntity in productTypeEntities)
        {
            productTypes.Add(ProductType.Create(productTypeEntity.Id, productTypeEntity.Name));
        }
        return productTypes;
    }
    public async Task Add(ProductType productType)
    {
        var productTypeEntity = new ProductTypeEntity
        {
            Id = productType.Id,
            Name = productType.Name
        };
        await _dbcontext.AddAsync(productTypeEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductType productType)
    {
        var productTypeEntity = await _dbcontext.ProductType.FirstOrDefaultAsync(c => c.Id == productType.Id)
            ?? throw new Exception();

        productTypeEntity.Name = productType.Name;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductType
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}