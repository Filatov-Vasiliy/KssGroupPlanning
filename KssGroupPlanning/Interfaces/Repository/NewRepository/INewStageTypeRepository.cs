using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewStageTypeRepository
    {
        Task<List<StageTypeEntity>> GetAll();
        Task<StageTypeEntity?> GetById(Guid id);
        Task<StageTypeEntity?> GetByName(string name);
        Task Add(StageTypeEntity stageType);
        Task Update(StageTypeEntity stageType);
        Task Delete(Guid id);
    }
}
