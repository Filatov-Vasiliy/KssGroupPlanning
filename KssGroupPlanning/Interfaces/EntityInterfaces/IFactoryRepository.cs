using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces;

public interface IFactoryRepository
{
    Task<List<FactoryEntity>> GetAll();
    Task<FactoryEntity?> GetById(Guid id);
    Task<FactoryEntity?> GetByName(string name);
    Task Add(FactoryEntity factory);
    Task Update(FactoryEntity factory);
    Task Delete(Guid id);
}