using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces
{
    public interface IWorkingPeriodStageMaterialRepository
    {
        Task<List<WorkingPeriodStageMaterial>> GetAll();
        Task<WorkingPeriodStageMaterial?> GetById(Guid id);
        Task<List<WorkingPeriodStageMaterial?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);
        Task<List<WorkingPeriodStageMaterial?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(WorkingPeriodStageMaterial workingPeriodStageMaterial);
        Task Update(WorkingPeriodStageMaterial workingPeriodStageMaterial);
        Task Delete(Guid id);
    }
}
