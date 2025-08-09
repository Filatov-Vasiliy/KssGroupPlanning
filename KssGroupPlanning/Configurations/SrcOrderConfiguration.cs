using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class SrcOrderConfiguration : IEntityTypeConfiguration<SrcOrderEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SrcOrderEntity> builder)
    {
        builder.HasKey(so => so.OrderName);

    }
}
