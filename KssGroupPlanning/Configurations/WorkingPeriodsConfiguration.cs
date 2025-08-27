using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodsConfiguration : IEntityTypeConfiguration<WorkingPeriodEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodEntity> builder)
    {
        builder.HasKey(wp => wp.Id);

    }
}
