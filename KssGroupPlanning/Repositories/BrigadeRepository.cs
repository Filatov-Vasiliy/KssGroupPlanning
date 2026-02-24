using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class BrigadeRepository : IBrigadeRepository
{
    private readonly ProjectDbContext _dbcontext;
    private readonly ILogger<BrigadeRepository> _logger;

    public BrigadeRepository(ProjectDbContext context, ILogger<BrigadeRepository> logger)
    {
        _dbcontext = context;
        _logger = logger;
    }
    public async Task<List<BrigadeEntity>> GetAll()
    {
        return await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).ToListAsync();
    }

    public async Task<List<BrigadeEntity?>> GetByFactoryId(Guid factoryId)
    {

        return await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).Where(c => c.FactoryId == factoryId).ToListAsync();

    }
    public async Task<List<BrigadeEntity?>> GetByName(string name)
    {

        return await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).Where(c => c.Name == name).ToListAsync();

    }
    public async Task<List<BrigadeEntity?>> GetByStageTypeId(Guid stageTypeId)
    {

        return await _dbcontext.Brigade.AsNoTracking().OrderBy(c => c.CountEmployee).Where(c => c.StageTypeId == stageTypeId).ToListAsync();
    }
    public async Task<BrigadeEntity?> GetById(Guid id)
    {
        return await _dbcontext.Brigade.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task Add(BrigadeEntity brigade)
    {
        await _dbcontext.AddAsync(brigade);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(BrigadeEntity brigade)
    {
        var brigadeEntity = await _dbcontext.Brigade.FirstOrDefaultAsync(c => c.Id == brigade.Id)
            ?? throw new Exception();
        brigadeEntity.Id = brigade.Id;
        brigadeEntity.Name = brigade.Name;
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