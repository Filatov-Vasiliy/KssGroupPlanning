using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodStageRepository
    {
        Task<List<WorkingPeriodStage>> GetAll();
        Task<WorkingPeriodStage?> GetById(Guid id);
        Task<List<WorkingPeriodStage?>> GetByWorkingPeriodId(Guid workingPeriodId);
        Task<List<WorkingPeriodStage?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);
        Task Add(WorkingPeriodStage workingPeriodStage);
        Task Update(WorkingPeriodStage workingPeriodStage);
        Task Delete(Guid id);
    }
}
