using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewBrigadeRepository
    {
        Task<List<BrigadeEntity>> GetAll();
        Task<BrigadeEntity?> GetById(Guid id);
        Task <List<BrigadeEntity?>> GetByFactoryId(Guid factoryId);
        Task <List<BrigadeEntity?>> GetByStageTypeId(Guid StageTypeId);
        Task Add(BrigadeEntity brigade);
        Task Update(BrigadeEntity brigade);
        Task Delete(Guid id);
    }
}
