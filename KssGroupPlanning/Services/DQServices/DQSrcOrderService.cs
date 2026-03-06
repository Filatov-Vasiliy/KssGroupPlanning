using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;

namespace KssGroupPlanning.Services.DQServices;

public class DQSrcOrderService
{
    private readonly IDQSrcOrderRepository _dqSrcOrderRepository;
    private readonly ILogger<DQSrcOrderService> _logger;
    public DQSrcOrderService(IDQSrcOrderRepository dqSrcOrderRepository, ILogger<DQSrcOrderService> logger)
    {
        _dqSrcOrderRepository = dqSrcOrderRepository;
        _logger = logger;
    }
    public async Task<List<DQSrcOrderEntity>> GetAll()
    {
        return await _dqSrcOrderRepository.GetAll();
    }

    public async Task Add(DQSrcOrderEntity dqSrcOrderEntity)
    {
        dqSrcOrderEntity.Id = Guid.NewGuid();
        await _dqSrcOrderRepository.Add(dqSrcOrderEntity);
    }
    public async Task Update(DQSrcOrderEntity dqSrcOrderEntity)
    {
        await _dqSrcOrderRepository.Update(dqSrcOrderEntity);
    }
    public async Task Delete(Guid id)
    {
        await _dqSrcOrderRepository.Delete(id);
    }
}
