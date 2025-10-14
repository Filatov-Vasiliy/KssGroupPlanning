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

public class MaterialStageRepository : IMaterialStageRepository
{
    private readonly ProjectDbContext _dbcontext;

    public MaterialStageRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<MaterialStage>> GetAll()
    {
        var materialStageEntities = await _dbcontext.MaterialStage.AsNoTracking().OrderBy(c => c.StageName).ToListAsync();
        List<MaterialStage> materialStages = new List<MaterialStage>();
        foreach (var materialStageEntity in materialStageEntities)
        {
            materialStages.Add(MaterialStage.Create(materialStageEntity.Id, materialStageEntity.StageName, materialStageEntity.GroupMaterialId));
        }
        return materialStages;
    }

    public async Task<List<MaterialStage?>> GetByGroupMaterialId(Guid groupMaterialId)
    {

        var materialStageEntities = await _dbcontext.MaterialStage.AsNoTracking().OrderBy(c => c.StageName).Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
        List<MaterialStage> materialStages = new List<MaterialStage>();
        foreach (var materialStageEntity in materialStageEntities)
        {
            materialStages.Add(MaterialStage.Create(materialStageEntity.Id, materialStageEntity.StageName, materialStageEntity.GroupMaterialId));
        }
        return materialStages;
    }
    public async Task<MaterialStage?> GetById(Guid id)
    {
        var materialStageEntity = await _dbcontext.MaterialStage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return MaterialStage.Create(materialStageEntity.Id, materialStageEntity.StageName, materialStageEntity.GroupMaterialId);
    }
    public async Task Add(MaterialStage materialStage)
    {
        var materialStageEntity = new MaterialStageEntity
        {
            Id = materialStage.Id,
            StageName = materialStage.StageName,
            GroupMaterialId = materialStage.GroupMaterialId,
        };
        await _dbcontext.AddAsync(materialStageEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(MaterialStage materialStage)
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