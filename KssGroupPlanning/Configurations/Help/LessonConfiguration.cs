using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Models.Help;

namespace KssGroupPlanning.Configurations.Help;

public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LessonEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne(l => l.Course).WithMany(c => c.Lessons).HasForeignKey(l => l.CourseId);
    }
}
