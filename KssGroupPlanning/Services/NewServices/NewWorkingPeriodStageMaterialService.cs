using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewWorkingPeriodStageMaterialService
{
    private readonly INewWorkingPeriodStageMaterialRepository _workingPeriodStageMaterialRepository;
    public NewWorkingPeriodStageMaterialService(INewWorkingPeriodStageMaterialRepository workingPeriodStageMaterialRepository)
    {
        _workingPeriodStageMaterialRepository = workingPeriodStageMaterialRepository;
    }
    public async Task<List<WorkingPeriodStageMaterialEntity>> GetAll()
    {
        return await _workingPeriodStageMaterialRepository.GetAll();
    }

    public async Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id)
    {
        return await _workingPeriodStageMaterialRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodStageMaterialEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        return await _workingPeriodStageMaterialRepository.GetByWorkingPeriodStageId( workingPeriodStageId);
    }
    public async Task<List<WorkingPeriodStageMaterialEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        return await _workingPeriodStageMaterialRepository.GetByGroupMaterialId(groupMaterialId);
    }
    public async Task Add(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial)
    {
        var workingPeriodStageMaterialEntity = new WorkingPeriodStageMaterialEntity
        {
            Id = Guid.NewGuid(),
            WorkingPeriodStageId = workingPeriodStageMaterial.WorkingPeriodStageId,
            GroupMaterialId = workingPeriodStageMaterial.GroupMaterialId,
            DateDelivery = workingPeriodStageMaterial.DateDelivery,
        };
        await _workingPeriodStageMaterialRepository.Add(workingPeriodStageMaterialEntity);
    }
    public async Task Update(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial)
    {
        await _workingPeriodStageMaterialRepository.Update(workingPeriodStageMaterial);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodStageMaterialRepository.Delete(id);
    }

}
