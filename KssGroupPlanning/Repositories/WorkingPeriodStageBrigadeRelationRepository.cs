using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodStageBrigadeRelationRepository : IWorkingPeriodStageBrigadeRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodStageBrigadeRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageBrigadeRelation>> GetAll()
    {
        var workingPeriodStageBrigadeRelationEntities = await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<WorkingPeriodStageBrigadeRelation> workingPeriodStageBrigadeRelations = new List<WorkingPeriodStageBrigadeRelation>();
        foreach (var workingPeriodStageBrigadeRelationEntity in workingPeriodStageBrigadeRelationEntities)
        {
            workingPeriodStageBrigadeRelations.Add(WorkingPeriodStageBrigadeRelation.Create(workingPeriodStageBrigadeRelationEntity.Id, workingPeriodStageBrigadeRelationEntity.WorkingPeriodStageId, workingPeriodStageBrigadeRelationEntity.BrigadeId));
        }
        return workingPeriodStageBrigadeRelations;
    }

    public async Task<WorkingPeriodStageBrigadeRelation?> GetById(Guid id)
    {

        var workingPeriodStageBrigadeRelationEntity = await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriodStageBrigadeRelation.Create(workingPeriodStageBrigadeRelationEntity.Id, workingPeriodStageBrigadeRelationEntity.WorkingPeriodStageId, workingPeriodStageBrigadeRelationEntity.BrigadeId);
    }
    public async Task<List<WorkingPeriodStageBrigadeRelation?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        var workingPeriodStageBrigadeRelationEntities = await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().Where(c => c.WorkingPeriodStageId == workingPeriodStageId).ToListAsync();
        List<WorkingPeriodStageBrigadeRelation> workingPeriodStageBrigadeRelations = new List<WorkingPeriodStageBrigadeRelation>();
        foreach (var workingPeriodStageBrigadeRelationEntity in workingPeriodStageBrigadeRelationEntities)
        {
            workingPeriodStageBrigadeRelations.Add(WorkingPeriodStageBrigadeRelation.Create(workingPeriodStageBrigadeRelationEntity.Id, workingPeriodStageBrigadeRelationEntity.WorkingPeriodStageId, workingPeriodStageBrigadeRelationEntity.BrigadeId));
        }
        return workingPeriodStageBrigadeRelations;
    }
    public async Task<List<WorkingPeriodStageBrigadeRelation?>> GetByBrigadeId(Guid brigadeId)
    {
        var workingPeriodStageBrigadeRelationEntities = await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().Where(c => c.BrigadeId == brigadeId).ToListAsync();
        List<WorkingPeriodStageBrigadeRelation> workingPeriodStageBrigadeRelations = new List<WorkingPeriodStageBrigadeRelation>();
        foreach (var workingPeriodStageBrigadeRelationEntity in workingPeriodStageBrigadeRelationEntities)
        {
            workingPeriodStageBrigadeRelations.Add(WorkingPeriodStageBrigadeRelation.Create(workingPeriodStageBrigadeRelationEntity.Id, workingPeriodStageBrigadeRelationEntity.WorkingPeriodStageId, workingPeriodStageBrigadeRelationEntity.BrigadeId));
        }
        return workingPeriodStageBrigadeRelations;
    }
    public async Task Add(WorkingPeriodStageBrigadeRelation workingPeriodStageBrigadeRelation)
    {
        var workingPeriodStageBrigadeRelationEntity = new WorkingPeriodStageBrigadeRelationEntity
        {
            Id = workingPeriodStageBrigadeRelation.Id,
            WorkingPeriodStageId = workingPeriodStageBrigadeRelation.WorkingPeriodStageId,
            BrigadeId = workingPeriodStageBrigadeRelation.BrigadeId,
        };
        await _dbcontext.AddAsync(workingPeriodStageBrigadeRelationEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageBrigadeRelation workingPeriodStageBrigadeRelation)
    {
        var WorkingPeriodStageMaterialStageEntity = await _dbcontext.WorkingPeriodStageBrigadeRelation.FirstOrDefaultAsync(c => c.Id == workingPeriodStageBrigadeRelation.Id)
            ?? throw new Exception();
        WorkingPeriodStageMaterialStageEntity.Id = workingPeriodStageBrigadeRelation.Id;
        WorkingPeriodStageMaterialStageEntity.WorkingPeriodStageId = workingPeriodStageBrigadeRelation.WorkingPeriodStageId;
        WorkingPeriodStageMaterialStageEntity.BrigadeId = workingPeriodStageBrigadeRelation.BrigadeId;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriodStageBrigadeRelation
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}