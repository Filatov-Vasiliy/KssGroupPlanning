
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Repositories;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class ProductSubTypeStageSampleService
{
    private readonly IProductSubTypeStageSampleRepository _productSubTypeStageSampleRepository;
    public ProductSubTypeStageSampleService(IProductSubTypeStageSampleRepository productSubTypeStageSampleRepository)
    {
        _productSubTypeStageSampleRepository = productSubTypeStageSampleRepository;
    }
    public async Task<List<ProductSubTypeStageSampleEntity>> GetAll()
    {
        return await _productSubTypeStageSampleRepository.GetAll();
    }

    public async Task<ProductSubTypeStageSampleEntity?> GetById(Guid id)
    {
        return await _productSubTypeStageSampleRepository.GetById(id);
    }
    public async Task<List<ProductSubTypeStageSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        return await _productSubTypeStageSampleRepository.GetByProductSubTypeId(productSubTypeId);
    }
    public async Task<List<ProductSubTypeStageSampleEntity?>> GetByMaterialStageId(Guid materialStageId)
    {
        return await _productSubTypeStageSampleRepository.GetByMaterialStageId(materialStageId);
    }
    public async Task Add(ProductSubTypeStageSampleEntity productSubTypeStageSample)
    {
        var productSubTypeStageSampleEntity = new ProductSubTypeStageSampleEntity
        {
            Id = Guid.NewGuid(),
            StageName = productSubTypeStageSample.StageName,
            ProductSubTypeId = productSubTypeStageSample.ProductSubTypeId,
            RowNumber = productSubTypeStageSample.RowNumber,
            MaterialStageId = productSubTypeStageSample.MaterialStageId,
            StandartTime = productSubTypeStageSample.StandartTime,
        };
        await _productSubTypeStageSampleRepository.Add(productSubTypeStageSampleEntity);
    }
    public async Task Update(ProductSubTypeStageSampleEntity productSubTypeStageSample)
    {
        await _productSubTypeStageSampleRepository.Update(productSubTypeStageSample);
    }
    public async Task Delete(Guid id)
    {
        await _productSubTypeStageSampleRepository.Delete(id);
    }

}
