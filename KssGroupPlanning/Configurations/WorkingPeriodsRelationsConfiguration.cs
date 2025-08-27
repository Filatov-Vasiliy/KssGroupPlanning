using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations
{
    public class WorkingPeriodsRelationsConfiguration : IEntityTypeConfiguration<WorkingPeriodsRelationsEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodsRelationsEntity> builder)
        {
            builder.HasKey(wpr => wpr.Id);

        }
    }
}
