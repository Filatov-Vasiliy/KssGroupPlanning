using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations
{
    public class WorkingPeriodsStageTypesConfiguration : IEntityTypeConfiguration<WorkingPeriodsStageTypesEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodsStageTypesEntity> builder)
        {
            builder.HasKey(wpst => wpst.Id);

        }
    }
}
