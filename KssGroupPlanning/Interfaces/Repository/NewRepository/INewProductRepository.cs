using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface INewProductRepository
{
    Task<List<ProductEntity>> GetAll();
    Task<ProductEntity?> GetById(Guid id);
    Task<ProductEntity?> GetByNumber(string number);
    Task Add(ProductEntity product);
    Task Update(ProductEntity product);
    Task Delete(Guid id);
}