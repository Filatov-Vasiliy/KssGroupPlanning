using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewMaterialStageRepository
    {
        Task<List<MaterialStageEntity>> GetAll();
        Task<MaterialStageEntity?> GetById(Guid id);
        Task<List<MaterialStageEntity?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(MaterialStageEntity materialStage);
        Task Update(MaterialStageEntity materialStage);
        Task Delete(Guid id);
    }
}