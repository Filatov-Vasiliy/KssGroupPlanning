using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class StageTypeConfiguration : IEntityTypeConfiguration<StageTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StageTypeEntity> builder)
    {
        builder.HasKey(st => st.Id);
        builder.HasMany(st => st.Brigades).WithOne(b => b.StageType);
        builder.HasMany(st => st.ProductSubTypeWorkingPeriodSamples).WithMany(pstwps => pstwps.StageTypes).UsingEntity<WorkingPeriodStageTypeRelationEntity>();
    }
}
