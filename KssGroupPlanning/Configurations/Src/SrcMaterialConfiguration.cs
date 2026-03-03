using KssGroupPlanning.Entities.Src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.Src;

public class SrcMaterialConfiguration : IEntityTypeConfiguration<SrcMaterialEntity>
{
    public void Configure(EntityTypeBuilder<SrcMaterialEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}