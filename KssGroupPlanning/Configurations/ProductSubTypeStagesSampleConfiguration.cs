using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeStagesSampleConfiguration : IEntityTypeConfiguration<ProductSubTypeStagesSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeStagesSampleEntity> builder)
    {
        builder.HasKey(pstss => pstss.Id);

    }
}
