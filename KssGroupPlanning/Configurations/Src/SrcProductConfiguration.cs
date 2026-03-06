using KssGroupPlanning.Entities.Src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.Src;

public class SrcProductConfiguration : IEntityTypeConfiguration<SrcProductEntity>
{
    public void Configure(EntityTypeBuilder<SrcProductEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}