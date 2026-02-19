
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class WorkingPeriodStageMaterialService
{
    private readonly IWorkingPeriodStageMaterialRepository _workingPeriodStageMaterialRepository;
    public WorkingPeriodStageMaterialService(IWorkingPeriodStageMaterialRepository workingPeriodStageMaterialRepository)
    {
        _workingPeriodStageMaterialRepository = workingPeriodStageMaterialRepository;
    }
    public async Task<List<WorkingPeriodStageMaterialEntity>> GetAll()
    {
        return await _workingPeriodStageMaterialRepository.GetAll();
    }

    public async Task<WorkingPeriodStageMaterialEntity?> GetByComplexKey(Guid ProductId, Guid GroupMaterialId)
    { 
        return await _workingPeriodStageMaterialRepository.GetByComplexKey(ProductId, GroupMaterialId);
    }
    public async Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id)
    {
        return await _workingPeriodStageMaterialRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodStageMaterialEntity?>> GetByProductId(Guid productId)
    {
        return await _workingPeriodStageMaterialRepository.GetByProductId( productId);
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
            ProductId = workingPeriodStageMaterial.ProductId,
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
