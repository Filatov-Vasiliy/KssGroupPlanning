using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeWorkingPeriodSampleRepository
    {
        Task<List<ProductSubTypeWorkingPeriodSample>> GetAll();
        Task<ProductSubTypeWorkingPeriodSample?> GetById(Guid id);
        Task<List<ProductSubTypeWorkingPeriodSample?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeWorkingPeriodSample productSubTypeWorkingPeriodSample);
        Task Update(ProductSubTypeWorkingPeriodSample productSubTypeWorkingPeriodSample);
        Task Delete(Guid id);
    }
}
