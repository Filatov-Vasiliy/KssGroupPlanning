using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewProductSubTypeStageSampleRepository
    {
        Task<List<ProductSubTypeStageSampleEntity>> GetAll();
        Task<ProductSubTypeStageSampleEntity?> GetById(Guid id);
        Task<List<ProductSubTypeStageSampleEntity?>> GetByProductSubTypeId(Guid productSubTypeId);
        Task Add(ProductSubTypeStageSampleEntity productSubTypeStageSample);
        Task Update(ProductSubTypeStageSampleEntity productSubTypeStageSample);
        Task Delete(Guid id);
    }
}
