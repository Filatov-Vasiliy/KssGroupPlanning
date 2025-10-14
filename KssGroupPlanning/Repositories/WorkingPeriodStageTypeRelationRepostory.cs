using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodStageTypeRelationRepository : IWorkingPeriodStageTypeRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodStageTypeRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageTypeRelation>> GetAll()
    {
        var workingPeriodStageTypeRelationEntities = await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodStageTypeRelation> workingPeriodStageTypeRelations = new List<WorkingPeriodStageTypeRelation>();
        foreach (var workingPeriodStageTypeRelationEntity in workingPeriodStageTypeRelationEntities)
        {
            workingPeriodStageTypeRelations.Add(WorkingPeriodStageTypeRelation.Create(workingPeriodStageTypeRelationEntity.Id, workingPeriodStageTypeRelationEntity.StageTypeId, workingPeriodStageTypeRelationEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodStageTypeRelations;
    }

    public async Task<WorkingPeriodStageTypeRelation?> GetById(Guid id)
    {

        var workingPeriodStageTypeEntity = await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStageTypeRelation.Create(workingPeriodStageTypeEntity.Id, workingPeriodStageTypeEntity.StageTypeId, workingPeriodStageTypeEntity.ProductSubTypeWorkingPeriodSampleId);
    }
    public async Task<List<WorkingPeriodStageTypeRelation?>> GetByStageTypeId(Guid stageTypeId)
    {
        var workingPeriodStageTypeRelationEntities = await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().Where(c => c.StageTypeId == stageTypeId).ToListAsync();
        List<WorkingPeriodStageTypeRelation> workingPeriodStageTypeRelations = new List<WorkingPeriodStageTypeRelation>();
        foreach (var workingPeriodStageTypeEntity in workingPeriodStageTypeRelationEntities)
        {
            workingPeriodStageTypeRelations.Add(WorkingPeriodStageTypeRelation.Create(workingPeriodStageTypeEntity.Id, workingPeriodStageTypeEntity.StageTypeId, workingPeriodStageTypeEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodStageTypeRelations; ;
    }
    public async Task<List<WorkingPeriodStageTypeRelation?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        var workingPeriodStageTypeRelationEntities = await _dbcontext.WorkingPeriodStageTypeRelation.AsNoTracking().Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodSampleId).ToListAsync();
        List<WorkingPeriodStageTypeRelation> workingPeriodStageTypeRelations = new List<WorkingPeriodStageTypeRelation>();
        foreach (var workingPeriodStageTypeEntity in workingPeriodStageTypeRelationEntities)
        {
            workingPeriodStageTypeRelations.Add(WorkingPeriodStageTypeRelation.Create(workingPeriodStageTypeEntity.Id, workingPeriodStageTypeEntity.StageTypeId, workingPeriodStageTypeEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodStageTypeRelations;
    }
    public async Task Add(WorkingPeriodStageTypeRelation workingPeriodStageTypeRelation)
    {
        var workingPeriodStageTypeRelationEntity = new WorkingPeriodStageTypeRelationEntity
        {
            Id = workingPeriodStageTypeRelation.Id,
            StageTypeId = workingPeriodStageTypeRelation.StageTypeId,
            ProductSubTypeWorkingPeriodSampleId = workingPeriodStageTypeRelation.ProductSubTypeWorkingPeriodSampleId,
        };
        await _dbcontext.AddAsync(workingPeriodStageTypeRelationEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageTypeRelation workingPeriodStageTypeRelation)
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