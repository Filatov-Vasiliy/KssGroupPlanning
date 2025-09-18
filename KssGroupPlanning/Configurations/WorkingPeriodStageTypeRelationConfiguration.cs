using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations
{
    public class WorkingPeriodStageTypeRelationConfiguration : IEntityTypeConfiguration<WorkingPeriodStageTypeRelationEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodStageTypeRelationEntity> builder)
        {
            builder.HasKey(wpst => wpst.Id);

        }
    }
}
