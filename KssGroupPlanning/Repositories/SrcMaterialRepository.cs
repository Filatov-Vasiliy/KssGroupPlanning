using KssGroupPlanning.Entities;

using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Infrastuction.Db;

public class SrcMaterialRepository(ProjectDbContext context) : ISrcMaterialRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(List<SrcMaterialEntity> srcMaterials)
    {
        foreach (SrcMaterialEntity srcMaterial in srcMaterials)
        {

            var srcMaterialEntity = new SrcMaterialEntity
            {
                Id = srcMaterial.Id,
                ProductOrderName = srcMaterial.ProductOrderName,
                ProductOrderDate = srcMaterial.ProductOrderDate,
                MaterialName = srcMaterial.MaterialName,
                MaterialGroup = srcMaterial.MaterialGroup,
                Qty = srcMaterial.Qty,
                CurrentQty = srcMaterial.CurrentQty,
                PostedDate = srcMaterial.PostedDate,
                ForAdmissionDate = srcMaterial.ForAdmissionDate,
                CreateDate = srcMaterial.CreateDate,
                ProductOrderNameChild = srcMaterial.ProductOrderNameChild,
            };
            await _dbcontext.SrcMaterial.AddAsync(srcMaterialEntity);
        }
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<SrcMaterialEntity>> GetSrcMaterials()
    {
        return await _dbcontext.SrcMaterial.AsNoTracking().ToListAsync();
    }
    public async Task RemoveDuplicates()
    {
        /*
        var duplicates = _dbcontext.SrcMaterial.AsNoTracking().GroupBy(m => m.MaterialName, m => m.ProductOrderName).Where(g => g.Count() > 1).SelectMany(g => g.Skip(1)).ToList();
        
        if (duplicates.Any())
        {
            _dbcontext.SrcMaterial.RemoveRange(duplicates);
            _dbcontext.SaveChanges();
        }
        */
    }
}