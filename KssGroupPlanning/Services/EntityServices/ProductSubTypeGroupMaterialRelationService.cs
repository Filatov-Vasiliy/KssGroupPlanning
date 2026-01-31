
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class ProductSubTypeGroupMaterialRelationService
{
    private readonly IProductSubTypeGroupMaterialRelationRepository _productSubTypeGroupMaterialRelationRepository;
    public ProductSubTypeGroupMaterialRelationService(IProductSubTypeGroupMaterialRelationRepository productSubTypeGroupMaterialRelationRepository)
    {
        _productSubTypeGroupMaterialRelationRepository = productSubTypeGroupMaterialRelationRepository;
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity>> GetAll()
    {
        return await _productSubTypeGroupMaterialRelationRepository.GetAll();
    }

    public async Task<ProductSubTypeGroupMaterialRelationEntity?> GetById(Guid id)
    {
        return await _productSubTypeGroupMaterialRelationRepository.GetById(id);
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        return await _productSubTypeGroupMaterialRelationRepository.GetByProductSubTypeWorkingPeriodSampleId(productSubTypeWorkingPeriodSampleId);
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        return await _productSubTypeGroupMaterialRelationRepository.GetByGroupMaterialId(groupMaterialId);
    }
    public async Task Add(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation)
    {
        var productSubTypeGroupMaterialRelationEntity = new ProductSubTypeGroupMaterialRelationEntity
        {
            Id = Guid.NewGuid(),
            GroupMaterialId = productSubTypeGroupMaterialRelation.GroupMaterialId,
            ProductSubTypeWorkingPeriodSampleId = productSubTypeGroupMaterialRelation.ProductSubTypeWorkingPeriodSampleId,
        };
        await _productSubTypeGroupMaterialRelationRepository.Add(productSubTypeGroupMaterialRelationEntity);
    }
    public async Task Update(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation)
    {
        await _productSubTypeGroupMaterialRelationRepository.Update(productSubTypeGroupMaterialRelation);
    }
    public async Task Delete(Guid id)
    {
        await _productSubTypeGroupMaterialRelationRepository.Delete(id);
    }

}
