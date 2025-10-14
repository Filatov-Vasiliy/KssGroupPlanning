using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodStageMaterialRepository : IWorkingPeriodStageMaterialRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodStageMaterialRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageMaterial>> GetAll()
    {
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.GroupMaterialId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }

    public async Task<WorkingPeriodStageMaterial?> GetById(Guid id)
    {

        var workingPeriodStageMaterialEntity = await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.GroupMaterialId, workingPeriodStageMaterialEntity.DateDelivery);
    }
    public async Task<List<WorkingPeriodStageMaterial?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.GroupMaterialId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }
    public async Task<List<WorkingPeriodStageMaterial?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().Where(c => c.WorkingPeriodStageId == workingPeriodStageId).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.GroupMaterialId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }
    public async Task Add(WorkingPeriodStageMaterial workingPeriodStageMaterial)
    {
        var workingPeriodStageMaterialEntity = new WorkingPeriodStageMaterialEntity
        {
            Id = workingPeriodStageMaterial.Id,
            WorkingPeriodStageId = workingPeriodStageMaterial.WorkingPeriodStageId,
            GroupMaterialId = workingPeriodStageMaterial.GroupMaterialId,
            DateDelivery = workingPeriodStageMaterial.DateDelivery,
        };
        await _dbcontext.AddAsync(workingPeriodStageMaterialEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageMaterial workingPeriodStageMaterial)
    {
        var workingPeriodStageMaterialEntity = await _dbcontext.WorkingPeriodStageMaterial.FirstOrDefaultAsync(c => c.Id == workingPeriodStageMaterial.Id)
            ?? throw new Exception();
        workingPeriodStageMaterialEntity.Id = workingPeriodStageMaterial.Id;
        workingPeriodStageMaterialEntity.WorkingPeriodStageId = workingPeriodStageMaterial.WorkingPeriodStageId;
        workingPeriodStageMaterialEntity.GroupMaterialId = workingPeriodStageMaterial.GroupMaterialId;
        workingPeriodStageMaterialEntity.DateDelivery = workingPeriodStageMaterial.DateDelivery;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStageMaterial
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}