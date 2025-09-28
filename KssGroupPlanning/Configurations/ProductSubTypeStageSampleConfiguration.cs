using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeStageSampleConfiguration : IEntityTypeConfiguration<ProductSubTypeStageSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeStageSampleEntity> builder)
    {
        builder.HasKey(pstss => pstss.Id);
        builder.HasOne(pstss => pstss.ProductSubType).WithMany(pst => pst.ProductSubTypeStageSamples).HasForeignKey(pstss => pstss.ProductSubTypeId);
    }
}
