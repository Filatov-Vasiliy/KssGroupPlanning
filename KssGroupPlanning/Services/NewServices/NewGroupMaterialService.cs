using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewGroupMaterialService
{
    private readonly INewGroupMaterialRepository _groupMaterialRepository;
    public NewGroupMaterialService(INewGroupMaterialRepository groupMaterialRepository)
    {
        _groupMaterialRepository = groupMaterialRepository;
    }
    public async Task<List<GroupMaterialEntity>> GetAll()
    {
        return await _groupMaterialRepository.GetAll();
    }

    public async Task<GroupMaterialEntity> GetById(Guid id)
    {
        return await _groupMaterialRepository.GetById(id);
    }
    public async Task<GroupMaterialEntity> GetByName(string name)
    {
        return await _groupMaterialRepository.GetByName(name);
    }

    public async Task Add(GroupMaterialEntity groupMaterial)
    {
        var groupMaterialEntity = new GroupMaterialEntity
        {
            Id = Guid.NewGuid(),
            Name = groupMaterial.Name,
        };
        await _groupMaterialRepository.Add(groupMaterialEntity);
    }
    public async Task Update(GroupMaterialEntity groupMaterial)
    {
        await _groupMaterialRepository.Update(groupMaterial);
    }
    public async Task Delete(Guid id)
    {
        await _groupMaterialRepository.Delete(id);
    }

}
