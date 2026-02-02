using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageMaterialConfiguration : IEntityTypeConfiguration<WorkingPeriodStageMaterialEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageMaterialEntity> builder)
    {
        builder.HasKey(wpsm => wpsm.Id);
        builder.HasOne(wpsm => wpsm.Product).WithMany(wps => wps.WorkingPeriodStageMaterials).HasForeignKey(wpsm => wpsm.ProductId);
        builder.HasOne(wpsm => wpsm.GroupMaterial).WithMany(gm => gm.WorkingPeriodStageMaterials).HasForeignKey(wpsm => wpsm.GroupMaterialId);
    }
}