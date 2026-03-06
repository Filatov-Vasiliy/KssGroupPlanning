using KssGroupPlanning.Entities.Src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.Src;

public class SrcOrderConfiguration : IEntityTypeConfiguration<SrcOrderEntity>
{
    public void Configure(EntityTypeBuilder<SrcOrderEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}
