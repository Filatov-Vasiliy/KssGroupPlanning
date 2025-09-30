using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodStageBrigadeRelationRepository
    {
        Task<List<WorkingPeriodStageBrigadeRelation>> GetAll();
        Task<WorkingPeriodStageBrigadeRelation?> GetById(Guid id);
        Task<List<WorkingPeriodStageBrigadeRelation?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);

        Task<List<WorkingPeriodStageBrigadeRelation?>> GetByBrigadeId(Guid BrigadeId);

        Task Add(WorkingPeriodStageBrigadeRelation workingPeriodStageBrigadeRelation);
        Task Update(WorkingPeriodStageBrigadeRelation workingPeriodStageBrigadeRelation);
        Task Delete(Guid id);
    }
}
