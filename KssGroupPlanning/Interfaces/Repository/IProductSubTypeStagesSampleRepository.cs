using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeStagesSampleRepository
    {
        Task<List<ProductSubTypeStagesSample>> GetAll();
        Task<ProductSubTypeStagesSample?> GetById(Guid id);
        Task<List<ProductSubTypeStagesSample?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeStagesSample productSubTypeStagesSample);
        Task Update(ProductSubTypeStagesSample productSubTypeStagesSample);
        Task Delete(Guid id);
    }
}
