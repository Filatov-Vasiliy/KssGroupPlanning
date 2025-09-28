using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IStageTypeRepository
    {
        Task<List<StageType>> GetAll();
        Task<StageType?> GetById(Guid id);
        Task<StageType?> GetByName(string name);
        Task Add(StageType stageType);
        Task Update(StageType stageType);
        Task Delete(Guid id);
    }
}
