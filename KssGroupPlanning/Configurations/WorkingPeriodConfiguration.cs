using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class WorkingPeriodConfiguration : IEntityTypeConfiguration<WorkingPeriodEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodEntity> builder)
    {
        builder.HasKey(wp => wp.Id);
        builder.HasOne(wp => wp.Product).WithMany(p => p.WorkingPeriods).HasForeignKey(wp => wp.ProductId);
        builder.HasMany(wp => wp.WorkingPeriodStages).WithOne(wps => wps.WorkingPeriod);
    }
}
