
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class ProductSubTypeWorkingPeriodSampleService
{
    private readonly IProductSubTypeWorkingPeriodSampleRepository _productSubTypeWorkingPeriodSampleRepository;
    public ProductSubTypeWorkingPeriodSampleService(IProductSubTypeWorkingPeriodSampleRepository productSubTypeWorkingPeriodSampleRepository)
    {
        _productSubTypeWorkingPeriodSampleRepository = productSubTypeWorkingPeriodSampleRepository;
    }
    public async Task<List<ProductSubTypeWorkingPeriodSampleEntity>> GetAll()
    {
        return await _productSubTypeWorkingPeriodSampleRepository.GetAll();
    }

    public async Task<ProductSubTypeWorkingPeriodSampleEntity?> GetById(Guid id)
    {
        return await _productSubTypeWorkingPeriodSampleRepository.GetById(id);
    }
    public async Task<List<ProductSubTypeWorkingPeriodSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId)
    {
        return await _productSubTypeWorkingPeriodSampleRepository.GetByProductSubTypeId(productSubTypeId);
    }
    public async Task Add(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample)
    {
        var productSubTypeWorkingPeriodSampleEntity = new ProductSubTypeWorkingPeriodSampleEntity
        {
            Id = Guid.NewGuid(),
            ProductSubTypeId = productSubTypeWorkingPeriodSample.ProductSubTypeId,
            RowNumber = productSubTypeWorkingPeriodSample.RowNumber,
            WorkingPeriodName = productSubTypeWorkingPeriodSample.WorkingPeriodName,
            StandartTime = productSubTypeWorkingPeriodSample.StandartTime,
            StandartEmployee = productSubTypeWorkingPeriodSample.StandartEmployee,
        };
        await _productSubTypeWorkingPeriodSampleRepository.Add(productSubTypeWorkingPeriodSampleEntity);
    }
    public async Task Update(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample)
    {
        await _productSubTypeWorkingPeriodSampleRepository.Update(productSubTypeWorkingPeriodSample);
    }
    public async Task Delete(Guid id)
    {
        await _productSubTypeWorkingPeriodSampleRepository.Delete(id);
    }

}
