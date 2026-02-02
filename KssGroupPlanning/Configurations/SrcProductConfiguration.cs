using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class SrcProductConfiguration : IEntityTypeConfiguration<SrcProductEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SrcProductEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}