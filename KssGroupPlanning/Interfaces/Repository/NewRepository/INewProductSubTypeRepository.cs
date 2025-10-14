using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewProductSubTypeRepository
    {
        Task<List<ProductSubTypeEntity>> GetAll();
        Task<ProductSubTypeEntity?> GetById(Guid id);
        Task<ProductSubTypeEntity?> GetByName(string name);
        Task<ProductSubTypeEntity?> GetByProductTypeId(Guid productTypeId);
        Task Add(ProductSubTypeEntity productSubType);
        Task Update(ProductSubTypeEntity productSubType);
        Task Delete(Guid id);
    }
}
