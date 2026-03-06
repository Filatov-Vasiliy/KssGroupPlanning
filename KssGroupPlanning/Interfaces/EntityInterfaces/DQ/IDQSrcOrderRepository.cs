using KssGroupPlanning.Entities.DQ;

namespace KssGroupPlanning.Interfaces.EntityInterfaces.DQ;

public interface IDQSrcOrderRepository
{
    Task<List<DQSrcOrderEntity>> GetAll();
    Task Add(DQSrcOrderEntity dqSrcOrderEntity);
    Task Update(DQSrcOrderEntity dqSrcOrderEntity);
    Task Delete(Guid id);
}
