using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageConfiguration : IEntityTypeConfiguration<WorkingPeriodStageEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageEntity> builder)
    {
        builder.HasKey(wps => wps.Id);
        builder.HasMany(wps => wps.Brigades).WithMany(b => b.WorkingPeriodStages).UsingEntity<WorkingPeriodStageBrigadeRelationEntity>();
        builder.HasOne(wps => wps.WorkingPeriod).WithMany(wp => wp.WorkingPeriodStages).HasForeignKey(wps => wps.WorkingPeriodId);
        builder.HasMany(wps => wps.WorkingPeriodStageMaterials).WithOne(wpsm => wpsm.WorkingPeriodStage);
        builder.HasOne(wps => wps.ProductSubTypeWorkingPeriodSample).WithMany(pstwps => pstwps.WorkingPeriodStages).HasForeignKey(wps => wps.ProductSubTypeWorkingPeriodSampleId);
    }
}
