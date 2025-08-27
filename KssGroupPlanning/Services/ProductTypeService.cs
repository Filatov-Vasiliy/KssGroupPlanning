using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class ProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }
    public async Task<List<ProductType>> GetAll()
    {
        return await _productTypeRepository.GetAll();
    }

    public async Task<ProductType> GetById(Guid id)
    {
        return await _productTypeRepository.GetById(id);
    }
    public async Task<ProductType> GetByName(string name)
    {
        return await _productTypeRepository.GetByName(name);
    }
    public async Task<List<ProductType>> GetByPage(int page, int pageSize)
    {
        return await _productTypeRepository.GetByPage(page, pageSize);
    }
    public async Task Add(string name)
    {
        await _productTypeRepository.Add(ProductType.Create(Guid.NewGuid(), name));
    }
    public async Task Update(ProductType productType)
    {
        await _productTypeRepository.Update(productType);
    }
    public async Task Delete(Guid id)
    {
        await _productTypeRepository.Delete(id);
    }
}
