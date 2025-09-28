using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class StageConfiguration : IEntityTypeConfiguration<StageEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StageEntity> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(s => s.Product).WithMany(p => p.Stages).HasForeignKey(s => s.ProductId);
    }
}
