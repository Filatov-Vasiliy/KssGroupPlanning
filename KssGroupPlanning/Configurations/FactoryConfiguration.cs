using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class FactoryConfiguration : IEntityTypeConfiguration<FactoryEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FactoryEntity> builder)
    {
        builder.HasKey(f => f.Id);
        builder.HasMany(f => f.Products).WithOne(p => p.Factory);
        builder.HasMany(f => f.Brigades).WithOne(b => b.Factory);
    }
}
