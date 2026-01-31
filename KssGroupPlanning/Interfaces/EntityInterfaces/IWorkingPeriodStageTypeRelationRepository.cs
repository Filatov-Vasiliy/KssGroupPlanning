using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodStageTypeRelationRepository
    {
        Task<List<WorkingPeriodStageTypeRelationEntity>> GetAll();
        Task<WorkingPeriodStageTypeRelationEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByStageTypeId(Guid stageTypeId);

        Task Add(WorkingPeriodStageTypeRelationEntity workingPeriodStageType);
        Task Update(WorkingPeriodStageTypeRelationEntity workingPeriodStageType);
        Task Delete(Guid id);
    }
}
