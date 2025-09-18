using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeWorkingPeriodSampleConfiguration : IEntityTypeConfiguration<ProductSubTypeWorkingPeriodSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeWorkingPeriodSampleEntity> builder)
    {
        builder.HasKey(pstwps => pstwps.Id);

    }
}
