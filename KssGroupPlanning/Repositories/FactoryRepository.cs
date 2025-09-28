using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class FactoryRepository : IFactoryRepository
{
    private readonly ProjectDbContext _dbcontext;

    public FactoryRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<Factory>> GetAll()
    {
        var factoryEntities = await _dbcontext.Factory.AsNoTracking().OrderBy(f => f.Name).ToListAsync();
        List<Factory> factories = new List<Factory>();
        foreach (var factoryEntity in factoryEntities)
        {
            factories.Add(Factory.Create(factoryEntity.Id, factoryEntity.Name));
        }
        return factories;
    }

    public async Task<Factory?> GetById(Guid id)
    {
        var factoryEntity = await _dbcontext.Factory.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        return Factory.Create(factoryEntity.Id, factoryEntity.Name);
    }
    public async Task<Factory?> GetByName(string name)
    {
        var factoryEntity = await _dbcontext.Factory.AsNoTracking().FirstOrDefaultAsync(f => f.Name == name);
        return Factory.Create(factoryEntity.Id, factoryEntity.Name);
    }
    public async Task Add(Factory factory)
    {
        var factoryEntity = new FactoryEntity
        {
            Id = factory.Id,
            Name = factory.Name
        };
        await _dbcontext.AddAsync(factoryEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(Factory factory)
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