using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;

using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class FactoryRepository : IFactoryRepository
{
    private readonly ProjectDbContext _dbcontext;

    public FactoryRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<FactoryEntity>> GetAll()
    {
        return await _dbcontext.Factory.AsNoTracking().OrderBy(f => f.Name).ToListAsync();
    }

    public async Task<FactoryEntity?> GetById(Guid id)
    {
        return await _dbcontext.Factory.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }
    public async Task<FactoryEntity?> GetByName(string name)
    {
        return await _dbcontext.Factory.AsNoTracking().FirstOrDefaultAsync(f => f.Name == name);
    }
    public async Task Add(FactoryEntity factory)
    {
        await _dbcontext.AddAsync(factory);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(FactoryEntity factory)
    {
        var factoryEntity = await _dbcontext.Factory.FirstOrDefaultAsync(f => f.Id == factory.Id)
            ?? throw new Exception();

        factoryEntity.Name = factory.Name;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.Factory
            .Where(f => f.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}