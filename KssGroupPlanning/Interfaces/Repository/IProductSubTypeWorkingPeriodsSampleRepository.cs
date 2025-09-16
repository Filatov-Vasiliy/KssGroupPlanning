using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeWorkingPeriodsSample
    {
        Task<List<ProductSubTypeWorkingPeriodsSample>> GetAll();
        Task<ProductSubTypeWorkingPeriodsSample?> GetById(Guid id);
        Task<List<ProductSubTypeWorkingPeriodsSample?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeWorkingPeriodsSample productSubTypeWorkingPeriodsSample);
        Task Update(ProductSubTypeWorkingPeriodsSample productSubTypeWorkingPeriodsSample);
        Task Delete(Guid id);
    }
}
