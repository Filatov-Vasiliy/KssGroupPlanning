using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodStageMaterialRepository
    {
        Task<List<WorkingPeriodStageMaterialEntity>> GetAll();
        Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Update(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Delete(Guid id);
    }
}
