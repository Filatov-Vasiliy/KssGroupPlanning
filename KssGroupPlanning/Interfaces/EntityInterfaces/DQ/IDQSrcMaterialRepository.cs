using KssGroupPlanning.Entities.DQ;

namespace KssGroupPlanning.Interfaces.EntityInterfaces.DQ;

public interface IDQSrcMaterialRepository
{
    Task<List<DQSrcMaterialEntity>> GetAll();
    Task Add(DQSrcMaterialEntity dqSrcMaterialEntity);
    Task Update(DQSrcMaterialEntity dqSrcMaterialEntity);
    Task Delete(Guid id);
}
