using KssGroupPlanning.Entities.DQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.DQ;

public class DQSrcOrderConfiguration : IEntityTypeConfiguration<DQSrcOrderEntity>
{
    public void Configure(EntityTypeBuilder<DQSrcOrderEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}