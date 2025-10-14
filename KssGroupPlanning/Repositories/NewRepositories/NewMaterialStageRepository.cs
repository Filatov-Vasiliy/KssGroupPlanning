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

public class NewMaterialStageRepository : INewMaterialStageRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewMaterialStageRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<MaterialStageEntity>> GetAll()
    {
        return await _dbcontext.MaterialStage.AsNoTracking().OrderBy(c => c.StageName).ToListAsync();
    }

    public async Task<List<MaterialStageEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {

        return await _dbcontext.MaterialStage.AsNoTracking().OrderBy(c => c.StageName).Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
    }
    public async Task<MaterialStageEntity?> GetById(Guid id)
    {
        return await _dbcontext.MaterialStage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task Add(MaterialStageEntity materialStage)
    {
        await _dbcontext.AddAsync(materialStage);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(MaterialStageEntity materialStage)
    {
        var materialStageEntity = await _dbcontext.MaterialStage.FirstOrDefaultAsync(c => c.Id == materialStage.Id)
            ?? throw new Exception();
        materialStageEntity.Id = materialStage.Id;
        materialStageEntity.StageName = materialStage.StageName;
        materialStageEntity.GroupMaterialId = materialStage.GroupMaterialId;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.MaterialStage
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}