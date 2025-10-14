using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewProductSubTypeGroupMaterialRelationRepository
    {
        Task<List<ProductSubTypeGroupMaterialRelationEntity>> GetAll();
        Task<ProductSubTypeGroupMaterialRelationEntity?> GetById(Guid id);
        Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);

        Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByGroupMaterialId(Guid groupMaterial);

        Task Add(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation);
        Task Update(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation);
        Task Delete(Guid id);
    }
}
