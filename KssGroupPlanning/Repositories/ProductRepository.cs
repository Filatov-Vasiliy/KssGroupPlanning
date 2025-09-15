using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<Product>> GetAll()
    {
        var productEntities = await _dbcontext.Products.AsNoTracking().OrderBy(f => f.Number).ToListAsync();
        List<Product> products = new List<Product>();
        foreach (var productEntity in productEntities)
        {
            products.Add(Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId, productEntity.Status, productEntity.Start_date, productEntity.End_date, productEntity.CreateTime, productEntity.UpdateTime));
        }
        return products;
    }

    public async Task<Product?> GetById(Guid id)
    {
        var productEntity = await _dbcontext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId, productEntity.Status, productEntity.Start_date, productEntity.End_date, productEntity.CreateTime, productEntity.UpdateTime);
    }
    public async Task<Product?> GetByNumber(int number)
    {
        var productEntity = await _dbcontext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Number == number);
        return Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId, productEntity.Status,productEntity.Start_date, productEntity.End_date, productEntity.CreateTime, productEntity.UpdateTime);
    }
    public async Task Add(Product product)
    {
        var productEntity = new ProductEntity
        {
            Id = product.Id,
            Number = product.Number,
            ProductSubTypeId = product.ProductSubTypeId,
            FactoryId = product.FactoryId,
            OrderId = product.OrderId,
            Status = product.Status,
            Start_date  = product.Start_date,
            End_date = product.End_date,
            CreateTime = product.CreateTime,
            UpdateTime = product.UpdateTime
        };
        await _dbcontext.AddAsync(productEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Product product)
    {
        var productEntity = await _dbcontext.Products.FirstOrDefaultAsync(p => p.Id == product.Id)
            ?? throw new Exception();

        productEntity.Name = product.Name;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.Products
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}