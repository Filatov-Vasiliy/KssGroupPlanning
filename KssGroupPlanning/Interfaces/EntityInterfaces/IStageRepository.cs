using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces;

public interface IStageRepository
{
    Task<List<StageEntity>> GetAll();
    Task<StageEntity?> GetById(Guid id);
    Task<StageEntity?> GetByProductId(Guid id);
    Task<StageEntity?> GetByProductSubTypeStageSampleId(Guid id);
    Task Add(StageEntity stage);
    Task Update(StageEntity stage);
    Task Delete(Guid id);
}