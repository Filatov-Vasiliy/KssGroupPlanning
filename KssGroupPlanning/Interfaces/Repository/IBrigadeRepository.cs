using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IBrigadeRepository
    {
        Task<List<Brigade>> GetAll();
        Task<Brigade?> GetById(Guid id);
        Task <List<Brigade?>> GetByFactoryId(Guid factoryId);
        Task <List<Brigade?>> GetByStageTypeId(Guid StageTypeId);
        Task Add(Brigade brigade);
        Task Update(Brigade brigade);
        Task Delete(Guid id);
    }
}
