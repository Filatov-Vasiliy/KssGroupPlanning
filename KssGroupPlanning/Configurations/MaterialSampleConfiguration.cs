using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class MaterialSampleConfiguration : IEntityTypeConfiguration<MaterialSampleEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MaterialSampleEntity> builder)
    {
        builder.HasKey(ms => ms.Id);

    }
}
