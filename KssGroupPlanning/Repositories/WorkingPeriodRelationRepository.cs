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
    public async Task<List<WorkingPeriodRelation>> GetAll()
    {
        var workingPeriodRelationEntities = await _dbcontext.WorkingPeriodRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodRelation> workingPeriodRelation = new List<WorkingPeriodRelation>();
        foreach (var workingPeriodRelationEntity in workingPeriodRelationEntities)
        {
           workingPeriodRelation.Add(WorkingPeriodRelation.Create(workingPeriodRelationEntity.Id, workingPeriodRelationEntity.ParentProductSubTypeWorkingPeriodSampleId, workingPeriodRelationEntity.ChildProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodRelation;
    }

    public async Task<WorkingPeriodRelation?> GetById(Guid id)
    {

        var workingPeriodRelationEntity = await _dbcontext.WorkingPeriodRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodRelation.Create(workingPeriodRelationEntity.Id, workingPeriodRelationEntity.ParentProductSubTypeWorkingPeriodSampleId, workingPeriodRelationEntity.ChildProductSubTypeWorkingPeriodSampleId);
    }
    public async Task<List<WorkingPeriodRelation?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId)
    {
        var workingPeriodRelationEntities = await _dbcontext.WorkingPeriodRelation.AsNoTracking().Where(c => c.ParentProductSubTypeWorkingPeriodSampleId == parentProductSubTypeWorkingPeriodSampleId).ToListAsync();
        List<WorkingPeriodRelation> workingPeriodsRelations = new List<WorkingPeriodRelation>();
        foreach (var workingPeriodRelationEntity in workingPeriodRelationEntities)
        {
            workingPeriodsRelations.Add(WorkingPeriodRelation.Create(workingPeriodRelationEntity.Id, workingPeriodRelationEntity.ParentProductSubTypeWorkingPeriodSampleId, workingPeriodRelationEntity.ChildProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodsRelations;
    }
    public async Task<List<WorkingPeriodRelation?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId)
    {
        var workingPeriodsRelationsEntities = await _dbcontext.WorkingPeriodRelation.AsNoTracking().Where(c => c.ChildProductSubTypeWorkingPeriodSampleId == childProductSubTypeWorkingPeriodSampleId).ToListAsync();
        List<WorkingPeriodRelation> workingPeriodsRelations = new List<WorkingPeriodRelation>();
        foreach (var workingPeriodsRelationsEntity in workingPeriodsRelationsEntities)
        {
            workingPeriodsRelations.Add(WorkingPeriodRelation.Create(workingPeriodsRelationsEntity.Id, workingPeriodsRelationsEntity.ParentProductSubTypeWorkingPeriodSampleId, workingPeriodsRelationsEntity.ChildProductSubTypeWorkingPeriodSampleId));
        }
        return workingPeriodsRelations;
    }
    public async Task Add(WorkingPeriodRelation workingPeriodRelation)
    {
        var workingPeriodRelationEntity = new WorkingPeriodRelationEntity
        {
            Id = workingPeriodRelation.Id,
            ParentProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ParentProductSubTypeWorkingPeriodSampleId,
            ChildProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ChildProductSubTypeWorkingPeriodSampleId,
        };
        await _dbcontext.AddAsync(workingPeriodRelationEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodRelation workingPeriodRelation)
    {
        var workingPeriodRelationEntity = await _dbcontext.WorkingPeriodRelation.FirstOrDefaultAsync(c => c.Id == workingPeriodRelation.Id)
            ?? throw new Exception();
        workingPeriodRelationEntity.Id = workingPeriodRelation.Id;
        workingPeriodRelationEntity.ParentProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ParentProductSubTypeWorkingPeriodSampleId;
        workingPeriodRelationEntity.ChildProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ChildProductSubTypeWorkingPeriodSampleId;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodRelation
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}