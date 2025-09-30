using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class GroupMaterialRepository : IGroupMaterialRepository
{
    private readonly ProjectDbContext _dbcontext;

    public GroupMaterialRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<GroupMaterial>> GetAll()
    {
        var groupMaterialEntities = await _dbcontext.GroupMaterial.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        List<GroupMaterial> groupMaterials = new List<GroupMaterial>();
        foreach (var groupMaterialEntity in groupMaterialEntities)
        {
            groupMaterials.Add(GroupMaterial.Create(groupMaterialEntity.Id, groupMaterialEntity.Name));
        }
        return groupMaterials;
    }

    public async Task<GroupMaterial?> GetById(Guid id)
    {

        var groupMaterialEntity = await _dbcontext.GroupMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return GroupMaterial.Create(groupMaterialEntity.Id, groupMaterialEntity.Name);
    }
    public async Task<GroupMaterial?> GetByName(string name)
    {

        var groupMaterialEntity = await _dbcontext.GroupMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        return GroupMaterial.Create(groupMaterialEntity.Id, groupMaterialEntity.Name);
    }
    public async Task Add(GroupMaterial groupMaterial)
    {
        var groupMaterialEntity = new GroupMaterialEntity
        {
            Id = groupMaterial.Id,
            Name = groupMaterial.Name
        };
        await _dbcontext.AddAsync(groupMaterialEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(GroupMaterial groupMaterial)
    {
        var groupMaterialEntity = await _dbcontext.GroupMaterial.FirstOrDefaultAsync(c => c.Id == groupMaterial.Id)
            ?? throw new Exception();

        groupMaterialEntity.Name = groupMaterial.Name;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.GroupMaterial
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}