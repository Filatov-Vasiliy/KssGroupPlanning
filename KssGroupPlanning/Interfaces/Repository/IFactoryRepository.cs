using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface IFactoryRepository
{
    Task<List<Factory>> GetAll();
    Task<Factory?> GetById(Guid id);
    Task<Factory?> GetByName(string name);
    Task Add(Factory factory);
    Task Update(Factory factory);
    Task Delete(Guid id);
}