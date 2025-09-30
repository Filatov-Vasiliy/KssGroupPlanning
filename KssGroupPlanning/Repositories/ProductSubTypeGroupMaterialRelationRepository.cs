using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class ProductSubTypeGroupMaterialRelationRepository : IProductSubTypeGroupMaterialRelationRepository
{
    private readonly ProjectDbContext _dbcontext;

    public ProductSubTypeGroupMaterialRelationRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task<List<ProductSubTypeGroupMaterialRelation>> GetAll()
    {
        var productSubTypeGroupMaterialRelationEntities = await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        List<ProductSubTypeGroupMaterialRelation> productSubTypeGroupMaterialRelations = new List<ProductSubTypeGroupMaterialRelation>();
        foreach (var productSubTypeGroupMaterialRelationEntity in productSubTypeGroupMaterialRelationEntities)
        {
            productSubTypeGroupMaterialRelations.Add(ProductSubTypeGroupMaterialRelation.Create(productSubTypeGroupMaterialRelationEntity.Id, productSubTypeGroupMaterialRelationEntity.GroupMaterialId, productSubTypeGroupMaterialRelationEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return productSubTypeGroupMaterialRelations;
    }

    public async Task<ProductSubTypeGroupMaterialRelation?> GetById(Guid id)
    {

        var productSubTypeGroupMaterialRelationEntity = await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return ProductSubTypeGroupMaterialRelation.Create(productSubTypeGroupMaterialRelationEntity.Id, productSubTypeGroupMaterialRelationEntity.GroupMaterialId, productSubTypeGroupMaterialRelationEntity.ProductSubTypeWorkingPeriodSampleId);

    }
    public async Task<List<ProductSubTypeGroupMaterialRelation?>> GetByGroupMaterialId(Guid groupMaterialId)
    {
        var productSubTypeGroupMaterialRelationEntities = await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().Where(c => c.GroupMaterialId == groupMaterialId).ToListAsync();
        List<ProductSubTypeGroupMaterialRelation> productSubTypeGroupMaterialRelations = new List<ProductSubTypeGroupMaterialRelation>();
        foreach (var productSubTypeGroupMaterialRelationEntity in productSubTypeGroupMaterialRelationEntities)
        {
            productSubTypeGroupMaterialRelations.Add(ProductSubTypeGroupMaterialRelation.Create(productSubTypeGroupMaterialRelationEntity.Id, productSubTypeGroupMaterialRelationEntity.GroupMaterialId, productSubTypeGroupMaterialRelationEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return productSubTypeGroupMaterialRelations;
    }
    public async Task<List<ProductSubTypeGroupMaterialRelation?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        var productSubTypeGroupMaterialRelationEntities = await _dbcontext.ProductSubTypeGroupMaterialRelation.AsNoTracking().Where(c => c.ProductSubTypeWorkingPeriodSampleId == productSubTypeWorkingPeriodSampleId).ToListAsync();
        List<ProductSubTypeGroupMaterialRelation> productSubTypeGroupMaterialRelations = new List<ProductSubTypeGroupMaterialRelation>();
        foreach (var productSubTypeGroupMaterialRelationEntity in productSubTypeGroupMaterialRelationEntities)
        {
            productSubTypeGroupMaterialRelations.Add(ProductSubTypeGroupMaterialRelation.Create(productSubTypeGroupMaterialRelationEntity.Id, productSubTypeGroupMaterialRelationEntity.GroupMaterialId, productSubTypeGroupMaterialRelationEntity.ProductSubTypeWorkingPeriodSampleId));
        }
        return productSubTypeGroupMaterialRelations;
    }
    public async Task Add(ProductSubTypeGroupMaterialRelation productSubTypeGroupMaterialRelation)
    {
        var productSubTypeGroupMaterialRelationEntity = new ProductSubTypeGroupMaterialRelationEntity
        {
            Id = productSubTypeGroupMaterialRelation.Id,
            ProductSubTypeWorkingPeriodSampleId = productSubTypeGroupMaterialRelation.ProductSubTypeWorkingPeriodSampleId,
            GroupMaterialId = productSubTypeGroupMaterialRelation.GroupMaterialId,
        };
        await _dbcontext.AddAsync(productSubTypeGroupMaterialRelationEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(ProductSubTypeGroupMaterialRelation productSubTypeGroupMaterialRelation)
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