using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Models.Help;

namespace KssGroupPlanning.Configurations.Help;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Course).WithOne(c => c.Author).HasForeignKey<AuthorEntity>(a=>a.CourseId);
    }
}
