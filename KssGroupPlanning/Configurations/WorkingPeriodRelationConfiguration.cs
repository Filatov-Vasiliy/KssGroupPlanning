using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations
{
    public class WorkingPeriodRelationConfiguration : IEntityTypeConfiguration<WorkingPeriodRelationEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodRelationEntity> builder)
        {
            builder.HasKey(wpr => wpr.Id);
            builder.HasOne(wpr => wpr.ChildProductSubTypeWorkingPeriodSample).WithMany(ppstwps => ppstwps.ParentWorkingPeriodRelations).HasForeignKey(wpr => wpr.ChildProductSubTypeWorkingPeriodSampleId);
            builder.HasOne(wpr => wpr.ParentProductSubTypeWorkingPeriodSample).WithMany(ppstwps => ppstwps.ChildWorkingPeriodRelations).HasForeignKey(wpr => wpr.ParentProductSubTypeWorkingPeriodSampleId);

        }
    }
}
