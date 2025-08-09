using KssGroupPlanning.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations;

public class StageTypeConfiguration : IEntityTypeConfiguration<StageTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StageTypeEntity> builder)
    {
        builder.HasKey(st => st.Id);

    }
}
