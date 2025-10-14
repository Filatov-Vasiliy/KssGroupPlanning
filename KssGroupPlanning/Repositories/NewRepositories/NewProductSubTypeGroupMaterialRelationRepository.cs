using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class NewProductSubTypeGroupMaterialRelationRepository : INewProductSubTypeGroupMaterialRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public NewProductSubTypeGroupMaterialRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity>> GetAll()
    {
        return await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<ProductSubTypeGroupMaterialRelationEntity?> GetById(Guid id)
    {

        return await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        return await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
    }
    public async Task<List<ProductSubTypeGroupMaterialRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        return await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodSampleId).ToListAsync();
    }
    public async Task Add(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation)
    {
        await _dbcontext.AddAsync(productSubTypeGroupMaterialRelation);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeGroupMaterialRelationEntity productSubTypeGroupMaterialRelation)
    {
        var productSubTypeGroupMaterialRelationEntity = await _dbcontext.ProductSubTypeGroupMaterialRelation.FirstOrDefaultAsync(c => c.Id == productSubTypeGroupMaterialRelation.Id)
            ?? throw new Exception();
        productSubTypeGroupMaterialRelationEntity.Id = productSubTypeGroupMaterialRelation.Id;
        productSubTypeGroupMaterialRelationEntity.ProductSubTypeWorkingPeriodSampleId = productSubTypeGroupMaterialRelation.ProductSubTypeWorkingPeriodSampleId;
        productSubTypeGroupMaterialRelationEntity.GroupMaterialId = productSubTypeGroupMaterialRelation.GroupMaterialId;

        await _dbcontext.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        await _dbcontext.ProductSubTypeGroupMaterialRelation
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbcontext.SaveChangesAsync();
    }
}