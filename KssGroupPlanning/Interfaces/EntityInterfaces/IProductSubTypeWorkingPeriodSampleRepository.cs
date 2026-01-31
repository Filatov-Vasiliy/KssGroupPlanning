using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IProductSubTypeWorkingPeriodSampleRepository
    {
        Task<List<ProductSubTypeWorkingPeriodSampleEntity>> GetAll();
        Task<ProductSubTypeWorkingPeriodSampleEntity?> GetById(Guid id);
        Task<List<ProductSubTypeWorkingPeriodSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample);
        Task Update(ProductSubTypeWorkingPeriodSampleEntity productSubTypeWorkingPeriodSample);
        Task Delete(Guid id);
    }
}
