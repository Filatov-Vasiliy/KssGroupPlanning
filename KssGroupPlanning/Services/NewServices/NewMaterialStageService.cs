using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewMaterialStageService
{
    private readonly INewMaterialStageRepository _materialStageRepository;
    public NewMaterialStageService(INewMaterialStageRepository materialStageRepository)
    {
        _materialStageRepository = materialStageRepository;
    }
    public async Task<List<MaterialStageEntity>> GetAll()
    {
        return await _materialStageRepository.GetAll();
    }

    public async Task<MaterialStageEntity?> GetById(Guid id)
    {
        return await _materialStageRepository.GetById(id);
    }
    public async Task<List<MaterialStageEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        return await _materialStageRepository.GetByGroupMaterialId(groupMaterialId);
    }

    public async Task Add(MaterialStageEntity materialStage)
    {
        var materialStageEntity = new MaterialStageEntity
        {
            Id = Guid.NewGuid(),
            StageName = materialStage.StageName,
            GroupMaterialId = materialStage.GroupMaterialId,
        };
        await _materialStageRepository.Add(materialStageEntity);
    }
    public async Task Update(MaterialStageEntity materialStage)
    {
        await _materialStageRepository.Update(materialStage);
    }
    public async Task Delete(Guid id)
    {
        await _materialStageRepository.Delete(id);
    }

}
