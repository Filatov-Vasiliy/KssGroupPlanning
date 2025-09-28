using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodRelationRepository
    {
        Task<List<WorkingPeriodRelation>> GetAll();
        Task<WorkingPeriodRelation?> GetById(Guid id);
        Task<List<WorkingPeriodRelation?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodRelation?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId);

        Task Add(WorkingPeriodRelation workingPeriodsRelations);
        Task Update(WorkingPeriodRelation workingPeriodsRelations);
        Task Delete(Guid id);
    }
}
