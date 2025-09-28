using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IProductSubTypeRepository
    {
        Task<List<ProductSubType>> GetAll();
        Task<ProductSubType?> GetById(Guid id);
        Task<ProductSubType?> GetByName(string name);
        Task<ProductSubType?> GetByProductTypeId(Guid productTypeId);
        Task Add(ProductSubType productSubType);
        Task Update(ProductSubType productSubType);
        Task Delete(Guid id);
    }
}
