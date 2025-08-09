using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Models.Help;

namespace KssGroupPlanning.Configurations.Help;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne(c => c.Author).WithOne(c => c.Course);
        builder.HasMany(c => c.Lessons).WithOne(l => l.Course).HasForeignKey(l => l.CourseId);
        builder.HasMany(c => c.Students).WithMany(s => s.Courses);

    }
}
