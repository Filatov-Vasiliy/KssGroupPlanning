
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class ProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }
    public async Task<List<ProductTypeEntity>> GetAll()
    {
        return await _productTypeRepository.GetAll();
    }
    public async Task<List<ProductTypeEntity>> GetAllDetailed()
    {
        return await _productTypeRepository.GetAllDetailed();
    }

    public async Task<ProductTypeEntity> GetById(Guid id)
    {
        return await _productTypeRepository.GetById(id);
    }
    public async Task<ProductTypeEntity> GetByName(string name)
    {
        return await _productTypeRepository.GetByName(name);
    }
    public async Task<List<ProductTypeEntity>> GetByPage(int page, int pageSize)
    {
        return await _productTypeRepository.GetByPage(page, pageSize);
    }
    public async Task Add(string name)
    {
        var productTypeEntity = new ProductTypeEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
        await _productTypeRepository.Add(productTypeEntity);
    }
    public async Task Update(ProductTypeEntity productType)
    {
        await _productTypeRepository.Update(productType);
    }
    public async Task Delete(Guid id)
    {
        await _productTypeRepository.Delete(id);
    }

}
