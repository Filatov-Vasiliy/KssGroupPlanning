using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class NewProductRepository : INewProductRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewProductRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductEntity>> GetAll()
    {
        return await _dbcontext.Product.AsNoTracking().OrderBy(f => f.Number).ToListAsync();
    }

    public async Task<ProductEntity?> GetById(Guid id)
    {
        return await _dbcontext.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<ProductEntity?> GetByNumber(string number)
    {
        return await _dbcontext.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Number == number);
    }
    public async Task Add(ProductEntity product)
    {
        await _dbcontext.AddAsync(product);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductEntity product)
    {
        var productEntity = await _dbcontext.Product.FirstOrDefaultAsync(p => p.Id == product.Id)
            ?? throw new Exception();

        productEntity.Number = product.Number;
        productEntity.ProductSubTypeId = product.ProductSubTypeId;
        productEntity.FactoryId = product.FactoryId;
        productEntity.OrderId = product.OrderId;
        productEntity.ParentProductId = product.ParentProductId;
        productEntity.Status = product.Status;
        productEntity.StartDate = product.StartDate;
        productEntity.EndDate = product.EndDate;
        productEntity.CreateTime = product.CreateTime;
        productEntity.UpdateTime = product.UpdateTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.Product
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}