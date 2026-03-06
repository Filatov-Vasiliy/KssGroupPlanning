using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;

namespace KssGroupPlanning.Services.DQServices;

public class DQSrcProductService
{
    private readonly IDQSrcProductRepository _dqSrcProductRepository;
    private readonly ILogger<DQSrcProductService> _logger;
    public DQSrcProductService(IDQSrcProductRepository dqSrcProductRepository, ILogger<DQSrcProductService> logger)
    {
        _dqSrcProductRepository = dqSrcProductRepository;
        _logger = logger;
    }
    public async Task<List<DQSrcProductEntity>> GetAll()
    {
        return await _dqSrcProductRepository.GetAll();
    }

    public async Task Add(DQSrcProductEntity dqSrcProductEntity)
    {
        dqSrcProductEntity.Id = Guid.NewGuid();
        await _dqSrcProductRepository.Add(dqSrcProductEntity);
    }
    public async Task Update(DQSrcProductEntity dqSrcProductEntity)
    {
        await _dqSrcProductRepository.Update(dqSrcProductEntity);
    }
    public async Task Delete(Guid id)
    {
        await _dqSrcProductRepository.Delete(id);
    }
}
