using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewWorkingPeriodStageBrigadeRelationRepository
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
