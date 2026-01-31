using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodStageBrigadeRelationRepository
    {
        Task<List<WorkingPeriodStageBrigadeRelationEntity>> GetAll();
        Task<WorkingPeriodStageBrigadeRelationEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);

        Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByBrigadeId(Guid BrigadeId);

        Task Add(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation);
        Task Update(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation);
        Task Delete(Guid id);
    }
}
