using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class BrigadeConfiguration : IEntityTypeConfiguration<BrigadeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BrigadeEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.WorkingPeriodStages).WithMany(wps => wps.Brigades);
        builder.HasOne(b => b.Factory).WithMany(f => f.Brigades).HasForeignKey(b => b.FactoryId);
        builder.HasOne(b => b.StageType).WithMany(st => st.Brigades).HasForeignKey(b => b.StageTypeId);
    }
}