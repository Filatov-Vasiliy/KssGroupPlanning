using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Models.Help;

namespace KssGroupPlanning.Configurations.Help;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(s=>s.Courses).WithMany(c => c.Students);

    }
}
