using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IProductSubTypeGroupMaterialRelationRepository
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
