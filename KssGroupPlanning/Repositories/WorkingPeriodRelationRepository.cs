using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;


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
    public async Task<List<WorkingPeriodRelationEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriodRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<WorkingPeriodRelationEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriodRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<WorkingPeriodRelationEntity?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId)
    {
        return await _dbcontext.WorkingPeriodRelation.AsNoTracking().Where(c => c.ParentProductSubTypeWorkingPeriodSampleId == parentProductSubTypeWorkingPeriodSampleId).ToListAsync();
    }
    public async Task<List<WorkingPeriodRelationEntity?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId)
    {
        return await _dbcontext.WorkingPeriodRelation.AsNoTracking().Where(c => c.ChildProductSubTypeWorkingPeriodSampleId == childProductSubTypeWorkingPeriodSampleId).ToListAsync();
    }
    public async Task Add(WorkingPeriodRelationEntity workingPeriodRelation)
    {
        await _dbcontext.AddAsync(workingPeriodRelation);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodRelationEntity workingPeriodRelation)
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