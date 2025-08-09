using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeWorkingPeriodsSampleConfiguration : IEntityTypeConfiguration<ProductSubTypeWorkingPeriodsSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeWorkingPeriodsSampleEntity> builder)
    {
        builder.HasKey(pstwps => pstwps.Id);

    }
}
