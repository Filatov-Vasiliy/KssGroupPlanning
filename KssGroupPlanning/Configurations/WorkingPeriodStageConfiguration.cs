using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageConfiguration : IEntityTypeConfiguration<WorkingPeriodStageEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageEntity> builder)
    {
        builder.HasKey(wps => wps.Id);

    }
}
