using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IMaterialStageRepository
    {
        Task<List<MaterialStage>> GetAll();
        Task<MaterialStage?> GetById(Guid id);
        Task<List<MaterialStage?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(MaterialStage materialStage);
        Task Update(MaterialStage materialStage);
        Task Delete(Guid id);
    }
}