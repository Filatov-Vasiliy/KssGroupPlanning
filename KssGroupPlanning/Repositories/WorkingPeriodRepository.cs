using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
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
    public async Task<List<WorkingPeriod>> GetAll()
    {
        var workingPeriodEntities = await _dbcontext.WorkingPeriods.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        List<WorkingPeriod> workingPeriods = new List<WorkingPeriod>();
        foreach (var workingPeriodEntity in workingPeriodEntities)
        {
            workingPeriods.Add(WorkingPeriod.Create(workingPeriodEntity.Id, workingPeriodEntity.Name,workingPeriodEntity.Status,workingPeriodEntity.ProductId, workingPeriodEntity.DateFrom, workingPeriodEntity.DateTo,workingPeriodEntity.CreateTime,workingPeriodEntity.UpdateTime));
        }
        return workingPeriods;
    }

    public async Task<WorkingPeriod?> GetById(Guid id)
    {

        var workingPeriodEntity = await _dbcontext.WorkingPeriods.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return WorkingPeriod.Create(workingPeriodEntity.Id, workingPeriodEntity.Name, workingPeriodEntity.Status, workingPeriodEntity.ProductId, workingPeriodEntity.DateFrom, workingPeriodEntity.DateTo, workingPeriodEntity.CreateTime, workingPeriodEntity.UpdateTime);
    }
    public async Task<WorkingPeriod?> GetByName(string name)
    {

        var workingPeriodEntity = await _dbcontext.WorkingPeriods.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        return WorkingPeriod.Create(workingPeriodEntity.Id, workingPeriodEntity.Name, workingPeriodEntity.Status, workingPeriodEntity.ProductId, workingPeriodEntity.DateFrom, workingPeriodEntity.DateTo, workingPeriodEntity.CreateTime, workingPeriodEntity.UpdateTime);
    }
    public async Task<WorkingPeriod?> GetByProductId(Guid productId)
    {
        var workingPeriodEntity = await _dbcontext.WorkingPeriods.AsNoTracking().FirstOrDefaultAsync(c => c.ProductId == productId);
        return WorkingPeriod.Create(workingPeriodEntity.Id, workingPeriodEntity.Name, workingPeriodEntity.Status, workingPeriodEntity.ProductId, workingPeriodEntity.DateFrom, workingPeriodEntity.DateTo, workingPeriodEntity.CreateTime, workingPeriodEntity.UpdateTime);
    }
    public async Task Add(WorkingPeriod workingPeriod)
    {
        var workingPeriodEntity = new WorkingPeriodEntity
        {
            Id = workingPeriod.Id,
            Name = workingPeriod.Name,
            Status = workingPeriod.Status,
            ProductId = workingPeriod.ProductId,
            DateFrom = workingPeriod.DateFrom,
            DateTo = workingPeriod.DateTo,
            CreateTime = workingPeriod.CreateTime,
            UpdateTime = workingPeriod.UpdateTime
        };
        await _dbcontext.AddAsync(workingPeriodEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(WorkingPeriod workingPeriod)
    {
        var workingPeriodEntity = await _dbcontext.WorkingPeriods.FirstOrDefaultAsync(c => c.Id == workingPeriod.Id)
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
        await _dbcontext.WorkingPeriods
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}