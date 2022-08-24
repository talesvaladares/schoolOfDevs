using Microsoft.EntityFrameworkCore;
using schoolOfDevs.Entities;
using schoolOfDevs.Helpers;

namespace schoolOfDevs.Services { 

    public interface ICourseService {
        public Task<List<Course>> GetAll();
        public Task<Course> Create(Course course);
        public Task<Course> GetById(int id);
        public Task Update(Course course);
        public Task Delete(int id);

    }

    public class CourseService : ICourseService
    {

        private readonly DataContext _context;

        public CourseService(DataContext context)
        {
            _context = context;
        }

        public async Task<Course> Create(Course course)
        {
           Course courseDb = await _context.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Name == course.Name);

            if (courseDb is not null )
            {
                throw new Exception($"Course {course.Name} already exist.");
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task Delete(int id)
        {
            Course courseDb = await _context.Courses.SingleOrDefaultAsync(c => c.Id == id);

            if (courseDb is null)
            {
                throw new Exception($"Course {id} not found");
            }

            _context.Courses.Remove(courseDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll() => await _context.Courses.ToListAsync();

        public async Task<Course> GetById(int id)
        {
            Course courseDb = await _context.Courses
               .SingleOrDefaultAsync(c => c.Id == id);

            if (courseDb is null)
            {
                throw new Exception($"Course {id} not found");
            }

            return courseDb;
        }

        public async Task Update(Course course)
        {
           
            Course courseDb = await _context.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == course.Id);

            if (courseDb is null)
            {
                throw new Exception($"Course {course.Id} not found");
            }

            course.CreatedAt = courseDb.CreatedAt;

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
