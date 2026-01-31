using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;

using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class GroupMaterialRepository : IGroupMaterialRepository
{
    private readonly ProjectDbContext _dbcontext;

    public GroupMaterialRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<GroupMaterialEntity>> GetAll()
    {
        return await _dbcontext.GroupMaterial.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<GroupMaterialEntity?> GetById(Guid id)
    {

        return await _dbcontext.GroupMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<GroupMaterialEntity?> GetByName(string name)
    {

        return await _dbcontext.GroupMaterial.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task Add(GroupMaterialEntity groupMaterial)
    {

        await _dbcontext.AddAsync(groupMaterial);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(GroupMaterialEntity groupMaterial)
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