
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class StageTypeService
{
    private readonly IStageTypeRepository _StageTypeRepository;
    public StageTypeService(IStageTypeRepository StageTypeRepository)
    {
        _StageTypeRepository = StageTypeRepository;
    }
    public async Task<List<StageTypeEntity>> GetAll()
    {
        return await _StageTypeRepository.GetAll();
    }

    public async Task<StageTypeEntity> GetById(Guid id)
    {
        return await _StageTypeRepository.GetById(id);
    }
    public async Task<StageTypeEntity> GetByName(string name)
    {
        return await _StageTypeRepository.GetByName(name);
    }
    public async Task Add(StageTypeEntity stageType)
    {
        var StageTypeEntity = new StageTypeEntity
        {
            Id = Guid.NewGuid(),
            Name = stageType.Name,
        };
        await _StageTypeRepository.Add(StageTypeEntity);
    }
    public async Task Update(StageTypeEntity StageType)
    {
        await _StageTypeRepository.Update(StageType);
    }
    public async Task Delete(Guid id)
    {
        await _StageTypeRepository.Delete(id);
    }

}
