using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;


using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductTypeEntity>> GetAll()
    {
        return await _dbcontext.ProductType.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<ProductTypeEntity?> GetById(Guid id)
    {
        return await _dbcontext.ProductType.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<ProductTypeEntity?> GetByName(string name)
    {

        return await _dbcontext.ProductType.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<List<ProductTypeEntity>> GetByPage(int page, int pageSize)
    {
        return await _dbcontext.ProductType
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    }
    public async Task Add(ProductTypeEntity productType)
    {
        await _dbcontext.AddAsync(productType);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductTypeEntity productType)
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