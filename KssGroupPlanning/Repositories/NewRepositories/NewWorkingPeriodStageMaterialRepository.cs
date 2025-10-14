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

public class NewWorkingPeriodStageMaterialRepository : INewWorkingPeriodStageMaterialRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewWorkingPeriodStageMaterialRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageMaterialEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<WorkingPeriodStageMaterialEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<WorkingPeriodStageMaterialEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        return await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
    }
    public async Task<List<WorkingPeriodStageMaterialEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        return await _dbcontext.WorkingPeriodStageMaterial.AsNoTracking().Where(c => c.WorkingPeriodStageId == workingPeriodStageId).ToListAsync();
    }
    public async Task Add(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial)
    {
        await _dbcontext.AddAsync(workingPeriodStageMaterial);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageMaterialEntity workingPeriodStageMaterial)
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