using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class BrigadeConfiguration : IEntityTypeConfiguration<BrigadeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BrigadeEntity> builder)
    {
        builder.HasKey(b => b.Id);

    }
}