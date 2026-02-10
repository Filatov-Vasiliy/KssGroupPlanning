using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;


using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeRepository : IProductSubTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeEntity>> GetAll()
    {
        return await _dbcontext.ProductSubType.AsNoTracking().OrderBy(f => f.Name).ToListAsync();
    }

    public async Task<ProductSubTypeEntity?> GetById(Guid id)
    {
        return await _dbcontext.ProductSubType.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<ProductSubTypeEntity?> GetByName(string name)
    {
        return await _dbcontext.ProductSubType.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
    }
    public async Task<List<ProductSubTypeEntity?>> GetByProductTypeId(Guid productTypeId)
    {
        return await _dbcontext.ProductSubType.AsNoTracking().Where(p => p.ProductTypeId == productTypeId).ToListAsync();
    }
    public async Task Add(ProductSubTypeEntity productSubType)
    {
        await _dbcontext.AddAsync(productSubType);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeEntity productSubType)
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