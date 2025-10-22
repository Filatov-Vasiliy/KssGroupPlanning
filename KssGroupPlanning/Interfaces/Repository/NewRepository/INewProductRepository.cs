using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Interfaces.Repository;

public interface INewProductRepository
{
    Task<List<ProductEntity>> GetAll();
    Task<ProductEntity?> GetById(Guid id);
    Task<ProductEntity?> GetByNumber(string number);
    Task<List<ProductEntity?>> GetByOrderId(Guid orderId);
    Task<List<ProductEntity?>> GetByProductSubTypeId(Guid productSubTypeId);
    Task<List<ProductEntity?>> GetByFactoryId(Guid factoryId);
    Task Add(ProductEntity product);
    Task Update(ProductEntity product);
    Task Delete(Guid id);
}