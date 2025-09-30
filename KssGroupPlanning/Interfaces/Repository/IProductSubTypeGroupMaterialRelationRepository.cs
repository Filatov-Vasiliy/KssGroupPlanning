using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeGroupMaterialRelationRepository
    {
        Task<List<ProductSubTypeGroupMaterialRelation>> GetAll();
        Task<ProductSubTypeGroupMaterialRelation?> GetById(Guid id);
        Task<List<ProductSubTypeGroupMaterialRelation?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);

        Task<List<ProductSubTypeGroupMaterialRelation?>> GetByGroupMaterialId(Guid groupMaterial);

        Task Add(ProductSubTypeGroupMaterialRelation productSubTypeGroupMaterialRelation);
        Task Update(ProductSubTypeGroupMaterialRelation productSubTypeGroupMaterialRelation);
        Task Delete(Guid id);
    }
}
