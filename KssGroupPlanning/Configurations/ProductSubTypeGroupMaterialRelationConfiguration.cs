using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;
namespace KssGroupPlanning.Configurations;

public class ProductSubTypeGroupMaterialRelationConfiguration : IEntityTypeConfiguration<ProductSubTypeGroupMaterialRelationEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeGroupMaterialRelationEntity> builder)
    {
        builder.HasKey(psgm => psgm.Id);
        builder.HasOne(psgm => psgm.ProductSubTypeWorkingPeriodSample).WithMany(pstwps => pstwps.ProductSubTypeGroupMaterialRelations).HasForeignKey(wpst => wpst.ProductSubTypeWorkingPeriodSampleId);
        builder.HasOne(wpst => wpst.GroupMaterial).WithMany(st => st.ProductSubTypeGroupMaterialRelations).HasForeignKey(wpst => wpst.GroupMaterialId);

    }
}