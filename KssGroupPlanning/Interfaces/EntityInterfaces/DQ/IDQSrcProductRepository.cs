using KssGroupPlanning.Entities.DQ;

namespace KssGroupPlanning.Interfaces.EntityInterfaces.DQ;

public interface IDQSrcProductRepository
{
    Task<List<DQSrcProductEntity>> GetAll();
    Task Add(DQSrcProductEntity dqSrcProductEntity);
    Task Update(DQSrcProductEntity dqSrcProductEntity);
    Task Delete(Guid id);
}
