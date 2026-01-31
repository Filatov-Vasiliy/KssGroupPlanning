using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodRelationRepository
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
