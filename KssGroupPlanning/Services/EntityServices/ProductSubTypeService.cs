
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Repositories;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class ProductSubTypeService
{
    private readonly IProductSubTypeRepository _productSubTypeRepository;
    public ProductSubTypeService(IProductSubTypeRepository productSubTypeRepository)
    {
        _productSubTypeRepository = productSubTypeRepository;
    }
    public async Task<List<ProductSubTypeEntity>> GetAll()
    {
        return await _productSubTypeRepository.GetAll();
    }

    public async Task<ProductSubTypeEntity?> GetById(Guid id)
    {
        return await _productSubTypeRepository.GetById(id);
    }
    public async Task<ProductSubTypeEntity?> GetByName(string name)
    {
        return await _productSubTypeRepository.GetByName(name);
    }
    public async Task<ProductSubTypeEntity?> GetByProductTypeId(Guid productTypeId)
    {
        return await _productSubTypeRepository.GetByProductTypeId(productTypeId);
    }
    public async Task Add(ProductSubTypeEntity productSubType)
    {
        var productSubTypeEntity = new ProductSubTypeEntity
        {
            Id = Guid.NewGuid(),
            Name = productSubType.Name,
            ProductTypeId = productSubType.ProductTypeId,
        };
        await _productSubTypeRepository.Add(productSubTypeEntity);
    }
    public async Task Update(ProductSubTypeEntity productSubType)
    {
        await _productSubTypeRepository.Update(productSubType);
    }
    public async Task Delete(Guid id)
    {
        await _productSubTypeRepository.Delete(id);
    }

}
