using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class NewStageTypeRepository : INewStageTypeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewStageTypeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<StageTypeEntity>> GetAll()
    {
        return await _dbcontext.StageType.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<StageTypeEntity?> GetById(Guid id)
    {

        return await _dbcontext.StageType.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<StageTypeEntity?> GetByName(string name)
    {

        return await _dbcontext.StageType.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task Add(StageTypeEntity stageType)
    {
        await _dbcontext.AddAsync(stageType);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(StageTypeEntity stageType)
    {
        var stageTypeEntity = await _dbcontext.StageType.FirstOrDefaultAsync(c => c.Id == stageType.Id)
            ?? throw new Exception();
        stageTypeEntity.Id = stageType.Id;
        stageTypeEntity.Name = stageType.Name;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.StageType
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}