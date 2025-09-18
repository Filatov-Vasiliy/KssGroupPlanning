using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodRelationRepository : IWorkingPeriodRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodsRelations>> GetAll()
    {
        var workingPeriodRelationEntities = await _dbcontext.WorkingPeriodsRelations.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodsRelations> workingPeriodRelation = new List<WorkingPeriodsRelations>();
        foreach (var workingPeriodRelationEntity in workingPeriodRelationEntities)
        {
           workingPeriodRelation.Add(WorkingPeriodsRelations.Create(workingPeriodRelationEntity.Id, workingPeriodRelationEntity.ParentProductSubTypeWokingPeriodsSampleId, workingPeriodRelationEntity.ChildProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodRelation;
    }

    public async Task<WorkingPeriodsRelations?> GetById(Guid id)
    {

        var workingPeriodRelationEntity = await _dbcontext.WorkingPeriodsRelations.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodsRelations.Create(workingPeriodRelationEntity.Id, workingPeriodRelationEntity.ParentProductSubTypeWokingPeriodsSampleId, workingPeriodRelationEntity.ChildProductSubTypeWokingPeriodsSampleId);
    }
    public async Task<List<WorkingPeriodsRelations?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId)
    {
        var workingPeriodsRelationsEntities = await _dbcontext.WorkingPeriodsRelations.AsNoTracking().Where(c => c.ParentProductSubTypeWokingPeriodsSampleId == parentProductSubTypeWorkingPeriodSampleId).ToListAsync();
        List<WorkingPeriodsRelations> workingPeriodsRelations = new List<WorkingPeriodsRelations>();
        foreach (var workingPeriodsRelationsEntity in workingPeriodsRelationsEntities)
        {
            workingPeriodsRelations.Add(WorkingPeriodsRelations.Create(workingPeriodsRelationsEntity.Id, workingPeriodsRelationsEntity.ParentProductSubTypeWokingPeriodsSampleId, workingPeriodsRelationsEntity.ChildProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodsRelations;
    }
    public async Task<List<WorkingPeriodsRelations?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodsSampleId)
    {
        var workingPeriodsRelationsEntities = await _dbcontext.WorkingPeriodsRelations.AsNoTracking().Where(c => c.ChildProductSubTypeWokingPeriodsSampleId == childProductSubTypeWorkingPeriodsSampleId).ToListAsync();
        List<WorkingPeriodsRelations> workingPeriodsRelations = new List<WorkingPeriodsRelations>();
        foreach (var workingPeriodsRelationsEntity in workingPeriodsRelationsEntities)
        {
            workingPeriodsRelations.Add(WorkingPeriodsRelations.Create(workingPeriodsRelationsEntity.Id, workingPeriodsRelationsEntity.ParentProductSubTypeWokingPeriodsSampleId, workingPeriodsRelationsEntity.ChildProductSubTypeWokingPeriodsSampleId));
        }
        return workingPeriodsRelations;
    }
    public async Task Add(WorkingPeriodsRelations workingPeriodRelation)
    {
        var workingPeriodRelationEntity = new WorkingPeriodsRelationsEntity
        {
            Id = workingPeriodRelation.Id,
            ParentProductSubTypeWokingPeriodsSampleId = workingPeriodRelation.ParentProductSubTypeWokingPeriodsSampleId,
            ChildProductSubTypeWokingPeriodsSampleId = workingPeriodRelation.ChildProductSubTypeWokingPeriodsSampleId,
        };
        await _dbcontext.AddAsync(workingPeriodRelationEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodsRelations workingPeriodRelation)
    {
        var workingPeriodRelationEntity = await _dbcontext.WorkingPeriodsRelations.FirstOrDefaultAsync(c => c.Id == workingPeriodRelation.Id)
            ?? throw new Exception();
        workingPeriodRelationEntity.Id = workingPeriodRelation.Id;
        workingPeriodRelationEntity.ParentProductSubTypeWokingPeriodsSampleId = workingPeriodRelation.ParentProductSubTypeWokingPeriodsSampleId;
        workingPeriodRelationEntity.ChildProductSubTypeWokingPeriodsSampleId = workingPeriodRelation.ChildProductSubTypeWokingPeriodsSampleId;

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