using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IStageTypeRepository
    {
        Task<List<StageTypeEntity>> GetAll();
        Task<StageTypeEntity?> GetById(Guid id);
        Task<StageTypeEntity?> GetByName(string name);
        Task Add(StageTypeEntity stageType);
        Task Update(StageTypeEntity stageType);
        Task Delete(Guid id);
    }
}
