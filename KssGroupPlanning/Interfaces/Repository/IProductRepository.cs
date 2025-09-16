using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task<Product?> GetByNumber(string number);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}