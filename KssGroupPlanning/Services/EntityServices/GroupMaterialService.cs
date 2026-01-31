
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class GroupMaterialService
{
    private readonly IGroupMaterialRepository _groupMaterialRepository;
    public GroupMaterialService(IGroupMaterialRepository groupMaterialRepository)
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
