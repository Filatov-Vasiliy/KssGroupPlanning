using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodRelationRepository
    {
        Task<List<WorkingPeriodsRelations>> GetAll();
        Task<WorkingPeriodsRelations?> GetById(Guid id);
        Task<List<WorkingPeriodsRelations?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodsRelations?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId);

        Task Add(WorkingPeriodsRelations workingPeriodsRelations);
        Task Update(WorkingPeriodsRelations workingPeriodsRelations);
        Task Delete(Guid id);
    }
}
