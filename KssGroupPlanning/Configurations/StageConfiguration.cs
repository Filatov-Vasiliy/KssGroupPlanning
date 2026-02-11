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
        builder.HasOne(s => s.ProductSubTypeStageSample).WithMany(pstss =>pstss.Stages).HasForeignKey(s => s.ProductSubTypeStageSampleId);
        builder.HasOne(ms => ms.SubProduct).WithOne(gm => gm.SubProductStage).HasForeignKey<ProductEntity>(p => p.SubProductStageId);

    }
}
