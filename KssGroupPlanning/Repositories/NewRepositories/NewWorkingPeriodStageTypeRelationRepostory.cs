using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class NewWorkingPeriodStageTypeRelationRepository : INewWorkingPeriodStageTypeRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewWorkingPeriodStageTypeRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<WorkingPeriodStageTypeRelationEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByStageTypeId(Guid stageTypeId)
    {
        return await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().Where(c => c.StageTypeId == stageTypeId).ToListAsync();
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        return await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodSampleId).ToListAsync();
    }
    public async Task Add(WorkingPeriodStageTypeRelationEntity workingPeriodStageTypeRelation)
    {
        await _dbcontext.AddAsync(workingPeriodStageTypeRelation);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageTypeRelationEntity workingPeriodStageTypeRelation)
    {
        var WorkingPeriodStageTypeRelationEntity = await _dbcontext.WorkingPeriodStageTypeRelation.FirstOrDefaultAsync(c => c.Id == workingPeriodStageTypeRelation.Id)
            ?? throw new Exception();
        WorkingPeriodStageTypeRelationEntity.Id = workingPeriodStageTypeRelation.Id;
        WorkingPeriodStageTypeRelationEntity.StageTypeId = workingPeriodStageTypeRelation.StageTypeId;
        WorkingPeriodStageTypeRelationEntity.ProductSubTypeWorkingPeriodSampleId = workingPeriodStageTypeRelation.ProductSubTypeWorkingPeriodSampleId;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStageTypeRelation
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}