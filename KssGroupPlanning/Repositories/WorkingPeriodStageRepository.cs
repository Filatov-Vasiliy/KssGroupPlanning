using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodStageRepository : IWorkingPeriodStageRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodStageRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStage>> GetAll()
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.DateFrom, workingPeriodStageEntity.DateTo,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling,workingPeriodStageEntity.ProductSubTypeWorkingPeriodSampleId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }

    public async Task<WorkingPeriodStage?> GetById(Guid id)
    {

        var workingPeriodStageEntity = await _dbcontext.WorkingPeriodStage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.DateFrom, workingPeriodStageEntity.DateTo,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodSampleId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime);
    }
    public async Task<List<WorkingPeriodStage?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodsSampleId)
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodsSampleId).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.DateFrom, workingPeriodStageEntity.DateTo,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodSampleId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }
    public async Task<List<WorkingPeriodStage?>> GetByWorkingPeriodId(Guid workingPeriodId)
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).Where(c => c.WorkingPeriodId == workingPeriodId).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.DateFrom, workingPeriodStageEntity.DateTo,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodSampleId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }
    public async Task Add(WorkingPeriodStage workingPeriodStage)
    {
        var workingPeriodStageEntity = new WorkingPeriodStageEntity
        {
            Id = workingPeriodStage.Id,
            WorkingPeriodId = workingPeriodStage.WorkingPeriodId,
            DateFrom = workingPeriodStage.DateFrom,
            DateTo = workingPeriodStage.DateTo,
            Status = workingPeriodStage.Status,
            Recycling = workingPeriodStage.Recycling,
            ProductSubTypeWorkingPeriodSampleId = workingPeriodStage.ProductSubTypeWorkingPeriodSampleId,
            CreateTime = workingPeriodStage.CreateTime,
            UpdateTime = workingPeriodStage.UpdateTime
        };
        await _dbcontext.AddAsync(workingPeriodStageEntity);
        await _dbcontext.SaveChangesAsync(); 
    }
    public async Task Update(WorkingPeriodStage workingPeriodStage)
    {
        var workingPeriodStageEntity = await _dbcontext.WorkingPeriodStage.FirstOrDefaultAsync(c => c.Id == workingPeriodStage.Id)
            ?? throw new Exception();
        workingPeriodStageEntity.Id = workingPeriodStage.Id;
        workingPeriodStageEntity.WorkingPeriodId = workingPeriodStage.WorkingPeriodId;
        workingPeriodStageEntity.DateFrom = workingPeriodStage.DateFrom;
        workingPeriodStageEntity.DateTo = workingPeriodStage.DateTo;
        workingPeriodStageEntity.Status = workingPeriodStage.Status;
        workingPeriodStageEntity.Recycling = workingPeriodStage.Recycling;
        workingPeriodStageEntity.ProductSubTypeWorkingPeriodSampleId = workingPeriodStage.ProductSubTypeWorkingPeriodSampleId;
        workingPeriodStageEntity.CreateTime = workingPeriodStage.CreateTime;
        workingPeriodStageEntity.UpdateTime = workingPeriodStage.UpdateTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStage
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}