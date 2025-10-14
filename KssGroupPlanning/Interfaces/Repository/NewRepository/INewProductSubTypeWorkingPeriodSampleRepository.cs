using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewProductSubTypeWorkingPeriodSampleRepository
    {
        Task<List<ProductSubTypeWorkingPeriodSampleEntity>> GetAll();
        Task<ProductSubTypeWorkingPeriodSampleEntity?> GetById(Guid id);
        Task<List<ProductSubTypeWorkingPeriodSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample);
        Task Update(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample);
        Task Delete(Guid id);
    }
}
