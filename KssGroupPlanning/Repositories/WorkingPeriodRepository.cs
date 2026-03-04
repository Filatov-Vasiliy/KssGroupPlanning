using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;


using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class WorkingPeriodRepository : IWorkingPeriodRepository
{
    private readonly ProjectDbContext _dbcontext;

    public WorkingPeriodRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<WorkingPeriodEntity>> GetAll()
    {
        return await _dbcontext.WorkingPeriod.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<WorkingPeriodEntity?> GetById(Guid id)
    {

        return await _dbcontext.WorkingPeriod.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<WorkingPeriodEntity?> GetByName(string name)
    {

        return await _dbcontext.WorkingPeriod.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<List<WorkingPeriodEntity?>> GetByProductId(Guid productId)
    {
        return await _dbcontext.WorkingPeriod.AsNoTracking().Where(c => c.ProductId == productId).ToListAsync();
    }
    public async Task Add(WorkingPeriodEntity workingPeriod)
    {
        await _dbcontext.AddAsync(workingPeriod);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriodEntity workingPeriod)
    {
        var workingPeriodEntity = await _dbcontext.WorkingPeriod.FirstOrDefaultAsync(c => c.Id == workingPeriod.Id)
            ?? throw new Exception();
        workingPeriodEntity.Id = workingPeriod.Id;
        workingPeriodEntity.Name = workingPeriod.Name;
        workingPeriodEntity.Status = workingPeriod.Status;
        workingPeriodEntity.ProductId = workingPeriod.ProductId;
        workingPeriodEntity.DateFrom = workingPeriod.DateFrom;
        workingPeriodEntity.DateTo = workingPeriod.DateTo;
        workingPeriodEntity.CreateTime = workingPeriod.CreateTime;
        workingPeriodEntity.UpdateTime = workingPeriod.UpdateTime;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.WorkingPeriod
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}