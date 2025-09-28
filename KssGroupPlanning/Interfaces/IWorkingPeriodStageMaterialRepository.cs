using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces
{
    public interface IWorkingPeriodStageMaterialRepository
    {
        Task<List<WorkingPeriodStageMaterial>> GetAll();
        Task<WorkingPeriodStageMaterial?> GetById(Guid id);
        Task<List<WorkingPeriodStageMaterial?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);
        Task<List<WorkingPeriodStageMaterial?>> GetByMaterialSampleId(Guid saterialSampleId);
        Task Add(WorkingPeriodStageMaterial workingPeriodStageMaterial);
        Task Update(WorkingPeriodStageMaterial workingPeriodStageMaterial);
        Task Delete(Guid id);
    }
}
