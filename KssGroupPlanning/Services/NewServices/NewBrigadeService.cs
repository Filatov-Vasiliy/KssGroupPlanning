using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewBrigadeService
{
    private readonly INewBrigadeRepository _brigadeRepository;
    public NewBrigadeService(INewBrigadeRepository brigadeRepository)
    {
        _brigadeRepository = brigadeRepository;
    }
    public async Task<List<BrigadeEntity>> GetAll()
    {
        return await _brigadeRepository.GetAll();
    }

    public async Task<BrigadeEntity> GetById(Guid id)
    {
        return await _brigadeRepository.GetById(id);
    }
    public async Task<List<BrigadeEntity?>> GetByFactoryId(Guid factoryId)
    {
        return await _brigadeRepository.GetByFactoryId(factoryId);
    }
    public async Task<List<BrigadeEntity?>> GetByStageTypeId(Guid stageTypeId)
    {
        return await _brigadeRepository.GetByStageTypeId(stageTypeId);
    }
    public async Task Add(BrigadeEntity brigade)
    {
        var brigadeEntity = new BrigadeEntity
        {
            Id = Guid.NewGuid(),
            FactoryId = brigade.FactoryId,
            StageTypeId = brigade.StageTypeId,
        };
        await _brigadeRepository.Add(brigadeEntity);
    }
    public async Task Update(BrigadeEntity brigade)
    {
        await _brigadeRepository.Update(brigade);
    }
    public async Task Delete(Guid id)
    {
        await _brigadeRepository.Delete(id);
    }

}
