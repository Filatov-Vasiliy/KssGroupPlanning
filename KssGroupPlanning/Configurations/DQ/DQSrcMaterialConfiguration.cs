using KssGroupPlanning.Entities.DQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.DQ;

public class DQSrcMaterialConfiguration : IEntityTypeConfiguration<DQSrcMaterialEntity>
{
    public void Configure(EntityTypeBuilder<DQSrcMaterialEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}