using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface INewProductTypeRepository
{
    Task<List<ProductTypeEntity>> GetAll();
    Task<ProductTypeEntity?> GetById(Guid id);
    Task<ProductTypeEntity?> GetByName(string name);
    Task<List<ProductTypeEntity>> GetByPage(int page, int pageSize);
    Task Add(ProductTypeEntity productType);
    Task Update(ProductTypeEntity productType);
    Task Delete(Guid id);
}