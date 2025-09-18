using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Configurations
{
    public class WorkingPeriodRelationConfiguration : IEntityTypeConfiguration<WorkingPeriodRelationEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkingPeriodRelationEntity> builder)
        {
            builder.HasKey(wpr => wpr.Id);

        }
    }
}
