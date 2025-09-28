using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class GroupMaterialConfiguration : IEntityTypeConfiguration<GroupMaterialEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GroupMaterialEntity> builder)
    {
        builder.HasKey(gm => gm.Id);
        builder.HasMany(gm => gm.WorkingPeriodStageMaterials).WithOne(wpsm => wpsm.GroupMaterial);
        builder.HasMany(gm => gm.ProductSubTypeWorkingPeriodSamples).WithMany(pstwps => pstwps.GroupMaterials);
    }
}