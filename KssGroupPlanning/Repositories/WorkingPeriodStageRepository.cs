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
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStages.AsNoTracking().OrderBy(c => c.StartDate).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.StartDate, workingPeriodStageEntity.EndDate,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling,workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId, workingPeriodStageEntity.BrigadeId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }

    public async Task<WorkingPeriodStage?> GetById(Guid id)
    {

        var workingPeriodStageEntity = await _dbcontext.WorkingPeriodStages.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.StartDate, workingPeriodStageEntity.EndDate,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId, workingPeriodStageEntity.BrigadeId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime);
    }
    public async Task<List<WorkingPeriodStage?>> GetByBrigadeId(Guid brigadeId)
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStages.AsNoTracking().OrderBy(c => c.StartDate).Where(c => c.BrigadeId == brigadeId).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.StartDate, workingPeriodStageEntity.EndDate,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId, workingPeriodStageEntity.BrigadeId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }
    public async Task<List<WorkingPeriodStage?>> GetByProductSubTypeWorkingPeriodsSampleId(Guid productSubTypeWorkingPeriodsSampleId)
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStages.AsNoTracking().OrderBy(c => c.StartDate).Where(c => c.ProductSubTypeWorkingPeriodsSampleId == productSubTypeWorkingPeriodsSampleId).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.StartDate, workingPeriodStageEntity.EndDate,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId, workingPeriodStageEntity.BrigadeId,
                workingPeriodStageEntity.CreateTime, workingPeriodStageEntity.UpdateTime));
        }
        return workingPeriodStages;
    }
    public async Task<List<WorkingPeriodStage?>> GetByWorkingPeriodId(Guid workingPeriodId)
    {
        var workingPeriodStageEntities = await _dbcontext.WorkingPeriodStages.AsNoTracking().OrderBy(c => c.StartDate).Where(c => c.WorkingPeriodId == workingPeriodId).ToListAsync();
        List<WorkingPeriodStage> workingPeriodStages = new List<WorkingPeriodStage>();
        foreach (var workingPeriodStageEntity in workingPeriodStageEntities)
        {
            workingPeriodStages.Add(WorkingPeriodStage.Create(workingPeriodStageEntity.Id, workingPeriodStageEntity.WorkingPeriodId, workingPeriodStageEntity.StartDate, workingPeriodStageEntity.EndDate,
                workingPeriodStageEntity.Status, workingPeriodStageEntity.Recycling, workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId, workingPeriodStageEntity.BrigadeId,
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
            StartDate = workingPeriodStage.StartDate,
            EndDate = workingPeriodStage.EndDate,
            Status = workingPeriodStage.Status,
            Recycling = workingPeriodStage.Recycling,
            ProductSubTypeWorkingPeriodsSampleId = workingPeriodStage.ProductSubTypeWorkingPeriodsSampleId,
            BrigadeId = workingPeriodStage.BrigadeId,
            CreateTime = workingPeriodStage.CreateTime,
            UpdateTime = workingPeriodStage.UpdateTime
        };
        await _dbcontext.AddAsync(workingPeriodStageEntity);
        await _dbcontext.SaveChangesAsync(); 
    }
    public async Task Update(WorkingPeriodStage workingPeriodStage)
    {
        var workingPeriodStageEntity = await _dbcontext.WorkingPeriodStages.FirstOrDefaultAsync(c => c.Id == workingPeriodStage.Id)
            ?? throw new Exception();
        workingPeriodStageEntity.Id = workingPeriodStage.Id;
        workingPeriodStageEntity.WorkingPeriodId = workingPeriodStage.WorkingPeriodId;
        workingPeriodStageEntity.StartDate = workingPeriodStage.StartDate;
        workingPeriodStageEntity.EndDate = workingPeriodStage.EndDate;
        workingPeriodStageEntity.Status = workingPeriodStage.Status;
        workingPeriodStageEntity.Recycling = workingPeriodStage.Recycling;
        workingPeriodStageEntity.ProductSubTypeWorkingPeriodsSampleId = workingPeriodStage.ProductSubTypeWorkingPeriodsSampleId;
        workingPeriodStageEntity.BrigadeId = workingPeriodStage.BrigadeId;
        workingPeriodStageEntity.CreateTime = workingPeriodStage.CreateTime;
        workingPeriodStageEntity.UpdateTime = workingPeriodStage.UpdateTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStages
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}