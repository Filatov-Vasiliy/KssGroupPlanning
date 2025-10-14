using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface INewFactoryRepository
{
    Task<List<FactoryEntity>> GetAll();
    Task<FactoryEntity?> GetById(Guid id);
    Task<FactoryEntity?> GetByName(string name);
    Task Add(FactoryEntity factory);
    Task Update(FactoryEntity factory);
    Task Delete(Guid id);
}