
using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class ProductSubTypeConfiguration : IEntityTypeConfiguration<ProductSubTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductSubTypeEntity> builder)
    {
        builder.HasKey(pst => pst.Id);

    }
}
