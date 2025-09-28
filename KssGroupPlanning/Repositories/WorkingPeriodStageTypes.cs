using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodsStageTypesRepository : IWorkingPeriodStageTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodsStageTypesRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodsStageTypes>> GetAll()
    {
        var workingPeriodsStageTypesEntities = await _dbcontext.WorkingPeriodStageTypes.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodsStageTypes> workingPeriodsStageTypess = new List<WorkingPeriodsStageTypes>();
        foreach (var workingPeriodsStageTypesEntity in workingPeriodsStageTypesEntities)
        {
            workingPeriodsStageTypess.Add(WorkingPeriodsStageTypes.Create(workingPeriodsStageTypesEntity.Id, workingPeriodsStageTypesEntity.StageTypeId, workingPeriodsStageTypesEntity.ProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodsStageTypess;
    }

    public async Task<WorkingPeriodsStageTypes?> GetById(Guid id)
    {

        var workingPeriodsStageTypesEntity = await _dbcontext.WorkingPeriodStageTypes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodsStageTypes.Create(workingPeriodsStageTypesEntity.Id, workingPeriodsStageTypesEntity.StageTypeId, workingPeriodsStageTypesEntity.ProductSubTypeWokingPeriodsSampleId);
    }
    public async Task<List<WorkingPeriodsStageTypes?>> GetByStageTypeId(Guid stageTypeId)
    {
        var workingPeriodsStageTypesEntities = await _dbcontext.WorkingPeriodStageTypes.AsNoTracking().Where(c => c.StageTypeId == stageTypeId).ToListAsync();
        List<WorkingPeriodsStageTypes> workingPeriodsStageTypes = new List<WorkingPeriodsStageTypes>();
        foreach (var workingPeriodsStageTypesEntity in workingPeriodsStageTypesEntities)
        {
            workingPeriodsStageTypes.Add(WorkingPeriodsStageTypes.Create(workingPeriodsStageTypesEntity.Id, workingPeriodsStageTypesEntity.StageTypeId, workingPeriodsStageTypesEntity.ProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodsStageTypes;
    }
    public async Task<List<WorkingPeriodsStageTypes?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodsSampleId)
    {
        var workingPeriodsStageTypesEntities = await _dbcontext.WorkingPeriodStageTypes.AsNoTracking().Where(c => c.ProductSubTypeWokingPeriodsSampleId == productSubTypeWorkingPeriodsSampleId).ToListAsync();
        List<WorkingPeriodsStageTypes> workingPeriodsStageTypes = new List<WorkingPeriodsStageTypes>();
        foreach (var workingPeriodsStageTypesEntity in workingPeriodsStageTypesEntities)
        {
            workingPeriodsStageTypes.Add(WorkingPeriodsStageTypes.Create(workingPeriodsStageTypesEntity.Id, workingPeriodsStageTypesEntity.StageTypeId, workingPeriodsStageTypesEntity.ProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodsStageTypes;
    }
    public async Task Add(WorkingPeriodsStageTypes workingPeriodsStageTypes)
    {
        var workingPeriodsStageTypesEntity = new WorkingPeriodsStageTypesEntity
        {
            Id = workingPeriodsStageTypes.Id,
            StageTypeId = workingPeriodsStageTypes.StageTypeId,
            ProductSubTypeWokingPeriodsSampleId = workingPeriodsStageTypes.ProductSubTypeWokingPeriodsSampleId,
        };
        await _dbcontext.AddAsync(workingPeriodsStageTypesEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodsStageTypes WorkingPeriodsStageTypes)
    {
        var WorkingPeriodsStageTypesEntity = await _dbcontext.WorkingPeriodStageTypes.FirstOrDefaultAsync(c => c.Id == WorkingPeriodsStageTypes.Id)
            ?? throw new Exception();
        WorkingPeriodsStageTypesEntity.Id = WorkingPeriodsStageTypes.Id;
        WorkingPeriodsStageTypesEntity.StageTypeId = WorkingPeriodsStageTypes.StageTypeId;
        WorkingPeriodsStageTypesEntity.ProductSubTypeWokingPeriodsSampleId = WorkingPeriodsStageTypes.ProductSubTypeWokingPeriodsSampleId;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStageTypes
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}