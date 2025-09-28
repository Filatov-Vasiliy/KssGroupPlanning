using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductTypeEntity> builder)
    {
        builder.HasKey(pt => pt.Id);
        builder.HasMany(pt => pt.ProductSubTypes).WithOne(pst => pst.ProductType);
    }
}
