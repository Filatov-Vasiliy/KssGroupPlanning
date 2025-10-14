using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewFactoryService
{
    private readonly INewFactoryRepository _factoryRepository;
    public NewFactoryService(INewFactoryRepository factoryRepository)
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
