using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IProductSubTypeRepository
    {
        Task<List<ProductSubTypeEntity>> GetAll();
        Task<ProductSubTypeEntity?> GetById(Guid id);
        Task<ProductSubTypeEntity?> GetByName(string name);
        Task<List<ProductSubTypeEntity?>> GetByProductTypeId(Guid productTypeId);
        Task Add(ProductSubTypeEntity productSubType);
        Task Update(ProductSubTypeEntity productSubType);
        Task Delete(Guid id);
    }
}
