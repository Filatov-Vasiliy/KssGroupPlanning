using KssGroupPlanning.Entities;

using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Infrastuction.Db;

public class SrcProductRepository(ProjectDbContext context) : ISrcProductRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(List<SrcProductEntity> srcProducts)
    {
        foreach (SrcProductEntity srcProduct in srcProducts)
        {

            var srcProductEntity = new SrcProductEntity
            {
                Id = srcProduct.Id,
                ProductOrderName = srcProduct.ProductOrderName,
                ProductOrderDate = srcProduct.ProductOrderDate,
                Comment = srcProduct.Comment,
                Factory = srcProduct.Factory,
                Status = srcProduct.Status,
                CreateDate = srcProduct.CreateDate,
                OrderNumber = srcProduct.OrderNumber,
                OrderDate = srcProduct.OrderDate,
                ProductName = srcProduct.ProductName,
                Qty = srcProduct.Qty
            };
            await _dbcontext.SrcProduct.AddAsync(srcProductEntity);
        }
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<SrcProductEntity>> GetSrcProducts()
    {
        return await _dbcontext.SrcProduct.AsNoTracking().ToListAsync();
    }
}