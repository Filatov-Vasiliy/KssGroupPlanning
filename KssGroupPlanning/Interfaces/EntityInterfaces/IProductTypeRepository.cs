using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces;

public interface IProductTypeRepository
{
    Task<List<ProductTypeEntity>> GetAll();
    Task<List<ProductTypeEntity>> GetAllDetailed();
    Task<ProductTypeEntity?> GetById(Guid id);
    Task<ProductTypeEntity?> GetByName(string name);
    Task<List<ProductTypeEntity>> GetByPage(int page, int pageSize);
    Task Add(ProductTypeEntity productType);
    Task Update(ProductTypeEntity productType);
    Task Delete(Guid id);
}