using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class MaterialStageConfiguration : IEntityTypeConfiguration<MaterialStageEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MaterialStageEntity> builder)
    {
        builder.HasKey(ms => ms.Id);
        builder.HasOne(ms => ms.GroupMaterial).WithOne(gm => gm.MaterialStage).HasForeignKey<MaterialStageEntity>(ms => ms.GroupMaterialId);
        builder.HasMany(ms => ms.ProductSubTypeStageSamples).WithOne(pstss => pstss.MaterialStage);
    }
}