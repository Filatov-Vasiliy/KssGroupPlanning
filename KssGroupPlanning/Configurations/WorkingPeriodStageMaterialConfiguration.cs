using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageMaterialConfiguration : IEntityTypeConfiguration<WorkingPeriodStageMaterialEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageMaterialEntity> builder)
    {
        builder.HasKey(wpsm => wpsm.Id);

    }
}