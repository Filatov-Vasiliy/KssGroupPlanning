using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeStageSampleRepository
    {
        Task<List<ProductSubTypeStageSample>> GetAll();
        Task<ProductSubTypeStageSample?> GetById(Guid id);
        Task<List<ProductSubTypeStageSample?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task<List<ProductSubTypeStageSample?>> GetByMaterialStageId(Guid productSubTypeId);
        Task Add(ProductSubTypeStageSample productSubTypeStageSample);
        Task Update(ProductSubTypeStageSample productSubTypeStageSample);
        Task Delete(Guid id);
    }
}
