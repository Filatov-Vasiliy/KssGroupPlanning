using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class NewWorkingPeriodStageBrigadeRelationRepository : INewWorkingPeriodStageBrigadeRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewWorkingPeriodStageBrigadeRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<WorkingPeriodStageBrigadeRelationEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        return await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().Where(c => c.WorkingPeriodStageId == workingPeriodStageId).ToListAsync();
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByBrigadeId(Guid brigadeId)
    {
        return await _dbcontext.WorkingPeriodStageBrigadeRelation.AsNoTracking().Where(c => c.BrigadeId == brigadeId).ToListAsync();
    }
    public async Task Add(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation)
    {
        await _dbcontext.AddAsync(workingPeriodStageBrigadeRelation);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation)
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