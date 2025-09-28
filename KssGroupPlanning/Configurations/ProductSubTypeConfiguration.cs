
using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeConfiguration : IEntityTypeConfiguration<ProductSubTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeEntity> builder)
    {
        builder.HasKey(pst => pst.Id);
        builder.HasOne(pst => pst.ProductType).WithMany(pt => pt.ProductSubTypes).HasForeignKey(pst => pst.ProductTypeId);
        builder.HasMany(pst => pst.Products).WithOne(p => p.ProductSubType);
        builder.HasMany(pst => pst.ProductSubTypeWorkingPeriodSamples).WithOne(pstwps => pstwps.ProductSubType);
        builder.HasMany(pst => pst.ProductSubTypeStageSamples).WithOne(pstss => pstss.ProductSubType);

    }
}
