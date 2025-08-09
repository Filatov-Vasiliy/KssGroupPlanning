using Microsoft.EntityFrameworkCore;
/*
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Repositories;

public class CoursesRepository
{
    private readonly ProjectDbContext _dbcontext;

    public CoursesRepository(ProjectDbContext context)
    { 
        _dbcontext = context;
    }
    public async Task<List<CourseEntity>> Get()
    { 
        return await _dbcontext.Courses.AsNoTracking().OrderBy(c=>c.Title).ToListAsync();
    }
    public async Task<List<CourseEntity>> GetWithLessons()
    {
        return await _dbcontext.Courses.AsNoTracking().Include(c => c.Lessons).ToListAsync();
    }
    public async Task<CourseEntity?> GetById(int id)
    {
        return await _dbcontext.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<CourseEntity>> GetByFilter(String title, decimal price)
    {
        var query = _dbcontext.Courses.AsNoTracking();
        if (!string.IsNullOrEmpty(title))
        { 
            query = query.Where(c => c.Title.Contains(title));
        }
        if (price > 0)
        {
            query = query.Where(c => c.Price > price);
        }
        return await query.ToListAsync();
    }
    public async Task<List<CourseEntity>> GetByPage(int page,int pageSize)
    {
        
        return await _dbcontext.Courses
            .AsNoTracking()
            .Skip((page - 1)*pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task Add(int id, int authorId, string title, string description, decimal price)
    {
        var courseEntity = new CourseEntity
        {
            Id = id,
            AuthorId = authorId,
            Title = title,
            Description = description,
            Price = price
        };
        await _dbcontext.AddAsync(courseEntity);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update(int id, int authorId, string title, string description, decimal price)
    {
        var courseEntity = await _dbcontext.Courses.FirstOrDefaultAsync(c=> c.Id == id);
            ?? throw new Exception();
            
        courseEntity.Title = title;
        courseEntity.Description = description;
        courseEntity.Price = price;
        await _dbcontext.SaveChangesAsync();
    }
    public async Task Update2(int id, int authorId, string title, string description, decimal price)
    {
        await _dbcontext.Courses
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(c => c.Title, title)
            .SetProperty(c => c.Description, description)
            .SetProperty(c => c.Price, price));
    }
    public async Task Delete(int id, int authorId, string title, string description, decimal price)
    {
        await _dbcontext.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}
public class LessonsRepository 
{
    private readonly ProjectDbContext _dbcontext;

    public LessonsRepository(ProjectDbContext context)
    {
        _dbcontext = context;
    }
    public async Task AddLesson(int courseId, LessonEntity lesson)
    {
        var course = await _dbcontext.Courses.FirstOrDefaultAsync(c => c.Id == courseId)
            ?? throw new Exception();

        course.Lessons.Add(lesson);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task AddLesson2(int courseId, string title)
    {
        var lesson = new LessonEntity
        {
            Title = title ,
            CourseId = courseId
        };
        await _dbcontext.AddAsync(lesson);
        await _dbcontext.SaveChangesAsync();
    }
}
*/