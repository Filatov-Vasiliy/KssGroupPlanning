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
        var productEntities = await _dbcontext.Product.AsNoTracking().OrderBy(f => f.Number).ToListAsync();
        List<Product> products = new List<Product>();
        foreach (var productEntity in productEntities)
        {
            products.Add(Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId, productEntity.ParentProductId, productEntity.Status, productEntity.StartDate, productEntity.EndDate, productEntity.CreateTime, productEntity.UpdateTime));
        }
        return products;
    }

    public async Task<Product?> GetById(Guid id)
    {
        var productEntity = await _dbcontext.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId, productEntity.ParentProductId, productEntity.Status, productEntity.StartDate, productEntity.EndDate, productEntity.CreateTime, productEntity.UpdateTime);
    }
    public async Task<Product?> GetByNumber(string number)
    {
        var productEntity = await _dbcontext.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Number == number);
        return Product.Create(productEntity.Id, productEntity.Number, productEntity.ProductSubTypeId, productEntity.FactoryId, productEntity.OrderId,productEntity.ParentProductId, productEntity.Status,productEntity.StartDate, productEntity.EndDate, productEntity.CreateTime, productEntity.UpdateTime);
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
            ParentProductId = product.ParentProductId,
            Status = product.Status,
            StartDate  = product.StartDate,
            EndDate = product.EndDate,
            CreateTime = product.CreateTime,
            UpdateTime = product.UpdateTime
        };
        await _dbcontext.AddAsync(productEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Product product)
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