namespace KssGroupPlanning.Models.Help;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } = 0;

    public List<LessonEntity> Lessons { get; set; } = [];

    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; }

    public List<StudentEntity> Students { get; set; } = [];
}
