using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodStageBrigadeRelationConfiguration : IEntityTypeConfiguration<WorkingPeriodStageBrigadeRelationEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageBrigadeRelationEntity> builder)
    {
        builder.HasKey(wpsbr => wpsbr.Id);

    }
}
