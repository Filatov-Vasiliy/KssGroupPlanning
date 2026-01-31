using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IMaterialStageRepository
    {
        Task<List<MaterialStageEntity>> GetAll();
        Task<MaterialStageEntity?> GetById(Guid id);
        Task<List<MaterialStageEntity?>> GetByGroupMaterialId(Guid groupMaterialId);
        Task Add(MaterialStageEntity materialStage);
        Task Update(MaterialStageEntity materialStage);
        Task Delete(Guid id);
    }
}