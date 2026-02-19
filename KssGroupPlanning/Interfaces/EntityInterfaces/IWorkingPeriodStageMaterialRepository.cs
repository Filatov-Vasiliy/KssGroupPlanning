using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodStageMaterialRepository
    {
        Task<List<WorkingPeriodStageMaterialEntity>> GetAll();
        Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByProductId(Guid productId);
        Task<List<WorkingPeriodStageMaterialEntity?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task<WorkingPeriodStageMaterialEntity?> GetByComplexKey(Guid ProductId, Guid GroupMaterialId);
        Task Add(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Update(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial);
        Task Delete(Guid id);
    }
}
