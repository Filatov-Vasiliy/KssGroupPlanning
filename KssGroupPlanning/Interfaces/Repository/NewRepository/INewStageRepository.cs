using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface INewStageRepository
{
    Task<List<StageEntity>> GetAll();
    Task<StageEntity?> GetById(Guid id);
    Task<StageEntity?> GetByProductId(Guid id);
    Task Add(StageEntity stage);
    Task Update(StageEntity stage);
    Task Delete(Guid id);
}