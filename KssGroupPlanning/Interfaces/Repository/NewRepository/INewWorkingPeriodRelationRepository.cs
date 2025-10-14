using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewWorkingPeriodRelationRepository
    {
        Task<List<WorkingPeriodRelationEntity>> GetAll();
        Task<WorkingPeriodRelationEntity?> GetById(Guid id);
        Task<List<WorkingPeriodRelationEntity?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId);

        Task<List<WorkingPeriodRelationEntity?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId);

        Task Add(WorkingPeriodRelationEntity workingPeriodsRelations);
        Task Update(WorkingPeriodRelationEntity workingPeriodsRelations);
        Task Delete(Guid id);
    }
}
