
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class FactoryService
{
    private readonly IFactoryRepository _factoryRepository;
    public FactoryService(IFactoryRepository factoryRepository)
    {
        _factoryRepository = factoryRepository;
    }
    public async Task<List<FactoryEntity>> GetAll()
    {
        return await _factoryRepository.GetAll();
    }

    public async Task<FactoryEntity> GetById(Guid id)
    {
        return await _factoryRepository.GetById(id);
    }
    public async Task<FactoryEntity> GetByName(string name)
    {
        return await _factoryRepository.GetByName(name);
    }
    public async Task Add(FactoryEntity factory)
    {
        var factoryEntity = new FactoryEntity
        {
            Id = Guid.NewGuid(),
            Name = factory.Name,
        };
        await _factoryRepository.Add(factoryEntity);
    }
    public async Task Update(FactoryEntity factory)
    {
        await _factoryRepository.Update(factory);
    }
    public async Task Delete(Guid id)
    {
        await _factoryRepository.Delete(id);
    }

}
