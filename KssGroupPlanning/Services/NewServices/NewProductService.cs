using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewProductService
{
    private readonly INewProductRepository _productRepository;
    public NewProductService(INewProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<List<ProductEntity>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<ProductEntity?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }
    public async Task<ProductEntity?> GetByNumber(string number)
    {
        return await _productRepository.GetByNumber(number);
    }
    public async Task<List<ProductEntity>> GetByProductSubTypeId(Guid id)
    {
        return await _productRepository.GetByProductSubTypeId(id);
    }
    public async Task<List<ProductEntity>> GetByOrderId(Guid id)
    {
        return await _productRepository.GetByOrderId(id);
    }
    public async Task<List<ProductEntity>> GetByFactoryId(Guid id)
    {
        return await _productRepository.GetByFactoryId(id);
    }
    public async Task Add(ProductEntity product)
    {
        var productEntity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Number = product.Number,
            ProductSubTypeId = product.ProductSubTypeId,
            OrderId = product.OrderId,
            FactoryId = product.FactoryId,
            ParentProductId = product.ParentProductId,
            Status = product.Status,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            StartDate = product.StartDate,
            EndDate = product.EndDate,
        };
        await _productRepository.Add(productEntity);
    }
    public async Task Update(ProductEntity product)
    {
        await _productRepository.Update(product);
    }
    public async Task Delete(Guid id)
    {
        await _productRepository.Delete(id);
    }

}
