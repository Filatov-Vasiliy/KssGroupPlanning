using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);

    }
}