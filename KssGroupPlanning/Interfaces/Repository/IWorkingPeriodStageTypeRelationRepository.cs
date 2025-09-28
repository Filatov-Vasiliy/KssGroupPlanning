using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodStageTypeRelationRepository
    {
        Task<List<WorkingPeriodStageTypeRelation>> GetAll();
        Task<WorkingPeriodStageTypeRelation?> GetById(Guid id);
        Task<List<WorkingPeriodStageTypeRelation?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodStageTypeRelation?>> GetByStageTypeId(Guid stageTypeId);

        Task Add(WorkingPeriodStageTypeRelation workingPeriodStageType);
        Task Update(WorkingPeriodStageTypeRelation workingPeriodStageType);
        Task Delete(Guid id);
    }
}
