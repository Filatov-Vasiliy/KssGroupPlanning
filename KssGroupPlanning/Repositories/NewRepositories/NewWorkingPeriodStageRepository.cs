using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class NewWorkingPeriodStageRepository : INewWorkingPeriodStageRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewWorkingPeriodStageRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).ToListAsync();
    }

    public async Task<WorkingPeriodStageEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriodStage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<WorkingPeriodStageEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodsSampleId)
    {
        return await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodsSampleId).ToListAsync();
    }
    public async Task<List<WorkingPeriodStageEntity?>> GetByWorkingPeriodId(Guid workingPeriodId)
    {
        return await _dbcontext.WorkingPeriodStage.AsNoTracking().OrderBy(c => c.DateFrom).Where(c => c.WorkingPeriodId == workingPeriodId).ToListAsync();
    }
    public async Task Add(WorkingPeriodStageEntity workingPeriodStage)
    {
        await _dbcontext.AddAsync(workingPeriodStage);
        await _dbcontext.SaveChangesAsync(); 
    }
    public async Task Update(WorkingPeriodStageEntity workingPeriodStage)
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