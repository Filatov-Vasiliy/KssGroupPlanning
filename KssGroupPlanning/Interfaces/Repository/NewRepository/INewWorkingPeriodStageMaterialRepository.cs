using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces
{
    public interface INewWorkingPeriodStageMaterialRepository
    {
        Task<List<WorkingPeriodStageMaterialEntity>> GetAll();
        Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Update(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Delete(Guid id);
    }
}
