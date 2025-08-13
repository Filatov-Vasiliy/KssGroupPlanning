using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface IProductTypeRepository
{
    Task<List<ProductType>> GetAll();
    Task<ProductType?> GetById(Guid id);
    Task<ProductType?> GetByName(string name);
    Task<List<ProductType>> GetByPage(int page, int pageSize);
    Task Add(ProductType productType);
    Task Update(ProductType productType);
    Task Delete(Guid id);
}