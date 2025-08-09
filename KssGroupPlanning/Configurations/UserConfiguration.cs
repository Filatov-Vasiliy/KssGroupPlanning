using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder) 
    {
        builder.HasKey(c => c.Id);

    }
}
