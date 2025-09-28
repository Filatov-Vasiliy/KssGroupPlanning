using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class StageTypeRepository : IStageTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public StageTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<StageType>> GetAll()
    {
        var stageTypeEntities = await _dbcontext.StageTypes.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        List<StageType> stageTypes = new List<StageType>();
        foreach (var stageTypeEntity in stageTypeEntities)
        {
            stageTypes.Add(StageType.Create(stageTypeEntity.Id, stageTypeEntity.Name));
        }
        return stageTypes;
    }

    public async Task<StageType?> GetById(Guid id)
    {

        var stageTypeEntity = await _dbcontext.StageTypes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return StageType.Create(stageTypeEntity.Id, stageTypeEntity.Name);
    }
    public async Task<StageType?> GetByName(string name)
    {

        var stageTypeEntity = await _dbcontext.StageTypes.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        return StageType.Create(stageTypeEntity.Id, stageTypeEntity.Name);
    }
    public async Task Add(StageType stageType)
    {
        var stageTypeEntity = new StageTypeEntity
        {
            Id = stageType.Id,
            Name = stageType.Name
        };
        await _dbcontext.AddAsync(stageTypeEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(StageType stageType)
    {
        var stageTypeEntity = await _dbcontext.StageTypes.FirstOrDefaultAsync(c => c.Id == stageType.Id)
            ?? throw new Exception();
        stageTypeEntity.Id = stageType.Id;
        stageTypeEntity.Name = stageType.Name;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.StageTypes
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}