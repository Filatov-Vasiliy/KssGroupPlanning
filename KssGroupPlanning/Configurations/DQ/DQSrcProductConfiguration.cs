using KssGroupPlanning.Entities.DQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KssGroupPlanning.Configurations.DQ;

public class DQSrcProductConfiguration : IEntityTypeConfiguration<DQSrcProductEntity>
{
    public void Configure(EntityTypeBuilder<DQSrcProductEntity> builder)
    {
        builder.HasKey(so => so.Id);

    }
}