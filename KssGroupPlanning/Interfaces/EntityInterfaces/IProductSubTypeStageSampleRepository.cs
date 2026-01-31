using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IProductSubTypeStageSampleRepository
    {
        Task<List<ProductSubTypeStageSampleEntity>> GetAll();
        Task<ProductSubTypeStageSampleEntity?> GetById(Guid id);
        Task<List<ProductSubTypeStageSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task<List<ProductSubTypeStageSampleEntity?>> GetByMaterialStageId(Guid materialStageId);
        Task Add(ProductSubTypeStageSampleEntity productSubTypeStageSample);
        Task Update(ProductSubTypeStageSampleEntity productSubTypeStageSample);
        Task Delete(Guid id);
    }
}
