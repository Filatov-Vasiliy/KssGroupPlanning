using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces;
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
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterials.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.MaterialSampleId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }

    public async Task<WorkingPeriodStageMaterial?> GetById(Guid id)
    {

        var workingPeriodStageMaterialEntity = await _dbcontext.WorkingPeriodStageMaterials.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.MaterialSampleId, workingPeriodStageMaterialEntity.DateDelivery);
    }
    public async Task<List<WorkingPeriodStageMaterial?>> GetByMaterialSampleId(Guid materialSampleId)
    {
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterials.AsNoTracking().Where(c => c.MaterialSampleId == materialSampleId).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.MaterialSampleId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }
    public async Task<List<WorkingPeriodStageMaterial?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        var workingPeriodStageMaterialEntities = await _dbcontext.WorkingPeriodStageMaterials.AsNoTracking().Where(c => c.WorkingPeriodStageId == workingPeriodStageId).ToListAsync();
        List<WorkingPeriodStageMaterial> workingPeriodStageMaterials = new List<WorkingPeriodStageMaterial>();
        foreach (var workingPeriodStageMaterialEntity in workingPeriodStageMaterialEntities)
        {
            workingPeriodStageMaterials.Add(WorkingPeriodStageMaterial.Create(workingPeriodStageMaterialEntity.Id, workingPeriodStageMaterialEntity.WorkingPeriodStageId, workingPeriodStageMaterialEntity.MaterialSampleId, workingPeriodStageMaterialEntity.DateDelivery));
        }
        return workingPeriodStageMaterials;
    }
    public async Task Add(WorkingPeriodStageMaterial workingPeriodStageMaterial)
    {
        var workingPeriodStageMaterialEntity = new WorkingPeriodStageMaterialEntity
        {
            Id = workingPeriodStageMaterial.Id,
            WorkingPeriodStageId = workingPeriodStageMaterial.WorkingPeriodStageId,
            MaterialSampleId = workingPeriodStageMaterial.MaterialSampleId,
            DateDelivery = workingPeriodStageMaterial.DateDelivery,
        };
        await _dbcontext.AddAsync(workingPeriodStageMaterialEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageMaterial workingPeriodStageMaterial)
    {
        var workingPeriodStageMaterialEntity = await _dbcontext.WorkingPeriodStageMaterials.FirstOrDefaultAsync(c => c.Id == workingPeriodStageMaterial.Id)
            ?? throw new Exception();
        workingPeriodStageMaterialEntity.Id = workingPeriodStageMaterial.Id;
        workingPeriodStageMaterialEntity.WorkingPeriodStageId = workingPeriodStageMaterial.WorkingPeriodStageId;
        workingPeriodStageMaterialEntity.MaterialSampleId = workingPeriodStageMaterial.MaterialSampleId;
        workingPeriodStageMaterialEntity.DateDelivery = workingPeriodStageMaterial.DateDelivery;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStageMaterials
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}