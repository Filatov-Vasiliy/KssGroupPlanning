using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class SrcMaterialConfiguration : IEntityTypeConfiguration<SrcMaterialEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SrcMaterialEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}