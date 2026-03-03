using CsvHelper.Configuration;
using CsvHelper;
using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Interfaces.EntityInterfaces.DQ;
using System.Globalization;

namespace KssGroupPlanning.Services.DQServices;

public class DQSrcMaterialService
{
    private readonly IDQSrcMaterialRepository _dqSrcMaterialRepository;
    private readonly ILogger<DQSrcMaterialService> _logger;
    public DQSrcMaterialService(IDQSrcMaterialRepository dqSrcMaterialRepository, ILogger<DQSrcMaterialService> logger)
    {
        _dqSrcMaterialRepository = dqSrcMaterialRepository;
        _logger = logger;
    }
    public async Task<List<DQSrcMaterialEntity>> GetAll()
    {
        return await _dqSrcMaterialRepository.GetAll();
    }
   
    public async Task Add(DQSrcMaterialEntity dqSrcMaterialEntity)
    {
        dqSrcMaterialEntity.Id = Guid.NewGuid();
        await _dqSrcMaterialRepository.Add(dqSrcMaterialEntity);
    }
    public async Task Update(DQSrcMaterialEntity dqSrcMaterialEntity)
    {
        await _dqSrcMaterialRepository.Update(dqSrcMaterialEntity);
    }
    public async Task Delete(Guid id)
    {
        await _dqSrcMaterialRepository.Delete(id);
    }
}
