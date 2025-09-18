using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodStageTypeRepository
    {
        Task<List<WorkingPeriodsStageTypes>> GetAll();
        Task<WorkingPeriodsStageTypes?> GetById(Guid id);
        Task<List<WorkingPeriodsStageTypes?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodsStageTypes?>> GetByStageTypeId(Guid stageTypeId);

        Task Add(WorkingPeriodsStageTypes workingPeriodStageType);
        Task Update(WorkingPeriodsStageTypes workingPeriodStageType);
        Task Delete(Guid id);
    }
}
