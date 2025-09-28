using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasOne(p => p.Order).WithMany(o => o.Products).HasForeignKey(p => p.OrderId);
        builder.HasMany(p => p.Stages).WithOne(s => s.Product);
        builder.HasMany(p => p.WorkingPeriods).WithOne(wp => wp.Product);
        builder.HasMany(p => p.ChildProducts).WithOne(p => p.ParentProduct);
        builder.HasOne(p => p.ParentProduct).WithMany(p => p.ChildProducts).HasForeignKey(p => p.ParentProductId);
        builder.HasOne(p => p.ProductSubType).WithMany(pst => pst.Products).HasForeignKey(p => p.ProductSubTypeId);
        builder.HasOne(p => p.Factory).WithMany(f => f.Products).HasForeignKey(p => p.FactoryId);

    }
}