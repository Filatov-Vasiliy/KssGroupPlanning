namespace KssGroupPlanning.Models.Help;

public class LessonEntity 
{ 
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string LessonText { get; set; }

    public int CourseId { get; set; }
    public CourseEntity? Course { get; set; }
}