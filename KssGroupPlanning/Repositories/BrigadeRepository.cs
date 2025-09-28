using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class BrigadeRepository : IBrigadeRepository
{
    private readonly ProjectDbContext _dbcontext;

    public BrigadeRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<Brigade>> GetAll()
    {
        var brigadeEntities = await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).ToListAsync();
        List<Brigade> brigades = new List<Brigade>();
        foreach (var brigadeEntity in brigadeEntities)
        {
            brigades.Add(Brigade.Create(brigadeEntity.Id, brigadeEntity.StageTypeId,brigadeEntity.FactoryId,brigadeEntity.CountEmployee));
        }
        return brigades;
    }

    public async Task<List<Brigade?>> GetByFactoryId(Guid factoryId)
    {

        var brigadeEntities = await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).Where(c => c.StageTypeId == factoryId).ToListAsync();
        List<Brigade> brigades = new List<Brigade>();
        foreach (var brigadeEntity in brigadeEntities)
        {
            brigades.Add(Brigade.Create(brigadeEntity.Id, brigadeEntity.StageTypeId, brigadeEntity.FactoryId, brigadeEntity.CountEmployee));
        }
        return brigades;
    }
    public async Task<List<Brigade?>> GetByStageTypeId(Guid stageTypeId)
    {

        var brigadeEntities = await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).Where(c => c.StageTypeId == stageTypeId).ToListAsync();
        List<Brigade> brigades = new List<Brigade>();
        foreach (var brigadeEntity in brigadeEntities)
        {
            brigades.Add(Brigade.Create(brigadeEntity.Id, brigadeEntity.StageTypeId, brigadeEntity.FactoryId, brigadeEntity.CountEmployee));
        }
        return brigades;
    }
    public async Task<Brigade?> GetById(Guid id)
    {
        var brigadeEntity = await _dbcontext.Brigade.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return Brigade.Create(brigadeEntity.Id, brigadeEntity.StageTypeId, brigadeEntity.FactoryId, brigadeEntity.CountEmployee);
    }
    public async Task Add(Brigade brigade)
    {
        var brigadeEntity = new BrigadeEntity
        {
            Id = brigade.Id,
            StageTypeId = brigade.StageTypeId,
            FactoryId = brigade.FactoryId,
            CountEmployee = brigade.CountEmployee,
        };
        await _dbcontext.AddAsync(brigadeEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Brigade brigade)
    {
        var brigadeEntity = await _dbcontext.Brigade.FirstOrDefaultAsync(c => c.Id == brigade.Id)
            ?? throw new Exception();
        brigadeEntity.Id = brigade.Id;
        brigadeEntity.FactoryId = brigade.FactoryId;
        brigadeEntity.StageTypeId = brigade.StageTypeId;
        brigadeEntity.CountEmployee = brigade.CountEmployee;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.Brigade
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}