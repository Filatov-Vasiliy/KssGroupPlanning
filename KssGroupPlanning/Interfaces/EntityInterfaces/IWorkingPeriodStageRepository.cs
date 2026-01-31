using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodStageRepository
    {
        Task<List<WorkingPeriodStageEntity>> GetAll();
        Task<WorkingPeriodStageEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageEntity?>> GetByWorkingPeriodId(Guid workingPeriodId);
        Task<List<WorkingPeriodStageEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);
        Task Add(WorkingPeriodStageEntity workingPeriodStage);
        Task Update(WorkingPeriodStageEntity workingPeriodStage);
        Task Delete(Guid id);
    }
}
